using Billbee.Api.Client.IntegrationTests;
using Xunit;

[assembly: CollectionBehavior(DisableTestParallelization = true)]
[assembly: TestCollectionOrderer(
    PriorityTestCollectionOrderer.TypeName,
    PriorityTestCollectionOrderer.AssemblyName)] 