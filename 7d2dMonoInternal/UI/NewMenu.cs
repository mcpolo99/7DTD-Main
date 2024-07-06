
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using O = SevenDTDMono.Objects;
using NS = SevenDTDMono.NewSettings;
using SETT = SevenDTDMono.NewSettings;
using static Setting;
using SevenDTDMono.Utils;
using SevenDTDMono.GuiLayoutExtended;
using UnityEngine.UIElements;
using DynamicMusic;
using UniLinq;
using System.Linq;
//using UnityExplorer;
using InControl;
using System.CodeDom;
using SevenDTDMono;
using TMPro;
//using UniverseLib;
using static PassiveEffect;
using System.Reflection;
using System.Xml.Linq;
using System.Collections.Concurrent;
using static UnityEngine.GridBrushBase;
using UnityExplorer;

namespace SevenDTDMono
{
    public class NewMenu : MonoBehaviour
    {

        public static Dictionary<string,object> Settings => NewSettings.Instance.SettingsDictionary;
        private static bool GetBool(string key)
        {
            if (Settings.TryGetValue(key, out object value) && value is bool boolValue)
            {
                return boolValue;
            }
            return false; // Return default value or handle as needed
        }

        private static Vector2 GetZeroVector2(string key)
        {
            if (Settings.TryGetValue(key, out object value) && value is Vector2 vector2)
            {
                return vector2;
            }
            return Vector2.zero;
        }


        #region Private bools

        private bool drawMenu = true;


        private bool FoldPlayer = false;
        private bool FoldZombie = false;
        private bool FoldBuff = false;
        private bool FoldCBuff = false;
        private bool FoldPassive = false;
        private bool FoldPGV = false;

        private bool lastBuffAdded = false;

        private bool lastzombieadded = false;
        private string inputFloat = "2";
        private string inputPassive = string.Empty;
        private string inputPassiveEffects = string.Empty;
        private string inputValuModType = string.Empty;
        private int currentIndex = 0;
        private int ValueModifierTypesIndex = 0;

        #endregion


        #region render/GUI stuff
        //private bool drawMenu = true;
        public int _group = 0;
        string[] CheatsString = { "Player", "Toggles and Modifiers", "Buffs and Stuff", "Some crap" };
        private int windowID;
        private Rect windowRect;
        private Vector2 scrollPlayer;
        private Vector2 scrollKill;
        private Vector2 scrollBuff;
        private Vector2 scrollCBuff;
        private Vector2 scrollZombie;
        private Vector2 scrollPassive;
        private Vector2 scrollPGV;

        private Vector2 ScrollMenu3 = Vector2.zero;
  //      private Vector2 scrollBuffBTN = Vector2.zero;

//        private Vector2 ScrollMenu2 = Vector2.zero;
        //private Vector2 ScrollMenu1 = Vector2.zero;
        //private Vector2 ScrollMenu0 = Vector2.zero;
        private Vector2 test;

        //public ToggleColors toggleColors;
        private GUIStyle customBoxStyleGreen;
        private GUIStyle defBoxStyle;
        GUIStyle centeredLabelStyle;
        #endregion



        #region Random
        private float floatValue = 0.0f;
        public float counter;
        public string Text;
        public Text counterText;
        private string buffSearch = "";
        private string PGVSearch = "";
        private string passiveSearch = "";
        private static string _itemcount = "2000";
        #endregion


        //public Dictionary<string, bool> ToggleStates = new Dictionary<string, bool>();


        //ConcurrentDictionary<object, Boolean> ToggleBools = new ConcurrentDictionary<object, Boolean>();


        void OnDestroy() 
        {
           //
           //O.ELP.Buffs.F
        }

        // Start is called before the first frame update
        void Start()
        {
            windowID = new System.Random(Environment.TickCount).Next(1000, 65535);
            windowRect = new Rect(10f, 400f, 400f, 500f);
            GUI.color = Color.white;

            Debug.LogWarning("THIS IS Start New!!!!");

            NS.Instance.ListForReset.Add("sda");
            //NS.Instance.SettingsDictionary.Add("IsGameStarted",false);
            Settings.Add("IsGameStarted", false);



            Settings.Add("scrollPlayer", new Vector2());
            Settings.Add("ScrollMenu0", Vector2.zero);
            Settings.Add("scrollBuffMenu", Vector2.zero);
            Settings.Add("FoldZombie",false);


        }
        //public static Dictionary<string, Vector2> SV21= new Dictionary<string, Vector2>
        //{
        //    { "scrollPlayer", new Vector2() },
        //    { "scrollBuff", new Vector2() },
        //    { "scrollCBuff", new Vector2() },
        //    { "scrollZombie", new Vector2() },
        //    { "scrollPassive", new Vector2() },
        //    { "scrollPGV", new Vector2() },
        //    { "scrollKill", new Vector2() },

