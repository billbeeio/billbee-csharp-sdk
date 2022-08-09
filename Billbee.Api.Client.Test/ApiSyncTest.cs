using System.Diagnostics;
using System.Reflection;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Extensions;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Readers;

namespace Billbee.Api.Client.Test;

[TestClass]
public class ApiSyncTest
{
    [DebuggerDisplay("{Path}:{HttpOperation}")]
    public class ApiOperation
    {
        public string Path { get; set; } = null!;
        public HttpOperation HttpOperation { get; set; }

        public override string ToString() => $"{Path}:{HttpOperation}";
    }
    
    [TestMethod]
    public async Task ApiSyncCheckTest()
    {
        var sdkOps = GetSdkOperations();
        var apiOps = await GetApiOperations();

        var missingSdkOps = new List<ApiOperation>();
        foreach (var apiOp in apiOps)
        {
            if (!sdkOps.Any(sdkOp => sdkOp.Path == apiOp.Path && sdkOp.HttpOperation == apiOp.HttpOperation))
            {
                missingSdkOps.Add(apiOp);
            }
        }

        Console.WriteLine($"#ApiOps={apiOps.Count}, #SdkOps={sdkOps.Count}, #MissingSdkOps={missingSdkOps.Count}");
        
        if (missingSdkOps.Count > 0)
        {
            Console.WriteLine("Missing Sdk Operations:");
            foreach (var missingSdkOp in missingSdkOps)
            {
                Console.WriteLine(missingSdkOp.ToString());
            }
        }
        
        Assert.AreEqual(0, missingSdkOps.Count);
    }

    private async Task<List<ApiOperation>> GetApiOperations()
    {
        var apiOps = new List<ApiOperation>();
        var openApiDoc = await GetOpenApiDoc();
        foreach (var path in openApiDoc.Paths)
        {
            foreach (var op in path.Value.Operations)
            {
                if (Enum.TryParse(op.Key.ToString(), out HttpOperation httpOperation))
                {
                    var apiOp = new ApiOperation
                    {
                        Path = path.Key,
                        HttpOperation = httpOperation
                    };
                    apiOps.Add(apiOp);
                }
                else
                {
                    Assert.Fail("unknown Path.OperationType");
                }
            }
        }

        return apiOps;
    }

    private static List<ApiOperation> GetSdkOperations()
    {
        var sdkOps = new List<ApiOperation>();
        var endpointTypes = Assembly.GetAssembly(typeof(ApiClient))?.GetTypes()
            .Where(t => t.Namespace == "Billbee.Api.Client.EndPoint");
        Assert.IsNotNull(endpointTypes);
        foreach (var endpoint in endpointTypes)
        {
            foreach (var methodInfo in endpoint.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic |
                                                           BindingFlags.Public))
            {
                if (methodInfo.GetCustomAttributes(true).FirstOrDefault(x => x is ApiMappingAttribute) is
                    ApiMappingAttribute apiMappingAttr)
                {
                    var op = new ApiOperation
                    {
                        Path = apiMappingAttr.ApiPath,
                        HttpOperation = apiMappingAttr.HttpOperation
                    };
                    sdkOps.Add(op);
                }
            }
        }

        return sdkOps;
    }

    private async Task<OpenApiDocument> GetOpenApiDoc()
    {
        var httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://app.billbee.io/")
        };

        var stream = await httpClient.GetStreamAsync("/swagger/docs/v1");

        return new OpenApiStreamReader().Read(stream, out var diagnostic);
    }
}