using System.Collections.Generic;
using System.Linq;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Billbee.Api.Client.IntegrationTests
{

    public class PriorityTestCaseOrderer : ITestCaseOrderer
    {
        internal const string TypeName = "Billbee.Api.Client.IntegrationTests." + nameof(PriorityTestCaseOrderer);
        internal const string AssemblyName = "Billbee.Api.Client.IntegrationTests";

        public IEnumerable<TTestCase> OrderTestCases<TTestCase>(IEnumerable<TTestCase> testCases) where TTestCase : ITestCase
        {
            return testCases
                .Select(testCase => (
                    testCase,
                    priority: GetPriority(testCase)
                ))
                .OrderBy(x => x.priority)
                .Select(x => x.testCase)
                .ToList()
                ;
        }

        private static int GetPriority(ITestCase testCase)
        {
            return testCase.TestMethod.Method
                .GetCustomAttributes(typeof(PriorityAttribute).AssemblyQualifiedName)
                .Select(attributeInfo => attributeInfo.GetNamedArgument<int>(nameof(PriorityAttribute.Priority)))
                .DefaultIfEmpty(int.MinValue)
                .FirstOrDefault();
        }
    }
}
