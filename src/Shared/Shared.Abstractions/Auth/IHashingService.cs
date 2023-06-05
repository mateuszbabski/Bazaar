namespace Shared.Abstractions.Auth
{
    public interface IHashingService
    {
        string GenerateSalt();
        string GenerateHashPassword(string password);
        bool ValidatePassword(string password, string correctHash);
    }
}
