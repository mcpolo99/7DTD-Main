using System;
using System.Runtime.InteropServices;
using UnityEngine;

using static Setting;
using Eutl = SevenDTDMono.ESPUtils;
using O = SevenDTDMono.Objects;
using System.Collections.Generic;
using System.Linq;
using static PassiveEffect;
using SevenDTDMono.GuiLayoutExtended;

//using SevenDTDMono.Objects;


namespace SevenDTDMono
{
    public class Cheat : MonoBehaviour
    {

     


        #region Unity
        /*
        ----------------------------------- MonoBehaviour Methods:
        Awake()
        Start()
        Update()
        FixedUpdate()
        LateUpdate()
        OnGUI() (not recommended for modern UI)
        OnDisable()
        OnEnable()
        OnDestroy()
        ----------------------------------- Collision and Trigger Events:
        OnCollisionEnter()
        OnCollisionStay()
        OnCollisionExit()
        OnTriggerEnter()
        OnTriggerStay()
        OnTriggerExit()
        Input Handling:
        OnMouseOver()
        OnMouseEnter()
        OnMouseExit()
        OnMouseDown()
        OnMouseUp()
        OnMouseDrag()
        OnMouseUpAsButton()
        OnBecameVisible()
        OnBecameInvisible()
        ----------------------------------- Physics:
        OnJointBreak()
        OnParticleCollision()
        ----------------------------------- Audio:
        OnAudioFilterRead()
        ----------------------------------- Animation:
        OnAnimatorMove()
        OnAnimatorIK()
        ----------------------------------- Application Lifecycle:
        OnApplicationFocus()
        OnApplicationPause()
        OnApplicationQuit()
        ----------------------------------- Network:
        OnServerInitialized()
        OnConnectedToServer()
        OnDisconnectedFromServer()
        OnFailedToConnect()
        OnPlayerConnected()
        OnPlayerDisconnected()
         */
        #endregion
        //--------------------------------------------------------------------------------------------------------

        #region Notes
        /*

  
        entityalive

        EnumDamageTypes
        EnumDamageSource
        public static readonly DamageSource suffocating = new DamageSource(EnumDamageSource.Internal, EnumDamageTypes.Suffocation);
        this.DamageEntity(DamageSource.suffocating, 1, false, 1f);
        JetpackActive




        */
        //--------------------------------------------------------------------------------------------------------

        #endregion
        //--------------------------------------------------------------------------------------------------------

        #region variables
        //public static Color _col = Color.blue;
        public  static string inputPassiveEffects = "none";
        public  static string inputFloat = "1";
        private static Dictionary<string, object> Settings => NewSettings.Instance.SettingsDictionary;

        #endregion
        //--------------------------------------------------------------------------------------------------------



        #region finished cheats
        //toggle
        public static void CmDm(bool boolean) //Creative and DEbug - Toggle
        {

            GameStats.Set(EnumGameStats.ShowSpawnWindow, boolean); // sets the GameStat to the value of CmDm
            GameStats.Set(EnumGameStats.IsCreativeMenuEnabled, boolean);
            GamePrefs.Set(EnumGamePrefs.DebugMenuEnabled, boolean);
        }
        public static Transform parentTransform;
        public static void editMode() //Creative and DEbug - Toggle
        {
            //GameStats.Set(EnumGameStats.ShowAllPlayersOnMap, SETT._isEditmode); // sets the GameStat to the value of CmDm
            //GameStats.Set(EnumGameStats.ShowSpawnWindow, SETT._isEditmode);
        }