        //    { "ScrollMenu0", Vector2.zero },
        //    { "ScrollMenu1", Vector2.zero },
        //    { "ScrollMenu2", Vector2.zero },
        //    { "ScrollMenu3", Vector2.zero },
        //    { "ScrollMenu4", Vector2.zero },
        //    { "ScrollMenu5", Vector2.zero },
        //    { "scrollBuffBTN", Vector2.zero }
        //};
        // Update is called once per frame
        void Update()
        {
            if (!Input.anyKey || !Input.anyKeyDown)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.F2))
            {
                drawMenu = !drawMenu;
            }
            if (Input.GetKeyDown(KeyCode.Delete))
            {
                //SETT.selfDestruct = false; //set bool to false so we can inject again
                Destroy(Loader.gameObject); // destroys our game object we injected
                //ObjectDestroyer.DestroyObjectByName("ExplorerBehaviour");
                ObjectDestroyer.DestroyObjectByName("7DTD----MENU");
            }
        }


        private void OnGUI()
        {

            GUIStyle customStyle = new GUIStyle(GUI.skin.window);
            if (GetBool("IsGameStarted")==true)
            {

                customStyle.normal.textColor = Color.green;
                customStyle.onNormal.textColor = Color.green;
                //customStyle.onFocused.textColor = Color.green;
                //customStyle.onActive.textColor = Color.green;
                //customStyle.focused.textColor = Color.green;
                //customStyle.onActive.textColor = Color.blue;
                //customStyle.onHover.textColor = Color.yellow;
                //customStyle.hover.textColor = Color.black;
                //customStyle.active.textColor = Color.cyan;


            }
            else
            {
                customStyle.normal.textColor = Color.red;
                customStyle.onNormal.textColor = Color.red;
                //customStyle.onFocused.textColor = Color.red;
                //customStyle.focused.textColor = Color.red;
                //customStyle.onActive.textColor = Color.red;
                //customStyle.onHover.textColor = Color.red;  
                //customStyle.hover.textColor = Color.red;  
                //customStyle.active.textColor = Color.red;


            }

            if (drawMenu)
            {
                windowRect = GUILayout.Window(windowID, windowRect, Window, "7Days To Die Cheat Menu",customStyle); //draws main menu
            }
            // Create a new GUIStyle based on the default GUI.skin.box
            // Set the desired background color for the box


            #region Styles BE OnGUI

            defBoxStyle = new GUIStyle(GUI.skin.box);
            //defBoxStyle.border = new RectOffset(-2,-2,-2,-2);
            defBoxStyle.padding = new RectOffset(0, 0, 0, 0);


            customBoxStyleGreen = new GUIStyle(GUI.skin.box);
            customBoxStyleGreen.normal.background = MakeTexture(2, 2, new Color(0f, 1f, 0f, 0.5f));




            //customBoxStyleGreen.border = new RectOffset(2, 2, 2, 2);


            centeredLabelStyle = new GUIStyle(GUI.skin.label);
            centeredLabelStyle.alignment = TextAnchor.MiddleCenter;

            #endregion
        }
        private Texture2D MakeTexture(int width, int height, Color color)
        {
            Color[] pixels = new Color[width * height];
            for (int i = 0; i < pixels.Length; i++)
            {
                pixels[i] = color;
            }

            Texture2D texture = new Texture2D(width, height);
            texture.SetPixels(pixels);
            texture.Apply();
            return texture;
        }
        private static string ValueModifierTypesToString(int index)
        {
            return ((PassiveEffect.ValueModifierTypes)index).ToString();
        }
        private void Window(int windowID)
        {
            //if (Settings.Istarted == true|| Settings.Istarted == false)
            //{
            //    windowRect.height = 50;
            //    GUILayout.Space(10f);
            //    GUILayout.BeginHorizontal();
            //    GUILayout.Label("Start a game to load the Menu",centeredLabelStyle);
            //    GUILayout.EndHorizontal();
            //    GUILayout.Space(10f);
            //    GUI.DragWindow();
            //}
            //else
            {
                windowRect.height = 500;
                _group = NewGUILayout.Toolbar4(_group, CheatsString, GUI.skin.box); //creating the group for switch comand    
                switch (_group)
                {
                    //case 0://switch to menu0
                    //    Menu0();
                    //    break;
                    //case 1://switch to menu1
                    //    Menu1();
                    //    break;
                    //case 2://switch to menu2
                    //    Menu2();
                    //    break;

                    case 3: //add more for more menu
                            //Log.Out("opening Menu3");
                        Menu3();
                        break;
                }
                GUILayout.Space(10f);

            }
        }
        //CGUILayout.BeginHorizontal(() => {});
        //CGUILayout.BeginVertical(() => {});


        private void Menu0() //player
        {

            //CGUILayout.BeginVertical(() => { });
            //CGUILayout.BeginHorizontal(() => { });

            NewGUILayout.BeginHorizontal(() =>
            {
                NewGUILayout.BeginVertical(GUI.skin.box, () =>
                {


                    Settings["ScrollMenu0"] = GUILayout.BeginScrollView(GetZeroVector2("ScrollMenu0"));
                    {
                        NewGUILayout.BeginHorizontal(() =>
                        {
                            NewGUILayout.BeginVertical(() =>
                            {
                                NewGUILayout.Button("Add skillpoints", Cheat.skillpoints);
                                NewGUILayout.Button("Kill Self", Cheat.KillSelf);
                                NewGUILayout.Button("Level Up", Cheat.levelup);
                                NewGUILayout.Button("Get my ID", Cheat.Getplayer);

                                if (SB.Count > 1)
                                {
                                    NewGUILayout.DictButton("IGNORE", "_BoolIGNORE");
                                    NewGUILayout.RButton("Ignored By AI", "_ignoreByAI");
                                    NewGUILayout.Button("Creative/Debug Mode", RB, "CmDm");
                                    NewGUILayout.SButton("Name Scramble2", "_nameScramble");
                                    NewGUILayout.Button("sda", () =>
                                    {
                                        NewSettings.Instance.Speed = !NewSettings.Instance.Speed;
                                    });
                                    NewGUILayout.BoolButton("TEstSpeed NameOF", nameof(NewSettings.Instance.Speed));

                                    //GuiLayoutExtended.NewGUILayout.ButtonBoolToggle("TestToggleBool", Settings.Instance.Speed);


                                    //THIS WORKS
                                    NewSettings.Instance.CreativeMode = GuiLayoutExtended.NewGUILayout.ButtonBoolToggle("Toggle CreativeMode", NewSettings.Instance.CreativeMode);

                                }


                            });
                            NewGUILayout.BeginVertical(() =>
                            {
                                if (NewGUILayout.Button($"Teleport To Marker "))
                                {

                                    O.ELP.TeleportToPosition(new Vector3(O.ELP.markerPosition.ToVector3().x, O.ELP.markerPosition.ToVector3().y+2, O.ELP.markerPosition.ToVector3().z));
                                }

                                NewGUILayout.Button("Instant Quest", RB,"_QuestComplete");
                                NewGUILayout.Button("Instant Craft",RB, "_instantCraft");
                                NewGUILayout.Button("Instant scrap", RB, "_instantScrap");
                                NewGUILayout.Button("Instant smelt", RB, "_instantSmelt");
                                NewGUILayout.Button("Loop LAST Quest Rewards",RB,"_LOQuestRewards");
                                NewGUILayout.RButton("Trader Open 24/7", "_EtraderOpen");


                                NewGUILayout.BeginHorizontal(() => {
                                    _itemcount = GUILayout.TextField(_itemcount, 10, GUILayout.MaxWidth(50));

                                    if (NewGUILayout.Button(" Set holding item"))
                                    {
                                        try
                                        {
                                            int intvalue = int.Parse(_itemcount);
                                            if (O.ELP.inventory.holdingItemStack.count >= 1)
                                            {

                                                //O.ELP.inventory.holdingCount;
                                                O.ELP.inventory.holdingItemStack.count = intvalue;
                                                //O.ELP.inventory.TryStackItem();
                                                Debug.LogWarning($"{O.ELP.inventory.holdingItem.Name} set to {intvalue}");
                                            }
                                            else
                                            {
                                                Debug.LogWarning($"{O.ELP.inventory.holdingItem.Name} Could not be set to {intvalue}");
                                               
                                            }
                                        }
                                        catch
                                        {
                                        }
                                    }


                                });

                            });
                        });
                    }
                    GUILayout.EndScrollView();

                });
            });
            GUI.DragWindow();

        }
        private void Menu1() //Toggles and modifiers
        {
            NewGUILayout.BeginHorizontal(GUI.skin.box, () => {
                NewGUILayout.BeginVertical(() => {
                    //SETT.crosshair = GUILayout.Toggle(SETT.crosshair, "Crosshair");
                    //SETT.fovCircle = GUILayout.Toggle(SETT.fovCircle, "Draw FOV");
                    //SETT.playerBox = GUILayout.Toggle(SETT.playerBox, "Player Box");
                    //SETT.playerName = GUILayout.Toggle(SETT.playerName, "Player Name");
                    //SETT.playerCornerBox = GUILayout.Toggle(SETT.playerCornerBox, "Player Corner Box");
                    //SETT.chams = GUILayout.Toggle(SETT.chams, "Chams");
                    //SETT.playerHealth = GUILayout.Toggle(SETT.playerHealth, "Player Health");
                });
                NewGUILayout.BeginVertical(() => {
                    //SETT.zombieBox = GUILayout.Toggle(SETT.zombieBox, "Zombie Box");
                    //SETT.zombieName = GUILayout.Toggle(SETT.zombieName, "Zombie Name");
                    //SETT.zombieHealth = GUILayout.Toggle(SETT.zombieHealth, "Zombie Health");
                    //SETT.zombieCornerBox = GUILayout.Toggle(SETT.zombieCornerBox, "Zombie Corner Box");
                    //SETT.noWeaponBob = GUILayout.Toggle(SETT.noWeaponBob, "No Weapon Bob");
                });
            });
            NewGUILayout.BeginVertical(GUI.skin.box, () =>
            {
                GUIStyle Labelstyle = new GUIStyle(GUI.skin.label);
                Labelstyle.alignment = TextAnchor.LowerCenter;
                Labelstyle.fontSize = 13;
                Labelstyle.padding = new RectOffset(0, 0, -4, 0);


                NewGUILayout.BeginHorizontal(() => { GUILayout.Label("Modifiers"); });
                NewGUILayout.BeginVertical(() =>
                {
                    //GUILayout.Space(10);
                    NewGUILayout.HorizontalScrollbarWithLabel("Attacks/minute", ref SETT._FL_APM,500f);
                    NewGUILayout.HorizontalScrollbarWithLabel("Jump Strength", ref SETT._FL_jmp, 5f);
                    NewGUILayout.HorizontalScrollbarWithLabel("Harvest Dubbler", ref SETT._FL_harvest, 20f);
                    NewGUILayout.HorizontalScrollbarWithLabel("Sprint Speed", ref SETT._FL_run, 25f);
                    NewGUILayout.HorizontalScrollbarWithLabel("Kill DMG xM", ref SETT._FL_killdmg, 1000f);
                    NewGUILayout.HorizontalScrollbarWithLabel("Block DMG", ref SETT._FL_blokdmg, 10000f);

                    //CGUILayout.BeginHorizontal(() =>
                    //{
                    //    GUILayout.Label("Attacks/minute", Labelstyle, GUILayout.MaxWidth(60));
                    //    SETT._FL_APM = GUILayout.HorizontalScrollbar(SETT._FL_APM, 0f, 0f, 20f);

                    //    //SETT._dmg = GUILayout.HorizontalSlider(SETT._dmg, 0f, 200f);
                    //    //SETT._dmg = GUILayout.HorizontalScrollbar(SETT._dmg, 0.1f, 0f, 200f, GUILayout.ExpandWidth(true));
                    //    GUILayout.Label(SETT._FL_APM.ToString("F1"), Labelstyle, GUILayout.MaxWidth(40));
                    //});
                });
                NewGUILayout.BeginVertical(GUI.skin.box ,() =>
                {
                    Settings["scrollBuffMenu"] = GUILayout.BeginScrollView(GetZeroVector2("scrollBuffMenu"), GUILayout.MaxHeight(170));//GUILayout.MaxWidth(250f), GUILayout.Width(250f),GUILayout.Height(50f)
                    {
                        NewGUILayout.BeginHorizontal(() =>
                        {
                            NewGUILayout.BeginVertical(() =>
                            {


                                NewGUILayout.RButton( "Block DMG", "_BL_Blockdmg");//button toggle bool
                                NewGUILayout.RButton("Kill DMG" , "_BL_Kill");//button toggle bool
                                NewGUILayout.RButton("Harvest Dubbler", "_BL_Harvest");//button toggle bool

                            });
                            NewGUILayout.BeginVertical(() =>
                            {
                                NewGUILayout.RButton( "Sprint Speed", "_BL_Run");//button toggle bool
                                NewGUILayout.RButton( "Jump Strength", "_BL_Jmp");//button toggle bool
                                NewGUILayout.RButton( "Attacks / minute", "_BL_APM");//button toggle bool



                            });

                        });
                    }
                    GUILayout.EndScrollView();
                });



            });
            GUI.DragWindow();
        }
        private void Menu2() //Buffs And stuff
        {
            NewGUILayout.BeginHorizontal(defBoxStyle, () =>
            {
                SV2["ScrollMenu2"] = GUILayout.BeginScrollView(SV2["ScrollMenu2"]);
                {
                    NewGUILayout.BeginHorizontal(() =>
                    {
                        NewGUILayout.BeginVertical("buffs", defBoxStyle, () =>
                        {
                            GUILayout.Space(20f);
                            NewGUILayout.BeginVertical(() =>
                            {
                                NewGUILayout.BeginVertical(() =>
                                {
                                    SV2["scrollBuffBTN"] = GUILayout.BeginScrollView(SV2["scrollBuffBTN"], GUILayout.MinHeight(80));//GUILayout.MaxWidth(250f), GUILayout.Width(250f),GUILayout.Height(50f)
                                    {
                                        NewGUILayout.BeginHorizontal(() =>
                                        {
                                            NewGUILayout.BeginVertical(() =>
                                            {
                                                //CGUILayout.Button("Set NOBad buff", ref SETT._NoBadBuff);
                                                //CGUILayout.Button("Set good buff", ref SETT._addgoodbuff);
                                                NewGUILayout.Button("Remove All Active Buffs", Cheat.RemoveAllBuff);//Cheat.RemoveBadBuff()

                                                NewGUILayout.Button("Clear CheatBuff", Cheat.ClearCheatBuff);//Cheat.custombuff();
                                              
                                   
                                                //CGUILayout.Button(ref ToggleStates, "NoBadBuff");
                                                //if (CGUILayout.Button("RELOAD Buffs"))
                                                //{
                                                //    if (SETT.reloadBuffs == false && O.buffClasses.Count <= 10)
                                                //    {
                                                //        SETT.reloadBuffs = true;
                                                //    }
                                                //}
                                                
                                                //CGUILayout.Button("cheat", Cheat);  //O.buffClasses = O.GetAvailableBuffClasses();
                                                //BuffManager.Buffs.Clear(); Removed all buffs from runtime
                                                //CGUILayout.Button("Remove Bad Buffs", Cheat.RemoveBadBuff);//Cheat.RemoveBadBuff();
                                             
                                                //CGUILayout.Button("Add Good Buffs", Cheat.AddGoodBuff);//Cheat.AddGoodBuff();

                                                //CGUILayout.Button(ref SETT._BL_Blockdmg, "One hit block");//button toggle bool
                                                //CGUILayout.Button(ref SETT._BL_Kill, "One hit kill");//button toggle bool
                                                //CGUILayout.Button(ref SETT._BL_Harvest, "Dubbel Harvest");//button toggle bool
                                                //GUILayout.Label("rigth", centeredLabelStyle);
                                            });
                                            NewGUILayout.BeginVertical(() =>
                                            {
                                                NewGUILayout.Button("Add CheatBuff to P", Cheat.AddCheatBuff);//Cheat.custombuff();
                                                NewGUILayout.Button("Add Effect Group", Cheat.AddEffectGroup);//Cheat.custombuff();
                                                //CGUILayout.Button("");
                                                //CGUILayout.Button("NoBadBuff", ref ToggleStates);
                                                //if (CGUILayout.Button("Print Buffs To Logfile"))
                                                //{
                                                //    Extras.LogAvailableBuffNames(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "load", "BuffsList.txt"));
                                                //    /*
                                                //    //CBuffs.ParseAddBuff();
                                                //    //XDocument xmlDoc = CBuffs.LoadEmbeddedXML("TESTBUFF.XML");


                                                //    //XElement xmlElmt = CBuffs.LoadEmbeddedXML("TESTBUFF.XML");

                                                //    //BuffsFromXml.CreateBuffs(xmlDoc);
                                                //    // Now you can work with the XML data (e.g., access nodes, read attributes, etc.).
                                                //    // For example, let's print the contents of the XML to the Unity console.
                                                //    //Debug.Log(xmlDoc.ToString());



                                                //    //Cheat.AddPassive(PassiveEffects.InfiniteAmmo, 1, PassiveEffect.ValueModifierTypes.base_set);
                                                //    //Cheat.AddPassive(PassiveEffects.HyperthermalResist, 1, PassiveEffect.ValueModifierTypes.base_set);
                                                //    */
                                                //}
                                                //if (CGUILayout.Button("add Passive crafttimer"))
                                                //{
                                                //    Cheat.AddPassive(PassiveEffects.CraftingTier, 3, PassiveEffect.ValueModifierTypes.base_set);
                                                //    Cheat.AddPassive(PassiveEffects.CraftingTime, 999, PassiveEffect.ValueModifierTypes.base_set);

                                                //    CGUILayout.Button1("Add Effect Group", new Action[] { Cheat.AddCheatBuff, () => Cheat.AddPassive(PassiveEffects.CraftingTime, 999, PassiveEffect.ValueModifierTypes.base_set) });
                                                //}
                                                //if (CGUILayout.Button("add Passive Jump"))
                                                //{
                                                //    Cheat.AddPassive(PassiveEffects.JumpStrength, 5, PassiveEffect.ValueModifierTypes.base_set);
                                                //    //Cheat.AddPassive(PassiveEffects.CraftingTime, 999, PassiveEffect.ValueModifierTypes.base_set);


                                                //}

                                                //CGUILayout.Button("add Jump buff for test", Cheat.AddEffectGRoup__);

                                            });

                                        });
                                    }
                                    GUILayout.EndScrollView();
                                });
                                NewGUILayout.BeginHorizontal(customBoxStyleGreen);

                                FoldCBuff = NewGUILayout.FoldableMenuHorizontal(FoldCBuff, "Custom Buffs", () => {
                                    scrollCBuff = GUILayout.BeginScrollView(scrollCBuff,GUILayout.MinHeight(100));//GUILayout.MaxWidth(250f), GUILayout.Width(250f),GUILayout.Height(50f)
                                    {
                                        Cheat.GetListCBuffs(O.ELP, O._listCbuffs);
                                    }
                                    GUILayout.EndScrollView();
                                }, 300f);

                                NewGUILayout.BeginHorizontal(customBoxStyleGreen);



                                NewGUILayout.BeginVertical(GUI.skin.box, () =>
                                {
                                    NewGUILayout.BeginVertical(() =>
                                    {
                                       
                                        FoldPassive = NewGUILayout.FoldableMenuHorizontal(FoldPassive, "Passive Effects", () => {


                                            NewGUILayout.BeginHorizontal(() => {
                                                Cheat.inputPassiveEffects = GUILayout.TextField(Cheat.inputPassiveEffects, 50);
                                                inputPassive = ValueModifierTypesToString(ValueModifierTypesIndex); // Set text field with the button's current value
                                                inputFloat = GUILayout.TextField(inputFloat, 10, GUILayout.MaxWidth(25));
                                                if (NewGUILayout.Button("", ref ValueModifierTypesIndex, GUILayout.MaxWidth(100)))
                                                {
                                                    ValueModifierTypes selectedType = (ValueModifierTypes)ValueModifierTypesIndex;
                                                    //Debug.Log("Selected Enum: " + selectedType);

                                                }
                                                if (NewGUILayout.Button("ADD", GUILayout.MaxWidth(40)))
                                                {
                                                    try
                                                    {
                                                        float floatValue = float.Parse(inputFloat);
                                                        if (System.Enum.TryParse(Cheat.inputPassiveEffects, out PassiveEffects passiveEffect))
                                                        {
                                                            Cheat.AddPassive(passiveEffect, floatValue, (PassiveEffect.ValueModifierTypes)ValueModifierTypesIndex);
                                                            Log.Out(inputPassiveEffects);
                                                            Log.Out($"{passiveEffect} added");
                                                        }
                                                        else
                                                        {
                                                            Log.Out(inputPassiveEffects);
                                                            Log.Out($"{passiveEffect} not added");
                                                            // Handle the case when the inputPassiveEffects cannot be parsed to the enum.
                                                            // Display an error message, provide a default value, or take appropriate action.
                                                        }
                                                    }
                                                    catch
                                                    { }



                                                    //ValueModifierTypes selectedType = (ValueModifierTypes)System.Enum.ToObject(typeof(ValueModifierTypes), currentIndex);
                                                    //inputPassive = selectedType.ToString();
                                                }
                                            });
                                            NewGUILayout.BeginHorizontal(() => {
                                                GUILayout.Label("Passive Search");
                                                passiveSearch = GUILayout.TextField(passiveSearch, GUILayout.MaxWidth(225f), GUILayout.Height(20f));
                                            });
                                            scrollPassive = GUILayout.BeginScrollView(scrollPassive, GUILayout.MinHeight(100));//GUILayout.MaxWidth(250f), GUILayout.Width(250f),GUILayout.Height(50f)
                                            {
                                                Cheat.GetListPassiveEffects(passiveSearch);
                                            }
                                            GUILayout.EndScrollView();
                                        }, 300f);
                                    });
                                });








                                NewGUILayout.BeginHorizontal(customBoxStyleGreen);
                                SB1["FoldBuff"] = NewGUILayout.FoldableMenuHorizontal(SB1["FoldBuff"], "Scroll All Buffs", () => {

                                    NewGUILayout.BeginHorizontal(() => {
                                        GUILayout.Label("Search for buff");
                                        buffSearch = GUILayout.TextField(buffSearch, GUILayout.MaxWidth(225f), GUILayout.Height(20f));
                                    });

                                    SV2["scrollBuff"] = GUILayout.BeginScrollView(SV2["scrollBuff"], GUILayout.MinHeight(100));//GUILayout.MaxWidth(250f), GUILayout.Width(250f),GUILayout.Height(50f)
                                    {
                                        Cheat.GetList(lastBuffAdded, O.ELP, O._listBuffClass, buffSearch);
                                    }
                                    GUILayout.EndScrollView();
                                }, 300f);
                                NewGUILayout.BeginHorizontal(customBoxStyleGreen);
                                SB1["FoldPGV"] = NewGUILayout.FoldableMenuHorizontal(SB1["FoldPGV"], "Scroll Perks", () =>
                                {

                                    NewGUILayout.BeginHorizontal(() => {
                                        GUILayout.Label("Search for Perk");
                                        PGVSearch = GUILayout.TextField(PGVSearch, GUILayout.MaxWidth(225f), GUILayout.Height(20f));
                                    });
                                    NewGUILayout.BeginHorizontal(customBoxStyleGreen);
                                    SV2["scrollPGV"] = GUILayout.BeginScrollView(SV2["scrollPGV"], GUILayout.MinHeight(250f), GUILayout.MaxHeight(450f));//GUILayout.MaxWidth(250f), GUILayout.Width(250f),, GUILayout.Height(200f)
                                    {
                                        //Cheat.ListButtonPlayer();
                                        Cheat.ListPGV(PGVSearch);
                                        //LISTDROPPDOWNMENU();
                                        //Cheat.GetList(lastBuffAdded, O.ELP, O.buffClasses);
                                    }
                                    GUILayout.EndScrollView();
                                }, 300f);
                                NewGUILayout.BeginHorizontal(customBoxStyleGreen);
                            });
                        });
                    });
                }
                GUILayout.EndScrollView();
            });
            GUI.DragWindow();

        } //Buffs an  stuff
        private void Menu3()//some crap
        {
            NewGUILayout.BeginVertical(GUI.skin.box, () =>
            {

                NewGUILayout.BeginScrollView("ScrollMenu3", () => {

                    NewGUILayout.BeginHorizontal(customBoxStyleGreen); //***************************************************************

                    NewGUILayout.DictFoldMenuHorizontal("TestMenu1", "TestMenu1", () =>
                    {
                        NewGUILayout.DictButton("Test1", "_BoolTest1");
                        NewGUILayout.DictButton("Test2", "_BoolTest2");
                        NewGUILayout.DictButton("Test2", "_BoolTest2");
                        NewGUILayout.DictButton("Test2", "_BoolTest2");

                    }, 300f, GUILayout.MinHeight(200f));

                    NewGUILayout.BeginHorizontal(customBoxStyleGreen); //***************************************************************

                    NewGUILayout.DictFoldMenuHorizontal("TestMenu3", "TestMenu3", () =>
                    {
                        NewGUILayout.BeginVertical(GUI.skin.box, () =>
                        {
                            NewGUILayout.BeginScrollView("TestScroll1", () => {
                                NewGUILayout.DictButton("Test5", "_BoolTest5");
                                NewGUILayout.DictButton("Test6", "_BoolTest6");
                                NewGUILayout.DictButton("Test7", "_BoolTest7");
                                NewGUILayout.DictButton("Test8", "_BoolTest8");
                                NewGUILayout.DictButton("Test9", "_BoolTest9");
                                NewGUILayout.DictButton("Test10", "_BoolTest10");
                                NewGUILayout.DictButton("Test11", "_BoolTest11");
                                NewGUILayout.DictButton("Test12", "_BoolTest12");
                                NewGUILayout.DictButton("Test13", "_BoolTest13");
                                NewGUILayout.DictButton("Test14", "_BoolTest14");
                                NewGUILayout.DictButton("Test15", "_BoolTest15");
                                NewGUILayout.DictButton("Test16", "_BoolTest16");
                                NewGUILayout.DictButton("Test17", "_BoolTest17");
                                NewGUILayout.DictButton("Test18", "_BoolTest18");

                            });
                        });
                    }, 300f, GUILayout.MinHeight(200f));

                    NewGUILayout.BeginHorizontal(customBoxStyleGreen); //***************************************************************

                    NewGUILayout.DictFoldMenuHorizontal("Scroll Zombies", "FoldZombie", () =>
                    {

                        NewGUILayout.BeginScrollView("scrollZombie", () => {
                            NewGUILayout.BeginVertical(GUI.skin.box, () =>
                            {
                                Cheat.ListZombie1();
                            });
                        }, GUILayout.MinHeight(250f), GUILayout.MaxHeight(350f));

                    }, 300f, GUILayout.MinHeight(200f));

                    NewGUILayout.BeginHorizontal(customBoxStyleGreen); //***************************************************************

                    NewGUILayout.DictFoldMenuHorizontal("Scroll Players", "FoldPlayer", () =>
                    {

                        NewGUILayout.BeginScrollView("scrollPlayer", () => {
                            NewGUILayout.BeginVertical(GUI.skin.box, () =>
                            {
                                Cheat.ListPlayer1();
                            });
                        }, GUILayout.MinHeight(250f), GUILayout.MaxHeight(350f));

                    }, 300f, GUILayout.MinHeight(200f));

                    NewGUILayout.BeginHorizontal(customBoxStyleGreen); //***************************************************************

                    NewGUILayout.DictFoldMenuHorizontal("Foldable Menu 5", "foldout5", () =>
                    {

                        NewGUILayout.BeginVertical(GUI.skin.box, () =>
                        {


                            NewGUILayout.BeginHorizontal(customBoxStyleGreen, () =>
                            {
                                NewGUILayout.BeginVertical(GUI.skin.box, () =>
                                {


                                    GUILayout.Label("EXPERIMENTAL ", centeredLabelStyle);
                                    //SETT.aimbot = GUILayout.Toggle(SETT.aimbot, "Aimbot (L-alt)");
                                    //SETT.magicBullet = GUILayout.Toggle(SETT.magicBullet, "Magic Bullet(L-alt");

                                });
                                NewGUILayout.BeginVertical(customBoxStyleGreen, () =>
                                {
                                    GUILayout.Label("L4 Content for Menu ", centeredLabelStyle);

                                    if (NewGUILayout.Button($"Log PGVC To file"))
                                    {
                                        O.LogprogclassClassesToFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "load", "_listProgressionClass.txt"));
                                    }
                                    if (NewGUILayout.Button("´Log Buffs To file"))
                                    {
                                        Extras.LogAvailableBuffNames(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "load", "BuffsList.txt"));
                                    }

                                });

                            });
                        }); ;

                    }, 300f, GUILayout.MinHeight(200f));

                    NewGUILayout.BeginHorizontal(customBoxStyleGreen);
                }, GUILayout.MinHeight(250f), GUILayout.MaxHeight(350f));

            });
            GUI.DragWindow();
        }

        #region

        /*
        private void Menu6()
        {
            CGUILayout.BeginVertical(GUI.skin.box ,() => 
            {

                ScrollMenu3 = GUILayout.BeginScrollView(ScrollMenu3);
                {
                    foldout1 = CGUILayout.FoldableMenuHorizontal(foldout1, "Foldable Menu 1", () =>
                    {  // Content to show when the foldout is open for Foldable Menu 1
                       // Add your UI elements here...
                       CGUILayout.BeginVertical(GUI.skin.box ,() => 
                       {
                           // Start a vertical layout group for the label and horizontal layoutqqq
                               GUILayout.Label("L1 Content for Menu 2", centeredLabelStyle); //Label for the menu
                           CGUILayout.BeginHorizontal(() =>
                           {
                               CGUILayout.BeginVertical(() => 
                               {
                                   GUILayout.Label("DMG Multiply " + SETT._dmg.ToString("F2"));

                               }, GUILayout.MaxWidth(50));

                               CGUILayout.BeginVertical(() => 
                               {
                                   SETT._dmg = GUILayout.HorizontalScrollbar(SETT._dmg, 0.1f, 0f, 300f);
                               });
                           });
                           CGUILayout.BeginHorizontal(customBoxStyleGreen ,() => 
                           {
                               CGUILayout.BeginVertical(centeredLabelStyle ,() =>
                               {
                                   GUILayout.Label("side left", centeredLabelStyle);
                               });
                               CGUILayout.BeginVertical(() => { });


                           });
                       });
                    }, 300f);




                    foldout2 = CGUILayout.FoldableMenuHorizontal(foldout2, "Foldable Menu 2", () =>
                    {  // Content to show when the foldout is open for Foldable Menu 1
                       // Add your UI elements here...
                        GUILayout.BeginVertical(GUI.skin.box);
                        { // Start a vertical layout group for the label and horizontal layout
                            GUILayout.Label("L1 Content for Menu 2", centeredLabelStyle); //Label for the menu
                            GUILayout.BeginHorizontal(customBoxStyleGreen);
                            {

                                GUILayout.BeginVertical();
                                {

                                    CGUILayout.Button(ref SETT._oneHitBlock, "One hit block", Color.green, Color.red, Color.yellow);//button toggle bool
                                    CGUILayout.Button(ref SETT._oneHitKill, "One hit kill", Color.green, Color.red, Color.yellow);//button toggle bool
                                }
                                GUILayout.EndVertical();
                                GUILayout.BeginVertical();
                                {
                                    GUILayout.Label("rigth", centeredLabelStyle);

                                }
                                GUILayout.EndVertical();
                            }
                            GUILayout.EndHorizontal();
                        }
                        GUILayout.EndVertical();
                    }, 300f);
                    foldout3 = CGUILayout.FoldableMenuHorizontal(foldout3, "Foldable Menu 3 Lists", () =>
                    {  // Content to show when the foldout is open for Foldable Menu 1
                       // Add your UI elements here...
                        GUILayout.BeginVertical(GUI.skin.box);
                        { // Start a vertical layout group for the label and horizontal layout
                          //
                            GUILayout.Label("L1 Content for Menu 2", centeredLabelStyle); //Label for the menu


                            GUILayout.BeginVertical("Kill zombie", GUI.skin.box);
                            {
                                GUILayout.Space(20f);

                                scrollKill = GUILayout.BeginScrollView(scrollKill, GUILayout.MaxWidth(300f));
                                {
                                    //Cheat.ListbuttonZombie2();
                                }
                                GUILayout.EndScrollView();
                            }
                            GUILayout.EndVertical();
                            GUILayout.BeginVertical("Teleport", GUI.skin.box);
                            {
                                GUILayout.Space(20f);

                                scrollPlayer = GUILayout.BeginScrollView(scrollPlayer, GUILayout.MaxWidth(300f));
                                {
                                    //Cheat.ListbuttonZombie1();
                                    //Cheat.ListButtonPlayer();
                                }
                                GUILayout.EndScrollView();
                            }
                            GUILayout.EndVertical();

                            //GUILayout.Label("L2 Content for Menu 2", centeredLabelStyle); //here are all the items placed

                        }
                        GUILayout.EndVertical();
                    }, 300f);
                    foldout4 = CGUILayout.FoldableMenuHorizontal(foldout4, "Foldable Menu 4 Some Stat Buffs", () =>
                    {  // Content to show when the foldout is open for Foldable Menu 1
                       // Add your UI elements here...
                        CGUILayout.BeginVertical(GUI.skin.box ,() => 
                        {
                            GUILayout.Label("L1 Content for Menu 2", centeredLabelStyle);
                            CGUILayout.BeginHorizontal(() => 
                            {
                                CGUILayout.BeginVertical(customBoxStyleGreen ,() => { });
                                CGUILayout.BeginVertical(customBoxStyleGreen ,() => { });

                            });

                        });
                    }, 300f);
                    foldout5 = CGUILayout.FoldableMenuHorizontal(foldout5, "Foldable Menu 5", () =>
                    {
                        CGUILayout.BeginVertical(GUI.skin.box, () => 
                        {


                            CGUILayout.BeginHorizontal(customBoxStyleGreen ,() =>
                            {
                                CGUILayout.BeginVertical(GUI.skin.box, () => 
                                {


                                    GUILayout.Label("EXPERIMENTAL ", centeredLabelStyle);
                                    SETT.aimbot = GUILayout.Toggle(SETT.aimbot, "Aimbot (L-alt)");
                                    SETT.magicBullet = GUILayout.Toggle(SETT.magicBullet, "Magic Bullet(L-alt");

                                });
                                CGUILayout.BeginVertical(customBoxStyleGreen, () => 
                                {
                                    GUILayout.Label("L4 Content for Menu ", centeredLabelStyle); //here are all the items placed
                                                                                                 //>-
                                });

                            });
                        });
                    }, 300f);
                }
                GUILayout.EndScrollView();
            });
            GUI.DragWindow();
        }
        */
        #endregion


    }
}









