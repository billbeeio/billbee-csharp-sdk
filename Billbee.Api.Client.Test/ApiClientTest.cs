using System.Reflection;
using System.Text;
using Billbee.Api.Client.EndPoint;
using Billbee.Api.Client.Enums;

namespace Billbee.Api.Client.Test;

[TestClass]
public class ApiClientTest
{
    [TestMethod]
    public void InitTest()
    {
        var uut = new ApiClient();
        
        Assert.IsNotNull(uut);
        Assert.IsNotNull(uut.Configuration);
        Assert.IsNull(uut.Configuration.Username);
        Assert.IsNull(uut.Configuration.Password);
        Assert.IsNull(uut.Configuration.ApiKey);
        Assert.AreEqual("https://app.billbee.io/api/v1", uut.Configuration.BaseUrl);
        Assert.AreEqual(ErrorHandlingEnum.ThrowException, uut.Configuration.ErrorHandlingBehaviour);
        
        Assert.IsNotNull(uut.AutomaticProvision);
        Assert.IsNotNull(uut.CloudStorages);
        Assert.IsNotNull(uut.Customer);
        Assert.IsNotNull(uut.Events);
        Assert.IsNotNull(uut.Orders);
        Assert.IsNotNull(uut.Products);
        Assert.IsNotNull(uut.Search);
        Assert.IsNotNull(uut.Shipment);
        Assert.IsNotNull(uut.Webhooks);
    }

    [TestMethod]
    public void InitWithConfigTest()
    {
        var config = new ApiConfiguration
        {
            Username = "myUser",
            Password = "myPwd",
            ApiKey = "myApiKey",
            BaseUrl = "myBaseUrl",
            ErrorHandlingBehaviour = ErrorHandlingEnum.ReturnErrorContent
        };
        
        var uut = new ApiClient(config);
        
        Assert.IsNotNull(uut);
        Assert.IsNotNull(uut.Configuration);
        Assert.AreEqual("myUser", uut.Configuration.Username);
        Assert.AreEqual("myPwd", uut.Configuration.Password);
        Assert.AreEqual("myApiKey", uut.Configuration.ApiKey);
        Assert.AreEqual("myBaseUrl", uut.Configuration.BaseUrl);
        Assert.AreEqual(ErrorHandlingEnum.ReturnErrorContent, uut.Configuration.ErrorHandlingBehaviour);
    }

    [TestMethod]
    public void InitWithConfigFileTest()
    {
        var fiDll = new FileInfo(Assembly.GetExecutingAssembly().Location);
        var path = Path.Combine(fiDll.Directory.FullName, "../../../config.test");
        var uut = new ApiClient(path);
        
        Assert.IsNotNull(uut);
        Assert.IsNotNull(uut.Configuration);
        Assert.AreEqual("myUserFromFile", uut.Configuration.Username);
        Assert.AreEqual("myPwdFromFile", uut.Configuration.Password);
        Assert.AreEqual("myApiKeyFromFile", uut.Configuration.ApiKey);
        Assert.AreEqual("myBaseUrlFromFile", uut.Configuration.BaseUrl);
        Assert.AreEqual(ErrorHandlingEnum.ReturnErrorContent, uut.Configuration.ErrorHandlingBehaviour);
    }

    private class TypeMapping
    {
        public TypeMapping(string uutClass, string uutTypeName, string testTypeName, bool foundMapping)
        {
            UutClass = uutClass;
            UutTypeName = uutTypeName;
            TestTypeName = testTypeName;
            FoundMapping = foundMapping;
        }

        public string UutClass { get; }
        public string UutTypeName { get; }
        public string TestTypeName { get; }
        public bool FoundMapping { get; }
    }
    
    [TestMethod]
    public void UnitTestsForAllEndpointsTest()
    {
        CheckTestMethods("Test", "Test");
    }

    [TestMethod]
    public void IntegrationTestsForAllEndpointsTest()
    {
        CheckTestMethods( "IntegrationTest", "_IntegrationTest");
    }

    private void CheckTestMethods(string testClassPostfix, string testMethodPostfix)
    {
        var clientAssembly = Assembly.Load("Billbee.Api.Client");
        var clientTypes = clientAssembly.GetTypes();
        var testAssembly = Assembly.GetExecutingAssembly();
        var testTypes = testAssembly.GetTypes();

        var typeMappingsTestMethods = new List<TypeMapping>();
        foreach (var clientType in clientTypes)
        {
            var clientTypeName = clientType.Name;
            if (clientType.Namespace == null || !clientType.Namespace.StartsWith("Billbee.Api.Client.EndPoint"))
            {
                continue;
            }

            var testTypeName = clientType.Name + testClassPostfix;
            var testType = testTypes.FirstOrDefault(t => t.IsClass && t.IsPublic && t.Name == testTypeName && t.GetCustomAttributes().Any(a => a is TestClassAttribute));

            var bindingFlags = BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Instance;
            var clientMethods = clientType.GetMethods(bindingFlags);
            var testMethods = testType.GetMethods(bindingFlags).Where(m => m.GetCustomAttributes().Any(a => a is TestMethodAttribute)).ToList();
            foreach (var clientMethod in clientMethods)
            {
                var clientMethodName = clientMethod.Name;
                
                var testMethodName = clientMethodName + testMethodPostfix;
                var foundMapping = testMethods.Any(t => t.Name == testMethodName);
                if (clientTypeName != nameof(EnumEndPoint))
                {
                    typeMappingsTestMethods.Add(new TypeMapping(clientTypeName, clientMethodName, testMethodName, foundMapping));
                }
            }
        }

        Console.WriteLine($"#SdkMethods={typeMappingsTestMethods.Count}, #{testMethodPostfix}Methods={typeMappingsTestMethods.Count(t => t.FoundMapping)}");
        Console.WriteLine();
        Console.WriteLine($"Test implemented,ClassName,UutTypeName,TestTypeName");
        foreach (var typeMapping in typeMappingsTestMethods)
        {
            Console.WriteLine($"{typeMapping.FoundMapping},{typeMapping.UutClass},{typeMapping.UutTypeName},{typeMapping.TestTypeName}");
        }
        
        Assert.IsTrue(typeMappingsTestMethods.All(t => t.FoundMapping));
    }
}