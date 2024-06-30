using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Linq;

public class ASMCHECK
{
    private static List<string> assembliesToCheck = new List<string>
    {
        "SevenDTDMono",
        "0Harmony",
        "MonoMod.Utils",
        "UniverseLib.mono",
        "UnityExplorer.STANDALONE.Mono"

    };

    public static bool CheckLoadedAssemblies()
    {
        // Get the loaded assemblies in the current application domain
        Assembly[] loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies();

        // Convert the assembly names to lowercase for case-insensitive comparison
        List<string> loadedAssemblyNames = loadedAssemblies.Select(assembly => assembly.GetName().Name.ToLower()).ToList();

        // Check if each assembly in assembliesToCheck is loaded
        foreach (string assemblyToCheck in assembliesToCheck)
        {
            string assemblyNameLowercase = assemblyToCheck.ToLower();
            if (!loadedAssemblyNames.Contains(assemblyNameLowercase))
            {
                //Console.WriteLine($"Assembly '{assemblyToCheck}' is not loaded.");
                Log.Out($"Assembly '{assemblyToCheck}' is not loaded.");
                return false;
            }
            Log.Out($"Assembly '{assemblyToCheck}' is loaded.");
        }
       
        // If all assemblies are loaded, return true
        return true;
    }
    public static void CheckLoadedAssemblies1()
    {
        // Get the loaded assemblies in the current application domain
        Assembly[] loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies();

        // Convert the assembly names to lowercase for case-insensitive comparison
        string[] loadedAssemblyNames = loadedAssemblies.Select(assembly => assembly.GetName().Name.ToLower()).ToArray();

        // Check if each assembly in assembliesToCheck is loaded
        foreach (string assemblyToCheck in assembliesToCheck)
        {
            string assemblyNameLowercase = assemblyToCheck.ToLower();
            if (loadedAssemblyNames.Contains(assemblyNameLowercase))
            {
                Log.Out($"Assembly '{assemblyToCheck}' is loaded.");
               // Console.WriteLine($"Assembly '{assemblyToCheck}' is loaded.");
            }
            else
            {
                Log.Out($"Assembly '{assemblyToCheck}' is not loaded.");
                //Console.WriteLine($"Assembly '{assemblyToCheck}' is not loaded.");
            }
        }
    }
}