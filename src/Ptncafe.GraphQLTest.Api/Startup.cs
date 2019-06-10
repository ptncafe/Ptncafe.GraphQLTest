using AutoMapper;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ptncafe.GraphQLTest.Api.GraphQLSchema;
using Ptncafe.GraphQLTest.Api.Mapper;
using Ptncafe.GraphQLTest.Proxy;

namespace Ptncafe.GraphQLTest.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient<ICommentProxy, CommentProxy>();

            services.AddAutoMapper(typeof(CommentMapProfile));

            services.AddScoped<GraphQL.IDependencyResolver>(x =>
               new GraphQL.FuncDependencyResolver(x.GetRequiredService));

            services.AddSingleton<CommentQuery>();
            services.AddSingleton<CommentMutation>();

            services.AddScoped<CommentSchema>();

            services.AddGraphQL(options =>
            {
                options.EnableMetrics = true;
                options.ExposeExceptions = true;
            })
            .AddGraphTypes(ServiceLifetime.Scoped)
            .AddUserContextBuilder(httpContext => httpContext.User)
            .AddDataLoader();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // use HTTP middleware for ChatSchema at path /graphql
            app.UseGraphQL<CommentSchema>("/graphql");

            // use graphql-playground middleware at default url /ui/playground
            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions());

            app.UseMvc();
        }
    }
}