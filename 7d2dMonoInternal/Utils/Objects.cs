////using HarmonyLib;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Reflection;
//using UnityEngine;
//using SevenDTDMono;
//using System.Runtime.InteropServices;
//using System.Collections;
//using UniverseLib;
////using HarmonyLib;
//using S = SevenDTDMono.NewSettings;


////using HarmonyLib;

//namespace SevenDTDMono
//{

//    public class Objects : MonoBehaviour
//    {
//        private static Objects _instance;
//        public static Objects Instance
//        {
//            get
//            {
//                if (_instance == null)
//                {
//                    //GameObject obj = GameObject.Find("7DTD----MENU");
//                    //if (obj != null)
//                    //{
//                    //    Debug.LogWarning($"Object with name '{"7DTD----MENU"}' not found.");
//                    //    obj.AddComponent<NewSettings>();
//                    //}
//                    //_instance = new GameObject("Settings").AddComponent<NewSettings>();
//                    //_instance = new GameObject("Settings").AddComponent<NewSettings>();
//                    _instance = GameObject.Find("7DTD----MENU").gameObject.GetComponent<Objects>();
//                    DontDestroyOnLoad(_instance.gameObject);
//                }
//                return _instance;
//            }
//        }
//        public static Dictionary<string, object> Settings => NewSettings.Instance.SettingsDictionary;
//        private static GameManager GameManager => NewSettings.GameManager;
//        private static EntityPlayerLocal Player => NewSettings.EntityLocalPlayer;



//        public static EntityPlayerLocal EntityPlayerLocal => _entityPlayerLocal;



//        private static EntityPlayerLocal _entityPlayerLocal;

//        private static List<EntityZombie> _listZombies = new List<EntityZombie>();
//        public static List<EntityZombie> ListZombie => _listZombies;



//        public static List<EntityEnemy> _listEntityEnemy = new List<EntityEnemy>();
//        public static List<EntityItem> _listEntityItem = new List<EntityItem>();
//        public static List<BuffClass> _listBuffClass = new List<BuffClass>();
//        public static List<BuffClass> _listCbuffs = new List<BuffClass>();
//        public static List<EntityPlayer> _listEntityPlayer = new List<EntityPlayer>();
//        public static List<TileEntityLootContainer> TELC1 = new List<TileEntityLootContainer>();
        
//        public static List<ProgressionValue> _listProgressionValue = new List<ProgressionValue>();
//        public static List<ProgressionClass> _listProgressionClass = new List<ProgressionClass>();
//        public static MinEffectController _minEffectController = new MinEffectController();
//        public static MinEffectController _minEC = new MinEffectController();
//        public static BuffClass CheatBuff;
//        public static EntityPlayerLocal ELP;
//        public static EntityTrader Etrader;
        
//        public static XUiM_PlayerInventory _playerinv; //null ???
//        public static TileEntity tileEntity;
//        public static WorldBase _worldbase;


//        #region private Vars
//        private float lastCachePlayer;
//        private float lastCacheZombies;
//        private float lastCacheItems;
//        private float Cachestart;
//        private float updateCount = 0;
//        private float fixedUpdateCount = 0;
//        private float updateUpdateCountPerSecond;
//        private float updateFixedUpdateCountPerSecond;

//        #endregion


        

//        //EntityPlayerLocal.xmitInventory


//        //private void Init() 
//        //{
//        //    S.BD.Add("Cheatbuff", false);
//        //    S.BD.Add("BuffClasses", false);
//        //    S.BD.Add("Cbuff", false);
//        //    S.BD.Add("PGV", false);
//        //}
//        private void initCheatBuff() //default 366 buffs
//        {


//            CheatBuff.Name = "CheatBuff";
//            CheatBuff.DamageType = EnumDamageTypes.None; // Set the appropriate damage type if applicable
//            CheatBuff.Description = $"This is a {CheatBuff.Name}";
//            CheatBuff.DurationMax = 99999999f;
//            CheatBuff.Icon = "ui_game_symbol_agility";
//            CheatBuff.ShowOnHUD = true;
//            CheatBuff.Hidden = false;
//            CheatBuff.IconColor = new Color(0.22f, 0.4f, 1f, 100f);
//            CheatBuff.DisplayType = EnumEntityUINotificationDisplayMode.IconOnly;
//            CheatBuff.LocalizedName = "CheatBuff Delux";
//            CheatBuff.Description = "This is the buff that manages all modiefer values we add to the player,\n for every passive effect we adding we adding it to thsi Buffclass to avoid crashes and nullreferences \n " +
//                "Also to avoid not being able to edit future values easier. \n i have not yet figured out how i can make the slider modifers realtime modifers or how to avoid passiveffects stacking  ";
//            CheatBuff.Effects = _minEffectController;
//            BuffManager.Buffs.Add(CheatBuff.Name, CheatBuff);  // need to add to buffmanager before init Everything before adding to buffmanager is what will define the buff
//            _listBuffClass.Add(CheatBuff);// add the buffs to our own list,  Want to make a list with the descriptive name , buff
//            Debug.LogWarning($"Buff {CheatBuff.Name} has ben added to {_listBuffClass} ");
//            Log.Out($"{CheatBuff.Name} Has been initieted");


