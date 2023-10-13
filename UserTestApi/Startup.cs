using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
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
            services.AddControllers();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddMemoryCache();

            services.AddDbContext<UserTestsContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

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

            app.UseHttpsRedirection();

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
