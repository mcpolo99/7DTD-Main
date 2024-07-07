using InControl;
using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using UnityEngine;
using UnityEngine.UI;




namespace SevenDTDMono
{
    public class NewSettings: MonoBehaviour
    {
    
        private static NewSettings _instance;
        public static NewSettings Instance
        {
            get
            {
                if (_instance == null)
                {
                    //GameObject obj = GameObject.Find("7DTD----MENU");
                    //if (obj != null)
                    //{
                    //    Debug.LogWarning($"Object with name '{"7DTD----MENU"}' not found.");
                    //    obj.AddComponent<NewSettings>();
                    //}
                    //_instance = new GameObject("Settings").AddComponent<NewSettings>();
                    //_instance = new GameObject("Settings").AddComponent<NewSettings>();
                    _instance = GameObject.Find("7DTD----MENU").gameObject.GetComponent<NewSettings>();
                    DontDestroyOnLoad(_instance.gameObject);
                }
                return _instance;
            }
        }
        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
            }



            //SettingsDictionary["TestBool"] = false;
        }

        public void Start()
        {
          

        }



        public bool Speed { get;  set; }
        public bool CreativeMode { get; set; }
        public bool FovCircle { get; set; }
        public bool AssemblyLoaded { get; set; }
        public bool AssemblyPreLoaded { get; set; }
        public bool GameStarted { get; set; }
        public bool GameMainMenu { get; set; }
        #region FLoats Sliders
        public static float FloatBlockDamageMultiplier = 0.5f;//blockdamage
        public static float FloatKillDamageMultiplier = 0.5f;// EntityDamage
        public static float FloatJumpStrengthMultiplier = 0.5f;//JumpStrength
        public static float FloatRunSpeedMultiplier = 0.5f;//RunSpeed
        public static float FloatHarvestCountMultiplier = 0.5f;//Harvestcount
        public static float FloatAttacksPerMinuteMultiplier = 0.5f;//AttacksPerMinute



        #endregion







        //#region FLoats Sliders
        //public static float _FL_dmg = 0.5f;
        //public static float _FL_run = 0.5f; //RunSpeed
        //public static float _FL_jmp = 0.5f; //JumpStrength
        //public static float _FL_killdmg = 0.5f; // EntityDamage
        //public static float _FL_blokdmg = 0.5f; //blockdamage
        //public static float _FL_harvest = 2f;   //Harvestcount
        //public static float _FL_APM = 2f; //AttacksPerMinute
        //#endregion


        public static Dictionary<string, bool> BD = new Dictionary<string, bool>();
        //public static GUIStyle BgStyle, OnStyle, OffStyle, LabelStyle, BtnStyle, BtnStyle1, BtnStyle2, BtnStyle3, ToggStyle1, ToggStyle2;
        //public static Dictionary<string, bool> BoolDictionary = new Dictionary<string, bool>();  //Dictionary that contains bool
        //public static Dictionary<string, float> FloatDictionary = new Dictionary<string, float>(); //Dictionary that contains floats
        //public Dictionary<string, bool> CustomBools { get; private set; } = new Dictionary<string, bool>();
        public Dictionary<string, object> SettingsDictionary { get; private set; } = new Dictionary<string, object>();

        private void AddSetting<T>(string key, T value)
        {

            if (!Instance.SettingsDictionary.ContainsKey(key))
            {
                //Instance.SettingsDictionary[key] = value;
                SettingsDictionary.Add(key, value);

            }
            else
            {
                Debug.LogWarning($"Key '{key}' already exists in SettingsDictionary.");
            }
        }
        public bool GetBoolSetting(string key)
        {
            if (SettingsDictionary.TryGetValue(key, out object value) && value is bool boolValue)
            {
                return boolValue;
            }
            return false; // Return default value or handle as needed
        }



        
        public static void CheckDictionaryForKey<T>(string key, T defaultValue = default) // Check and initialize the dictionary key with a default value if it does not exist
        {
            // Check if the key exists in the dictionary
            if (!Instance.SettingsDictionary.ContainsKey(key))
            {
                // Add the key with a default value if it does not exist
                Instance.SettingsDictionary[key] = defaultValue;
            }

            // Ensure the value associated with the key is of type T
            if (!(Instance.SettingsDictionary[key] is T))
            {
                Debug.LogError($"Key '{key}' is not of type {typeof(T)}.");
            }
        }

        public static void GetKey(string key)
        {
            CheckDictionaryForKey(key, false);
        }

        public static bool GetBool(string key)
        {
            if (!Instance.SettingsDictionary.ContainsKey(key))
            {
                // Add the key with a default value if it does not exist
                Instance.SettingsDictionary[key] = false;
                return (bool)Instance.SettingsDictionary[key];
            }
            return (bool)Instance.SettingsDictionary[key];
        }

        public static T Get<T>(string key)// Get the value from the dictionary with the specified key
        {

            if (Instance.SettingsDictionary.ContainsKey(key) && Instance.SettingsDictionary[key] is T value)
            {
                return value;
            }
            Debug.LogError($"Key '{key}' is not of type {typeof(T)}.");
            return default;
        }





     





        //*******************************************************old

        #region BOOLS undefined
        // Add more settings/options to your cheat!



        public static bool aimbot;
        public static bool infiniteAmmo;
        public static bool noWeaponBob;
        public static bool magicBullet;

        //public static bool CmDm;
        public static bool cm;


        public static bool drpbp, onht;
        #endregion

        #region Bools defined

        //ESP
        public static bool PlayerBox = false;
        public static bool PlayerName = false;
        public static bool playerName = false;
        public static bool zombieName = false;
        public static bool playerBox = false;
        public static bool zombieBox = false;
        internal static bool crosshair = false;
        internal static bool playerCornerBox = false;
        public static bool zombieCornerBox = false;
        public static bool zombieHealth = false;
        public static bool playerHealth = false;
        public static bool chams = false;
        public static bool fovCircle = false;

        //MISC

        internal static bool drawDebug = false;
        internal static bool selfDestruct = false;
        public static bool IsGameStarted; //when we are loaded into the gameworld
        public static bool IsVarsLoaded; //buffs and stuff to be loaded and stuff
                                         //public static bool IsGameStartMenu; //


        public static bool ASMloaded = false; //assemblies loader preventing looploading
        public static bool ASMPreload = false; //assemblies loader preventing looploading



        #endregion
    }
}
