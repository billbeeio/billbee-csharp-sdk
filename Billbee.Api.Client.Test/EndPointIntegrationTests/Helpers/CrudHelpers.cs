using Billbee.Api.Client.Model;

namespace Billbee.Api.Client.Test.EndPointIntegrationTests.Helpers;

public static class CrudHelpers
{
    public static List<T> GetAll<T>(Func<List<T>> func)
    {
        string typeName = typeof(T).Name;
        
        Console.WriteLine();
        Console.WriteLine($"Get all {typeName}s...");
        
        var result = func();
        Assert.IsNotNull(result);
        Console.WriteLine($"Got {result.Count} {typeName}s");
        
        return result;
    }

    public static T Create<T>(Func<T> func)
    {
        string typeName = typeof(T).Name;
        
        Console.WriteLine();
        Console.WriteLine($"Create new {typeName}...");
        
        dynamic result = func();
        var createdItem = result;
        Assert.IsNotNull(createdItem);
        Console.WriteLine($"{typeName} created, id={createdItem.Id}");

        return createdItem;
    }

    public static T GetOne<T>(Func<dynamic, T> func, dynamic id)
    {
        string typeName = typeof(T).Name;
        
        Console.WriteLine();
        Console.WriteLine($"Get {typeName} with id={id}...");
        
        var result = func(id);
        var gotItem = result;
        Assert.IsNotNull(gotItem);
        Assert.AreEqual(id, gotItem.Id);
        Console.WriteLine($"Got {typeName}, id={gotItem.Id}");

        return gotItem;
    }

    public static void DeleteOne<T>(Action<dynamic> func, dynamic id)
    {
        string typeName = typeof(T).Name;
        
        Console.WriteLine();
        Console.WriteLine($"Delete {typeName} with id={id}");
        
        func(id);
        Console.WriteLine($"Deleted {typeName}");
    }

    public static void DeleteAll<T>(Action func)
    {
        string typeName = typeof(T).Name;
        
        Console.WriteLine();
        Console.WriteLine($"Delete all {typeName}s");
        
        func();
        Console.WriteLine($"Deleted all {typeName}s");
    }

    public static void GetOneExpectException<T>(Func<dynamic, T> func, dynamic id)
    {
        string typeName = typeof(T).Name;
        
        Console.WriteLine();
        Console.WriteLine($"Get {typeName} with id={id}...");
        
        Assert.ThrowsException<Exception>(() => func(id));
        Console.WriteLine($"{typeName} could not be found (as expected)");
    }

    public static T Put<T>(Func<T, T> func, dynamic item)
    {
        string typeName = typeof(T).Name;
        
        Console.WriteLine();
        Console.WriteLine($"Put {typeName} with id={item.Id}...");

        var updatedItem = func(item);
        Console.WriteLine($"Got {typeName}, id={updatedItem.Id}");

        return updatedItem;
    }
    
    public static void Put<T>(Action<T> func, dynamic item)
    {
        string typeName = typeof(T).Name;
        
        Console.WriteLine();
        Console.WriteLine($"Put {typeName} with id={item.Id}...");

        func(item);
        Console.WriteLine($"Done");
    }
}