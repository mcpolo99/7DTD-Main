
using SevenDTDMono;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using UnityEngine;


namespace SevenDTDMono.Utils
{
    public class AssemblyHelper //: IDisposable
    {
        private readonly AppDomain _assemblyDomain;

        //private readonly List<Assembly> _loadedAssembliesList = new List<Assembly>();
        private static readonly Dictionary<string, Assembly> LoadedAssemblies = new Dictionary<string, Assembly>();

        private static readonly List<string> AssembliesToLoad = new List<string>
        {
            //"SevenDTDMono.dll",
            "0Harmony", "MonoMod.Utils", "UniverseLib.mono", "UnityExplorer.STANDALONE.Mono"
        };

        public AssemblyHelper()
        {
            _assemblyDomain = AppDomain.CreateDomain("AssemblyDomain");
        }


        /// <summary>
        /// Loop list of assemblies to load and Loads the reference Assemblies for use with Unity Explorer
        /// </summary>
        public void TryLoad()
        {
            if (AssembliesToLoad != null)
            {
                foreach (string assemblyName in AssembliesToLoad)
                {
                    LoadAssembly(assemblyName);
                }
            }
        }


        /// <summary>
        /// Load a assembly into current appdomain.
        /// </summary>
        /// <param name="assemblyName"> Name of assembly to be loaded</param>
        private static void LoadAssembly(string assemblyName)
        {
            if (IsAssemblyActuallyLoaded(assemblyName))
            {
                Log.Out($"{assemblyName} is already loaded.");
            }
            else
            {
                string assemblyPath =
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "load", $"{assemblyName}.dll");
                if (File.Exists(assemblyPath))
                {
                    Assembly assembly = Assembly.LoadFrom(assemblyPath);
                    LoadedAssemblies[assemblyName] = assembly;
                    Log.Out($"{assemblyName} has been loaded.");

                }
                else
                {
                    Log.Out($"{assemblyName} is not present at location: {assemblyPath}");
                    NewSettings.Instance.AssemblyPreLoaded = false;
                }
            }
        }

        /// <summary>
        /// Checks if all assemblies has been loaded! 
        /// </summary>
        /// <returns></returns>
        public bool AreAllAssembliesLoaded()
        {
            foreach (string assemblyName in AssembliesToLoad)
            {
                if (!IsAssemblyActuallyLoaded(assemblyName))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Checks if single assembly has ben loaded
        /// </summary>
        /// <param name="assemblyName">Name of assembly to check</param>
        /// <returns></returns>
        private static bool IsAssemblyLoaded(string assemblyName)
        {
            return LoadedAssemblies.ContainsKey(assemblyName);
        }

        private static bool IsAssemblyActuallyLoaded(string assemblyName)
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (assembly.GetName().Name.Equals(assemblyName, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }
    }

    public class ObjectDestroyer : MonoBehaviour
    {
        // Method to destroy the object if it matches the specified assembly
        public static void DestroyObjectFromAssembly(string objectName, string assemblyName)
        {
            // Find the object by name
            GameObject obj = GameObject.Find(objectName);
            if (obj == null)
            {
                Debug.LogWarning($"Object with name '{objectName}' not found.");
                return;
            }

            // Verify the assembly of the object's script components
            MonoBehaviour[] components = obj.GetComponents<MonoBehaviour>();
            foreach (var component in components)
            {
                Assembly assembly = component.GetType().Assembly;
                if (assembly.FullName.Contains(assemblyName))
                {
                    // Destroy the object if it was loaded from the specified assembly
                    Debug.Log($"Destroying object '{objectName}' loaded from assembly '{assemblyName}'.");
                    Destroy(obj);
                    return;
                }
            }

            Debug.LogWarning($"No component on object '{objectName}' was loaded from assembly '{assemblyName}'.");
        }


        public static void DestroyObjectByName(string objectName)
        {
            // Find the object by name
            GameObject obj = GameObject.Find(objectName);
            if (obj == null)
            {
                Debug.LogWarning($"Object with name '{objectName}' not found.");
                return;
            }

            // Destroy the object
            Debug.Log($"Destroying object '{objectName}'.");
            Destroy(obj);
        }
    }
}