//GUILayout.BeginVertical();
//{
//    SETT.CmDm = CGUILayout.Toggle(SETT.CmDm, "Creative/Debug Mode");//toggle on/off bool
//    SETT.TESTTOG = CGUILayout.Toggle(SETT.TESTTOG, "TESTTOG");

//    SETT.drpbp = CGUILayout.Toggle(SETT.drpbp, "drpbkssdffsdfsdrfsfsd");//toggle on/off bool
//    SETT.speed = CGUILayout.Toggle(SETT.speed, "speed");//toggle on/off bool
//    SETT.speed = CGUILayout.Toggle(SETT.speed, "Game speed");//toggle on/off bool
//                                                             //if (GUILayout.Button("xp-modifier"))
//                                                             //{
//                                                             //}
//                                                             //if (GUILayout.Button("Label"))
//                                                             //{
//                                                             //}
//                                                             //if (GUILayout.Button("PasswordField"))
//                                                             //{
//                                                             //}
//                                                             //if (GUILayout.Button("TextField"))
//                                                             //{
//                                                             //}
//                                                             //if (GUILayout.Button("Box"))
//                                                             //{

//    //}
//    //if (GUILayout.Button("RepeatButton"))
//    //{

//    //}
//}
//GUILayout.EndVertical();

//GUILayout.BeginVertical();
//{
//    if (CGUILayout.Button("CONSOLEPRINT"))
//    {
//        Cheat.SOMECONSOLEPRINTOUT();
//    }

