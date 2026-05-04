using FrontiersDemo.Application.Users.Commands.RegisterUser;
using MediatR;

namespace FrontiersDemo.Api.Endpoints;

public static class UserEndpoints
{
    public const string GroupPrefix = "/users";
    private const string Tag = "Users";
    private const string RegisterUserRouteName = "RegisterUser";

    public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGroup(GroupPrefix)
            .WithTags(Tag)
            .MapPost("/", RegisterUser)
            .WithName(RegisterUserRouteName);

        return app;
    }

    private static async Task<IResult> RegisterUser(
        RegisterUserCommand command, ISender sender, CancellationToken ct)
    {
        var result = await sender.Send(command, ct);
        return Results.Created($"{GroupPrefix}/{result.User.Id}", result);
    }
}
