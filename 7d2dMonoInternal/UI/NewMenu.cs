
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
using SevenDTDMono.Features;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;


namespace SevenDTDMono
{
    public class NewMenu : MonoBehaviour
    {

        public static Dictionary<string,object> Settings => NewSettings.Instance.SettingsDictionary;
        private static NewSettings SettingsInstance => NewSettings.Instance;
        private static EntityPlayerLocal player => NewSettings.EntityLocalPlayer;
        private static List<string> ResetList => NewSettings.ResetVariableList;


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
            windowRect = new Rect(10f, 400f, 400f, 500f);
            GUI.color = Color.white;

           

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
                                NewGUILayout.ButtonToggleDictionary("Name Scramble2", "bool_nameScramble");





                                //********************* FINISHED REMAKE***************************
                                NewGUILayout.Button("Level Up", Cheat.LevelUp);
                                NewGUILayout.Button("Add Skill Points", Cheat.SkillPoints);
                         
                                NewGUILayout.Button("Kill Self", Cheat.KillSelf);
                                NewGUILayout.ButtonToggleDictionary("Ignore By AI", nameof(player.isIgnoredByAI), () =>
                                    {
                                        //this is only working with zombies for some reason.. not with all entites,
                                        if (player)
                                        {
                                            bool bool1 = (bool)Settings[nameof(player.isIgnoredByAI)];
                                            player.SetIgnoredByAI(bool1);
                                            NewSettings.AddReset(nameof(player.isIgnoredByAI));
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
                                    if (player)
                                    {
                                        //bool bool1 = (bool)Settings[nameof(player.isIgnoredByAI)];
                                        //player.SetIgnoredByAI(bool1);


                                        EntityAlive.SetupAllDebugNameHUDs((bool)Settings[nameof(EnumGamePrefs.DebugMenuShowTasks)]);

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
                                        if (player)
                                        {
                                            bool bool1 = (bool)Settings[nameof(EnumGamePrefs.DebugMenuEnabled)];
                                            GamePrefs.Set(EnumGamePrefs.DebugMenuEnabled, bool1);
                                            GamePrefs.Set(EnumGamePrefs.DebugPanelsEnabled, bool1);
                                            GamePrefs.Set(EnumGamePrefs.DebugStopEnemiesMoving, bool1);
                                            NewSettings.AddReset(nameof(EnumGamePrefs.DebugMenuEnabled));
                                        }
                                    });
                                NewGUILayout.ButtonToggleDictionary("Creative", nameof(EnumGameStats.IsCreativeMenuEnabled), () =>
                                {
                                    GameStats.Set(EnumGameStats.IsCreativeMenuEnabled, (bool)Settings[nameof(EnumGameStats.IsCreativeMenuEnabled)]);
                                });
                                NewGUILayout.ButtonToggleDictionary("Show All Players On Map", nameof(EnumGameStats.ShowAllPlayersOnMap), () =>
                                {
                                    if (player)
                                    {
                                        bool bool1 = (bool)Settings[nameof(EnumGameStats.ShowAllPlayersOnMap)];
                                        GameStats.Set(EnumGameStats.ShowAllPlayersOnMap, bool1);
                                        NewSettings.AddReset(nameof(EnumGameStats.ShowAllPlayersOnMap));
                                    }
                                });



                            });
                            NewGUILayout.BeginVertical(() =>
                            {
                                if (NewGUILayout.Button($"Teleport To Marker "))
                                {
                                    if (player)
                                    {
                                        player.TeleportToPosition(new Vector3(player.markerPosition.ToVector3().x, player.markerPosition.ToVector3().y + 2, player.markerPosition.ToVector3().z));
                                    }
                                }


                                //NewGUILayout.ButtonToggleDictionary("Instant Craft", nameof(PassiveEffects.CraftingTime));
                                //NewGUILayout.ButtonToggleDictionary("Instant Scrap", nameof(PassiveEffects.ScrappingTime));
                                //NewGUILayout.ButtonToggleDictionary("Instant Smelt", nameof(PassiveEffects.CraftingSmeltTime));
                                NewGUILayout.ButtonToggleDictionary("Instant Quest", nameof(Quest.QuestState.InProgress)); 
                                NewGUILayout.ButtonToggleDictionary("Loop LAST Quest Rewards", nameof(Quest.QuestState.Completed));
                                NewGUILayout.ButtonTriggerAction("Open Trader", nameof(EntityTrader) , Cheat.OpenTrader);
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
                                NewGUILayout.ButtonToggleDictionary("Infinity durability", nameof(PassiveEffects.CraftingSmeltTime), () =>
                                {
                                    Cheat.AddPassiveEffectToPlayer(PassiveEffects.DegradationPerUse, 0f, ValueModifierTypes.base_set);
                                });
                                NewGUILayout.ButtonToggleDictionary("BlockDamage", nameof(PassiveEffects.BlockDamage), () =>
                                {
                                    Cheat.AddPassiveEffectToPlayer(PassiveEffects.BlockDamage, 999999f, ValueModifierTypes.base_set);
                                });


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
                                            if (player.inventory.holdingItemStack.count >= 1)
                                            {
                                                player.inventory.holdingItemStack.count = intvalue;
               
                                                Debug.LogWarning($"{player.inventory.holdingItem.Name} set to {intvalue}");
                                            }
                                            else
                                            {
                                                Debug.LogWarning($"{player.inventory.holdingItem.Name} Could not be set to {intvalue}");
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

                    NewGUILayout.HorizontalScrollbarWithLabelAndButton1("Block DMG", nameof(PassiveEffects.BlockDamage), ref NewSettings.FloatBlockDamageMultiplier, 1000f, () =>
                    {

                            Cheat.AddPassiveEffectToPlayer(PassiveEffects.BlockDamage, NewSettings.FloatBlockDamageMultiplier, ValueModifierTypes.base_set);
                    });
                    NewGUILayout.HorizontalScrollbarWithLabelAndButton1("Kill DMG", nameof(PassiveEffects.EntityDamage), ref NewSettings.FloatKillDamageMultiplier, 1000f, () =>
                    {

                        Cheat.AddPassiveEffectToPlayer(PassiveEffects.EntityDamage, NewSettings.FloatKillDamageMultiplier, ValueModifierTypes.base_set);
                    });
                    NewGUILayout.HorizontalScrollbarWithLabelAndButton1("Jump Strength", nameof(PassiveEffects.JumpStrength), ref NewSettings.FloatJumpStrengthMultiplier, 5f, () =>
                    {

                        Cheat.AddPassiveEffectToPlayer(PassiveEffects.JumpStrength, NewSettings.FloatJumpStrengthMultiplier, ValueModifierTypes.base_set);
                    });
                    NewGUILayout.HorizontalScrollbarWithLabelAndButton1("Run Speed", nameof(PassiveEffects.RunSpeed), ref NewSettings.FloatRunSpeedMultiplier, 25f, () =>
                    {

                        Cheat.AddPassiveEffectToPlayer(PassiveEffects.RunSpeed, NewSettings.FloatRunSpeedMultiplier, ValueModifierTypes.base_set);
                    });
                    NewGUILayout.HorizontalScrollbarWithLabelAndButton1("Attacks/minute", nameof(PassiveEffects.AttacksPerMinute), ref NewSettings.FloatAttacksPerMinuteMultiplier, 500f, () =>
                    {

                        Cheat.AddPassiveEffectToPlayer(PassiveEffects.AttacksPerMinute, NewSettings.FloatAttacksPerMinuteMultiplier, ValueModifierTypes.base_set);
                    });
                    NewGUILayout.HorizontalScrollbarWithLabelAndButton1("Harvest multiplier", nameof(PassiveEffects.HarvestCount), ref NewSettings.FloatHarvestCountMultiplier, 20f, () =>
                    {

                        Cheat.AddPassiveEffectToPlayer(PassiveEffects.HarvestCount, NewSettings.FloatHarvestCountMultiplier, ValueModifierTypes.base_set);
                    });


                    //NewGUILayout.HorizontalScrollbarWithLabel("Block DMG", ref NewSettings.FloatBlockDamageMultiplier, 10000f);
                    //NewGUILayout.HorizontalScrollbarWithLabel("Kill DMG", ref NewSettings.FloatKillDamageMultiplier, 10000f);
                    //NewGUILayout.HorizontalScrollbarWithLabel("Jump Strength", ref NewSettings.FloatJumpStrengthMultiplier, 10000f);
                    //NewGUILayout.HorizontalScrollbarWithLabel("Block DMG", ref NewSettings.FloatBlockDamageMultiplier, 10000f);
                    //NewGUILayout.HorizontalScrollbarWithLabel("Block DMG", ref NewSettings.FloatBlockDamageMultiplier, 10000f);
                    //GUILayout.HorizontalScrollbar(0f, 1f, 0f, 500f);
                });


                //NewGUILayout.BeginVertical(GUI.skin.box ,() =>
                //{
                //    NewGUILayout.BeginScrollView("vector2_scrollBuffMenu", () => {
                //        NewGUILayout.BeginHorizontal(() =>
                //        {
                //            NewGUILayout.BeginVertical(() =>
                //            {
                //                //NewGUILayout.ButtonToggleDictionary("Enable Block DMG", nameof(PassiveEffects.BlockDamage));
                //                //NewGUILayout.ButtonToggleDictionary("Kill DMG", "bool_KillDamage");
                //                //NewGUILayout.ButtonToggleDictionary("Harvest Dubbler", "bool_Harvest");


                //                NewGUILayout.ButtonToggleDictionary("Enable Block DMG", nameof(PassiveEffects.BlockDamage), () =>
                //                {
                //                    Cheat.AddPassiveEffectToPlayer(PassiveEffects.BlockDamage, NewSettings.FloatBlockDamageMultiplier, ValueModifierTypes.perc_add);
                //                });
                //                NewGUILayout.ButtonToggleDictionary("Kill DMG", nameof(PassiveEffects.EntityDamage), () =>
                //                {
                //                    Cheat.AddPassiveEffectToPlayer(PassiveEffects.EntityDamage, NewSettings.FloatKillDamageMultiplier, ValueModifierTypes.perc_add);
                //                });
                //                NewGUILayout.ButtonToggleDictionary("Harvest Dubbler", nameof(PassiveEffects.HarvestCount), () =>
                //                {
                //                    Cheat.AddPassiveEffectToPlayer(PassiveEffects.HarvestCount, NewSettings.FloatHarvestCountMultiplier, ValueModifierTypes.perc_add);
                //                });
                              
                //            });
                //            NewGUILayout.BeginVertical(() =>
                //            {
                //                //CheatPassiveEffect(RB["_BL_Run"], PassiveEffects.RunSpeed, SETT._FL_run, ValueModifierTypes.base_set);

                //                //NewGUILayout.ButtonToggleDictionary("Run Speed", "bool_Run");
                //                //NewGUILayout.ButtonToggleDictionary("Kill DMG", "bool_Jump");
                //                //NewGUILayout.ButtonToggleDictionary("Attack/min", "bool_AttackPerMin");

                //                NewGUILayout.ButtonToggleDictionary("Run Speed", nameof(PassiveEffects.RunSpeed), () =>
                //                {
                //                    Cheat.AddPassiveEffectToPlayer(PassiveEffects.RunSpeed, NewSettings.FloatRunSpeedMultiplier, ValueModifierTypes.base_set);
                //                });
                //                NewGUILayout.ButtonToggleDictionary("Jump Strength", nameof(PassiveEffects.EntityDamage), () =>
                //                {
                //                    Cheat.AddPassiveEffectToPlayer(PassiveEffects.JumpStrength, NewSettings.FloatJumpStrengthMultiplier, ValueModifierTypes.base_set);
                //                });
                //                NewGUILayout.ButtonToggleDictionary("Attack/min", nameof(PassiveEffects.AttacksPerMinute), () =>
                //                {
                //                    Cheat.AddPassiveEffectToPlayer(PassiveEffects.JumpStrength, NewSettings.FloatJumpStrengthMultiplier, ValueModifierTypes.base_set);
                //                });
                //            });
                //        },GUILayout.MaxHeight(170));

                //    }, GUILayout.MinHeight(100));
                //});



            });
            GUI.DragWindow();
        }
        private void Tab3() //Buffs And stuff
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
 
                                                NewGUILayout.Button("Remove All Active Buffs", Cheat.RemoveAllBuff);//Cheat.RemoveBadBuff() //OK
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

                                                Cheat.GetListPassiveEffects(passiveSearch);
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
                        O.LogProgressionClassToFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "load", "_listProgressionClass.txt"));
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
       
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        private void Menu9()//some crap
        {
            NewGUILayout.BeginVertical(GUI.skin.box, () =>
            {

                NewGUILayout.BeginScrollView("vector2_scrollMenu3", () => {

                    NewGUILayout.BeginHorizontal(customBoxStyleGreen); //***************************************************************

                    NewGUILayout.DictFoldMenuHorizontalTest("Some Logging Functions", "bool_TestMenu1", () =>
                    {
                        if (NewGUILayout.Button("log started"))
                        {
                            Debug.LogWarning($"{NewSettings.GameManager.gameStateManager.bGameStarted}");
                        }

                        NewGUILayout.Button("Get and log my ID", Cheat.GetPlayerId);

                        if (NewGUILayout.Button($"Log PGVC To file"))
                        {
                            O.LogProgressionClassToFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "load", "_listProgressionClass.txt"));
                        }
                        if (NewGUILayout.Button("´Log Buffs To file"))
                        {
                            Extras.LogAvailableBuffNames(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "load", "BuffsList.txt"));
                        }


                    }, 300f, GUILayout.MinHeight(200f));

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
                                Cheat.ListEntityZombie();
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
                                        O.LogProgressionClassToFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "load", "_listProgressionClass.txt"));
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