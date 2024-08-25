using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Minio;
using Mouse.NET.Auth.Services;
using Mouse.NET.Common;
using Mouse.NET.Data;
using Mouse.NET.Data.Models;
using Mouse.NET.Invites.Data;
using Mouse.NET.Invites.Services;
using Mouse.NET.LevelComments.Data;
using Mouse.NET.LevelComments.services;
using Mouse.NET.Levels.Data;
using Mouse.NET.Levels.services;
using Mouse.NET.Messages.Data;
using Mouse.NET.Messages.services;
using Mouse.NET.Storage;
using Mouse.NET.Tags.Data;
using Mouse.NET.Tags.services;
using Mouse.NET.Tips.Data;
using Mouse.NET.Tips.services;
using Mouse.NET.Users.Data;
using Mouse.NET.Users.services;
using Mouse.Stick.Controllers.Auth;

namespace Mouse.NET;

    public class Startup
    {
        private readonly IConfiguration configuration;
        
        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddCors();
            services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
            {
                builder
                    .WithOrigins(this.configuration.GetSection("CorsSettings:AllowedOrigins").Get<string[]>())
                    .WithMethods(this.configuration.GetSection("CorsSettings:AllowedMethods").Get<string[]>())
                    .WithHeaders(this.configuration.GetSection("CorsSettings:AllowedHeaders").Get<string[]>());
            }));
            services.AddSingleton<IMemoryCache, MemoryCache>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddDbContext<MouseDbContext>(c => 
                c.UseNpgsql(this.configuration.GetConnectionString("DefaultConnection"))
            );
            services.AddRouting();
            services.AddAuthorization();
            services.AddControllers();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidIssuer = "MyAuthServer",
                        ValidateAudience = false,
                        ValidAudience = "MyAuthClient",
                        ValidateLifetime = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("this is my custom Secret key for authnetication")),
                        ValidateIssuerSigningKey = true,
                    };
                });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "MouseAPI", Version = "v1"});
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                //c.EnableAnnotations();
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    { 
                        new OpenApiSecurityScheme 
                        { 
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" } 
                        },
                        new string[] { } 
                    } 
                });
            });
            this.ConfigureDependencyInjection(services);
        }
        
        public void Configure(IApplicationBuilder app)
        {
            
            app.UseCors("CorsPolicy");
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mouse.Stick v1"));
            
            // app.Run(
            //     async (context) =>
            //     {
            //         context.Response.StatusCode = 405;
            //         await context.Response.WriteAsync(string.Empty);
            //     });    

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
        
        private void ConfigureDependencyInjection(IServiceCollection services)
        {
            services.AddScoped<ILevelRepository, LevelRepository>();
            services.AddScoped<ILevelService, LevelService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ILevelCommentService, LevelCommentService>();
            services.AddScoped<ILevelCommentRepository, LevelCommentRepository>();
            services.AddScoped<ITipService, TipService>();
            services.AddScoped<ITipRepository, TipRepository>();
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddTransient<JwtService>();
            services.AddScoped<IMinioService, MinioService>();
            services.AddScoped<IStorageService, StorageService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IInviteService, InviteService>();
            services.AddScoped<IInviteRepository, InviteRepository>();
        }
    }
