using InControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using UnityEngine;
using UnityEngine.UI;




namespace SevenDTDMono
{
    public class NewSettings: MonoBehaviour
    {
        //*******************************************************old

        #region BOOLS undefined
        // Add more settings/options to your cheat!



        public static bool aimbot;
        public static bool infiniteAmmo;
        public static bool noWeaponBob;
        public static bool magicBullet;

        //public static bool CmDm;



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
        public static bool IsVarsLoaded; //buffs and stuff to be loaded and stuff
        //public static bool IsGameStartMenu; //


        public static bool ASMloaded = false; //assemblies loader preventing looploading
        public static bool ASMPreload = false; //assemblies loader preventing looploading



        #endregion



        private static readonly GameManager _gameManager = FindObjectOfType<GameManager>(); //get the game manager
        public static GameManager GameManager => _gameManager; //make property 
        public static GameManager GameManagerInstance => GameManager.Instance; //Better way then using FindObjectOfType ???
        public static EntityPlayerLocal EntityLocalPlayer => GameManager.myEntityPlayerLocal; //make EntityLocalPlayer

        public static EntityTrader EntityTrader => FindObjectOfType<EntityTrader>();
        public static Dictionary<string, BuffClass>.KeyCollection DictionaryBuffClassKeyCollection;



        public static List<string> ResetVariableList = new List<string>();
        public static void AddReset(string variable)
        {
            if (!ResetVariableList.Contains(variable))
            {
                ResetVariableList.Add(variable);
            }
        }



        /// <summary>
        /// This includes all entities that are Players! 
        /// </summary>
        public static List<EntityPlayer> EntityPlayers
        {
            get
            {
                if (_gameManager != null)
                    if (_gameManager.World != null)
                        return _gameManager.World.GetPlayers();
                return new List<EntityPlayer>();
            }
        }

        /// <summary>
        /// EntityAlive seems to be only Entities that are alive and not EntityPlayerLocal
        /// </summary>
        public static List<EntityAlive> EntityAlive
        {
            get
            {
                if (_gameManager != null)
                    if (_gameManager.World != null)
                        return _gameManager.World.EntityAlives;
                return new List<EntityAlive>();
            }
        }

        /// <summary>
        /// we need nothing else to update the Entity list then this. This is all entities as well as "dead" entities such as backpacks(EntityBackPack),dropped items (EntityItem) etc
        /// But also includes EntityPlayerLocal.
        /// To filter out Entities form list Entity we can make use of a for all loop and use EntityFlags.Zombie or EntityFlags.Animal etc. This dies not work for dropped items. (entityFlags=null)
        /// But instead we can use EntityClass.classname is of type EntityItem or EntityBackpack
        /// 
        /// </summary>
        public static List<Entity> Entities
        {
            get
            {
                if (GameManager.Instance != null)
                    if (GameManager.Instance.World != null) 
                        return GameManager.Instance.World.Entities.list; //this works as as well doing : _gameManager.World.Entities.list. We can probably save some memory by doing it this way??

                return new List<Entity>();
            }
        }


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



        public bool AssemblyLoaded { get; set; }
        public bool AssemblyPreLoaded { get; set; }
        public bool GameStarted { get; set; }


        #region FLoats Sliders
        public static float FloatBlockDamageMultiplier = 0.5f;//blockdamage
        public static float FloatKillDamageMultiplier = 0.5f;// EntityDamage
        public static float FloatJumpStrengthMultiplier = 0.5f;//JumpStrength
        public static float FloatRunSpeedMultiplier = 0.5f;//RunSpeed
        public static float FloatHarvestCountMultiplier = 0.5f;//Harvestcount
        public static float FloatAttacksPerMinuteMultiplier = 0.5f;//AttacksPerMinute



        #endregion



        //public static Dictionary<string, bool> BD = new Dictionary<string, bool>();
        //public static GUIStyle BgStyle, OnStyle, OffStyle, LabelStyle, BtnStyle, BtnStyle1, BtnStyle2, BtnStyle3, ToggStyle1, ToggStyle2;
        //public static Dictionary<string, bool> BoolDictionary = new Dictionary<string, bool>();  //Dictionary that contains bool
        //public static Dictionary<string, float> FloatDictionary = new Dictionary<string, float>(); //Dictionary that contains floats
        //public Dictionary<string, bool> CustomBools { get; private set; } = new Dictionary<string, bool>();
        public Dictionary<string, object> SettingsDictionary { get; private set; } = new Dictionary<string, object>();
        public Dictionary<string, object> TempDictionary { get; private set; } = new Dictionary<string, object>();



        







        private void Awake()
        {
            Debug.LogWarning($"Awake: {nameof(NewSettings)}");
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
            Debug.LogWarning($"Start: {nameof(NewSettings)}");


        }















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




        public static T Get<T>(string key)// Get the value from the dictionary with the specified key
        {

            if (Instance.SettingsDictionary.ContainsKey(key) && Instance.SettingsDictionary[key] is T value)
            {
                return value;
            }
            Debug.LogError($"Key '{key}' is not of type {typeof(T)}.");
            return default;
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
            //CheckDictionaryForKey(key, false);
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


    }
}