//            _minEffectController.EffectGroups = new List<MinEffectGroup>
//           {
//               new MinEffectGroup
//               {
//                   OwnerTiered = true,
//                   PassiveEffects = new List<PassiveEffect>
//                   {
//                         new PassiveEffect
//                            {
//                                MatchAnyTags = true,
//                                Type = PassiveEffects.None,
//                                Modifier = PassiveEffect.ValueModifierTypes.base_add,
//                                Values = new float[] { 20}
//                                //Set other properties of PassiveEffect if needed
//                            }
//                   },
//                   PassivesIndex = new List<PassiveEffects>
//                       {
//                            PassiveEffects.None,
//                       }
//               }

//           };
//            _minEffectController.PassivesIndex = new HashSet<PassiveEffects>
//           {
//                   PassiveEffects.None,
//           };
//            buffsDict = BuffManager.Buffs.Keys;
//        }
//        private void Start() //everything in here is things that need to declared at once on startup and not again. IF injecting ingame more ingame dependet vars can be here
//        {

          

//            lastCachePlayer = Time.time + 5f;
//            lastCacheZombies = Time.time + 3f;
//            lastCacheItems = Time.time + 4f;
//            lastCacheItems = Time.time + 1000f;
//            Cachestart = Time.time + 10f; //time now + 10 sec

//            //_minEC.EffectGroups = _minEffectController.EffectGroups;
//            //_minEC.PassivesIndex = _minEffectController.PassivesIndex;
//            //Init();

//            //Settings.Add(nameof(GameStateManager.bGameStarted), false);

//            Debug.LogWarning($"Start: {nameof(Objects)}");
//        }



//        void Update()
//        {
//            updateCount += 1;

//           /*
//            if (SB.Count <= 0 || RB.Count <= 0 || SB1.Count <= 0)
//            {
//                //initBools();
//            }
//            else
//            {

//                SB["IsVarsLoaded"] = VSB.Values.All(b => b);
//                if (SB["IsGameStarted"] == true && SB["IsVarsLoaded"] != true)
//                {
//                    try
//                    {
//                        if (VSB["Cheatbuff"] != true)
//                        {
//                            Debug.LogWarning($"amount of buffs before load {BuffManager.Buffs.Count}");
//                            if (CheatBuff == null)
//                            {
//                                CheatBuff = new BuffClass();
//                                Debug.Log($"{CheatBuff} as been init as a BuffClass() ");
//                            }
//                            else if (CheatBuff != null)
//                            {
//                                Debug.LogWarning($"{CheatBuff.Name} begin init");
//                                initCheatBuff();
//                                Debug.LogWarning($"{CheatBuff.Name} finished init");
//                                int count2 = BuffManager.Buffs.Count;
//                                Debug.LogWarning($"amount of buffs after init {count2}");
//                                Debug.LogWarning($"amount of buffs after init {count2}");
//                                VSB["Cheatbuff"] = true;
//                                Debug.LogWarning($"1 = {VSB["Cheatbuff"]}");
//                            }
//                            else
//                            {
//                                Log.Out($"{CheatBuff.Name} Has Not been init");
//                            }
//                        }
//                        else if (VSB["BuffClasses"] != true)
//                        {
//                            Log.Out("Reloading buffs");
//                            _listBuffClass.Clear();
//                            foreach (var buffClass in BuffManager.Buffs)
//                            {
//                                _listBuffClass.Add(buffClass.Value);
//                            }
//                            _listBuffClass.Sort((buff1, buff2) => string.Compare(buff1.Name, buff2.Name));
//                            LogListBuffClass(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "load", "_listBuffClass.txt"));
//                            VSB["BuffClasses"] = true;
//                            Debug.LogWarning($"2 = {VSB["BuffClasses"]}");

