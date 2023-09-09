namespace ASE3040.Application.Common.Interfaces;

public interface IUser
{
    string? UserId { get; }
    string? UserName { get; }
    string? Email { get; }
}