//    if (GUILayout.Button("Close gameobject"))
//    {
//        SETT.selfDestruct = true;
//    }
//    CGUILayout.Button(ref SETT._QuickScrap, "Quick scrap", Color.green, Color.red);


//    //if (GUILayout.Button("TextArea"))
//    //{

//    //}
//    //if (GUILayout.Button("HorizontalSlider"))
//    //{

//    //}
//    //if (GUILayout.Button("VerticalSlider"))
//    //{
//    //}
//    //if (GUILayout.Button("DrawTexture"))
//    //{

//    //}
//    //if (GUILayout.Button("Window"))
//    //{
//    //}
//}
//GUILayout.EndVertical();


























//GUILayout.BeginHorizontal();
//{
//    GUILayout.BeginVertical();
//    {

//    }GUILayout.EndVertical();
//}GUILayout.EndHorizontal();




//if (GUILayout.Button("xp-modifier"))
//{
//}
//if (GUILayout.Button("Label"))
//{
//}
//if (GUILayout.Button("PasswordField"))
//{
//}
//if (GUILayout.Button("TextField"))
//{
//}
//if (GUILayout.Button("Box"))
//{

//}
//if (GUILayout.Button("RepeatButton"))
//{

//}
//GUILayout.EndVertical();
//GUILayout.BeginVertical();

//if (GUILayout.Button("TextArea"))
//{

//}
//if (GUILayout.Button("HorizontalSlider"))
//{

//}
//if (GUILayout.Button("VerticalSlider"))
//{
//}
//if (GUILayout.Button("DrawTexture"))
//{

//}
//if (GUILayout.Button("Window"))
//{
//}
//if (GUILayout.Button("Toggle"))
//{
//    SETT.selfDestruct = true;
//}



