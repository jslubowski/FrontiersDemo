using FrontiersDemo.Application.Users.Commands.RegisterUser;
using FrontiersDemo.Application.Users.Queries.GetUserById;
using FrontiersDemo.Application.Users.Queries.GetUsers;
using MediatR;

namespace FrontiersDemo.Api.Endpoints;

public static class UserEndpoints
{
    public const string GroupPrefix = "/users";
    private const string Tag = "Users";
    private const string RegisterUserRouteName = "RegisterUser";
    private const string GetUserByIdRouteName = "GetUserById";
    private const string GetUsersRouteName = "GetUsers";

    public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup(GroupPrefix).WithTags(Tag);

        group.MapPost("/", RegisterUser).WithName(RegisterUserRouteName);
        group.MapGet("/", GetUsers).WithName(GetUsersRouteName);
        group.MapGet("/{id:int}", GetUserById).WithName(GetUserByIdRouteName);

        return app;
    }

    private static async Task<IResult> RegisterUser(
        RegisterUserCommand command, ISender sender, CancellationToken ct)
    {
        var result = await sender.Send(command, ct);
        return Results.Created($"{GroupPrefix}/{result.User.Id}", result);
    }

    private static async Task<IResult> GetUsers(ISender sender, CancellationToken ct)
    {
        var result = await sender.Send(new GetUsersQuery(), ct);
        return Results.Ok(result);
    }

    private static async Task<IResult> GetUserById(int id, ISender sender, CancellationToken ct)
    {
        var result = await sender.Send(new GetUserByIdQuery(id), ct);
        return Results.Ok(result);
    }
}
