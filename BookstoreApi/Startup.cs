using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookstoreAplication.Commands;
using EfCommands;
using EfDataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BookstoreApi
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddDbContext<BookstoreContext>();

            services.AddTransient<IGetBooksCommand, EfGetBooksCommand>();
            services.AddTransient<IGetBookCommand, EfGetBookCommand>();
            services.AddTransient<IAddBookCommand, EfAddBookCommand>();
            services.AddTransient<IDeleteBookCommand, EfDeleteBookCommand>();
            services.AddTransient<IEditBookCommand, EfEditBookCommand>();

            services.AddTransient<IGetAuthorsCommand, EfGetAuthorsCommand>();
            services.AddTransient<IGetAuthorCommand, EfGetAuthorCommand>();
            services.AddTransient<IAddAuthorCommand, EfAddAuthorCommand>();
            services.AddTransient<IDeleteAuthorCommand, EfDeleteAuthorCommand>();
            services.AddTransient<IEditAuthorCommand, EfEditAuthorCommand>();

            services.AddTransient<IGetAccessoriesCommand, EfGetAccessoriesCommand>();
            services.AddTransient<IGetAccessoryCommand, EfGetAccessoryCommand>();
            services.AddTransient<IAddAccessoryCommand, EfAddAccessoryCommand>();
            services.AddTransient<IDeleteAccessoryCommand, EfDeleteAccessoryCommand>();
            services.AddTransient<IEditAccessoryCommand, EfEditAccessoryCommand>();
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

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