        //Trigger
        public static void skillpoints()//add skillpoints - Trigger once
        {
            if (O.ELP)
            {
                Progression prog = O.ELP.Progression;
                prog.SkillPoints += 10;
                Log.Out($"Skillpoints added by 10is now {prog.SkillPoints}");
            }
        }
        public static void KillSelf()
        {

            O.ELP.DamageEntity(new DamageSource(EnumDamageSource.Internal, EnumDamageTypes.Suicide), 99999, false, 1f);
            SingletonMonoBehaviour<SdtdConsole>.Instance.Output("Gave 99999 damage to entity ");
        }
        public static void levelup()//up one level-Trigger once
        {
            if (O.ELP) //this cheat first checks if Local player is existing
            {
                //yeeh  ther esure will be



                Progression prog = O.ELP.Progression;
                prog.AddLevelExp(prog.ExpToNextLevel);
               

            }
        }
        public static void Getplayer()//add skillpoints - Trigger once
        {
            string num = O.ELP.name.ToString();
            Debug.LogError($"player ID: {num}");
        }
        public static void ClearCheatBuff()
        {
            Debug.LogWarning("Clearing CheatBuff");
            O._minEffectController.EffectGroups[0].PassiveEffects.Clear();
            O._minEffectController.PassivesIndex.Clear();
        }
        public static void RemoveAllBuff()
        {
            List<BuffValue> activeBuffs = O.ELP.Buffs.ActiveBuffs;
            foreach (BuffValue buff in activeBuffs)
            {
                O.ELP.Buffs.RemoveBuff(buff.BuffName);

                if (ButtonTState.ContainsKey(buff.BuffName))
                {
                    ButtonTState[buff.BuffName] = false;
                }
            }
        }
        public static void SOMECONSOLEPRINTOUT()  //Creative and debug mode -- Trigger
        {
            string _value = null;

            string _type = "SETT.cmDm";

            if (O.ELP)
            {
                _value = O.ELP.DebugNameInfo;

            }

            Debug.developerConsoleVisible = true;
            Console.WriteLine("THIS IS WRITE LINE" + _type);
            Debug.LogWarning($"Debug <color=_col>LogWarnig</color> for {_type}: " + _value);
            Debug.LogError($"Debug <color=_col>LogError</color> for {_type}: " + _value);
            Debug.Log($"Debug <color=_col>Log</color> for {_type}: " + _type);
            print($"This is a <color=_col_col>Print Message</color> for {_type}: " + _value);
            //Debug.developerConsoleVisible = true;
            GameSparks.Platforms.DefaultPlatform.print($"This is Actually the <color=green>INF/Print </color> console out for {_type} +cm+: " + _value);
            Log.Out("BYYYYYYYYYYY");

        }
        //voids
        public static void CheatPassiveEffect(bool toggle, PassiveEffects passive, float modifier, ValueModifierTypes valueModifierTypes)
        {
            NewSettings.CheckDictionaryForKey("bool_BlockDamage",false);


            if (O.ELP && (bool)Settings["bool_IsGameStarted"] == true)
            {
                if (toggle == true)
                {
                    AddPassive(passive, modifier, valueModifierTypes);

                }
                else if (toggle == false)
                {
                   RemovePassive(passive);
                }
            }
        }
        private static void RemovePassive(PassiveEffects passiveEffects)
        {
            var passiveEffectsList = O._minEffectController.EffectGroups[0].PassiveEffects;
            for (int i = passiveEffectsList.Count - 1; i >= 0; i--)
            {
                var effect = passiveEffectsList[i];
                if (
                    //effect.MatchAnyTags == newPassiveEffect.MatchAnyTags &&
                    //effect.Modifier == newPassiveEffect.Modifier &&
                    effect.Type == passiveEffects
                    /*&&*/
                    //effect.Values.SequenceEqual(newPassiveEffect.Values)
                    ) //if the passive effect is found remove it else nothing
                {
                    passiveEffectsList.RemoveAt(i);
                }
                O._minEffectController.PassivesIndex.Remove(passiveEffects);
            }
        }
        public static void AddPassive(PassiveEffects passiveEffects, float value, ValueModifierTypes valueModifierTypes)
        {



            /*valueModifierTypes
            Base_set - Sets to valu w eshoose
            Base_add - adds ontop of defalut base
            base_subtract - subtract from base valu
            perc_set _ Multiplier by default eg 100base * 5 float = 500
            perc_add --||--
            perc_subtract - Removes by multiplier
            count - No clue
            */



            if (O.ELP.Buffs.HasBuff("CheatBuff") == false)
            {
                try 
                {
                    Log.Out($"{O.CheatBuff.Name} was not active, try adding");
                    O.ELP.Buffs.AddBuff("CheatBuff");
                } catch
                { 
                }
            }

            List<PassiveEffect> pE1 = new List<PassiveEffect>();
            MinEffectGroup effectGroup = O._minEffectController.EffectGroups[0];

            PassiveEffect newPassiveEffect = new PassiveEffect
            {
                MatchAnyTags = true,
                Modifier = valueModifierTypes,
                Type = passiveEffects,
                Values = new float[] { value }, // Adjust the values accordingly
                                                // Set other properties if needed
            };//this is just the passive effects




            var passiveEffectsList = O._minEffectController.EffectGroups[0].PassiveEffects;
            for (int i = passiveEffectsList.Count - 1; i >= 0; i--)
            {
                var effect = passiveEffectsList[i];
                if (
                    //effect.MatchAnyTags == newPassiveEffect.MatchAnyTags &&
                    //effect.Modifier == newPassiveEffect.Modifier &&
                    effect.Type == newPassiveEffect.Type
                    /*&&*/
                    //effect.Values.SequenceEqual(newPassiveEffect.Values)
                    )
                {
                    passiveEffectsList.RemoveAt(i);
                }
            }

            O._minEffectController.PassivesIndex.Add(passiveEffects); // adds to MinEffectController.PassivesIndex MUST DO OTHERWISE NULL REFERENCE ERROR
            //effectGroup.PassiveEffects.Add(newPassiveEffect);           // MinEffectController.MinEffectGroup.PassivesIndex __ This location just adds buffs on top if added multiple times
            O._minEffectController.EffectGroups[0].PassiveEffects.Add(newPassiveEffect);           // MinEffectController.MinEffectGroup.PassivesIndex __ This location just adds buffs on top if added multiple times
        }
        //other 
        private static void DisplayToggleButton(PassiveEffects effect)
        {
            // Get or set the toggle state in the dictionary.
            if (!PVETState.ContainsKey(effect))
            {
                PVETState[effect] = false; // Set the initial state to false for new effects.
            }
            bool toggleState = PVETState[effect];

            // Display the toggle button and update the toggle state in the dictionary.
            bool buttonPressed = NewGUILayout.Button(effect.ToString(), GUILayout.MaxWidth(150));
            PVETState[effect] = buttonPressed;

            // If the button is pressed, set the input text field to the same string as the button text
            if (buttonPressed)
            {
                inputPassiveEffects = effect.ToString();
            }
        }
        #region Lists
        public static void ListZombie1() ///////////////////////////////
        {
            if (O._listEntityEnemy.Count > 1)
            {
                foreach (EntityEnemy enemy in O._listEntityEnemy)
                {
                    if (!enemy || enemy == O.ELP || !enemy.IsAlive())
                    {
                        continue;
                    }

                    //string xm1 = zombie.entityFlags.ToString();
                    //string ZM1= zombie.EntityName.ToString();
                    string zombieIID = enemy.entityId.ToString();
                    string zm = enemy.EntityName;
                    string zmIID = zm + zombieIID;
                    // Get or set the zombie's toggle state in the dictionary.
                    if (!MenuDropTState.ContainsKey(zmIID))
                    {
                        MenuDropTState[zmIID] = false; // Set the initial state to false for new zombies.
                    }

                    bool toggleState = MenuDropTState[zmIID];
                    NewGUILayout.DropDownForMethods(zmIID, () =>
                    {
                        NewGUILayout.BeginHorizontal(() =>
                        {
                            if (GUILayout.Button("Teleport"))
                            {
                                // Perform teleport action for the zombie.
                                O.ELP.TeleportToPosition(enemy.GetPosition());
                            }
                            if (GUILayout.Button("Kill"))
                            {

                                // Perform kill action for the zombie.
                                enemy.DamageEntity(new DamageSource(EnumDamageSource.Internal, EnumDamageTypes.Suicide), 99999, false, 1f);

                            }
                        });
                    }, ref toggleState);
                    MenuDropTState[zmIID] = toggleState;
                }
            }
            else
            {
                GUILayout.Label("No zombies found.");
            }
        }
        public static void ListPlayer1() ///////////////////////////////
        {
            if (O.PlayerList.Count > 1)
            {
                foreach (EntityPlayer player in O.PlayerList)
                {
                    if (!player || player == O.ELP || !player.IsAlive())
                    {
                        continue;
                    }


                    string PID = player.entityId.ToString();
                    string PName = player.EntityName;
                    string PIdentity = PName + PID;

                    string playerName = player.EntityName.ToString()+player.entityId.ToString();
                    string zm = player.EntityName.ToString();

                    // Get or set the zombie's toggle state in the dictionary.
                    if (!MenuDropTState.ContainsKey(PIdentity))
                    {
                        MenuDropTState[PIdentity] = false; // Set the initial state to false for new zombies.
                    }

                    bool toggleState = MenuDropTState[PIdentity];
                    NewGUILayout.DropDownForMethods(PIdentity, () =>
                    {
                        NewGUILayout.BeginHorizontal(() => 
                        {
                            //CGUILayout.Button("whatever", Color.yellow, Color.blue);
                            if (GUILayout.Button("Teleport"))
                            {
                                // Perform teleport action for the zombie.
                                O.ELP.TeleportToPosition(player.GetPosition());
                            }
                            if (GUILayout.Button("Kill"))
                            {
                                // Perform kill action for the zombie.
                                player.DamageEntity(new DamageSource(EnumDamageSource.Internal, EnumDamageTypes.Suicide), 99999, false, 1f);
                            }//zombie.DamageEntity(new DamageSource(EnumDamageSource.Internal, EnumDamageTypes.Suicide), 99999, false, 1f);
                        });
      
                    }, ref toggleState);

                    // Update the toggle state in the dictionary.
                    MenuDropTState[PIdentity] = toggleState;
                }
            }
            else
            {
                GUILayout.Label("No Players found.");
            }
        }
        



