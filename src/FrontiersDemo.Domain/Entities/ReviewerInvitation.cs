using FrontiersDemo.Domain.Enums;

namespace FrontiersDemo.Domain.Entities;

public sealed class ReviewerInvitation
{
    public int Id { get; private set; }
    public int UserId { get; private set; }
    public InvitationStatus Status { get; private set; }
    public DateTimeOffset InvitedAt { get; private set; }

    private ReviewerInvitation() { }

    public ReviewerInvitation(int userId, DateTimeOffset invitedAt)
    {
        UserId = userId;
        Status = InvitationStatus.Pending;
        InvitedAt = invitedAt;
    }
}
