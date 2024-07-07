
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using O = SevenDTDMono.Objects;
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
using static Twitch.PubSub.PubSubChannelPointMessage;
using static InvStat;

namespace SevenDTDMono
{
    public class NewMenu : MonoBehaviour
    {

        public static Dictionary<string,object> Settings => NewSettings.Instance.SettingsDictionary;
        private static NewSettings SettingsInstance => NewSettings.Instance;


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
        private bool lastBuffAdded = false;
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

            //NS.Instance.ListForReset.Add("sda");
            //NS.Instance.SettingsDictionary.Add("IsGameStarted",false);
            



            //Debug.LogWarning($"bool_IsGameStarted = {NewSettings.Get<bool>("bool_IsGameStarted")}");

        }

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
            if ((bool)Settings["bool_IsGameStarted"])
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
                    case 0://switch to menu0
                        Menu0();
                        break;
                    //case 1://switch to menu1
                    //    Menu1();
                    //    break;
                    //case 2://switch to menu2
                    //    Menu2();
                    //    break;

                    //case 3: //add more for more menu
                    //        //Log.Out("opening Menu3");
                    //    Menu3();
                    //    break;
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
                    NewGUILayout.BeginScrollView("vector2_ScrollMenu0", () => {
                        NewGUILayout.BeginHorizontal(() =>
                        {
                            NewGUILayout.BeginVertical(() =>
                            {
                                NewGUILayout.Button("Add skillpoints", Cheat.skillpoints);
                                NewGUILayout.Button("Kill Self", Cheat.KillSelf);
                                NewGUILayout.Button("Level Up", Cheat.levelup);
                                NewGUILayout.Button("Get and log my ID", Cheat.Getplayer);

                                if (Settings.Count > 1)
                                {
                                    NewGUILayout.DictButton("Ignore", "bool_IgnoreByAi");
                                    NewGUILayout.DictButton("Creative", "bool_CreativeMode");
                                    NewGUILayout.ButtonToggle("Creative2", "bool_CreativeMode", () => { Cheat.CmDm(true); }, () => { Cheat.CmDm(false); });
                                    NewGUILayout.DictButton("Debug Mode", "bool_DebugMode");
                  
                                    //NewGUILayout.RButton("Ignored By AI", "_ignoreByAI");
                                    //NewGUILayout.Button("Creative/Debug Mode", RB, "CmDm");
                                    NewGUILayout.DictButton("Name Scramble2", "bool_nameScramble");
                                    //NewGUILayout.Button("sda", () =>
                                    //{
                                    //    NewSettings.Instance.Speed = !NewSettings.Instance.Speed;
                                    //});
                                    //NewGUILayout.BoolButton("TEstSpeed NameOF", nameof(NewSettings.Instance.Speed));

                                    //GuiLayoutExtended.NewGUILayout.ButtonBoolToggle("TestToggleBool", Settings.Instance.Speed);


                                    //THIS WORKS
                                    //NewSettings.Instance.CreativeMode = GuiLayoutExtended.NewGUILayout.ButtonBoolToggle("Toggle CreativeMode", NewSettings.Instance.CreativeMode);

                                }


                            });
                            NewGUILayout.BeginVertical(() =>
                            {
                                if (NewGUILayout.Button($"Teleport To Marker "))
                                {

                                    O.ELP.TeleportToPosition(new Vector3(O.ELP.markerPosition.ToVector3().x, O.ELP.markerPosition.ToVector3().y + 2, O.ELP.markerPosition.ToVector3().z));
                                }

                                NewGUILayout.DictButton("Instant Quest", "bool_instantQuest");
                                NewGUILayout.DictButton("Instant Craft", "bool_instantCraft");
                                NewGUILayout.DictButton("Instant Scrap", "bool_instantScrap");
                                NewGUILayout.DictButton("Instant Smelt", "bool_instantSmelt");
                                NewGUILayout.DictButton("Loop LAST Quest Rewards", "bool_LoopQuestRewards");
                                NewGUILayout.DictButton("Trader Open 24/7", "bool_traderOpen");

                                //NewGUILayout.Button("Instant Quest", RB, "_QuestComplete");
                                //NewGUILayout.Button("Instant Craft", RB, "_instantCraft");
                                //NewGUILayout.Button("Instant scrap", RB, "_instantScrap");
                                //NewGUILayout.Button("Instant smelt", RB, "_instantSmelt");
                                //NewGUILayout.Button("Loop LAST Quest Rewards", RB, "_LOQuestRewards");
                                //NewGUILayout.RButton("Trader Open 24/7", "_EtraderOpen");


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
                    });
                });
            });
            GUI.DragWindow();

        }


        
        private void Menu1() //Toggles and modifiers
        {
            NewGUILayout.BeginHorizontal(GUI.skin.box, () => {
                NewGUILayout.BeginVertical(() => {
 
                    NewGUILayout.Toggle("DrawFov", "bool_FOV");
                    NewGUILayout.Toggle("Cross hair", "bool_CrossHair");
                    NewGUILayout.Toggle("Player Box", "bool_playerBox");
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
                    //NewGUILayout.HorizontalScrollbarWithLabel("Attacks/minute", "float_apm", 500f);
                    //NewGUILayout.HorizontalScrollbarWithLabel("Jump Strength", "float_jumpStrength", 5f);
                    //NewGUILayout.HorizontalScrollbarWithLabel("Harvest Dubbler", "float_harvestDubbler", 20f);
                    //NewGUILayout.HorizontalScrollbarWithLabel("Sprint Speed", "float_sprintSpeed", 25f);
                    //NewGUILayout.HorizontalScrollbarWithLabel("Kill DMG xM", "float_KillDamageMultiplier", 1000f);
                    NewGUILayout.HorizontalScrollbarWithLabel("Block DMG", ref NewSettings.FloatBlockDamageMultiplier, 10000f);
                    GUILayout.HorizontalScrollbar(0f, 1f, 0f, 500f);
                });
                NewGUILayout.BeginVertical(GUI.skin.box ,() =>
                {
                    NewGUILayout.BeginScrollView("vector2_scrollBuffMenu", () => {
                        NewGUILayout.BeginHorizontal(() =>
                        {
                            NewGUILayout.BeginVertical(() =>
                            {
                                NewGUILayout.DictButton("Block DMG", "bool_BlockDamage");
                                NewGUILayout.DictButton("Kill DMG", "bool_KillDamage");
                                NewGUILayout.DictButton("Harvest Dubbler", "bool_Harvest");
                            });
                            NewGUILayout.BeginVertical(() =>
                            {
                                NewGUILayout.DictButton("Run Speed", "bool_Run");
                                NewGUILayout.DictButton("Kill DMG", "bool_Jump");
                                NewGUILayout.DictButton("Attack/min", "bool_AttackPerMin");
                            });
                        },GUILayout.MaxHeight(170));

                    }, GUILayout.MinHeight(100));
                });
            });
            GUI.DragWindow();
        }
        private void Menu2() //Buffs And stuff
        {
            NewGUILayout.BeginHorizontal(defBoxStyle, () =>
            {
                NewGUILayout.BeginScrollView("vector2_scrollMenu2", () => {

                    NewGUILayout.BeginHorizontal(() =>
                    {
                        NewGUILayout.BeginVertical("Buffs", defBoxStyle, () =>
                        {
                            GUILayout.Space(20f);
                            NewGUILayout.BeginVertical(() =>
                            {
                                NewGUILayout.BeginVertical(() =>
                                {
                                    NewGUILayout.BeginScrollView("scrollBuffMenu", () => {
                                        NewGUILayout.BeginHorizontal(() =>
                                        {
                                            NewGUILayout.BeginVertical(() =>
                                            {
 
                                                NewGUILayout.Button("Remove All Active Buffs", Cheat.RemoveAllBuff);//Cheat.RemoveBadBuff()
                                                NewGUILayout.Button("Clear CheatBuff", Cheat.ClearCheatBuff);//Cheat.custombuff();

                                            });
                                            NewGUILayout.BeginVertical(() =>
                                            {
                                                NewGUILayout.Button("Add CheatBuff to P", Cheat.AddCheatBuff);//Cheat.custombuff();
                                                NewGUILayout.Button("Add Effect Group", Cheat.AddEffectGroup);//Cheat.custombuff();
                                              
                                            });

                                        });
                                    }, GUILayout.MinHeight(80));
                                });

                                NewGUILayout.BeginHorizontal(customBoxStyleGreen); //***************************************************************

                                NewGUILayout.DictFoldMenuHorizontal("Custom Buffs", "bool_foldCBuff", () =>
                                {
                                    NewGUILayout.BeginScrollView("vector2_scrollCustomBuff", () => {
                                        //Cheat.GetListCBuffs(O.ELP, O._listCbuffs);
                                    }, GUILayout.MinHeight(100));
                                }, 300f);

                                NewGUILayout.BeginHorizontal(customBoxStyleGreen); //***************************************************************

                                NewGUILayout.BeginVertical(GUI.skin.box, () =>
                                {
                                    NewGUILayout.BeginVertical(() =>
                                    {
                                        NewGUILayout.DictFoldMenuHorizontal("Passive Effects", "bool_foldPassive", () =>
                                        {
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
                                            NewGUILayout.BeginScrollView("vector2_scrollPassive", () => {

                                                //Cheat.GetListPassiveEffects(passiveSearch);
                                            }, GUILayout.MinHeight(100));
                                        }, 300f);
                                    });
                                });

                                NewGUILayout.BeginHorizontal(customBoxStyleGreen); //***************************************************************


                                NewGUILayout.DictFoldMenuHorizontal("Scroll All Buffs", "bool_foldAllBuffs", () =>
                                {
                                    NewGUILayout.BeginHorizontal(() => {
                                        GUILayout.Label("Search for buff");
                                        buffSearch = GUILayout.TextField(buffSearch, GUILayout.MaxWidth(225f), GUILayout.Height(20f));
                                    });
                                    NewGUILayout.BeginScrollView("vector2_scrollBuff", () => {
                                        //Cheat.GetList(lastBuffAdded, O.ELP, O._listBuffClass, buffSearch);
                                    }, GUILayout.MinHeight(100));
                                }, 300f);


                                NewGUILayout.BeginHorizontal(customBoxStyleGreen); //***************************************************************


                                NewGUILayout.DictFoldMenuHorizontal("Perks", "bool_foldPerks", () =>
                                {
                                    NewGUILayout.BeginHorizontal(() => {
                                        GUILayout.Label("Search for Perk");
                                        PGVSearch = GUILayout.TextField(PGVSearch, GUILayout.MaxWidth(225f), GUILayout.Height(20f));
                                    });

                                    NewGUILayout.BeginScrollView("vector2_scrollPerks", () => {
                                        //Cheat.ListPGV(PGVSearch);

                                    }, GUILayout.MinHeight(250f), GUILayout.MaxHeight(450f));
                                }, 300f, GUILayout.MinHeight(200f));

                                NewGUILayout.BeginHorizontal(customBoxStyleGreen); //***************************************************************
                            });
                        });
                    });
                });
            });
            GUI.DragWindow();

        } //Buffs an  stuff
        private void Menu3()//some crap
        {
            NewGUILayout.BeginVertical(GUI.skin.box, () =>
            {

                NewGUILayout.BeginScrollView("vector2_scrollMenu3", () => {

                    NewGUILayout.BeginHorizontal(customBoxStyleGreen); //***************************************************************

                    NewGUILayout.DictFoldMenuHorizontal("TestMenu1", "bool_TestMenu1", () =>
                    {
                        NewGUILayout.DictButton("Test1", "bool_Test1");
                        NewGUILayout.DictButton("Test2", "bool_Test2");
                        NewGUILayout.DictButton("Test2", "bool_Test2");
                        NewGUILayout.DictButton("Test2", "bool_Test2");

                    }, 300f, GUILayout.MinHeight(200f));

                    NewGUILayout.BeginHorizontal(customBoxStyleGreen); //***************************************************************

                    NewGUILayout.DictFoldMenuHorizontal("TestMenu3", "bool_TestMenu3", () =>
                    {
                        NewGUILayout.BeginVertical(GUI.skin.box, () =>
                        {
                            NewGUILayout.BeginScrollView("vector2_testScroll3", () => {
                                NewGUILayout.DictButton("Test5", "bool_Test5");
                                NewGUILayout.DictButton("Test6", "bool_Test6");
                                NewGUILayout.DictButton("Test8", "bool_Test7");
                           

                            });
                        });
                    }, 300f, GUILayout.MinHeight(200f));

                    NewGUILayout.BeginHorizontal(customBoxStyleGreen); //***************************************************************

                    NewGUILayout.DictFoldMenuHorizontal("Scroll Zombies", "bool_foldZombie", () =>
                    {

                        NewGUILayout.BeginScrollView("vector2_scrollZombie", () => {
                            NewGUILayout.BeginVertical(GUI.skin.box, () =>
                            {
                                //Cheat.ListZombie1();
                            });
                        }, GUILayout.MinHeight(250f), GUILayout.MaxHeight(350f));

                    }, 300f, GUILayout.MinHeight(200f));

                    NewGUILayout.BeginHorizontal(customBoxStyleGreen); //***************************************************************

                    NewGUILayout.DictFoldMenuHorizontal("Scroll Players", "bool_foldPlayer", () =>
                    {

                        NewGUILayout.BeginScrollView("vector2_scrollPlayer", () => {
                            NewGUILayout.BeginVertical(GUI.skin.box, () =>
                            {
                                //Cheat.ListPlayer1();
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