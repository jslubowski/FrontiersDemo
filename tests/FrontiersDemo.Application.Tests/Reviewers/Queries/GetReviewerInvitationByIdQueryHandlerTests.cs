using FrontiersDemo.Application.Common.Exceptions;
using FrontiersDemo.Application.Common.Interfaces;
using FrontiersDemo.Application.Reviewers.Queries.GetReviewerInvitationById;
using FrontiersDemo.Domain.Entities;
using NSubstitute;

namespace FrontiersDemo.Application.Tests.Reviewers.Queries;

public sealed class GetReviewerInvitationByIdQueryHandlerTests
{
    private readonly IReviewerInvitationRepository _invitations = Substitute.For<IReviewerInvitationRepository>();
    private readonly GetReviewerInvitationByIdQueryHandler _handler;

    public GetReviewerInvitationByIdQueryHandlerTests()
    {
        _handler = new GetReviewerInvitationByIdQueryHandler(_invitations);
    }

    [Fact]
    public async Task Handle_InvitationNotFound_ThrowsNotFoundException()
    {
        _invitations.GetByIdAsync(99, Arg.Any<CancellationToken>()).Returns((ReviewerInvitation?)null);

        await Assert.ThrowsAsync<NotFoundException>(() =>
            _handler.Handle(new GetReviewerInvitationByIdQuery(99), CancellationToken.None));
    }

    [Fact]
    public async Task Handle_InvitationFound_ReturnsDto()
    {
        var invitedAt = new DateTimeOffset(2026, 1, 1, 0, 0, 0, TimeSpan.Zero);
        var invitation = new ReviewerInvitation(userId: 1, invitedAt);
        _invitations.GetByIdAsync(1, Arg.Any<CancellationToken>()).Returns(invitation);

        var result = await _handler.Handle(new GetReviewerInvitationByIdQuery(1), CancellationToken.None);

        Assert.Equal(1, result.UserId);
        Assert.Equal(invitedAt, result.InvitedAt);
    }
}