        public static void MaxSkill() ///////////////////////////////
        {
            if (O._listProgressionValue.Count > 0)
            {
                foreach (ProgressionValue PGV in O._listProgressionValue)
                {
                    int lvl = PGV.Level;
                    int max = PGV.ProgressionClass.MaxLevel;

                    if (lvl < max)
                    {
                        PGV.Level = max;
                    }
                }
            }
        }
        public static void ListPGV(string search) //////////Progression Value/////////////////////
        {
            if (O._listProgressionValue.Count > 0) 
            {

                var groupedValues = O._listProgressionValue.GroupBy(pgv => pgv.ProgressionClass.Type);
                Dictionary<ProgressionType, List<ProgressionValue>> groupedValuesDict = groupedValues.ToDictionary(g => g.Key, g => g.ToList());

                foreach (var kvp in groupedValuesDict)
                {
                    ProgressionType type = kvp.Key;
                    List<ProgressionValue> values = kvp.Value;

                    if (!string.IsNullOrEmpty(search))
                    {
                        values = values.Where(pgv => pgv.Name.Contains(search)).ToList();
                    }

                    string stype = type.ToString();

                    if (!MenuDropTState.ContainsKey(stype))
                    {
                        MenuDropTState[stype] = false; // Set the initial state to false for the bool toggle
                    }


                    bool state = MenuDropTState[stype];
                    NewGUILayout.DropDownForMethods("Progression Type: " +stype, () =>
                    {
                        foreach (ProgressionValue PGV in values)
                        {
                            string id = PGV.Name;
                            //if (!TogBL.ContainsKey(id))
                            //{
                            //    TogBL[id] = false; // Set the initial state to false for the bool toggle
                            //}

                            //bool state = TogBL[id];
                            //CGUILayout.DropDownForMethods(id, () =>
                            //{
                            NewGUILayout.BeginHorizontal(() =>
                                {
                                    GUILayout.Label(id);
                                    if (GUILayout.Button("+1", GUILayout.MaxWidth(50)))
                                    {
                                        int lvl = PGV.Level;
                                        int max = PGV.ProgressionClass.MaxLevel;
                                        if (lvl < max)
                                        {
                                            PGV.Level++;
                                        }
                                    }
                                    if (GUILayout.Button("MAX", GUILayout.MaxWidth(50)))
                                    {
                                        int max = PGV.ProgressionClass.MaxLevel;
                                        PGV.Level = max;
                                    }
                                });
                            //}, ref state);

                            
                        }
                    }, ref state);
                    MenuDropTState[stype] = state;
                }
                //if (CGUILayout.Button($"Max Skill"))
                //{
                //    MaxSkill();
                //}

            }
        }
        public static void GetListCBuffs(EntityPlayerLocal entityLocalPlayer, List<BuffClass> ListOFClass)
        {
            if (ListOFClass != null)
            {
                if (entityLocalPlayer != null || ListOFClass != null)
                {
                    if (ListOFClass.Count > 0)
                    {
                        foreach (BuffClass buffClass in ListOFClass)
                        {
                            string buffName = buffClass.Name;

                            // Add the buff name to the _ToggleStates dictionary with a default value of false if it doesn't exist
                            if (!ButtonTState.ContainsKey(buffName))
                            {
                                ButtonTState[buffName] = false;
                            }

                            // Use the boolean value from the _ToggleStates dictionary to determine the button's toggle state
                            bool toggleState = ButtonTState[buffName];

                            // Use GUILayout.Toggle to create a toggle button for each buff name
                            // The toggle state is controlled by the _ToggleStates dictionary

                            // Use GUILayout.Toggle to create a toggle button for each buff name
                            // The toggle state is controlled by the _ToggleStates dictionary
                            bool newToggleState = GUILayout.Toggle(toggleState, buffName);

                            if (newToggleState != toggleState)
                            {
                                // If the toggle state changes, update the _ToggleStates dictionary with the new state
                                ButtonTState[buffName] = newToggleState;

                                if (newToggleState)
                                {
                                    // If the button is toggled on, add the buff to the player
                                    entityLocalPlayer.Buffs.AddBuff(buffName);
                                    //Debug.LogWarning($"{buffName} Added to player {O.ELP.gameObject.name}");
                                }
                                else
                                {
                                    // If the button is toggled off, remove the buff from the player
                                    entityLocalPlayer.Buffs.RemoveBuff(buffName);
                                    //Debug.LogWarning($"{buffName} Removed from player {O.ELP.gameObject.name}");
                                }
                            }
                        }

                    }
                    else
                    {
                        GUILayout.Label("No buffs found.");

                    }

                }
                else
                {
                    //if (ListOFClass == null)
                    //{
                    //    ListOFClass = O.GetAvailableBuffClasses();
                    //}


                    GUILayout.Label("Not ingame");
                }
            }

        }
        public static void GetList(bool _bool, EntityPlayerLocal entityLocalPlayer, List<BuffClass> ListOFClass, string searchText)
        {
            if (ListOFClass != null)


                if (entityLocalPlayer != null || ListOFClass != null)
                {
                    if (ListOFClass.Count > 0)
                    {
                        foreach (BuffClass buffClass in ListOFClass)
                        {
                            if (searchText == "" || buffClass.Name.Contains(searchText)) //case sensitve  . Possible ignore case buffClass.Name.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0
                            {
                                // se GUILayout.Button to create a button for each buff name
                                if (GUILayout.Button(buffClass.Name))
                                {
                                    entityLocalPlayer.Buffs.AddBuff(buffClass.Name, -1, true, false, false, 99999f);
                                    Debug.LogWarning($"{buffClass.Name} Added to player {O.ELP.gameObject.name}");
                                    //Logic when the button is clicked
                                }
                                if (_bool)
                                {
                                    break;
                                }
                            }
                        }

                    }
                    else
                    {
                        GUILayout.Label("No buffs found.");

                    }

                }
                else
                {
                    if (ListOFClass == null)
                    {
                        //ListOFClass = O.GetAvailableBuffClasses();
                    }


                    GUILayout.Label("Not ingame");
                }

        }
        public static void GetListPassiveEffects(string searchText) //should make a chache for this one to lower cpu usage
        {

            // Get all enum values and sort them alphabetically
            PassiveEffects[] enumValues = System.Enum.GetValues(typeof(PassiveEffects)).Cast<PassiveEffects>().OrderBy(effect => effect.ToString()).ToArray();
            // Filter the items based on the search text
            PassiveEffects[] filteredEffects = string.IsNullOrEmpty(searchText)
                ? enumValues
                : enumValues.Where(effect => effect.ToString().IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0).ToArray();

            // Calculate the half amount of buttons
            int halfAmount = Mathf.CeilToInt(enumValues.Length / 2f);
            PassiveEffects[] leftColumnEffects = filteredEffects.Take(halfAmount).ToArray();
            PassiveEffects[] rightColumnEffects = filteredEffects.Skip(halfAmount).ToArray();

            //bool toggleState = passiveToggleStates[effect];
            NewGUILayout.BeginHorizontal(() =>
            {
                NewGUILayout.BeginVertical(() =>
                {
                    // Display the buttons in the left column
                    foreach (PassiveEffects effect in leftColumnEffects)
                    {
                        //CGUILayout.Button(effect.ToString(), GUILayout.MaxWidth(150));
                        DisplayToggleButton(effect);
                    }
                });
                NewGUILayout.BeginVertical(() =>
                {
                    // Display the buttons in the right column
                    foreach (PassiveEffects effect in rightColumnEffects)
                    {
                        //CGUILayout.Button(effect.ToString(), GUILayout.MaxWidth(150));
                        DisplayToggleButton(effect);
                    }
                });
            });
        }
        #endregion




