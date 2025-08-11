using SevenDTDMono.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;
#if RELEASE_UE || DEBUG
using UnityExplorer;

#endif

namespace SevenDTDMono
{
    public class Loader
    {
        //internal static UnityEngine.GameObject gameObject;
        public static UnityEngine.GameObject GameObject { get; protected set; }
        public static string ObjectName = "7DTD----MENU";


       
        public static SevenDTDMono.Utils.AssemblyHelper AssemblyHelper; // Add a member variable

        public static void Load()
        {
            GameObject = new UnityEngine.GameObject();


#if RELEASE_UE || DEBUG
            AssemblyHelper = new SevenDTDMono.Utils.AssemblyHelper();
            AssemblyHelper.TryLoad();
#endif


            GameObject.name = ObjectName;
            //gameObject.AddComponent<Objects>();
            GameObject.AddComponent<NewSettings>();
            GameObject.AddComponent<NewMenu>();
            GameObject.AddComponent<Features.Cheat>();
            GameObject.AddComponent<Features.Render.ESP>();
            GameObject.AddComponent<Features.Render.Render>();
            GameObject.AddComponent<Features.Render.Visuals>();
            //gameObject.AddComponent<Aimbot>();

            //gameObject.AddComponent<SceneDebugger>();
            //gameObject.AddComponent<CBuffs>();
            //gameObject.AddComponent<EasterEggManager>();
            //


#if RELEASE_UE || DEBUG
            InitializeUnityExplorer();
#endif

            UnityEngine.Object.DontDestroyOnLoad(GameObject);
            var settingsInstance = NewSettings.Instance;
        }

        /// <summary>
        /// Init the unity explorer mod
        /// </summary>
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
            UnityEngine.Object.Destroy(GameObject);
        }
    }
}
