namespace DataMigration.Domain.Constants;

public static class AuthenticationConstants
{
    public const int MaxFailedLoginAttempts = 5;
    public const int LockoutDurationMinutes = 15;
    public const int MinPasswordLength = 12;
    public const int MaxPasswordLength = 128;
    public const int MfaCodeLength = 6;
    public const int RefreshTokenExpirationDays = 30;
    public const int AccessTokenExpirationMinutes = 60;
}