        #endregion


        public void OnHud()
        {

        }
        //--------------------------------------------------------------------------------------------------------
        private void Start()
        {

            Debug.LogWarning("THIS IS Start CH!!!!");
        }
        //--------------------------------------------------------------------------------------------------------
        private void Update()
        {
            
            if ((bool)Settings["bool_IsGameStarted"] == true)
            {
                //CheatPassiveEffect(RB["_BL_Blockdmg"], PassiveEffects.BlockDamage, SETT._FL_blokdmg, ValueModifierTypes.perc_add);
                CheatPassiveEffect(NewSettings.GetBool("bool_BlockDamage"), PassiveEffects.BlockDamage, NewSettings.FloatBlockDamageMultiplier, ValueModifierTypes.perc_add);
                //CheatPassiveEffect(RB["_BL_Kill"], PassiveEffects.EntityDamage, SETT._FL_killdmg, ValueModifierTypes.perc_add);
                //CheatPassiveEffect(RB["_BL_Harvest"], PassiveEffects.HarvestCount, SETT._FL_harvest, ValueModifierTypes.perc_add);
                //CheatPassiveEffect(RB["_BL_Jmp"], PassiveEffects.JumpStrength, SETT._FL_jmp, ValueModifierTypes.base_set);
                //CheatPassiveEffect(RB["_BL_APM"], PassiveEffects.AttacksPerMinute, SETT._FL_APM, ValueModifierTypes.base_set);
                //CheatPassiveEffect(RB["_BL_Run"], PassiveEffects.RunSpeed, SETT._FL_run, ValueModifierTypes.base_set);
                //CheatPassiveEffect(RB["_instantScrap"], PassiveEffects.ScrappingTime, 0f, ValueModifierTypes.base_set);
                //CheatPassiveEffect(RB["_instantCraft"], PassiveEffects.CraftingTime, 0f, ValueModifierTypes.base_set);
                //CheatPassiveEffect(RB["_instantSmelt"], PassiveEffects.CraftingSmeltTime, 0f, ValueModifierTypes.base_set);
                //CheatPassiveEffect(RB["_infDurability"], PassiveEffects.DegradationPerUse, 0f, ValueModifierTypes.base_set);

                ////////if ((bool)Settings["bool_LoopQuestRewards"] == true) { LoopLASTQuestRewards(); };
                ////////if ((bool)Settings["bool_instantQuest"] == true) { InstantQuestFinish(); };
                ////////if ((bool)Settings["bool_traderOpen"] == true) { Trader(); };


               

                //if ((SETT._ignoreByAI || !SETT._ignoreByAI)&&O.ELP)
                //{
                //    O.ELP.SetIgnoredByAI(SETT._ignoreByAI);
                //};

                ////////if (((bool)Settings["bool_IgnoreByAi"] || !(bool)Settings["bool_IgnoreByAi"]) && O.ELP)
                ////////{
                ////////    O.ELP.SetIgnoredByAI((bool)Settings["bool_IgnoreByAi"]);
                ////////};

                //if (SETT.noWeaponBob && O.ELP) // When noWeaponBob is active enable 
                //{
                //    vp_FPWeapon weapon = O.ELP.vp_FPWeapon;

                //    if (weapon)
                //    {
                //        weapon.BobRate = Vector4.zero;
                //        weapon.ShakeAmplitude = Vector3.zero;
                //        weapon.RenderingFieldOfView = 120f;
                //        weapon.StepForceScale = 0f;
                //    }
                //}//no weapon bob

                if (Input.GetKeyDown(KeyCode.O)) //infinity ammo ???
                {
                    if (!O.ELP)
                    {
                        return;
                    }

                    Inventory inventory = O.ELP.inventory;

                    if (inventory != null)
                    {
                        ItemActionAttack gun = inventory.GetHoldingGun();

                        if (gun != null)
                        {
                            gun.InfiniteAmmo = !gun.InfiniteAmmo;
                           
                        }
                    }
                }//infinity ammo

                if (Input.GetKeyDown(KeyCode.F9)) //checks if the key is being pressed. if it does execute F9 is empty in game
                {
                    // we can put cheat here
         
                }


                ////////if ((bool)Settings["bool_CreativeMode"] || !(bool)Settings["bool_CreativeMode"]) //Toggle for ingame Creative and Debug Working like a sharm
                ////////{
                ////////    CmDm((bool)Settings["bool_CreativeMode"]);
                ////////}
                ////////if (RB["_isEditmode"] || !RB["_isEditmode"]) //Toggle for ingame Creative and Debug Working like a sharm
                ////////{
                ////////    editMode();
                ////////}
            }
           

            //function that does not work 
            /*
            if (SB.Count > 1) 
            {
                if (SB["_nameScramble"] || !SB["_nameScramble"])
                {
                    //Log.Out($" sett name1 {SETT._nameScramble}");
                    //Log.Out($" sett name {SETT._nameScramble}");
                    //if (!SBO.ContainsKey("NSCAMBLE"))
                    //{
                    //    SBO["NSCAMBLE"] = false;
                    //    SBO["NSCAMBLE1"] = false;// Set the initial state to false for the bool toggle
                    //    //Log.Out($"nsc1 {TogBL["NSCAMBLE"]}");
                    //    //Log.Out($"nsc2 {TogBL["NSCAMBLE1"]}");
                    //}

                    if (SB["_nameScramble"] == true)
                    {
                        //Log.Out($" sett name2  = {SETT._nameScramble}");
                        //Log.Out($" nsc1.2 = {TogBL["NSCAMBLE"]}");
                        if (O._gameManager.persistentLocalPlayer != null && RB["NSCRAM1"] != true)
                        {
                            //Log.Out($" sett name3 {SETT._nameScramble} and ");
                            O._gameManager.persistentLocalPlayer.PlayerName = Extras.ScrambleString(O._gameManager.persistentLocalPlayer.PlayerName);
                            O._gameManager.persistentLocalPlayer.PlayerName = Extras.ScrambleString(O._gameManager.persistentLocalPlayer.PlayerName);
                            RB["NSCRAM1"] = true;
                            //Log.Out($" sett name3 {SETT._nameScramble} and nsc1 {TogBL["NSCAMBLE"]}");
                        }//
                        if (O.ELP != null && RB["NSCRAM2"] == false)
                        {
                            //Log.Out($" sett name2 {SETT._nameScramble}");
                            if (O.ELP.EntityName != null)
                            {
                                SS["Playername"] = O.ELP.EntityName;

                                O.ELP.EntityName = Extras.ScrambleString(O.ELP.EntityName);
                            }
                            RB["NSCRAM2"] = true;
                            //Log.Out($" sett name3 {SETT._nameScramble} and nsc1 {TogBL["NSCAMBLE1"]}");
                        }


                    }
                    else if (SB["_nameScramble"] == false && RB["NSCRAM1"] == true && RB["NSCRAM2"] == true)
                    {
                        O.ELP.EntityName = SS["Playername"];
                        O._gameManager.persistentLocalPlayer.PlayerName = SS["Playername"];
                        RB["NSCRAM1"] = false;
                        RB["NSCRAM2"] = false;
                    }

                }
            }
            
            */


        }

