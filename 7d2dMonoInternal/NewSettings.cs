
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using UnityEngine;
using UnityEngine.UI;
using Logger = UnityEngine.Logger;


namespace SevenDTDMono
{

    public class NewSettings: MonoBehaviour
    {
        #region define
        private static readonly GameManager _gameManager = FindObjectOfType<GameManager>(); //get the game manager
        public static GameManager GameManager => _gameManager; //make public property GameManager (i think this should make like one base reference to game manager)
        public static GameManager GameManagerInstance => GameManager.Instance; //Better way then using FindObjectOfType ??? need more check!
        public static EntityPlayerLocal EntityLocalPlayer => GameManager.myEntityPlayerLocal; //make public property "EntityLocalPlayer" for accessing the player around our code.
        public static EntityTrader EntityTrader => FindObjectOfType<EntityTrader>();//make public property EntityTrader
        public static Dictionary<string, BuffClass>.KeyCollection DictionaryBuffClassKeyCollection; //do not remember why this one rn
        public static Dictionary<string, BuffClass> DictionaryBuffClassCollection; //do not remember why this one rn

        public static List<BuffClass> ListBuffClasses = new List<BuffClass>();
        public static List<BuffClass> ListCheatBuffs = new List<BuffClass>();
        public static List<ProgressionValue> ListProgressionValues = new List<ProgressionValue>();

        /// <summary>
        /// some bool that should not be in dictionary
        /// </summary>
        public bool AssemblyLoaded { get; set; }
        public bool AssemblyPreLoaded { get; set; }
        public bool GameStarted { get; set; }




        /// <summary>
        /// Diffrent floats that we want to keep track of,
        /// </summary>
        public static float FloatBlockDamageMultiplier = 0.5f; //block damage
        public static float FloatKillDamageMultiplier = 0.5f; // Entity Damage
        public static float FloatJumpStrengthMultiplier = 0.5f; //Jump Strength
        public static float FloatRunSpeedMultiplier = 0.5f;//Run Speed
        public static float FloatHarvestCountMultiplier = 0.5f;//Harvest count
        public static float FloatAttacksPerMinuteMultiplier = 0.5f;//Attacks Per Minute


        #endregion //define

        #region Settings dictionary

        public Dictionary<string, object> SettingsDictionary { get; private set; } = new Dictionary<string, object>(); //Our main Dictionary containing all settings for the cheat? Primarly for dynamic loading of setting values instead of defining many static ones.
        public Dictionary<string, object> TempDictionary { get; private set; } = new Dictionary<string, object>();
        private Dictionary<string, bool> BoolDictionary { get; set; } = new Dictionary<string, bool>();
        private Dictionary<string, Vector2> Vector2Dictionary { get; set; } = new Dictionary<string, Vector2>();



        // Generic method to get a child dictionary from main dictionary
        public Dictionary<string, T> GetChildDictionary<T>(string key)
        {
            if (SettingsDictionary.TryGetValue(key, out object dictObj) && dictObj is Dictionary<string, T> dict)
            {
                return dict;
            }
            else
            {
                Debug.LogError($"Dictionary for key '{key}' not found or type mismatch.");
                return null;
            }
        }

