using FrontiersDemo.Application.Reviewers.Dtos;
using MediatR;

namespace FrontiersDemo.Application.Reviewers.Queries.GetReviewerInvitations;

public sealed record GetReviewerInvitationsQuery : IRequest<IReadOnlyList<ReviewerInvitationDto>>;
