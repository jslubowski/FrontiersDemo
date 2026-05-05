using FrontiersDemo.Application.Reviewers.Commands.InviteReviewer;
using FrontiersDemo.Application.Reviewers.Queries.GetReviewerInvitationById;
using FrontiersDemo.Application.Reviewers.Queries.GetReviewerInvitations;
using MediatR;

namespace FrontiersDemo.Api.Endpoints;

public static class ReviewerEndpoints
{
    public const string GroupPrefix = "/reviewers";
    private const string InviteRoute = "/invite";
    private const string InvitationsSegment = "/invitations";
    private const string Tag = "Reviewers";
    private const string InviteReviewerRouteName = "InviteReviewer";
    private const string GetReviewerInvitationsRouteName = "GetReviewerInvitations";
    private const string GetReviewerInvitationByIdRouteName = "GetReviewerInvitationById";

    public static IEndpointRouteBuilder MapReviewerEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup(GroupPrefix).WithTags(Tag);

        group.MapPost(InviteRoute, InviteReviewer).WithName(InviteReviewerRouteName);
        group.MapGet(InvitationsSegment, GetReviewerInvitations).WithName(GetReviewerInvitationsRouteName);
        group.MapGet($"{InvitationsSegment}/{{id:int}}", GetReviewerInvitationById).WithName(GetReviewerInvitationByIdRouteName);

        return app;
    }

    private static async Task<IResult> InviteReviewer(
        InviteReviewerCommand command, ISender sender, CancellationToken ct)
    {
        var result = await sender.Send(command, ct);
        return Results.Ok(result);
    }

    private static async Task<IResult> GetReviewerInvitations(ISender sender, CancellationToken ct)
    {
        var result = await sender.Send(new GetReviewerInvitationsQuery(), ct);
        return Results.Ok(result);
    }

    private static async Task<IResult> GetReviewerInvitationById(int id, ISender sender, CancellationToken ct)
    {
        var result = await sender.Send(new GetReviewerInvitationByIdQuery(id), ct);
        return Results.Ok(result);
    }
}
