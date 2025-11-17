using Dirstribute;
using Dirstribute.Helpers;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddSingleton(new ConfigurationService(builder.Configuration));
builder.Services.AddHostedService<MainWorker>();
builder.Services.AddSingleton<DirstributeServer>();
builder.Services.AddHostedService<NamedPipeServer>();

var host = builder.Build();
host.Run();
