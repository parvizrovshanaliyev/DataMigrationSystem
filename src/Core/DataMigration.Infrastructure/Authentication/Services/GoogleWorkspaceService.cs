//using DataMigration.Domain.Contracts;
//using DataMigration.Domain.ValueObjects;
//using Google.Apis.Auth;
//using Microsoft.Extensions.Configuration;

//namespace DataMigration.Infrastructure.Authentication.Services;

//public class GoogleWorkspaceService : IGoogleWorkspaceService
//{
//    private readonly IConfiguration _configuration;

//    public GoogleWorkspaceService(IConfiguration configuration)
//    {
//        _configuration = configuration;
//    }

//    public async Task<GoogleUserInfo> ValidateTokenAsync(string token)
//    {
//        try
//        {
//            var settings = new GoogleJsonWebSignature.ValidationSettings
//            {
//                Audience = new[] { _configuration["Google:ClientId"] }
//            };

//            var payload = await GoogleJsonWebSignature.ValidateAsync(token, settings);

//            return new GoogleUserInfo(
//                payload.Subject,
//                payload.Email,
//                payload.Name,
//                payload.Picture,
//                payload.HostedDomain);
//        }
//        catch
//        {
//            return null;
//        }
//    }
//} 