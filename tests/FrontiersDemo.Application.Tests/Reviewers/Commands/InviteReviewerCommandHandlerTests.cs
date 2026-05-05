using FrontiersDemo.Application.Common.Exceptions;
using FrontiersDemo.Application.Common.Interfaces;
using FrontiersDemo.Application.Reviewers.Commands.InviteReviewer;
using FrontiersDemo.Domain.Entities;
using FrontiersDemo.Domain.ValueObjects;
using NSubstitute;

namespace FrontiersDemo.Application.Tests.Reviewers.Commands;

public sealed class InviteReviewerCommandHandlerTests
{
    private readonly IUserRepository _users = Substitute.For<IUserRepository>();
    private readonly IReviewerInvitationRepository _invitations = Substitute.For<IReviewerInvitationRepository>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
    private readonly TimeProvider _clock = Substitute.For<TimeProvider>();
    private readonly InviteReviewerCommandHandler _handler;

    public InviteReviewerCommandHandlerTests()
    {
        _clock.GetUtcNow().Returns(DateTimeOffset.UtcNow);
        _handler = new InviteReviewerCommandHandler(_users, _invitations, _unitOfWork, _clock);
    }

    [Fact]
    public async Task Handle_UserNotFound_ThrowsNotFoundException()
    {
        _users.GetByIdAsync(1, Arg.Any<CancellationToken>()).Returns((User?)null);

        await Assert.ThrowsAsync<NotFoundException>(() =>
            _handler.Handle(new InviteReviewerCommand(1), CancellationToken.None));
    }

    [Fact]
    public async Task Handle_PublicationsNotSufficient_ReturnsNotEligible()
    {
        _users.GetByIdAsync(1, Arg.Any<CancellationToken>()).Returns(CreateUser(publications: 2, score: 80));

        var result = await _handler.Handle(new InviteReviewerCommand(1), CancellationToken.None);

        Assert.False(result.IsEligible);
        await _invitations.DidNotReceive().AddAsync(Arg.Any<ReviewerInvitation>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_UniversityScoreNotSufficient_ReturnsNotEligible()
    {
        _users.GetByIdAsync(1, Arg.Any<CancellationToken>()).Returns(CreateUser(publications: 5, score: 40));

        var result = await _handler.Handle(new InviteReviewerCommand(1), CancellationToken.None);

        Assert.False(result.IsEligible);
        await _invitations.DidNotReceive().AddAsync(Arg.Any<ReviewerInvitation>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_EligibleUser_CreatesInvitationAndReturnsSuccess()
    {
        _users.GetByIdAsync(1, Arg.Any<CancellationToken>()).Returns(CreateUser(publications: 5, score: 80));

        var result = await _handler.Handle(new InviteReviewerCommand(1), CancellationToken.None);

        Assert.True(result.IsEligible);
        await _invitations.Received(1).AddAsync(Arg.Any<ReviewerInvitation>(), Arg.Any<CancellationToken>());
        await _unitOfWork.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    private static User CreateUser(int publications, double score) =>
        new("Jakub", publications, new Organization(1L, "Warsaw University of Technology", null, null, null, null, score));
}
