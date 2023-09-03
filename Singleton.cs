using System;
using UnityEngine;

namespace FZFUI
{
    /// <summary>
    /// Must have parameterless constructor and static methods to extend functionality.
    /// </summary>
    /// <typeparam name="T">Singleton class type</typeparam>
    public abstract class Singleton<T> where T: new()
    {
        protected static T Instance { get; private set; } = new();
    }
    
    /// <summary>
    /// Must be active in the scene to bind the instance on awake. Otherwise it will work like lazy initialization instead.
    /// Must have 
    /// </summary>
    /// <typeparam name="T">Singleton class type</typeparam>
    public abstract class MonoSingleton<T>: MonoBehaviour where T: MonoBehaviour
    {
        protected static T Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
                Instance = this as T;
            else if(Instance != this)
                throw new ArgumentException($"There are two instances of {GetType().FullName} singleton.");
        }
    }
}