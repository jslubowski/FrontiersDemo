using FrontiersDemo.Api.Dtos;
using FrontiersDemo.Application.Reviewers.Commands.InviteReviewer;
using MediatR;

namespace FrontiersDemo.Api.Endpoints;

public static class ReviewerEndpoints
{
    public const string GroupPrefix = "/reviewers";
    private const string InviteRoute = "/invite";
    private const string InvitationsSegment = "/invitations";
    private const string Tag = "Reviewers";
    private const string InviteReviewerRouteName = "InviteReviewer";

    public static IEndpointRouteBuilder MapReviewerEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGroup(GroupPrefix)
            .WithTags(Tag)
            .MapPost(InviteRoute, InviteReviewer)
            .WithName(InviteReviewerRouteName);

        return app;
    }

    private static async Task<IResult> InviteReviewer(
        InviteReviewerCommand command, ISender sender, CancellationToken ct)
    {
        var id = await sender.Send(command, ct);
        var location = $"{GroupPrefix}{InvitationsSegment}/{id}";
        return Results.Created(location, new InviteReviewerResponse { Id = id });
    }
}
