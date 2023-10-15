using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Data;
using System.Text;
using System.Text.Json;
using UserTestApi.Business.Services;
using UserTestApi.Domain.EF;
using UserTestApi.Domain.Entities;
using UserTestApi.Domain.Repositories;

namespace UserTestApi
{
    public static class Startup
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors();

            services.AddControllers();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(o =>
            {
                o.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer"
                });
                o.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Name = "Bearer"
                        },
                        new List<string>()
                     }
                });
            });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddMemoryCache();

            services.AddDbContext<UserTestsContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddAuthorization();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidateAudience = true,
                        ValidAudience = configuration["Jwt:Audience"],
                        ValidateLifetime = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!)),
                        ValidateIssuerSigningKey = true,
                    };
                });

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITestRepository, TestRepository>();
            services.AddScoped<IUserTestRepository, UserTestRepository>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITestService, TestService>();
            services.AddSingleton<IAnswersCheckerService, AnswersCheckerService>();
        }
        public static void Configure(this WebApplication app, IWebHostEnvironment environment)
        {
            ConfigureDbContext(app.Services, environment);

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

                app.UseCors(builder => builder
                    .WithOrigins("http://localhost:8080")
                    .AllowAnyHeader()
                    .AllowAnyMethod());
            }
            else
            {
                app.UseExceptionHandler(appBuilder =>
                    appBuilder.Run(context =>
                    {
                        context.Response.StatusCode = 500;
                        return Task.CompletedTask;
                    }));
            }

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseHttpsRedirection();

            app.UseSpa(c => { });

            app.MapControllers();
        }

        private static void ConfigureDbContext(IServiceProvider services, IWebHostEnvironment environment)
        {
            using (var scope = services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<UserTestsContext>();

                if (environment.IsDevelopment())
                {
                    context.Database.EnsureDeleted();
                    context.Database.EnsureCreated();
                    SeedTestData(context);
                }
                else
                {
                    context.Database.EnsureCreated();
                }
            }
        }
        private static void SeedTestData(UserTestsContext context) 
        {
            var user = new UserEntity { Name = "User1" };

            var test1 = new TestEntity
            {
                Name = "Math Test",
                TotalPoints = 20,
                Questions = JsonSerializer.Deserialize<List<QuestionEntity>>(@"
                [
                  {
                    ""Number"": 1,
                    ""Description"": ""2 + 2 = ?"",
                    ""Options"": [
                      {
                        ""Number"": 1,
                        ""Name"": ""3"",
                        ""Points"": 0
                      },
                      {
                        ""Number"": 2,
                        ""Name"": ""4"",
                        ""Points"": 10
                      },
                      {
                        ""Number"": 3,
                        ""Name"": ""5"",
                        ""Points"": 0
                      }
                    ]
                  },
                  {
                    ""Number"": 2,
                    ""Description"": ""2 * 2 = ?"",
                    ""Options"": [
                      {
                        ""Number"": 1,
                        ""Name"": ""4"",
                        ""Points"": 10
                      },
                      {
                        ""Number"": 2,
                        ""Name"": ""22"",
                        ""Points"": 0
                      }
                    ]
                  }
                ]")!
            };
            var test2 = new TestEntity
            {
                Name = "Math Test 2",
                TotalPoints = 30,
                Questions = JsonSerializer.Deserialize<List<QuestionEntity>>(@"
                [
                  {
                    ""Number"": 1,
                    ""Description"": ""2 - 2 = ?"",
                    ""Options"": [
                      {
                        ""Number"": 1,
                        ""Name"": ""2"",
                        ""Points"": 0
                      },
                      {
                        ""Number"": 2,
                        ""Name"": ""0"",
                        ""Points"": 10
                      },
                      {
                        ""Number"": 3,
                        ""Name"": ""1"",
                        ""Points"": 0
                      }
                    ]
                  },
                  {
                    ""Number"": 2,
                    ""Description"": ""2 / 2 = ?"",
                    ""Options"": [
                      {
                        ""Number"": 1,
                        ""Name"": ""0"",
                        ""Points"": 0
                      },
                      {
                        ""Number"": 2,
                        ""Name"": ""1"",
                        ""Points"": 10
                      }
                    ]
                  },
                  {
                    ""Number"": 3,
                    ""Description"": ""2 ^ 2 = ?"",
                    ""Options"": [
                      {
                        ""Number"": 1,
                        ""Name"": ""2"",
                        ""Points"": 0
                      },
                      {
                        ""Number"": 2,
                        ""Name"": ""4"",
                        ""Points"": 10
                      }
                    ]
                  }
                ]")!
            };

            var userTest1 = new UserTestEntity
            {
                Points = 20,
                User = user,
                Test = test1
            };
            var userTest2 = new UserTestEntity
            {
                User = user,
                Test = test2
            };

            context.UserTests.AddRange(userTest1, userTest2);
            context.SaveChanges();
        }
    }
}
