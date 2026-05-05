namespace FrontiersDemo.Application.Reviewers.Dtos;

public sealed record ReviewerInvitationDto(
    int Id,
    int UserId,
    DateTimeOffset InvitedAt);