//                        }
//                        else if (VSB["Cbuff"] != true)
//                        {
//                            string CbuffRssName = "SevenDTDMono.Features.Buffs.Cbuffs.XML";
//                            Log.Out($"{CbuffRssName} begin Load....");
//                            _listCbuffs.Clear();
//                            //CBuffs.LoadCustomXml(CbuffRssName);
//                            _listCbuffs.Sort((a, b) => string.Compare(a.Name, b.Name));
//                            Debug.LogWarning($"{CbuffRssName} finished load!");
//                            VSB["Cbuff"] = true;
//                            Debug.LogWarning($"3 = {VSB["Cbuff"]}");
//                        }
//                        else if (VSB["PGV"] != true && ELP.Progression != null)
//                        {
//                            if (_listProgressionValue.Count <= 0)
//                            {
//                                Debug.LogWarning("List empty");

//                            }
//                            _listProgressionValue.Clear();
//                            foreach (KeyValuePair<int, ProgressionValue> keyValuePair in ELP.Progression.GetDict())
//                            {
//                                ProgressionValue value = keyValuePair.Value;
//                                bool flag;
//                                if (value == null)
//                                {
//                                    flag = null != null;
//                                }
//                                else
//                                {
//                                    ProgressionClass progressionClass = value.ProgressionClass;
//                                    flag = ((progressionClass != null) ? progressionClass.Name : null) != null;
//                                }
//                                if (flag)
//                                {

//                                    _listProgressionValue.Add(keyValuePair.Value);
//                                }

//                            }
//                            _listProgressionValue.Sort((a, b) => string.Compare(a.Name, b.Name));
//                            VSB["PGV"] = true;
//                            Debug.LogWarning($"4 = {VSB["PGV"]}");
//                        }
//                        SB["IsVarsLoaded"] = VSB.Values.All(b => b);
//                        Debug.LogWarning($"AllLoaded =  {SB["IsVarsLoaded"]}");
//                    }
//                    catch
//                    {
//                    }
//                }

//                if (ELP != null && RB["reloadBuffs"] == true && _listBuffClass.Count <= 1)
//                {

//                    Log.Out("Reloading buffs");
//                    foreach (var buffClass in BuffManager.Buffs)
//                    {
//                        _listBuffClass.Add(buffClass.Value);
//                    }
//                    //_listBuffClass = GetAvailableBuffClasses();
//                    RB["reloadBuffs"] = false;
//                }

//            }

//            */



//            if (Time.time >= lastCachePlayer) // i dont remember why we need to use a loop to chach the player but it was from argons source code!
//            {
//                //ELP = FindObjectOfType<EntityPlayerLocal>(); // FindObjectOfType is a unity function to find the gameobject of desired Class so we assing our variable ELP(of class EntityPlayerLocal) to the games EntityPlayerLocal. Now we can use ELP as EntityPlayerLocal to whatever we want
//                //Etrader = FindObjectOfType<EntityTrader>();
//                lastCachePlayer = Time.time + 5f; // cahing player each timn now + 5 sec. so 5 secound ahead
//            }
//            else if (Time.time >= lastCacheZombies)
//            {
//                //_listZombies = FindObjectsOfType<EntityZombie>().ToList();
//                //_listEntityEnemy = FindObjectsOfType<EntityEnemy>().ToList();

//                lastCacheZombies = Time.time + 3f;
//            }
//            else if (Time.time >= lastCacheItems)
//            {
//                //_listEntityItem = FindObjectsOfType<EntityItem>().ToList();
//                lastCacheItems = Time.time + 4f;
//            }
//        }
//        // Increase the number of calls to FixedUpdate.
//        void FixedUpdate()
//        {
//            fixedUpdateCount += 1;
//            if (Settings.Count > 1)
//            {
//                if (GameManager.Instance.World != null)
//                {
//                    Settings["hasStartedOnce"] = true;
//                    Settings[nameof(GameStateManager.bGameStarted)] = true;
//                    Settings["IsGameMainMenu"] = false;
//                }
//                else if (GameManager.Instance.World == null)
//                {
//                    NewSettings.Instance.GameStarted = true;
//                    Settings["IsGameMainMenu"] = true;
//                    Settings[nameof(GameStateManager.bGameStarted)] = false;



//                    //foreach (string key in S.BD.Keys.ToList()) // ToList creates a copy of the keys so that you can modify the dictionary while iterating.
//                    //{
//                    //    S.BD[key] = false;
//                    //}

//                    //if ((bool)Settings["hasStartedOnce"]  && (bool)Settings["IsGameMainMenu"])
//                    //{
//                    //    Settings["BoolReset"] = true;
//                    //    initReset();
//                    //    Settings["hasStartedOnce"] = false;
//                    //}


//                }

//                if (GameManager.Instance.IsQuitting)
//                {

//                }


//            }
//        }


