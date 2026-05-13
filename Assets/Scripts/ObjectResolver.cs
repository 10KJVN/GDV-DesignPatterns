using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectResolver
{
    private readonly HashSet<Type> _registrations = new();
    private readonly Dictionary<Type, object> _instancePerTypeMap = new();

    // Registry
    public void RegisterInstance<T>(T instance)
    {
        _instancePerTypeMap[typeof(T)] = instance;
    }

    public void UnregisterType<T>()
    {
        _instancePerTypeMap.Remove(typeof(T));
    }

    public T Resolve<T>()
    {
        Type instanceType = typeof(T);
        if (_instancePerTypeMap.TryGetValue(instanceType, out var instance))
        {
            return (T)instance;
        }
        
        Debug.LogError($"Couldn't resolve type {instanceType}");
        return default;
    }

    // Reflection
    private object Resolve(Type instanceType)
    {
        if (_instancePerTypeMap.TryGetValue(instanceType, out var instance))
        {
            return instance;
            
        }

        if (!_registrations.Contains(instanceType))
        {
            Debug.LogError($"Couldn't resolve type {instanceType}");
            return null;
        }

        var constructor = instanceType.GetConstructors().First();
        var args = constructor.GetParameters().Select(p => Resolve(p.ParameterType)).ToArray();
        instance = Activator.CreateInstance(instanceType, args);
        
        _instancePerTypeMap[instanceType] = instance;
        return instance;
    }
}