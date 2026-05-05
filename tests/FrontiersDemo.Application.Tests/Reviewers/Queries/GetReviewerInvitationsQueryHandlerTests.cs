using FrontiersDemo.Application.Common.Interfaces;
using FrontiersDemo.Application.Reviewers.Queries.GetReviewerInvitations;
using FrontiersDemo.Domain.Entities;
using NSubstitute;

namespace FrontiersDemo.Application.Tests.Reviewers.Queries;

public sealed class GetReviewerInvitationsQueryHandlerTests
{
    private readonly IReviewerInvitationRepository _invitations = Substitute.For<IReviewerInvitationRepository>();
    private readonly GetReviewerInvitationsQueryHandler _handler;

    public GetReviewerInvitationsQueryHandlerTests()
    {
        _handler = new GetReviewerInvitationsQueryHandler(_invitations);
    }

    [Fact]
    public async Task Handle_ReturnsAllInvitationsMappedToDtos()
    {
        var now = DateTimeOffset.UtcNow;
        IReadOnlyList<ReviewerInvitation> invitations =
        [
            new(userId: 1, now),
            new(userId: 2, now)
        ];
        _invitations.GetAllAsync(Arg.Any<CancellationToken>()).Returns(invitations);

        var result = await _handler.Handle(new GetReviewerInvitationsQuery(), CancellationToken.None);

        Assert.Equal(2, result.Count);
        Assert.Equal(1, result[0].UserId);
        Assert.Equal(2, result[1].UserId);
    }
}
