using GetMeALibrary.Sql;
using GetMeALifeLibrary.GraphQL.GraphQLQueries;
using GetMeALifeLibrary.GraphQL.GraphQLSchema;
using GetMeALifeLibrary.GraphQL.Types.Get;
using GetMeALifeLibrary.GraphQL.Types.Input;
using GraphQL;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GetMeALifeAPI
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
            services.Add(new ServiceDescriptor(typeof(Database), new Database("server=35.238.128.54;port=3306;database=DATABASELOOKSGOOD;user=USER;password=PSSWRD;")));

            services.AddScoped<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));
            services.AddScoped<AppSchema>();
            services.AddScoped<AppQuery>();
            services.AddScoped<UserGetType>();
            services.AddScoped<UserSettingGetType>();
            services.AddScoped<UserTypeGetType>();
            services.AddScoped<EventGetType>();
            services.AddScoped<EventTypeGetType>();
            services.AddScoped<AppMutation>();
            services.AddScoped<UserInputType>();
            services.AddScoped<UserSettingInputType>();
            services.AddScoped<UserTypeInputType>();
            services.AddScoped<EventTypeInputType>();
            services.AddScoped<EventInputType>();

            services.AddGraphQL(o => { o.ExposeExceptions = false; })
                .AddGraphTypes(ServiceLifetime.Scoped);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseGraphQL<AppSchema>();
            app.UseGraphQLPlayground(options: new GraphQLPlaygroundOptions());

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
