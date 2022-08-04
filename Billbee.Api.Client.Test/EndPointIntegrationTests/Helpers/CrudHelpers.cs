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
    
    public static ApiPagedResult<List<T>> GetAll<T>(Func<ApiPagedResult<List<T>>> func)
    {
        string typeName = typeof(T).Name;
        
        Console.WriteLine();
        Console.WriteLine($"Get all {typeName}s...");
        
        var result = func();
        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Data);
        Console.WriteLine($"Got {result.Data.Count} {typeName}s");
        
        return result;
    }
    
    public static ApiResult<List<T>> GetAll<T>(Func<ApiResult<List<T>>> func)
    {
        string typeName = typeof(T).Name;
        
        Console.WriteLine();
        Console.WriteLine($"Get all {typeName}s...");
        
        var result = func();
        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Data);
        Console.WriteLine($"Got {result.Data.Count} {typeName}s");
        
        return result;
    }

    public static T Create<T, TCreate>(Func<TCreate, T> func, TCreate newItem)
    {
        string typeName = typeof(T).Name;
        
        Console.WriteLine();
        Console.WriteLine($"Create new {typeName}...");
        
        dynamic result = func(newItem);
        Assert.IsNotNull(result);
        Console.WriteLine($"{typeName} created, id={result.Id}");

        return result;
    }
    
    public static ApiResult<T> CreateApiResult<T, TCreate>(Func<TCreate, ApiResult<T>> func, TCreate newItem, bool hasId = true)
    {
        string typeName = typeof(T).Name;
        
        Console.WriteLine();
        Console.WriteLine($"Create new {typeName}...");
        
        dynamic result = func(newItem);
        Assert.IsNotNull(result);
        Assert.IsNotNull(result!.Data);
        Console.WriteLine(hasId ? $"{typeName} created, id={result.Data.Id}" : $"{typeName} created");

        return result;
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
    
    public static ApiResult<T> GetOneApiResult<T>(Func<dynamic, ApiResult<T>> func, dynamic id)
    {
        string typeName = typeof(T).Name;
        
        Console.WriteLine();
        Console.WriteLine($"Get {typeName} with id={id}...");
        
        var result = func(id);
        var gotItem = result;
        Assert.IsNotNull(gotItem);
        Assert.IsNotNull(gotItem.Data);
        Assert.AreEqual(id.ToString(), gotItem.Data.Id.ToString());
        Console.WriteLine($"Got {typeName}, id={gotItem.Data.Id}");

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
    
    public static void Put<T>(Action<T> func, dynamic item)
    {
        string typeName = typeof(T).Name;
        
        Console.WriteLine();
        Console.WriteLine($"Put {typeName} with id={item.Id}...");

        func(item);
        Console.WriteLine($"Done");
    }
    
    public static ApiResult<T> Put<T>(Func<T, ApiResult<T>> func, dynamic item)
    {
        string typeName = typeof(T).Name;
        
        Console.WriteLine();
        Console.WriteLine($"Put {typeName} with id={item.Id}...");

        var updatedItem = func(item);
        Assert.IsNotNull(updatedItem);
        Assert.IsNotNull(updatedItem.Data);
        Console.WriteLine($"Got {typeName}, id={updatedItem.Data.Id}");

        return updatedItem;
    }
    
    public static ApiResult<T> Patch<T>(Func<dynamic, Dictionary<string, string>, ApiResult<T>> func, dynamic id, Dictionary<string, string> fieldsToPatch)
    {
        string typeName = typeof(T).Name;
        
        Console.WriteLine();
        Console.WriteLine($"Patch {typeName} with id={id}...");
        Console.WriteLine($"   Patched fields:");
        foreach (var fields in fieldsToPatch)
        {
            Console.WriteLine($"      {fields.Key}: {fields.Value}");
        }

        var patchedItem = func(id, fieldsToPatch);
        Assert.IsNotNull(patchedItem);
        Assert.IsNotNull(patchedItem.Data);
        Console.WriteLine($"Got {typeName}, id={patchedItem.Data.Id}");

        return patchedItem;
    }
}