
using SevenDTDMono.Features;
using SevenDTDMono.GuiLayoutExtended;
using SevenDTDMono.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using static PassiveEffect;



namespace SevenDTDMono
{
    public class NewMenu : MonoBehaviour
    {
        private static NewSettings SettingsInstance => NewSettings.Instance;
        public static Dictionary<string,object> Settings => NewSettings.Instance.SettingsDictionary;
        
        private static Dictionary<string, bool> _boolDict = SettingsInstance.GetChildDictionary<bool>(nameof(Dictionaries.BOOL_DICTIONARY));

        private static EntityPlayerLocal Player => NewSettings.EntityLocalPlayer;
        private static List<string> ResetList => NewSettings.ResetVariableList;



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



        void OnDestroy() 
        {

        }
        void Awake()
        {
            Debug.LogWarning($"Awake: {nameof(NewMenu)}");
        }
        // Start is called before the first frame update
        void Start()
        {
            Debug.LogWarning($"Start: {nameof(NewMenu)}");
            windowID = new System.Random(Environment.TickCount).Next(1000, 65535);
            windowRect = new Rect(50f, 400f, 400f, 50f);
            GUI.color = Color.white;


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

        private void test()
        {
            _boolDict["AddTestBOOL"] = true;



            if (!_boolDict[nameof(SettingsBools.ZOMBIE_BOX)])
            {
                Debug.Log($"debug log test for {SettingsBools.ZOMBIE_BOX} and it is {_boolDict[nameof(SettingsBools.ZOMBIE_BOX)]} ");

            }

        }


        private void OnGUI()
        {

            //NewGUILayout.Button("test add dict", test);



            GUIStyle customStyle = new GUIStyle(GUI.skin.window);
            if (NewSettings.GameManager.gameStateManager.bGameStarted)
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
            //helper for search string 
            return ((PassiveEffect.ValueModifierTypes)index).ToString();
        }
        private void Window(int windowID)
        {
            //if (!NewSettings.GameManager.gameStateManager.bGameStarted)
            //{
                
                
            //    NewGUILayout.BeginHorizontal(() =>
            //    {
            //        //GUILayout.Space(10f);
            //        GUILayout.Label("Start a game to load the Menu", centeredLabelStyle);
            //        NewGUILayout.Button("test add dict", test);
            //        //GUILayout.Space(10f);
            //    });
           
               
                
            //    GUI.DragWindow();
            //}
            //else
            {
                windowRect.height = 500;

                
                _group = NewGUILayout.Toolbar4(_group, CheatsString, GUI.skin.box); //creating the group for switch comand    
                switch (_group)
                {
                    case 0://switch to menu0
                        Tab1();
                        break;
                    case 1://switch to menu1
                        Tab2();
                        break;
                    case 2://switch to menu2
                        Tab3();
                        break;

                    case 3: //add more for more menu
                            //Log.Out("opening Menu3");
                        Tab4();
                        break;
                }
                GUILayout.Space(10f);

            }
        }
        //CGUILayout.BeginHorizontal(() => {});
        //CGUILayout.BeginVertical(() => {});


        private void Tab1() //player
        {
            NewGUILayout.BeginHorizontal(() =>
            {
                NewGUILayout.BeginVertical( () =>
                {
                    NewGUILayout.BeginScrollView("vector2_ScrollMenu0", () => {
                        NewGUILayout.BeginHorizontal(() =>
                        {
                            NewGUILayout.BeginVertical(() =>
                            {
                                NewGUILayout.ButtonToggleDictionary("Name Scramble2", "bool_nameScramble");





                                //********************* FINISHED REMAKE***************************
                                NewGUILayout.Button("Level Up", Cheat.LevelUp);
                                NewGUILayout.Button("Add Skill Points", Cheat.SkillPoints);
                         
                                NewGUILayout.Button("Kill Self", Cheat.KillSelf);
                                NewGUILayout.ButtonToggleDictionary("Ignore By AI", nameof(Player.isIgnoredByAI), () =>
                                    {
                                        //this is only working with zombies for some reason.. not with all entites,
                                        if (Player)
                                        {
                                            bool bool1 = _boolDict[nameof(Player.isIgnoredByAI)];
                                            Player.SetIgnoredByAI(bool1);
                                            NewSettings.AddReset(nameof(Player.isIgnoredByAI));
                                        }


                                        //public void updateDebugKeys()
                                        //bool flag4 = !GamePrefs.GetBool(EnumGamePrefs.DebugStopEnemiesMoving);
                                        //GamePrefs.Set(EnumGamePrefs.DebugStopEnemiesMoving, flag4);
                                        //if (shiftKeyPressed)
                                        //{
                                        //    this.entityPlayerLocal.SetIgnoredByAI(!this.entityPlayerLocal.IsIgnoredByAI());
                                        //    return;
                                        //}
                                        //bool flag4 = !GamePrefs.GetBool(EnumGamePrefs.DebugStopEnemiesMoving);
                                        //GamePrefs.Set(EnumGamePrefs.DebugStopEnemiesMoving, flag4);
                                        //if (flag4)
                                        //{
                                        //    this.entityPlayerLocal.Buffs.AddBuff("buffShowAIDisabled", -1, true, false, -1f);
                                        //    return;
                                        //}
                                        //this.entityPlayerLocal.Buffs.RemoveBuff("buffShowAIDisabled", true);



                                    });
                                NewGUILayout.ButtonToggleDictionary("DebugMenuShowTasks", nameof(EnumGamePrefs.DebugMenuShowTasks), () =>       
                                {
                                    //this is only working with zombies for some reason.. not with all entites,
                                    if (Player)
                                    {
                                        //bool bool1 = (bool)Settings[nameof(player.isIgnoredByAI)];
                                        //player.SetIgnoredByAI(bool1);


                                        EntityAlive.SetupAllDebugNameHUDs(SettingsInstance.GetBoolValue(nameof(EnumGameStats.IsCreativeMenuEnabled)));

                                        NewSettings.AddReset(nameof(EnumGamePrefs.DebugMenuShowTasks));
                                    }


                                    //public void updateDebugKeys()
                                    //bool flag4 = !GamePrefs.GetBool(EnumGamePrefs.DebugStopEnemiesMoving);
                                    //GamePrefs.Set(EnumGamePrefs.DebugStopEnemiesMoving, flag4);
                                    //if (shiftKeyPressed)
                                    //{
                                    //    this.entityPlayerLocal.SetIgnoredByAI(!this.entityPlayerLocal.IsIgnoredByAI());
                                    //    return;
                                    //}
                                    //bool flag4 = !GamePrefs.GetBool(EnumGamePrefs.DebugStopEnemiesMoving);
                                    //GamePrefs.Set(EnumGamePrefs.DebugStopEnemiesMoving, flag4);
                                    //if (flag4)
                                    //{
                                    //    this.entityPlayerLocal.Buffs.AddBuff("buffShowAIDisabled", -1, true, false, -1f);
                                    //    return;
                                    //}
                                    //this.entityPlayerLocal.Buffs.RemoveBuff("buffShowAIDisabled", true);



                                });
                                NewGUILayout.ButtonToggleDictionary("Debug Menu Enable", nameof(EnumGamePrefs.DebugMenuEnabled), () =>
                                    {
                                        if (Player)
                                        {
                                            bool bool1 = _boolDict[nameof(EnumGamePrefs.DebugMenuEnabled)];
                                            GamePrefs.Set(EnumGamePrefs.DebugMenuEnabled, bool1);
                                            GamePrefs.Set(EnumGamePrefs.DebugPanelsEnabled, bool1);
                                            GamePrefs.Set(EnumGamePrefs.DebugStopEnemiesMoving, bool1);
                                            NewSettings.AddReset(nameof(EnumGamePrefs.DebugMenuEnabled));
                                        }
                                    });
                                NewGUILayout.ButtonToggleDictionary("Creative", nameof(EnumGameStats.IsCreativeMenuEnabled), () =>
                                {
                                    GameStats.Set(EnumGameStats.IsCreativeMenuEnabled,SettingsInstance.GetBoolValue(nameof(EnumGameStats.IsCreativeMenuEnabled)));
                                });
                                NewGUILayout.ButtonToggleDictionary("Show All Players On Map", nameof(EnumGameStats.ShowAllPlayersOnMap), () =>
                                {
                                    if (Player)
                                    {
                                        bool bool1 = _boolDict[nameof(EnumGameStats.ShowAllPlayersOnMap)];
                                        GameStats.Set(EnumGameStats.ShowAllPlayersOnMap, bool1);
                                        NewSettings.AddReset(nameof(EnumGameStats.ShowAllPlayersOnMap));
                                    }
                                });



                            });
                            NewGUILayout.BeginVertical(() =>
                            {
                                if (NewGUILayout.Button($"Teleport To Marker "))
                                {
                                    if (Player)
                                    {
                                        Player.TeleportToPosition(new Vector3(Player.markerPosition.ToVector3().x, Player.markerPosition.ToVector3().y + 2, Player.markerPosition.ToVector3().z));
                                    }
                                }


                                //NewGUILayout.ButtonToggleDictionary("Instant Craft", nameof(PassiveEffects.CraftingTime));
                                //NewGUILayout.ButtonToggleDictionary("Instant Scrap", nameof(PassiveEffects.ScrappingTime));
                                //NewGUILayout.ButtonToggleDictionary("Instant Smelt", nameof(PassiveEffects.CraftingSmeltTime));
                                NewGUILayout.ButtonToggleDictionary("Instant Quest", nameof(Quest.QuestState.InProgress)); 
                                NewGUILayout.ButtonToggleDictionary("Loop LAST Quest Rewards", nameof(Quest.QuestState.Completed));
                                NewGUILayout.ButtonToggleDictionary("Open Trader", nameof(EntityTrader) , Cheat.OpenTrader);
                                NewGUILayout.ButtonToggleDictionary("Instant Craft", nameof(PassiveEffects.CraftingTime), () =>
                                {
                                    Cheat.AddPassiveEffectToPlayer(PassiveEffects.CraftingTime, 0f, ValueModifierTypes.base_set);
                                });
                                NewGUILayout.ButtonToggleDictionary("Instant Smelt", nameof(PassiveEffects.CraftingSmeltTime), () =>
                                {
                                    Cheat.AddPassiveEffectToPlayer(PassiveEffects.CraftingSmeltTime, 0f, ValueModifierTypes.base_set);
                                });
                                NewGUILayout.ButtonToggleDictionary("Instant Scrap", nameof(PassiveEffects.ScrappingTime), () =>
                                {
                                    Cheat.AddPassiveEffectToPlayer(PassiveEffects.ScrappingTime, 0f, ValueModifierTypes.base_set);
                                });
                                NewGUILayout.ButtonToggleDictionary("Infinity durability", nameof(PassiveEffects.DegradationPerUse), () =>
                                {
                                    Cheat.AddPassiveEffectToPlayer(PassiveEffects.DegradationPerUse, 0f, ValueModifierTypes.base_set);
                                });
                                //NewGUILayout.ButtonToggleDictionary("BlockDamage", nameof(PassiveEffects.BlockDamage), () =>
                                //{
                                //    Cheat.AddPassiveEffectToPlayer(PassiveEffects.BlockDamage, 999999f, ValueModifierTypes.base_set);
                                //});

                                NewGUILayout.BeginHorizontal(() => {
                                    _itemcount = GUILayout.TextField(_itemcount, 10, GUILayout.MaxWidth(50));

                                    if (NewGUILayout.Button(" Set holding item"))
                                    {
                                        try
                                        {
                                            int intvalue = int.Parse(_itemcount);
                                            if (Player.inventory.holdingItemStack.count >= 1)
                                            {
                                                Player.inventory.holdingItemStack.count = intvalue;
               
                                                Debug.LogWarning($"{Player.inventory.holdingItem.Name} set to {intvalue}");
                                            }
                                            else
                                            {
                                                Debug.LogWarning($"{Player.inventory.holdingItem.Name} Could not be set to {intvalue}");
                                            }
                                        }
                                        catch (Exception e)
                                        {
                                            Debug.LogError(e);
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


        
        private void Tab2() //Toggles and modifiers
        {
            NewGUILayout.BeginVertical(GUI.skin.box, () => {
                GUILayout.Label("Render");
                NewGUILayout.BeginHorizontal(() =>
                {
                    NewGUILayout.BeginVertical(() => {
                        NewGUILayout.ButtonToggleDictionary("Zombie Box", nameof(SettingsBools.ZOMBIE_BOX));
                        NewGUILayout.ButtonToggleDictionary("Zombie Corner Box", nameof(SettingsBools.ZOMBIE_CORNER_BOX));
                        NewGUILayout.ButtonToggleDictionary("Zombie Health", nameof(SettingsBools.ZOMBIE_HEALTH));
                        NewGUILayout.ButtonToggleDictionary("Zombie Name", nameof(SettingsBools.ZOMBIE_NAME));
                        NewGUILayout.ButtonToggleDictionary("Chams", nameof(SettingsBools.CHAMS));
                    });
                    NewGUILayout.BeginVertical(() => {
                        NewGUILayout.ButtonToggleDictionary("Player Box", nameof(SettingsBools.PLAYER_BOX));
                        NewGUILayout.ButtonToggleDictionary("Player Corner Box", nameof(SettingsBools.PLAYER_CORNER_BOX));
                        NewGUILayout.ButtonToggleDictionary("Player Health", nameof(SettingsBools.PLAYER_HEALTH));
                        NewGUILayout.ButtonToggleDictionary("Player Name", nameof(SettingsBools.PLAYER_NAME));
                        NewGUILayout.ButtonToggleDictionary("Field Of view", nameof(SettingsBools.FOV_CIRCLE));
                        NewGUILayout.ButtonToggleDictionary("Cross", nameof(SettingsBools.CROSS_HAIR));
                        NewGUILayout.ButtonToggleDictionary("NoWeaponBob", nameof(SettingsBools.NO_WEAPON_BOB));
                    });
                });
            });
            NewGUILayout.BeginVertical(GUI.skin.box, () =>
            {
                GUIStyle LabelStyle = new GUIStyle(GUI.skin.label)
                {
                    alignment = TextAnchor.MiddleCenter,
                    fontSize = 15,
                    padding = new RectOffset(0, 0, 0, 0),
                    margin = new RectOffset(0, 0, 0, 10),
                };
                NewGUILayout.BeginVertical(() =>
                {
                    GUILayout.Label("Modifiers",LabelStyle);

                    NewGUILayout.HorizontalScrollbarWithLabelAndButton("Block DMG", nameof(PassiveEffects.BlockDamage), ref NewSettings.FloatBlockDamageMultiplier, 1000f, () =>
                    {
                        Cheat.AddPassiveEffectToPlayer(PassiveEffects.BlockDamage, NewSettings.FloatBlockDamageMultiplier, ValueModifierTypes.base_set);
                    });
                    NewGUILayout.HorizontalScrollbarWithLabelAndButton("Kill DMG", nameof(PassiveEffects.EntityDamage), ref NewSettings.FloatKillDamageMultiplier, 1000f, () =>
                    {

                        Cheat.AddPassiveEffectToPlayer(PassiveEffects.EntityDamage, NewSettings.FloatKillDamageMultiplier, ValueModifierTypes.base_set);
                    });
                    NewGUILayout.HorizontalScrollbarWithLabelAndButton("Jump Strength", nameof(PassiveEffects.JumpStrength), ref NewSettings.FloatJumpStrengthMultiplier, 5f, () =>
                    {

                        Cheat.AddPassiveEffectToPlayer(PassiveEffects.JumpStrength, NewSettings.FloatJumpStrengthMultiplier, ValueModifierTypes.base_set);
                    });
                    NewGUILayout.HorizontalScrollbarWithLabelAndButton("Run Speed", nameof(PassiveEffects.RunSpeed), ref NewSettings.FloatRunSpeedMultiplier, 25f, () =>
                    {

                        Cheat.AddPassiveEffectToPlayer(PassiveEffects.RunSpeed, NewSettings.FloatRunSpeedMultiplier, ValueModifierTypes.base_set);
                    });
                    NewGUILayout.HorizontalScrollbarWithLabelAndButton("Attacks/minute", nameof(PassiveEffects.AttacksPerMinute), ref NewSettings.FloatAttacksPerMinuteMultiplier, 500f, () =>
                    {

                        Cheat.AddPassiveEffectToPlayer(PassiveEffects.AttacksPerMinute, NewSettings.FloatAttacksPerMinuteMultiplier, ValueModifierTypes.base_set);
                    });
                    NewGUILayout.HorizontalScrollbarWithLabelAndButton("Harvest multiplier", nameof(PassiveEffects.HarvestCount), ref NewSettings.FloatHarvestCountMultiplier, 20f, () =>
                    {

                        Cheat.AddPassiveEffectToPlayer(PassiveEffects.HarvestCount, NewSettings.FloatHarvestCountMultiplier, ValueModifierTypes.base_set);
                    });





                });

            });
            GUI.DragWindow();
        }
        private void Tab3() //Buffs And stuff
        {
            NewGUILayout.BeginScrollView("vector2_ScrollMenuBuffs", () => {
                GUILayout.Label("Buffs");
                //NewGUILayout.BeginVertical("Buffs", defBoxStyle, () =>
                //{
                    GUILayout.Space(20f);
                    NewGUILayout.BeginHorizontal(() =>
                    {
                        NewGUILayout.BeginVertical(() =>
                        {

                            NewGUILayout.Button("Remove All Active Buffs", Cheat.RemoveAllBuff);//Cheat.RemoveBadBuff() //OK
                            NewGUILayout.Button("Clear CheatBuff", Cheat.ClearCheatBuff);//Cheat.custombuff();

                        });
                        NewGUILayout.BeginVertical(() =>
                        {
                            NewGUILayout.Button("Add CheatBuff to P", Cheat.AddCheatBuff);//Cheat.custombuff();
                            NewGUILayout.Button("Add Effect Group", Cheat.AddEffectGroup);//Cheat.custombuff();

                        });

                    });
                    NewGUILayout.DictFoldMenuHorizontal("Custom Buffs", "bool_foldCBuff", () =>
                    {
                        NewGUILayout.BeginScrollView("vector2_scrollCustomBuff", () => {
                            Cheat.GetListCBuffs(Player, NewSettings.ListCheatBuffs);
                        }, GUILayout.MinHeight(100),GUILayout.ExpandHeight(true));
                    });
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

                            Cheat.GetListPassiveEffects(passiveSearch);
                        }, GUILayout.MinHeight(100), GUILayout.ExpandHeight(true));
                    });
                    NewGUILayout.DictFoldMenuHorizontal("Scroll All Buffs", "bool_foldAllBuffs", () =>
                    {
                        NewGUILayout.BeginHorizontal(() => {
                            GUILayout.Label("Search for buff");
                            buffSearch = GUILayout.TextField(buffSearch, GUILayout.MaxWidth(225f), GUILayout.Height(20f));
                        });
                        NewGUILayout.BeginScrollView("vector2_scrollBuff", () => {

                            Cheat.GetList(lastBuffAdded, Player, NewSettings.ListBuffClasses, buffSearch);
                        }, GUILayout.MinHeight(100), GUILayout.ExpandHeight(true));
                    });
                    NewGUILayout.DictFoldMenuHorizontal("Perks", "bool_foldPerks", () =>
                    {
                        NewGUILayout.BeginHorizontal(() => {
                            GUILayout.Label("Search for Perk");
                            PGVSearch = GUILayout.TextField(PGVSearch, GUILayout.MaxWidth(225f), GUILayout.Height(20f));
                        });

                        NewGUILayout.BeginScrollView("vector2_scrollPerks", () => {
                            Cheat.ListProgressionValues(PGVSearch);

                        }, GUILayout.MinHeight(250f), GUILayout.MaxHeight(450f), GUILayout.ExpandHeight(true));
                    });

                //});



            }, GUILayout.ExpandHeight(true), GUILayout.ExpandWidth(true));
            GUI.DragWindow();

        } //Buffs an  stuff
 
        private void Tab4()//some crap
        {

            NewGUILayout.BeginScrollView("vector2_scrollMenu4", () =>
            {
                NewGUILayout.DictFoldMenuHorizontal("Some Logging Functions", "bool_TestFold1", () =>
                {
                    if (NewGUILayout.Button("log started"))
                    {
                        Debug.LogWarning($"{NewSettings.GameManager.gameStateManager.bGameStarted}");
                    }

                    NewGUILayout.Button("Get and log my ID", Cheat.GetPlayerId);

                    if (NewGUILayout.Button($"Log PGVC To file"))
                    {
                        //Cheat.LogProgressionClassToFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "load", "_listProgressionClass.txt"));
                    }
                    if (NewGUILayout.Button("´Log Buffs To file"))
                    {
                        Extras.LogAvailableBuffNames(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "load", "BuffsList.txt"));
                    }

                });

                NewGUILayout.DictFoldMenuHorizontal("TestMenu3", "bool_TestFold2", () =>
                {
                    NewGUILayout.BeginVertical(() =>
                    {
                        NewGUILayout.ButtonToggleDictionary("Test5", "bool_Test5");
                        NewGUILayout.ButtonToggleDictionary("Test6", "bool_Test6");
                        NewGUILayout.ButtonToggleDictionary("Test8", "bool_Test7");
                        NewGUILayout.ButtonToggleDictionary("Test5", "bool_Test5");
                        NewGUILayout.ButtonToggleDictionary("Test6", "bool_Test6");
                        NewGUILayout.ButtonToggleDictionary("Test8", "bool_Test7");
                        NewGUILayout.ButtonToggleDictionary("Test5", "bool_Test5");
                        NewGUILayout.ButtonToggleDictionary("Test6", "bool_Test6");
                        NewGUILayout.ButtonToggleDictionary("Test8", "bool_Test7");
                        NewGUILayout.ButtonToggleDictionary("Test5", "bool_Test5");
                        NewGUILayout.ButtonToggleDictionary("Test6", "bool_Test6");
                        NewGUILayout.ButtonToggleDictionary("Test8", "bool_Test7");

                    }, GUILayout.MaxHeight(200f));
                });

                NewGUILayout.DictFoldMenuHorizontal("Scroll Zombies", "bool_foldZombie", () =>
                {
                    NewGUILayout.BeginScrollView("vector2_scrollZombie", () => {
                        NewGUILayout.BeginVertical( () =>
                        {
                            Cheat.ListEntityZombie();
                        });
                    }, GUILayout.MinHeight(250f), GUILayout.MaxHeight(350f));
                });
                NewGUILayout.DictFoldMenuHorizontal("Scroll Players", "bool_foldPlayer", () =>
                {
                    NewGUILayout.BeginScrollView("vector2_scrollPlayer", () => {
                        NewGUILayout.BeginVertical(() =>
                        {
                            Cheat.ListEntityPlayer();
                        });
                    }, GUILayout.MinHeight(250f), GUILayout.MaxHeight(350f));
                });


            },GUILayout.ExpandHeight(true), GUILayout.ExpandWidth(true));

            GUI.DragWindow();
        }
       
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        /*
        private void Menu9()//some crap
        {
            NewGUILayout.BeginVertical(GUI.skin.box, () =>
            {

                NewGUILayout.BeginScrollView("vector2_scrollMenu3", () => {

                    NewGUILayout.BeginHorizontal(customBoxStyleGreen); //***************************************************************

                    //NewGUILayout.DictFoldMenuHorizontalTest("Some Logging Functions", "bool_TestMenu1", () =>
                    //{
                    //    if (NewGUILayout.Button("log started"))
                    //    {
                    //        Debug.LogWarning($"{NewSettings.GameManager.gameStateManager.bGameStarted}");
                    //    }

                    //    NewGUILayout.Button("Get and log my ID", Cheat.GetPlayerId);

                    //    if (NewGUILayout.Button($"Log PGVC To file"))
                    //    {
                    //        O.LogProgressionClassToFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "load", "_listProgressionClass.txt"));
                    //    }
                    //    if (NewGUILayout.Button("´Log Buffs To file"))
                    //    {
                    //        Extras.LogAvailableBuffNames(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "load", "BuffsList.txt"));
                    //    }


                    //}, 300f, GUILayout.MinHeight(200f));

                    NewGUILayout.BeginHorizontal(customBoxStyleGreen); //***************************************************************

                    NewGUILayout.DictFoldMenuHorizontal("TestMenu3", "bool_TestMenu3", () =>
                    {
                        NewGUILayout.BeginVertical(GUI.skin.box, () =>
                        {
                            NewGUILayout.BeginScrollView("vector2_testScroll3", () => {
                                NewGUILayout.ButtonToggleDictionary("Test5", "bool_Test5");
                                NewGUILayout.ButtonToggleDictionary("Test6", "bool_Test6");
                                NewGUILayout.ButtonToggleDictionary("Test8", "bool_Test7");


                            });
                        });
                    }, 300f, GUILayout.MinHeight(200f));

                    NewGUILayout.BeginHorizontal(customBoxStyleGreen); //***************************************************************

                    NewGUILayout.DictFoldMenuHorizontal("Scroll Zombies", "bool_foldZombie", () =>
                    {
                        NewGUILayout.BeginScrollView("vector2_scrollZombie", () => {
                            NewGUILayout.BeginVertical(GUI.skin.box, () =>
                            {
                                //Cheat.ListEntityZombie();
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
                                        //O.LogProgressionClassToFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "load", "_listProgressionClass.txt"));
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
        
        */
        
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