//        IEnumerator Loop()
//        {
//            while (true)
//            {
//                yield return new WaitForSeconds(1);
//                updateUpdateCountPerSecond = updateCount;
//                updateFixedUpdateCountPerSecond = fixedUpdateCount;

//                updateCount = 0;
//                fixedUpdateCount = 0;
//            }
//        }

//        public static List<EntityPlayer> PlayerList
//        {
//            get
//            {
//                if (GameManager.Instance != null)
//                    if (GameManager.Instance.World != null)
//                        return GameManager.Instance.World.GetPlayers();
//                return new List<EntityPlayer>();
//            }
//        }


     

       
//        void OnGUI()
//        {
//            //display update times
//            //GUIStyle fontSize = new GUIStyle(GUI.skin.GetStyle("label"));
//            //fontSize.fontSize = 15;
//            //fontSize.normal.textColor = Color.green;
//            //GUI.Label(new Rect(10, 50, 200, 50), "Counter: " + NewSettings.GameManager.fps.Counter, fontSize);
//            //GUI.Label(new Rect(10, 70, 200, 50), "Frames: " + NewSettings.GameManager.fps.frames, fontSize);
//        }
//        void Awake()
//        {

//            // Uncommenting this will cause framerate to drop to 10 frames per second.
//            // This will mean that FixedUpdate is called more often than Update.
//            //Application.targetFrameRate = 10;

//            //init our updateloop
//            //StartCoroutine(Loop());

//            Debug.LogWarning($"Awake: {nameof(Objects)}");
//        }
//        void OnDisable()
//        {

//            Debug.LogWarning($"OnDisable: {nameof(Objects)}");
//        }
//        void OnEnable()
//        {

//            Debug.LogWarning($"OnEnable: {nameof(Objects)}");
//        }
//        void OnDestroy()
//        {

//            Debug.LogWarning($"OnDestroy: {nameof(Objects)}");
//        }
//        void OnDisconnectedFromServer()
//        {

//            Debug.LogWarning($"OnDisconnectedFromServer: {nameof(Objects)}");
           
//        }

//        //*************************Log/dump Related 

//        private static string EscapeForCsv(string value)
//        {
//            if (string.IsNullOrEmpty(value))
//            {
//                return string.Empty;
//            }

//            // If the value contains a comma or a double quote, enclose it in double quotes and escape any existing double quotes
//            if (value.Contains(',') || value.Contains('"'))
//            {
//                return $"\"{value.Replace("\"", "\"\"")}\"";
//            }

//            return value;
//        }
//        public static void LogProgressionClassToFile(string filePath)
//        {
//            if (_listProgressionValue != null && _listProgressionValue.Count > 0)
//            {
//                try
//                {
//                    using (StreamWriter writer = new StreamWriter(filePath))
//                    {
//                        writer.WriteLine("name,type,sortorder");

//                        foreach (var prog in _listProgressionValue)
//                        {

//                            string str1 = prog.Name;
//                            string str2 = prog.ProgressionClass.Type.ToString();
//                            string str3 = prog.ProgressionClass.ListSortOrder.ToString();


//                            //writer.WriteLine($"{EscapeForCsv(buffClass.Name)},{buffClass.DamageType},{EscapeForCsv(buffClass.NameTag.ToString())}");
//                            writer.WriteLine($"{EscapeForCsv(str1)},{EscapeForCsv(str2)},{EscapeForCsv(str3)}");
//                        }
//                    }

//                    Console.WriteLine("progressionclasses as been logged");
//                }
//                catch (Exception ex)
//                {
//                    Console.WriteLine($"Error occurred while logging buff classes: {ex.Message}");
//                }
//            }

//        }
//        private static void LogListBuffClass(string filePath)
//        {
//            if (_listBuffClass != null && _listBuffClass.Count > 0)
//            {
//                try
//                {
//                    using (StreamWriter writer = new StreamWriter(filePath))
//                    {
//                        writer.WriteLine("Buff Name,Damage Type,Description");

//                        foreach (var buffClass in _listBuffClass)
//                        {

//                            string str1 = buffClass.DamageSource.ToString();
//                            string str2 = buffClass.Effects.EffectGroups[0].ToString();


//                            //writer.WriteLine($"{EscapeForCsv(buffClass.Name)},{buffClass.DamageType},{EscapeForCsv(buffClass.NameTag.ToString())}");
//                            writer.WriteLine($"{EscapeForCsv(buffClass.Name)},,");
//                        }
//                    }

//                    Console.WriteLine("Buff classes have been logged to the file.");
//                }
//                catch (Exception ex)
//                {
//                    Console.WriteLine($"Error occurred while logging buff classes: {ex.Message}");
//                }
//            }
//        }


//    }

//}