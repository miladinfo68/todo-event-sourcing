using OutboxWorker;

IConfiguration Configuration = null;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((hostingContext, cfg) =>
    {
        var env = hostingContext.HostingEnvironment;
        Configuration = new ConfigurationBuilder()
            .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
            .Build();
    })
    .ConfigureServices(services =>
    {
        services.AddDependencies(Configuration);

        services.AddHostedService<WriteOutboxWorker>();
    })
    .Build();

await host.RunAsync();
