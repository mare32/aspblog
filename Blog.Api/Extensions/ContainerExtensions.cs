using Blog.Implementation.UseCases.Commands;
using Blog.Implementation.UseCases.Queries.Ef;
using Blog.Implementation.Validators;
using Blog.Application.UseCases.Commands;
using Blog.Application.UseCases.Queries;
using Blog.DataAccess;
using Blog.Api.Core;
using Blog.Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Blog.Api.Core.ImageHelpers;
using Blog.Implementation.UseCases.Queries.SP;

namespace Blog.Api.Extensions
{
    public static class ContainerExtensions
    {
        public static void AddJwt(this IServiceCollection services, AppSettings settings)
        {
            services.AddTransient(x =>
            {
                var context = x.GetService<BlogContext>();
                var settings = x.GetService<AppSettings>();

                return new JwtManager(context, settings.JwtSettings);
            });


            services.AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = settings.JwtSettings.Issuer,
                    ValidateIssuer = true,
                    ValidAudience = "Any",
                    ValidateAudience = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.JwtSettings.SecretKey)),
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
        }
        public static void AddBlogContext(this IServiceCollection services)
        {
            services.AddTransient(x =>
            {
                var optionsBuilder = new DbContextOptionsBuilder();

                var conString = x.GetService<AppSettings>().ConnString;

                optionsBuilder.UseSqlServer(conString).UseLazyLoadingProxies();
                optionsBuilder.EnableSensitiveDataLogging();
                var options = optionsBuilder.Options;

                return new BlogContext(options);
            });
        }

        public static void AddUseCases(this IServiceCollection services)
        {
            #region Queries
            services.AddTransient<IGetCategoriesQuery, EfGetCategoriesQuery>();
            services.AddTransient<IGetOneUserQuery, EfGetOneUserQuery>();
            services.AddTransient<ISearchUsersQuery, EfSearchUsersQuery>();
            services.AddTransient<ISearchBlogPostsQuery, EfSearchBlogPostsQuery>();
            services.AddTransient<IGetOneBlogPostQuery, EfGetOneBlogPostQuery>();
            services.AddTransient<ISearchRolesQuery, EfSearchRolesQuery>();
            services.AddTransient<IShowCommentsQuery, EfShowCommentsQuery>();
            services.AddTransient<IGetUseCaseLogsQuery, SpGetUseCaseLogsQuery>();
            services.AddTransient<ISearchUseCasesQuery, EfSearchUseCasesQuery>();
            services.AddTransient<IGetOneCategoryQuery, EfGetOneCategoryQuery>();
            services.AddTransient<ISearchBlogPostImagesQuery, EfSearchBlogPostImagesQuery>();
            services.AddTransient<ISearchUserUseCasesQuery, EfSearchUserUseCasesQuery>();
            services.AddTransient<ISearchUsersVotesQuery, EfSearchUsersVotesQuery>();
            #endregion

            #region Commands
            services.AddTransient<ICreateCategoryCommand, EfCreateCategoryCommand>();
            services.AddTransient<IRegisterUserCommand, EfRegisterUserCommand>();
            services.AddTransient<IDeleteCategoryCommand, EfDeleteCategoryCommand>();
            services.AddTransient<ICreateBlogPostCommand, EfCreateBlogPostCommand>();
            services.AddTransient<IPatchBlogPostCommand, EfPatchBlogPostCommand>();
            services.AddTransient<IAddImageToBlogPostCommand, EfAddImageToBlogPostCommand>();
            services.AddTransient<ICreateCommentCommand, EfCreateCommentCommand>();
            services.AddTransient<ICreateVoteCommand, EfCreateVoteCommand>();
            services.AddTransient<IChangeUserRoleCommand, EfChangeUserRoleCommand>();
            services.AddTransient<IUpdateUserUseCasesCommand, EfUpdateUserUseCasesCommand>();
            services.AddTransient<IUpdateBlogPostCategoriesCommand, EfUpdateBlogPostCategoriesCommand>();
            services.AddTransient<IUpdateUserProfileCommand, EfUpdateUserProfileCommand>();
            services.AddTransient<IDeleteOneImageCommand, EfDeleteOneImageCommand>();
            services.AddTransient<IDeleteCommentCommand, EfDeleteCommentCommand>();
            services.AddTransient<IDeleteVoteCommand, EfDeleteVoteCommand>();
            services.AddTransient<IDeleteBlogPostCommand, EfDeleteBlogPostCommand>();
            services.AddTransient<IDeleteUserCommand, EfDeleteUserCommand>();
            services.AddTransient<IAddUserUseCaseCommand, EfAddUserUseCaseCommand>();
            services.AddTransient<IRemoveUserUseCaseCommand, EfRemoveUserUseCaseCommand>();
            #endregion

            #region Validators
            services.AddTransient<RegisterUserValidator>();
            services.AddTransient<CreateCategoryValidator>();
            services.AddTransient<CreateBlogPostValidator>();
            services.AddTransient<SearchUseCaseLogsValidator>();
            services.AddTransient<UpdateUserUseCasesValidator>();
            services.AddTransient<CreateCommentValidator>();
            services.AddTransient<CreateVoteValidator>();
            services.AddTransient<UpdateUserProfileValidator>();
            services.AddTransient<PatchBlogPostValidator>();
            services.AddTransient<UpdateBlogPostCategoriesValidator>();
            services.AddTransient<UpdateUserUseCaseValidator>();
            #endregion
        }

        public static void AddApplicationUser(this IServiceCollection services)
        {
            services.AddTransient<IApplicationUser>(x =>
            {
                var accessor = x.GetService<IHttpContextAccessor>();
                var header = accessor.HttpContext.Request.Headers["Authorization"];

                //Pristup payload-u
                var claims = accessor.HttpContext.User;

                if (claims == null || claims.FindFirst("UserId") == null)
                {
                    return new AnonimousUser();
                }

                var actor = new JwtUser
                {
                    Email = claims.FindFirst("Email").Value,
                    Id = Int32.Parse(claims.FindFirst("UserId").Value),
                    Identity = claims.FindFirst("Email").Value,
                    UseCaseIds = JsonConvert.DeserializeObject<List<int>>(claims.FindFirst("UseCases").Value)
                };

                return actor;
            });
        }
    }
}
