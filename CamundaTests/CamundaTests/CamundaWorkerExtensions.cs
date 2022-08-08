using System.Reflection;
using Camunda.Worker;
using Camunda.Worker.Execution;
using Microsoft.Extensions.DependencyInjection;

namespace CamundaTests;

public static class CamundaWorkerExtensions
{
    public static ICamundaWorkerBuilder AddTypeHandler(this ICamundaWorkerBuilder builder, Type type)
    {
        var services = builder.Services;
        services.AddTransient(type);

        var metadata = CollectMetadataFromAttributes(type);
        return builder.AddHandler((c) => HandlerDelegate(c, type), metadata);
    }
    
    private static Task HandlerDelegate(IExternalTaskContext context, Type type)
    {
        IExternalTaskHandler? handler = context.ServiceProvider.GetRequiredService(type) as IExternalTaskHandler;
        if (handler == null) throw new InvalidOperationException("invalid external task handler provided");
        
        var invoker = new HandlerInvoker(handler, context);
        return invoker.InvokeAsync();
    }
    
    private static HandlerMetadata CollectMetadataFromAttributes(Type handlerType)
    {
        var topicsAttribute = handlerType.GetCustomAttribute<HandlerTopicsAttribute>();

        if (topicsAttribute == null)
        {
            throw new Exception($"\"{handlerType.FullName}\" doesn't provide any \"HandlerTopicsAttribute\"");
        }

        var variablesAttribute = handlerType.GetCustomAttribute<HandlerVariablesAttribute>();

        return new HandlerMetadata(topicsAttribute.TopicNames, topicsAttribute.LockDuration)
        {
            LocalVariables = variablesAttribute?.LocalVariables ?? false,
            Variables =  variablesAttribute?.AllVariables ?? false ? null : variablesAttribute?.Variables,
            IncludeExtensionProperties = topicsAttribute.IncludeExtensionProperties
        };
    }
}