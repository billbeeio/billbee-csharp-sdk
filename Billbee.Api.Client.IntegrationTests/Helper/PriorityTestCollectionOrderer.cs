using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;
using Xunit.Abstractions;

namespace Billbee.Api.Client.IntegrationTests
{
    public class PriorityTestCollectionOrderer : ITestCollectionOrderer
    {
        internal const string TypeName = "Billbee.Api.Client.IntegrationTests." + nameof(PriorityTestCollectionOrderer);
        internal const string AssemblyName = "Billbee.Api.Client.IntegrationTests";

        public IEnumerable<ITestCollection> OrderTestCollections(IEnumerable<ITestCollection> testCollections)
        {
            return testCollections
                .Select(testCollection => (
                    testCollection,
                    priority: GetPriority(testCollection)
                ))
                .OrderBy(x => x.priority)
                .Select(x => x.testCollection)
                .ToList()
                ;
        }

        /// <summary>
        /// Test collections are not bound to a specific class, however they
        /// are named by default with the type name as a suffix. We try to
        /// get the class name from the DisplayName and then use reflection to
        /// find the class and OrderAttribute.
        /// </summary>
        private static int GetPriority(
            ITestCollection testCollection)
        {
            var i = testCollection.DisplayName.LastIndexOf(' ');
            if (i <= -1)
            {
                return int.MinValue;
            }

            var className = testCollection.DisplayName.Substring(i + 1);
            var type = Type.GetType(className);
            if (type is null)
            {
                return int.MinValue;
            }

            var attr = type.GetCustomAttribute<PriorityAttribute>();
            return attr?.Priority ?? int.MinValue;
        }
    }
}