        //--------------------------------------------------------------------------------------------------------
        void OnGUI()
        {
        }
        //--------------------------------------------------------------------------------------------------------

        #region Toggles
        //public static EntityAlive Entity;

     
        public static void Trader()
        {
            if (O.Etrader != null && O.ELP)
            {
                //if (O.Etrader.aiClosestPlayer.ToString().ToLower() == O.ELP.name.ToString().ToLower())
                //{
                    O.Etrader.IsDancing = true;
                    ulong OCTime = 0;
                    O.Etrader.TraderInfo.CloseTime = OCTime;
                    O.Etrader.TraderInfo.OpenTime = OCTime;

                //}

            }
            else if (O.Etrader == null) 
            {

                RB["_EtraderOpen"] = false;
            }
        }
        public static void InstantQuestFinish()
        {
            if (RB["_QuestComplete"] == true && O.ELP)
            {
                foreach (Quest quest in O.ELP.QuestJournal.quests)
                {


                    //QuestClass.Category == "Challenge"
                    if ((quest.Tracked==true||quest.Active==true) && quest.CurrentState == Quest.QuestState.InProgress) 
                    {

                        quest.CurrentState = Quest.QuestState.ReadyForTurnIn;
                    }
                }
            }
        }
        public static void LoopLASTQuestRewards()
        {
            if (RB["_LOQuestRewards"] == true && O.ELP)
            {
                if (O.ELP.QuestJournal.quests.Count > 0)
                {
                    int lastIndex = O.ELP.QuestJournal.quests.Count - 1;
           
                    Quest lastQuest = O.ELP.QuestJournal.quests[lastIndex];

                    if (lastQuest.CurrentState == Quest.QuestState.Completed )
                    {
                        lastQuest.CurrentState = Quest.QuestState.ReadyForTurnIn;
                        Debug.LogWarning($" {lastQuest.ID} is ready for turn in");
                    }



                }
                else
                {
                    // The list is empty, handle this case accordingly
                }
            }
        }

