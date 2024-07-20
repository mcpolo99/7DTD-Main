
using SevenDTDMono;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using UnityEngine;

public class AssemblyHelper //: IDisposable
{
    private readonly AppDomain AssemblyDomain;
    //private readonly List<Assembly> _loadedAssembliesList = new List<Assembly>();
    private static readonly Dictionary<string, Assembly> LoadedAssemblies = new Dictionary<string, Assembly>();
    private static readonly List<string> AssembliesToLoad = new List<string>
    {
        //"SevenDTDMono.dll",
        "0Harmony",
        "MonoMod.Utils",
        "UniverseLib.mono",
        "UnityExplorer.STANDALONE.Mono"

    };

    public AssemblyHelper()
    {
        AssemblyDomain = AppDomain.CreateDomain("AssemblyDomain");
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
        if (IsAssemblyLoaded(assemblyName))
        {
            Log.Out($"{assemblyName} is already loaded.");
        }
        else
        {
            string assemblyPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "load", $"{assemblyName}.dll");
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
            if (!IsAssemblyLoaded(assemblyName))
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





    //public void LoadAndExecuteAssembly(string assemblyPath)
    //{
    //    Assembly assembly = assemblyDomain.Load(AssemblyName.GetAssemblyName(assemblyPath));
    //    _loadedAssembliesList.Add(assembly);
    //    // Execute the assembly's entry point or other methods as needed
    //    // ...
    //}

    //public void UnloadAssembly(string assemblyName)
    //{
    //    Assembly assemblyToUnload = _loadedAssembliesList.Find(assembly => assembly.GetName().Name.Equals(assemblyName));
    //    if (assemblyToUnload != null)
    //    {
    //        AppDomain.Unload(assemblyDomain);
    //        assemblyDomain = AppDomain.CreateDomain("AssemblyDomain");
    //        _loadedAssembliesList.Remove(assemblyToUnload);
    //    }
    //}

    //public void Dispose()
    //{
    //    foreach (Assembly assembly in _loadedAssembliesList)
    //    {
    //        AppDomain.Unload(assemblyDomain);
    //    }
    //    assemblyDomain = null;
    //}



    //    public bool IsAssemblyLoaded1(string assemblyName)
    //    {
    //        return _loadedAssembliesList.Exists(assembly => assembly.GetName().Name.Equals(assemblyName));
    //    }



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



#region
//public class AssemblyHelper : IDisposable
//{
//    private AppDomain assemblyDomain;
//    private List<Assembly> loadedAssembliesList = new List<Assembly>();
//    private List<string> assembliesToLoad; 
//    private static Dictionary<string, Assembly> loadedAssemblies = new Dictionary<string, Assembly>();
//    private static List<string> assembliesToLoad1 = new List<string>
//        {

//            "0Harmony",
//            "MonoMod.Utils",
//            "UniverseLib.mono",
//            "UnityExplorer.STANDALONE.Mono"

//        };

//    public AssemblyHelper(List<string> assembliesToLoad) //a#1
//    {
//        this.assembliesToLoad = assembliesToLoad;
//        assemblyDomain = AppDomain.CreateDomain("AssemblyDomain");
//    }

//    public void TryLoad()//a#2
//    {
//        foreach (string assemblyName in assembliesToLoad)
//        {
//            string assemblyPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "load", $"{assemblyName}.dll");
//            if (File.Exists(assemblyPath))
//            {
//                LoadAssembly(assemblyPath);
//            }
//            else
//            {
//                Log.Out($"{assemblyName} is not present at location: {assemblyPath}");
//            }
//        }
//    }
//    public void LoadAssembly(string assemblyPath) //a#3
//    {
//        Assembly assembly = assemblyDomain.Load(AssemblyName.GetAssemblyName(assemblyPath));
//        loadedAssembliesList.Add(assembly);
//        // Execute the assembly's entry point or other methods as needed
//        // ...
//    }
//    public bool AreAllAssembliesLoaded() //a#4
//    {
//        foreach (string assemblyName in assembliesToLoad)
//        {
//            if (!IsAssemblyLoaded(assemblyName))
//            {
//                return false;
//            }
//        }
//        return true;
//    }
//    private bool IsAssemblyLoaded(string assemblyName) //a#5
//    {
//        return loadedAssembliesList.Exists(assembly => assembly.GetName().Name.Equals(assemblyName));
//    }
//    public void Dispose()
//    {
//        foreach (Assembly assembly in loadedAssembliesList)
//        {
//            AppDomain.Unload(assemblyDomain);
//        }
//        assemblyDomain = null;
//    }
//    public void UnloadAssembly(string assemblyName)
//    {
//        Assembly assemblyToUnload = loadedAssembliesList.Find(assembly => assembly.GetName().Name.Equals(assemblyName));
//        if (assemblyToUnload != null)
//        {
//            AppDomain.Unload(assemblyDomain);
//            assemblyDomain = AppDomain.CreateDomain("AssemblyDomain");
//            loadedAssembliesList.Remove(assemblyToUnload);
//        }
//    }


//    public static void TryLoad1() //b#1
//    {
//        //string targetDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Load\\");



//        foreach (string assemblyName in assembliesToLoad1)
//        {
//           LoadAssembly1(assemblyName);
//        }

//        //"7DaysToDie_Data\\Managed\\"
//        // Load additional DLLs here using Assembly.LoadFrom()
//        // For example:
//        // Assembly additionalAssembly = Assembly.LoadFrom("path/to/additional.dll");
//        // Add logic here to use types and methods from the loaded assembly as needed.
//    } 
//    public static void LoadAssembly1(string assemblyName)//b#2
//    {
//        if (!IsAssemblyLoaded1(assemblyName))
//        {


//            string assemblyPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "load", $"{assemblyName}.dll");
//            if (File.Exists(assemblyPath))
//            {
//                Assembly assembly = Assembly.LoadFrom(assemblyPath);
//                loadedAssemblies[assemblyName] = assembly;
//                Log.Out($"{assemblyName} has been loaded.");

//            }
//            else
//            {
//                Log.Out($"{assemblyName} is not present at location: {assemblyPath}");
//            }

//            //Assembly assembly = Assembly.LoadFrom(assemblyPath);
//            //loadedAssemblies[assemblyName] = assembly;

//            //Log.Out($"{assemblyName} has been loaded.");
//        }
//    }
//    public static bool IsAssemblyLoaded1(string assemblyName) //b#3
//    {
//        return loadedAssemblies.ContainsKey(assemblyName);
//    }
//    public static bool AreAllAssembliesLoaded1() // b use outside
//    {
//        foreach (string assemblyName in assembliesToLoad1)
//        {
//            if (!IsAssemblyLoaded1(assemblyName))
//            {
//                return false;
//            }
//        }

//        return true;
//    }





//}


#endregion