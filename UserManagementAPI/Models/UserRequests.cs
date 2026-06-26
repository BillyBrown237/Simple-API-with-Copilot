namespace UserManagementAPI.Models;

public sealed record CreateUserRequest(string? FirstName, string? LastName, string? Email, string? Department, bool IsActive);

public sealed record UpdateUserRequest(string? FirstName, string? LastName, string? Email, string? Department, bool IsActive);
