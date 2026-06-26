using System.Text.RegularExpressions;
using UserManagementAPI.Models;

namespace UserManagementAPI.Services;

public sealed class InMemoryUserService : IUserService
{
    private readonly Dictionary<int, User> _users = new()
    {
        [1] = new User(1, "Ada", "Lovelace", "ada.lovelace@techhive.com", "Engineering", true),
        [2] = new User(2, "Grace", "Hopper", "grace.hopper@techhive.com", "IT", true)
    };

    public IReadOnlyList<User> GetAll() => _users.Values.OrderBy(user => user.Id).ToList();

    public User? GetById(int id) => _users.TryGetValue(id, out var user) ? user : null;

    public User Create(CreateUserRequest request)
    {
        var validationError = ValidateUserRequest(request);
        if (validationError is not null)
        {
            throw new ArgumentException(validationError);
        }

        var id = _users.Count > 0 ? _users.Keys.Max() + 1 : 1;
        var user = new User(
            id,
            request!.FirstName!.Trim(),
            request.LastName!.Trim(),
            request.Email!.Trim(),
            request.Department!.Trim(),
            request.IsActive);

        _users[id] = user;
        return user;
    }

    public User? Update(int id, UpdateUserRequest request)
    {
        var validationError = ValidateUserRequest(request);
        if (validationError is not null)
        {
            throw new ArgumentException(validationError);
        }

        if (!_users.ContainsKey(id))
        {
            return null;
        }

        var user = new User(
            id,
            request!.FirstName!.Trim(),
            request.LastName!.Trim(),
            request.Email!.Trim(),
            request.Department!.Trim(),
            request.IsActive);

        _users[id] = user;
        return user;
    }

    public bool Delete(int id) => _users.Remove(id);

    private static string? ValidateUserRequest(CreateUserRequest? request)
    {
        if (request is null)
        {
            return "The request body is required.";
        }

        if (string.IsNullOrWhiteSpace(request.FirstName) ||
            string.IsNullOrWhiteSpace(request.LastName) ||
            string.IsNullOrWhiteSpace(request.Email) ||
            string.IsNullOrWhiteSpace(request.Department))
        {
            return "First name, last name, email, and department are required.";
        }

        var firstName = request.FirstName.Trim();
        var lastName = request.LastName.Trim();
        var department = request.Department.Trim();
        var email = request.Email.Trim();

        if (firstName.Length > 100 || lastName.Length > 100 || department.Length > 100)
        {
            return "Name and department values must be 100 characters or fewer.";
        }

        if (!IsValidEmail(email))
        {
            return "A valid email address is required.";
        }

        return null;
    }

    private static string? ValidateUserRequest(UpdateUserRequest? request)
    {
        if (request is null)
        {
            return "The request body is required.";
        }

        if (string.IsNullOrWhiteSpace(request.FirstName) ||
            string.IsNullOrWhiteSpace(request.LastName) ||
            string.IsNullOrWhiteSpace(request.Email) ||
            string.IsNullOrWhiteSpace(request.Department))
        {
            return "First name, last name, email, and department are required.";
        }

        var firstName = request.FirstName.Trim();
        var lastName = request.LastName.Trim();
        var department = request.Department.Trim();
        var email = request.Email.Trim();

        if (firstName.Length > 100 || lastName.Length > 100 || department.Length > 100)
        {
            return "Name and department values must be 100 characters or fewer.";
        }

        if (!IsValidEmail(email))
        {
            return "A valid email address is required.";
        }

        return null;
    }

    private static bool IsValidEmail(string? email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return false;
        }

        return Regex.IsMatch(email.Trim(), @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
    }
}
