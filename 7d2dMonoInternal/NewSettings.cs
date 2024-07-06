using System.Collections.Generic;
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
                    _instance = new GameObject("Settings").AddComponent<NewSettings>();
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



        public bool Speed { get;  set; }
        public bool CreativeMode { get; set; }
        public bool FovCircle { get; set; }

        public bool AssemblyLoaded { get; set; }
        public bool AssemblyPreLoaded { get; set; }
        public bool GameStarted { get; set; }
        public bool GameMainMenu { get; set; }






        #region FLoats Sliders
        public static float _FL_dmg = 0.5f;
        public static float _FL_run = 0.5f; //RunSpeed
        public static float _FL_jmp = 0.5f; //JumpStrength
        public static float _FL_killdmg = 0.5f; // EntityDamage
        public static float _FL_blokdmg = 0.5f; //blockdamage
        public static float _FL_harvest = 2f;   //Harvestcount
        public static float _FL_APM = 2f; //AttacksPerMinute
        #endregion


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



        void LoadSettings()
        {
            if (Instance != null)
            {
                foreach (string key in _listOfBool)
                {
                    AddSetting(key, false);
                }

                foreach (string key in _listOfNewVector)
                {
                    AddSetting(key, new Vector2());
                }

                foreach (string key in _listOfVectorZero)
                {
                    AddSetting(key, Vector2.zero);
                }
                //foreach (string key in VarStringBools)
                //{
                //    if (!VSB.ContainsKey(key)) { VSB.Add(key, false); }

                //}
                //foreach (string key in ResetBools)
                //{
                //    if (!RB.ContainsKey(key)) { RB.Add(key, false); }

                //}

            }
        }
        //public static void ResetSettings()
        //{
        //    if ((bool)Instance.SettingsDictionary["BoolReset"] == true)
        //    {
        //        foreach (string key in _ListForReset)
        //        {
        //            if (RB.ContainsKey(key) == true)
        //            {
        //                RB[key] = false;
        //            }

        //        }
        //        SB["BoolReset"] = false;
        //    }
        //}






        private readonly List<string> _listOfBool = new List<string>
        {
            "NSCAMBLE",
            "NSCAMBLE1",
            "_nameScramble",
            "IsGameMainMenu",
            "IsGameStarted",
            "IsVarsLoaded",
            "hasStartedOnce",
            "BoolReset",
            "FoldBuff",
            "FoldPassive",
            "FoldPGV",
            "FoldPlayer",
            "FoldZombie",
            "foldout1",
            "foldout2",
            "foldout3",
            "foldout4",
            "foldout5",
        };

        private readonly List<string> _listOfNewVector = new List<string>
        {
            "scrollPlayer",
            "scrollBuff",
            "scrollCBuff",
            "scrollZombie",
            "scrollPassive",
            "scrollPGV",
            "scrollKill",
        };

        private readonly List<string> _listOfVectorZero = new List<string>
        {
            "ScrollMenu0",
            "ScrollMenu1",
            "ScrollMenu2",
            "ScrollMenu3",
            "ScrollMenu4",
            "ScrollMenu5",
            "scrollBuffBTN",
        };



        public List<string> ListForReset => _listForReset; 
      
        private List<string> _listForReset = new List<string>
        {
            "first",
            "second",
            "third",
            "NSCRAM1",
            "NSCRAM2",
            "_trystackitems",
            "_InstantLoot" ,
            "_instantScrap",
            "_instantCraft" ,
            "_instantSmelt",
            "_infDurability",
            "fovCircle",
            "_isEditmode",
            "_QuestComplete",
            "_EtraderOpen",
            "_LOQuestRewards",
            "_healthNstamina",
            "_foodNwater",
            "_ignoreByAI",
            "_NoBadBuff",
            "_BL_Harvest",
            "_BL_Blockdmg",
            "_BL_Kill",
            "_BL_Run",
            "_BL_Jmp",
            "_BL_APM",
            "reloadBuffs",
            "CmDm",
            "drawDebug"
        };







        //*******************************************************old

        #region BOOLS undefined
        // Add more settings/options to your cheat!


        public static bool speed;
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


        ////not sure
        //public static bool _trystackitems = false;
        //public static bool _nameScramble = false;
        //public static bool _InstantLoot = false;
        //public static bool _instantScrap = false;
        //public static bool _instantCraft = false;
        //public static bool _instantSmelt = false;
        //public static bool _infDurability = false;
        ////public static bool _InstantLoot = false;


        //MISC

        internal static bool drawDebug = false;
        internal static bool selfDestruct = false;
        public static bool IsGameStarted; //when we are loaded into the gameworld
        public static bool IsVarsLoaded; //buffs and stuff to be loaded and stuff
                                         //public static bool IsGameStartMenu; //


        public static bool ASMloaded = false; //assemblies loader preventing looploading
        public static bool ASMPreload = false; //assemblies loader preventing looploading




        //public static bool reloadBuffs = false;



        ////cheats toggle

        //public static bool _isEditmode = false;
        //public static bool _QuestComplete = false;
        //public static bool _EtraderOpen = false;
        //public static bool _LOQuestRewards = false;
        //public static bool _healthNstamina = false;
        //public static bool _foodNwater = false;
        //public static bool _ignoreByAI = false;
        //public static bool _NoBadBuff = false;
        //public static bool _BL_Harvest = false;
        //public static bool _BL_Blockdmg = false;
        //public static bool _BL_Kill = false;
        //public static bool _BL_Run = false;
        //public static bool _BL_Jmp = false;
        //public static bool _BL_APM = false;


        #endregion






        //**********************************************************

















        //#region BOOLS undefined
        //// Add more settings/options to your cheat!

        //public static bool speed;
        //public static bool aimbot;
        //public static bool infiniteAmmo;
        //public static bool noWeaponBob;
        //public static bool magicBullet;

        //public static bool CmDm;
        //public static bool cm;


        //public static bool drpbp, onht;
        //#endregion

        //#region Bools defined

        ////ESP
        //public static bool PlayerBox = false;
        //public static bool PlayerName = false;
        //public static bool playerName = false;
        //public static bool zombieName = false;
        //public static bool playerBox = false;
        //public static bool zombieBox = false;
        //internal static bool crosshair = false;
        //internal static bool playerCornerBox = false;
        //public static bool zombieCornerBox = false;
        //public static bool zombieHealth = false;
        //public static bool playerHealth = false;
        //public static bool chams = false;
        //public static bool fovCircle = false;



        ////not sure
        //public static bool _trystackitems = false;
        //public static bool _nameScramble = false;
        //public static bool _InstantLoot = false;
        //public static bool _instantScrap = false;
        //public static bool _instantCraft = false;
        //public static bool _instantSmelt = false;
        //public static bool _infDurability = false;
        ////public static bool _InstantLoot = false;


        ////MISC

        //internal static bool drawDebug = false;
        //internal static bool selfDestruct = false;
        //public static bool IsGameStarted; //when we are loaded into the gameworld
        //public static bool IsVarsLoaded; //buffs and stuff to be loaded and stuff
        //public static bool IsGameStartMenu; //


        //public static bool ASMloaded=false; //assemblies loader preventing looploading
        //public static bool ASMPreload=false; //assemblies loader preventing looploading

        //public static bool StartMenuStarted = false;


        //public static bool reloadBuffs = false;


        //#endregion






    }
}
