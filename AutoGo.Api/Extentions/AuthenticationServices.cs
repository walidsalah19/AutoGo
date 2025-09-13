using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace AutoGo.Api.Extentions
{
    public static class AuthenticationServices
    {
        public static IServiceCollection AddAuthServices(this IServiceCollection services,IConfiguration configuration)
        {


            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false; 
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = configuration["JWT:IssuerIP"],
                    ValidateAudience = true,
                    ValidAudience = configuration["JWT:AudienceIP"],
                    IssuerSigningKey =
                        new SymmetricSecurityKey(
                            System.Text.Encoding.UTF8.GetBytes(configuration["JWT:SecritKey"]))

                };
            });

            return services;
        }
    }
}