        #endregion


        #region Methods



        public static void AddCheatBuff()
        {
            /*
            float WalkSlider = SETT._run;  //set 1 to 5
            float RunSlider = SETT._run;  //set 1 to 5
            float BlockDMGSlider = SETT._dmg;   //set 1 to 50

            //BuffValue buff;


            
            BuffClass test = BuffManager.GetBuff("testbuff");
            test.DamageType = EnumDamageTypes.None; // Set the appropriate damage type if applicable
            test.Description = "This is a custom buff.";
            test.DurationMax = 99999999f;
            //test.Icon = "ui_game_symbol_agility";
            test.ShowOnHUD = true;
            //test.Hidden = false;
            test.IconColor = new Color(0.22f, 0.4f, 1f, 100f);
            test.DisplayType = EnumEntityUINotificationDisplayMode.IconPlusDetail;
            

            
            O._minEffectController.EffectGroups = new List<MinEffectGroup>
            {
                new MinEffectGroup
                {
                    OwnerTiered = true,
                    PassiveEffects = new List<PassiveEffect> 
                    {
                          new PassiveEffect
                             {
                                 MatchAnyTags = true,
                                 Type = PassiveEffects.WalkSpeed,
                                 Modifier = PassiveEffect.ValueModifierTypes.base_add,
                                 Values = new float[] { RunSlider }
                                 //Set other properties of PassiveEffect if needed
                             },
                            new PassiveEffect
                            {
                                // Set the properties of the PassiveEffect instance accordingly
                                 MatchAnyTags = true,
                                 Modifier = PassiveEffect.ValueModifierTypes.base_add,
                                 Type = PassiveEffects.RunSpeed,
                                 Values = new float[] { WalkSlider },

                                 //Set other properties if needed
                            },
                            new PassiveEffect
                            {
                                // Set the properties of the PassiveEffect instance accordingly
                                 MatchAnyTags = true,
                                 Modifier = PassiveEffect.ValueModifierTypes.base_add,
                                 Type = PassiveEffects.BlockDamage,
                                 Values = new float[] { BlockDMGSlider },
                                 //Set other properties if needed
                            },
                            new PassiveEffect
                            {
                                // Set the properties of the PassiveEffect instance accordingly
                                 MatchAnyTags = true,
                                 Modifier = PassiveEffect.ValueModifierTypes.base_add,
                                 Type = PassiveEffects.CraftingTime,
                                 Values = new float[] { 0 },
                                 //Set other properties if needed
                            },
                            new PassiveEffect
                            {
                                // Set the properties of the PassiveEffect instance accordingly
                                 MatchAnyTags = true,
                                 Modifier = PassiveEffect.ValueModifierTypes.base_add,
                                 Type = PassiveEffects.FoodGain,
                                 Values = new float[] { 9999 },
                                 //Set other properties if needed
                            }
                    },
                    PassivesIndex = new List<PassiveEffects>
                        {
                             PassiveEffects.WalkSpeed,
                             PassiveEffects.BlockDamage,
                             PassiveEffects.CraftingTime,
                             PassiveEffects.FoodGain,
                        }
                }

            };
            O._minEffectController.PassivesIndex = new HashSet<PassiveEffects> 
            {
                
                    PassiveEffects.WalkSpeed,
                    PassiveEffects.RunSpeed,
                    PassiveEffects.BlockDamage,
                    PassiveEffects.CraftingTime,
                    PassiveEffects.None
            };
            /*
            // Set the properties of the custom buff



            /*


            MinEffectController effectController = new MinEffectController 
            { 
                EffectGroups = new List<MinEffectGroup>
                {
                    new MinEffectGroup
                    {
                        OwnerTiered = true,
                        PassiveEffects = new List<PassiveEffect>
                        {
                             new PassiveEffect
                             {
                                 MatchAnyTags = true,
                                 Type = PassiveEffects.WalkSpeed,
                                 Modifier = PassiveEffect.ValueModifierTypes.base_add,
                                 Values = new float[] { RunSlider }
                                 //Set other properties of PassiveEffect if needed
                             },
                            new PassiveEffect
                            {
                                // Set the properties of the PassiveEffect instance accordingly
                                 MatchAnyTags = true,
                                 Modifier = PassiveEffect.ValueModifierTypes.base_add,
                                 Type = PassiveEffects.RunSpeed,
                                 Values = new float[] { WalkSlider },

                                 //Set other properties if needed
                            },
                            new PassiveEffect
                            {
                                // Set the properties of the PassiveEffect instance accordingly
                                 MatchAnyTags = true,
                                 Modifier = PassiveEffect.ValueModifierTypes.base_add,
                                 Type = PassiveEffects.BlockDamage,
                                 Values = new float[] { BlockDMGSlider },
                                 //Set other properties if needed
                            },
                            new PassiveEffect
                            {
                                // Set the properties of the PassiveEffect instance accordingly
                                 MatchAnyTags = true,
                                 Modifier = PassiveEffect.ValueModifierTypes.base_add,
                                 Type = PassiveEffects.CraftingTime,
                                 Values = new float[] { 0 },
                                 //Set other properties if needed
                            },
                            new PassiveEffect
                            {
                                // Set the properties of the PassiveEffect instance accordingly
                                 MatchAnyTags = true,
                                 Modifier = PassiveEffect.ValueModifierTypes.base_add,
                                 Type = PassiveEffects.FoodGain,
                                 Values = new float[] { 9999 },
                                 //Set other properties if needed
                            }
                        },
     
                        PassivesIndex = new List<PassiveEffects>
                        {
                             PassiveEffects.WalkSpeed,
                             PassiveEffects.BlockDamage,
                             PassiveEffects.CraftingTime,
                             PassiveEffects.FoodGain,
                        }

                    }
                },
                PassivesIndex = new HashSet<PassiveEffects>
                {
                    PassiveEffects.WalkSpeed,
                    PassiveEffects.RunSpeed,
                    PassiveEffects.BlockDamage,
                    PassiveEffects.CraftingTime,
                    PassiveEffects.FoodGain,
                }
            };

            
            //customBuff.Name = "customBuff";
            //customBuff.DamageType = EnumDamageTypes.None; // Set the appropriate damage type if applicable
            //customBuff.Description = "This is a custom buff.";
            //customBuff.DurationMax = 99999999f;
            //customBuff.Icon = "ui_game_symbol_agility";
            //customBuff.ShowOnHUD = true;
            //customBuff.Hidden = false;
            //customBuff.IconColor = new Color(0.22f, 0.4f, 1f, 100f);
            //customBuff.DisplayType = EnumEntityUINotificationDisplayMode.IconOnly;
            //customBuff.LocalizedName = "This is name in inventory";
            //customBuff.Effects = O._minEffectController;
          
            //O.ELP.Buffs.AddBuff("testbuff");
            */
            if (BuffManager.GetBuff(O.CheatBuff.Name) == null)
            {

                Log.Out($"Buff {O.CheatBuff} has ben added");

                O.ELP.Buffs.AddBuff(O.CheatBuff.Name);
                Log.Out($"Buff {O.CheatBuff.Name} has ben added to{O.ELP.Buffs.ActiveBuffs.GetInternalArray()} ");
            }
            else
            {
                Debug.LogWarning($"Buff {O.CheatBuff.Name} was already added to the system");
                if (O.ELP.Buffs.GetBuff(O.CheatBuff.Name) == null)
                {

                    O.ELP.Buffs.AddBuff(O.CheatBuff.Name);
                    Log.Out($"Buff {O.CheatBuff.Name} was Added to to Player again");
                }
            }
        }
        public static void AddEffectGroup()
        {
            Debug.LogWarning("adding effectGroup");
            O._minEffectController.EffectGroups = new List<MinEffectGroup>
            {

                new MinEffectGroup
               {
                   OwnerTiered = true,
                   PassiveEffects = new List<PassiveEffect>
                   {
                   },
                   PassivesIndex = new List<PassiveEffects>
                       {
                       }
               }
            };
            //O._minEffectController.PassivesIndex = new HashSet<PassiveEffects>();
        }
     
        #endregion
    }
}


