using FrontiersDemo.Application.Reviewers.Dtos;
using MediatR;

namespace FrontiersDemo.Application.Reviewers.Queries.GetReviewerInvitationById;

public sealed record GetReviewerInvitationByIdQuery(int Id) : IRequest<ReviewerInvitationDto>;
