namespace FrontiersDemo.Application.Reviewers.Commands.InviteReviewer;

public sealed record InviteReviewerResult(bool IsEligible, string Message, int? InvitationId = null);