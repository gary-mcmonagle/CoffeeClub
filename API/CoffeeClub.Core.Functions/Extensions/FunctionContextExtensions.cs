using System.Net;
using System.Reflection;
using System.Text.Json;
using CoffeeClub.Domain.Models;
using Microsoft.Azure.Functions.Worker;

namespace CoffeeClub_Core_Functions.Extensions;

public static class FunctionContextExtensions
{
    public static void SetHttpResponseStatusCode(
    this FunctionContext context,
    HttpStatusCode statusCode)
    {
        var coreAssembly = Assembly.Load("Microsoft.Azure.Functions.Worker.Core");
        var featureInterfaceName = "Microsoft.Azure.Functions.Worker.Context.Features.IFunctionBindingsFeature";
        var featureInterfaceType = coreAssembly.GetType(featureInterfaceName);
        var bindingsFeature = context.Features.Single(
            f => f.Key.FullName == featureInterfaceType.FullName).Value;
        var invocationResultProp = featureInterfaceType.GetProperty("InvocationResult");

        var grpcAssembly = Assembly.Load("Microsoft.Azure.Functions.Worker.Grpc");
        var responseDataType = grpcAssembly.GetType("Microsoft.Azure.Functions.Worker.GrpcHttpResponseData");
        var responseData = Activator.CreateInstance(responseDataType, context, statusCode);

        invocationResultProp.SetMethod.Invoke(bindingsFeature, new object[] { responseData });
    }

    public static bool TryGetTokenFromHeaders(this FunctionContext context, out string token)
    {
        token = null;
        // HTTP headers are in the binding context as a JSON object
        // The first checks ensure that we have the JSON string
        if (!context.BindingContext.BindingData.TryGetValue("Headers", out var headersObj))
        {
            return false;
        }

        if (headersObj is not string headersStr)
        {
            return false;
        }

        // Deserialize headers from JSON
        var headers = JsonSerializer.Deserialize<Dictionary<string, string>>(headersStr);
        var normalizedKeyHeaders = headers.ToDictionary(h => h.Key.ToLowerInvariant(), h => h.Value);
        if (!normalizedKeyHeaders.TryGetValue("authorization", out var authHeaderValue))
        {
            // No Authorization header present
            return false;
        }

        if (!authHeaderValue.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
        {
            // Scheme is not Bearer
            return false;
        }

        token = authHeaderValue.Substring("Bearer ".Length).Trim();
        return true;
    }


    public static User GetAuthenticatedUser(this FunctionContext context) => context.Features.Get<User>()!;
}
