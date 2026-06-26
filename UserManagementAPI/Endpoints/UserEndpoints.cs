using UserManagementAPI.Models;
using UserManagementAPI.Services;

namespace UserManagementAPI.Endpoints;

public static class UserEndpoints
{
    public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        var usersApi = app.MapGroup("/users").WithTags("Users");

        usersApi.MapGet("/", (IUserService userService) => Results.Ok(userService.GetAll()));

        usersApi.MapGet("/{id:int}", (int id, IUserService userService) =>
        {
            var user = userService.GetById(id);
            return user is null ? Results.NotFound() : Results.Ok(user);
        });

        usersApi.MapPost("/", (CreateUserRequest? request, IUserService userService) =>
        {
            try
            {
                var user = userService.Create(request!);
                return Results.Created($"/users/{user.Id}", user);
            }
            catch (ArgumentException ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });

        usersApi.MapPut("/{id:int}", (int id, UpdateUserRequest? request, IUserService userService) =>
        {
            try
            {
                var user = userService.Update(id, request!);
                return user is null ? Results.NotFound() : Results.Ok(user);
            }
            catch (ArgumentException ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });

        usersApi.MapDelete("/{id:int}", (int id, IUserService userService) =>
        {
            var deleted = userService.Delete(id);
            return deleted ? Results.NoContent() : Results.NotFound();
        });

        return app;
    }
}
