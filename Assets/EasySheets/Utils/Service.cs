using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public sealed class Service<T> where T : class {
    static T _instance;

    /// <summary>
    /// Gets global instance of T type.
    /// </summary>
    /// <param name="createIfNotExists">If true and instance not exists - new instance will be created.</param>
    [MethodImpl (MethodImplOptions.AggressiveInlining)]
    public static T Get (bool createIfNotExists = false) {
        if (_instance != null) {
            return _instance;
        }
        if (createIfNotExists) {
            _instance = (T) Activator.CreateInstance (typeof (T), true);
        }
        return _instance;
    }

    /// <summary>
    /// Sets global instance of T type.
    /// </summary>
    /// <param name="instance">New instance of T type.</param>
    [MethodImpl (MethodImplOptions.AggressiveInlining)]
    public static void Set (T instance) {
        _instance = instance;
    }
}