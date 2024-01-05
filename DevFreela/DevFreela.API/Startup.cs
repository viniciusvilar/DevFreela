using System.Reflection;
using System;

namespace DevFreela.API;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        //var conectionString = Configuration.GetConnectionString("ContextConnection");

        /*services.AddDbContext<AppDbContext>(opts => opts
            .UseLazyLoadingProxies()
            .UseMySQL(conectionString!)
        );*/

        services.AddCors();

        /*services.AddAuthentication(opts =>
        {
            opts.DefaultAuthenticateScheme =
                JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(opts =>
        {
            opts.TokenValidationParameters =
                new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes("fedaf7d8863b48e197b9287d492b708e")),
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ClockSkew = TimeSpan.Zero
                };
        });*/

        //AddInjectableServices(services);

        services.AddControllers();
        services.AddSwaggerGen();
        //services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }

    /*private static void AddInjectableServices(IServiceCollection services)
    {
        Assembly assembly = Assembly.GetExecutingAssembly();
        Type markupInterface = typeof(IInjectable);

        List<Type> injectableClasses = assembly.GetTypes()
            .Where(t => markupInterface.IsAssignableFrom(t) && !t.IsInterface)
            .ToList();

        foreach (Type injectableClass in injectableClasses)
        {
            Type serviceInterface = injectableClass.GetInterfaces()
                .First(i => i.Name == $"I{injectableClass.Name}");

            services.AddScoped(serviceInterface, injectableClass);
        }*/

    public void AddJsonFiles(IConfigurationBuilder builder, IWebHostEnvironment env)
    {
        builder
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json");
            //.AddJsonFile("applogger.json")
            //.AddJsonFile($"applogger.{env.EnvironmentName}.json");
    }
}
