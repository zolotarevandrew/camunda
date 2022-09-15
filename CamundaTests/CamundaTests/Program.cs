
using System.Reflection;
using Camunda.Api.Client;
using Camunda.Api.Client.Deployment;
using Camunda.Api.Client.Message;
using Camunda.Worker;
using Camunda.Worker.Client;
using CamundaTests;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddExternalTaskClient(client =>
        {
            client.BaseAddress = new Uri("http://localhost:8080/engine-rest");
        });
        services.AddScoped<CamundaClient>(s => CamundaClient.Create("http://localhost:8080/engine-rest"));
        var worker = services.AddCamundaWorker("sampleWorker");
        var types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a => a.GetTypes());

        var filteredTypes = types.Where(t => t.GetInterfaces().Contains(typeof(IExternalTaskHandler)));
        worker = filteredTypes.Aggregate(worker, (current, type) => current.AddTypeHandler(type));

        worker.ConfigurePipeline(pipeline =>
        {
            pipeline.Use(next => async context =>
            {
                var logger = context.ServiceProvider.GetRequiredService<ILogger<Program>>();
                logger.LogInformation("Started processing of task {Id}", context.Task.Id);
                await next(context);
                logger.LogInformation("Finished processing of task {Id}", context.Task.Id);
            });
        });
    })
    .Build();

var camunda = host.Services.GetRequiredService<CamundaClient>();
string tenantId = "test";
await camunda.CreateTenant(tenantId);
var name = "timer_start_event";
var process = await camunda.StartProcessByKey(tenantId, name, new Dictionary<string, VariableValue>
{
    
});


async Task<DeploymentInfo?> Deploy(string fileName)
{
    var file = Path.Combine(Directory.GetCurrentDirectory(), "bpmn", $"{fileName}.bpmn");
    var bytes = await File.ReadAllBytesAsync(file);
    var filename = Path.GetFileName(file);
    return await camunda.UploadProcessDefinition(tenantId, bytes, filename);
}

await host.RunAsync();