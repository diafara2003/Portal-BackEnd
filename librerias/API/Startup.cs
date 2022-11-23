using API.Configuration;
using API.Helper;
using Code.Repository.Email.Model;
using Code.Repository.EntityFramework.Context;
using Code.Repository.Session;
using Code.Repository.Session.Implementation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IO;
using System.Text;

namespace API
{


    public class Startup
    {

        const string origins = "CorsPolicy";
        public IConfiguration Configuration { get; }




        public Startup(IConfiguration configuration)
        {
            //Data Source=cumbal;Trusted_connection=yes;Initial Catalog=ADPROVEEDOR
            Configuration = configuration;
            //configuration = new ConfigurationBuilder()
            //    .SetBasePath(System.IO.Directory.GetCurrentDirectory())
            //    .AddJsonFile("appsettings.json")
            //.Build();

        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var key = Encoding.UTF8.GetBytes(Configuration["Authentication:SecretKey"]);

            services.Configure<JwtConfig>(Configuration.GetSection("Authentication"));
            services.Configure<EmailSettingsDTO>(Configuration.GetSection("EmailSettings"));

            DbConexion._cnn = Configuration.GetSection("ConnectionStrings:zupplaconnection").Value;


            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "clientapp/build";
            });

            services.AddCors(options =>
            {
                options.AddPolicy(origins,
                    //builder => builder.WithOrigins("http://10.1.10.31")
                    builder => builder.WithOrigins("*")
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IconstructoraRepository, ConstructoraRepository>();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    RequireExpirationTime = false
                };

            });


            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            });


            services.AddAuthorization();




            //   ConfigurateContainer(_builder);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //    app.UseSwagger();
            //    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
            //}


            // global error handler
            app.UseMiddleware<ErrorHandlerMiddleware>();

            // global cors policy
          



            app.UseHttpsRedirection();
            //app.UseStaticFiles();
            //app.UseStaticFiles(new StaticFileOptions
            //{
            //    FileProvider = new PhysicalFileProvider(
            // Path.Combine(env.ContentRootPath, "clientapp/build/assets")),
            //    RequestPath = "/assets"
            //});
          


            app.UseRouting();

            app.UseCors(origins);

            app.UseAuthentication();
            app.UseAuthorization();
           
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "api",
                    pattern: "api/{controller}/{action=Index}/{id?}");

                endpoints.MapControllers();
            });

            

            //Inicia el Cliente            
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "clientapp";
                // if (env.IsDevelopment()) { spa.UseReactDevelopmentServer(npmScript: "start"); }
            });
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //});


            
        }
    }
}
