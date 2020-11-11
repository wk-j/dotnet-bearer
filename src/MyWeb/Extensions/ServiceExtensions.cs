using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using MyWeb.Controllers.Accounts;

namespace MyWeb {

    public static class ServiceExtensions {
        public static void AddJwt(this IServiceCollection services) {
            var symmetricSecurityKey = TokenUtils.GetSymmetricKey();
            services
                .AddAuthentication(o => {
                    o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(cfg => {
                    cfg.SaveToken = true;
                    cfg.RequireHttpsMetadata = false;
                    cfg.IncludeErrorDetails = true;
                    cfg.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters {
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidIssuer = "bcircle",
                        ValidAudience = "bcircle",
                        ValidateLifetime = true,
                        IssuerSigningKey = symmetricSecurityKey
                    };
                    cfg.Events = new JwtBearerEvents() {
                        OnAuthenticationFailed = c => {
                            c.NoResult();
                            c.Response.StatusCode = 401;
                            c.Response.ContentType = "text/plain";
                            return c.Response.WriteAsync(c.Exception.ToString());
                        }
                    };
                });
        }
    }
}