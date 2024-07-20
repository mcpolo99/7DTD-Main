using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;



namespace SevenDTDMono
{
    public class Loader
    {
        internal static UnityEngine.GameObject gameObject;
        public static string baseName = "7DTD----MENU";
        public static string newObjectName = baseName;
        public static int index = 1;

       
        public static AssemblyHelper AssemblyHelper; // Add a member variable

        public static void Load()
        {
            gameObject = new UnityEngine.GameObject();


    #if RELEASE_UE || DEBUG
            AssemblyHelper = new AssemblyHelper();
            AssemblyHelper.TryLoad();
    #endif


            gameObject.name = baseName;
            gameObject.AddComponent<Objects>();
            gameObject.AddComponent<NewMenu>();
            gameObject.AddComponent<Features.Cheat>();
            gameObject.AddComponent<NewSettings>();
            //gameObject.AddComponent<ESP>();
            //gameObject.AddComponent<Visuals>();
            //gameObject.AddComponent<Aimbot>();
            //gameObject.AddComponent<Render>();
            //gameObject.AddComponent<SceneDebugger>();
            //gameObject.AddComponent<CBuffs>();
            //gameObject.AddComponent<EasterEggManager>();          
#if RELEASE_UE || DEBUG
            InitializeUnityExplorer();
#endif
            UnityEngine.Object.DontDestroyOnLoad(gameObject);
            var settingsInstance = NewSettings.Instance;
        }
        public static void InitializeUnityExplorer()
        {
            if (AssemblyHelper.AreAllAssembliesLoaded() == true && NewSettings.Instance.AssemblyLoaded == false)
            {
                NewSettings.Instance.AssemblyLoaded = true;

#if RELEASE_UE || DEBUG
                UnityExplorer.ExplorerStandalone.CreateInstance();
#endif
            }

        }

        public static void Unload()
        {
            UnityEngine.Object.Destroy(gameObject.GetComponent<SceneDebugger>());
            UnityEngine.Object.Destroy(gameObject);
        }
    }
}
