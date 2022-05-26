using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MyPersonalToDoApp.Api.Mapper;
using MyPersonalToDoApp.Data;
using MyPersonalToDoApp.Data.Contracts;
using MyPersonalToDoApp.Data.Repositories;
using MyPersonalToDoApp.DataModel.Identity;
using System;

namespace MyPersonalToDoApp.Api.StartupConfig;
public class Services
{
	public static void Configure(WebApplicationBuilder builder)
	{
		IConfiguration configuration = builder.Configuration;

		builder.Services.AddDbContext<ToDoContext>(options =>
	            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
	        );

		builder.Services.AddCors(options =>
			{
				options.AddPolicy("AllowAllOrigins ", builder => builder.AllowAnyMethod().AllowAnyHeader().SetIsOriginAllowed(o => true));
			});

		builder.Services.AddDatabaseDeveloperPageExceptionFilter();
		builder.Services.AddControllers();
		builder.Services.AddApiVersioning(setup =>
		{
			setup.DefaultApiVersion = new ApiVersion(1, 0);
			setup.AssumeDefaultVersionWhenUnspecified = true;
			setup.ReportApiVersions = true;
		});

		ConfigureSwagger(builder);
		ConfigureAuth(builder);
		ConfigureRepositories(builder);
		ConfigureMapper(builder);

	}

	#region helpers

	private static void ConfigureSwagger(WebApplicationBuilder builder)
	{
		builder.Services.AddSwaggerGen(c =>
		{
			c.SwaggerDoc("v1", new OpenApiInfo
			{
				Title = "Personal Todo App",
				Version = "v1"
			});

			c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
			{
				Name = "Authorization",
				Type = SecuritySchemeType.ApiKey,
				Scheme = "Bearer",
				BearerFormat = "JWT",
				In = ParameterLocation.Header,
				Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
			});

			c.AddSecurityRequirement(new OpenApiSecurityRequirement {
					{
						new OpenApiSecurityScheme {
							Reference = new OpenApiReference {
								Type = ReferenceType.SecurityScheme,
								Id = "Bearer"
							}
						},
						System.Array.Empty<string>()
					}
			});
		});
	}

	private static void ConfigureAuth(WebApplicationBuilder builder)
	{
		IConfiguration configuration = builder.Configuration;

		builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
			.AddEntityFrameworkStores<ToDoContext>()
			.AddDefaultTokenProviders();

		builder.Services.AddAuthentication(options =>
		{
			options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
		}).
		AddJwtBearer(options =>
		{
			options.SaveToken = true;
			options.RequireHttpsMetadata = false;
			options.TokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuer = true,
				ValidateAudience = true,
				ValidAudience = configuration["AuthJwt:ValidAudience"],
				ValidIssuer = configuration["AuthJwt:ValidIssuer"],
				IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration["AuthJwt:Secret"])),
				ClockSkew = TimeSpan.Zero
			};
		});

		// Identity Configuration
		builder.Services.Configure<IdentityOptions>(options =>
		{
			options.User.AllowedUserNameCharacters = configuration.GetValue<string>("Identity:User:AllowedUserNameCharacters");
			options.User.RequireUniqueEmail = configuration.GetValue<bool>("Identity:User:RequireUniqueEmail");

			options.Password.RequireDigit = true;
			options.Password.RequiredLength = 8;
			options.Password.RequireUppercase = true;
			options.Password.RequireNonAlphanumeric = true;

			// 
			options.Lockout.MaxFailedAccessAttempts = configuration.GetValue<int>("Identity:Lockout:MaxFailedAccessAttempts");
			options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(configuration.GetValue<int>("Identity:Lockout:DefaultLockoutTimeSpan"));
		});
	}

	private static void ConfigureRepositories(WebApplicationBuilder builder)
	{
		builder.Services.AddScoped<IActivityRepository, ActivityRepository>();
		builder.Services.AddScoped<IRegisterRepository, RegisterRepository>();
		builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
		builder.Services.AddScoped<ITodoRepository, TodoRepository>();
	}

	private static void ConfigureMapper(WebApplicationBuilder builder)
	{
		// AutoMapper
		var myTodoMapper = new MapperConfiguration(configure =>
		{
			configure.AddProfile(new MyPersonalToDoProfile());
		});
		builder.Services.AddSingleton(myTodoMapper.CreateMapper());
	}

	#endregion

}