        internal bool CheckBoolKeyExist(string boolKey)
        {
            try
            {
                if (!BoolDictionary.ContainsKey(boolKey))
                {
                    // Add the key with a default value of false if it does not exist
                    BoolDictionary[boolKey] = false;
                    return true;
                }
                return true;
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
            return false;
        }
        internal bool GetBoolValue(string boolKey)
        {
            return (bool)BoolDictionary[boolKey];
        }

        internal bool CheckVector2ZeroExist(string vector2Key)
        {
            try
            {
                if (!Vector2Dictionary.ContainsKey(vector2Key))
                {
                    // Add the key with a default value of false if it does not exist
                    Vector2Dictionary[vector2Key] = Vector2.zero;
                    return true;
                }
                return true;
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
            return false;
        }
        internal Vector2 GetVector2(string vector2Key)
        {
            return Vector2Dictionary[vector2Key];
        }
        internal void SetVector2(string vector2Key,Vector2 vector2)
        {
            Vector2Dictionary[vector2Key]=vector2;
        }

        public bool CheckVector2NewExist(string vector2Key)
        {
            try
            {
                if (!Vector2Dictionary.ContainsKey(vector2Key))
                {
                    // Add the key with a default value of false if it does not exist
                    Vector2Dictionary[vector2Key] = new Vector2();
                    return true;
                }
                return true;
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
            return false;
        }

        #region ResetList
        /// <summary>
        /// need to improve this to have like a variable for each object that need resetting to be init already in the making of settingsDictionary
        /// this should be used to reset all toggled values to default values. Maybe it is possible to make a copy of main dictionary when loading the game?
        /// </summary>
        public static List<string> ResetVariableList = new List<string>();
        public static void AddReset(string variable)
        {
            if (!ResetVariableList.Contains(variable))
            {
                ResetVariableList.Add(variable);
            }
        }

        #endregion //ResetList


        #endregion //Settings dictionary

        #region gamelists
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
        #endregion

        private static NewSettings _instance;
        public static NewSettings Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = GameObject.Find("7DTD----MENU").gameObject.GetComponent<NewSettings>();
                    DontDestroyOnLoad(_instance.gameObject);
                }
                return _instance;
            }
        }


        private void Awake() //called when being loaded!
        {
            //first and foremost init all the dictionaries!
            
            var floatDictionary = new Dictionary<string, float>();
            var vector2Dictionary = new Dictionary<string, Vector2>();


            SettingsDictionary["NewSettingsAwake"] = true;
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
            // Initialize inner dictionaries


            BoolDictionary["bool1"] = true;
            floatDictionary["float1"] = 2f;
            vector2Dictionary["vector1"] = new Vector2();

            //Make sure that all Booleans are loaded into the dictionary so we do not get reference error
            foreach (SettingsBools setting in Enum.GetValues(typeof(SettingsBools)))
            {
                Debug.LogFormat("Setting {0} is being processed.", setting);
                

                if (!BoolDictionary.ContainsKey(nameof(setting)))
                {
                    // Add the key with a default value of false if it does not exist
                    BoolDictionary[setting.ToString()] = false;
                    //SettingsDictionary[setting.ToString()] = false;
                    //Debug.Log($" Added {setting} to Dictionary");
                    //NewSettings.AddSetting(boolKey, false);
                }
            }
            //After all the neccessary setting loading add all child Dictionaries to main Dictionary
            SettingsDictionary[nameof(Dictionaries.BOOL_DICTIONARY)] = BoolDictionary;
            SettingsDictionary[nameof(Dictionaries.FLOAT_DICTIONARY)] = floatDictionary;
            SettingsDictionary[nameof(Dictionaries.VECTOR2_DICTIONARY)] = vector2Dictionary;

        }

        public void Start()
        {
            Debug.LogWarning($"Start: {nameof(NewSettings)}");

        }




     

    }
}


/*
 
    //************************************ STUFF THATS NOT ÙSED!!


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



        //public static Dictionary<string, bool> BD = new Dictionary<string, bool>();
        //public static GUIStyle BgStyle, OnStyle, OffStyle, LabelStyle, BtnStyle, BtnStyle1, BtnStyle2, BtnStyle3, ToggStyle1, ToggStyle2;
        //public static Dictionary<string, bool> BoolDictionary = new Dictionary<string, bool>();  //Dictionary that contains bool
        //public static Dictionary<string, float> FloatDictionary = new Dictionary<string, float>(); //Dictionary that contains floats
        //public Dictionary<string, bool> CustomBools { get; private set; } = new Dictionary<string, bool>();

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

 */