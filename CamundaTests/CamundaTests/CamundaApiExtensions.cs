using System.Reflection;
using Camunda.Api.Client;
using Camunda.Api.Client.Deployment;
using Camunda.Api.Client.History;
using Camunda.Api.Client.ProcessDefinition;
using Camunda.Api.Client.ProcessInstance;
using Camunda.Api.Client.Tenant;
using Newtonsoft.Json;
using Refit;

namespace CamundaTests;

public static class CamundaApiExtensions
{
    public static async Task<bool> CreateTenant(this CamundaClient client, string tenant)
    {
        var tenants = await client.Tenants
            .Query(new TenantQuery())
            .List();

        if (tenants.Any(t => t.Name == tenant)) return false;

        await client.Tenants.Create(new TenantInfo
        {
            Id = tenant,
            Name = tenant
        });
        return true;
    }
    
    public static async Task<ProcessInstanceWithVariables> StartProcessByKey(this CamundaClient client, string tenantId, string key, Dictionary<string, VariableValue> variables)
    {
        return await client.ProcessDefinitions.ByKey(key, tenantId).StartProcessInstance(new StartProcessInstance()
        {
            
            Variables = variables
        });
    }
    
    public static async Task<DeploymentInfo?> UploadProcessDefinition(this CamundaClient client, string tenantId, byte[] bytes, string fileName)
    {
        await using var memoryStream = new MemoryStream();
        memoryStream.Write(bytes, 0, bytes.Length);
        memoryStream.Position = 0;
        var resource = new ResourceDataContent(memoryStream, fileName);
        var deployment = await client.Deployments.Create("", false, false, "", tenantId, resource);
        return deployment;
    }


    public static async Task DeliverMessage(this CamundaClient client, ProcessInstanceDeliverMessage message)
    {
        string? hostUrl = typeof(CamundaClient).GetField("_hostUrl", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(client) as string;
        RefitSettings? refitSettings = typeof(CamundaClient).GetField("_refitSettings", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(client) as RefitSettings;

        var restService = RestService.For<IProcessInstanceExtensionsClient>(hostUrl, refitSettings);
        await restService.CorrelateMessage(message);
    }
}

public interface IProcessInstanceExtensionsClient
{
    [Post("/process-instance/message-async")]
    Task CorrelateMessage(
        [Body] ProcessInstanceDeliverMessage body);
}

public class ProcessInstanceDeliverMessage
{
    [JsonProperty("messageName")]
    public string MessageName { get; set; }
    
    [JsonProperty("processInstanceIds")]
    public string[] InstanceId { get; set; }
}