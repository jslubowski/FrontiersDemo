using MediatR;

namespace FrontiersDemo.Application.Reviewers.Commands.InviteReviewer;

public sealed record InviteReviewerCommand(int UserId) : IRequest<int>;
