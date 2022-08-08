using Camunda.Api.Client;
using Camunda.Api.Client.Deployment;
using Camunda.Api.Client.ProcessDefinition;
using Camunda.Api.Client.ProcessInstance;
using Camunda.Api.Client.Tenant;

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
}