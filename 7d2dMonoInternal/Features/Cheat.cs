using System;
using System.Runtime.InteropServices;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using static PassiveEffect;
using SevenDTDMono.GuiLayoutExtended;
using  System.IO;
using SevenDTDMono.Utils;

//using SevenDTDMono.Objects;


namespace SevenDTDMono.Features
{
    public partial class Cheat : MonoBehaviour
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
        private static NewSettings SettingsInstance => NewSettings.Instance;
        private static Dictionary<string, object> Settings => NewSettings.Instance.SettingsDictionary;
        private static Dictionary<string, bool> _boolDict = SettingsInstance.GetChildDictionary<bool>(nameof(Dictionaries.BOOL_DICTIONARY));
        private static Dictionary<string, object> TempSettings => NewSettings.Instance.TempDictionary; //get instance of SettingsDictionary
        private static EntityPlayerLocal Player => NewSettings.EntityLocalPlayer;  //get instance of Player. 
        private static GameManager GameManager => NewSettings.GameManager; //Get instance of GameManager
        private static EntityTrader Trader => NewSettings.EntityTrader;
         
        
        
        
        #endregion
        //--------------------------------------------------------------------------------------------------------



        #region finished cheats
        //toggle




        //Trigger
        public static void SkillPoints()//add skillpoints - Trigger once
        {
            if (Player)
            {
                Progression prog = Player.Progression;
                prog.SkillPoints += 10;
                Log.Out($"Skillpoints added by 10, is now {prog.SkillPoints}");
            }
        }
        public static void KillSelf()
        {
            if (Player)
            {
                Player.DamageEntity(new DamageSource(EnumDamageSource.Internal, EnumDamageTypes.Suicide), 99999, false, 1f);
                SingletonMonoBehaviour<SdtdConsole>.Instance.Output("Gave 99999 damage to entity ");
            }
        }
        public static void LevelUp()//up one level-Trigger once
        {
            if (Player) //this cheat first checks if Local player is existing
            {
                Progression prog = Player.Progression;
                prog.AddLevelExp(prog.ExpToNextLevel);
            }
        }
        public static void GetPlayerId()//add skillpoints - Trigger once
        {
            string num = Player.name.ToString();
            Debug.LogError($"player ID: {num}");
        }

        
        public static void OpenTrader()
        {
            if (Trader != null && Player && Trader.aiClosestPlayer == Player)
            {
                Trader.IsDancing = true;
                ulong OCTime = 0;
                Trader.TraderInfo.CloseTime = OCTime;
                Trader.TraderInfo.OpenTime = OCTime;

                _boolDict[nameof(EntityTrader)] = true;
            }




            //if ((bool)Settings[nameof(EntityTrader)] == true)
            //    {
            //        //Seems like this should only be triggered and not lopped. Looping this makes the game very laggy!!


            //};



        }


        public static void SOMECONSOLEPRINTOUT()  //Creative and debug mode -- Trigger
        {
            string _value = null;

            string _type = "SETT.cmDm";

            if (Player)
            {
                _value = Player.DebugNameInfo;

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
     



        //passive effects!

        public static void AddPassiveEffectToPlayer(PassiveEffects passiveEffects, float modifier, ValueModifierTypes valueModifierTypes)
        {

            if (!GameManager.gameStateManager.bGameStarted && !Player)
            {
                Debug.LogWarning($"Game Not Started cannot add {passiveEffects}");
                //if game is not started and player is null return
                return;
            }

            string str = _boolDict[$"{passiveEffects}"] ? "Adding " : "Removing ";

            Debug.LogWarning($"{str}{passiveEffects}");
            if (_boolDict[$"{passiveEffects}"])
            {
                AddPassive(passiveEffects, modifier, valueModifierTypes);
            }
            if (!_boolDict[$"{passiveEffects}"])
            {
                RemovePassive(passiveEffects);

            }
        }
        private static void RemovePassive(PassiveEffects passiveEffects)
        {
            var passiveEffectsList = MinEffectController.EffectGroups[0].PassiveEffects;
            for (int i = passiveEffectsList.Count - 1; i >= 0; i--)
            {
                var effect = passiveEffectsList[i];
                if (effect.Type == passiveEffects) //if the passive effect is found remove it, else nothing
                {
                    passiveEffectsList.RemoveAt(i);
                }
                MinEffectController.PassivesIndex.Remove(passiveEffects);
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

            if (Player.Buffs.HasBuff(nameof(_cheatBuff)) == false)
            {
                try 
                {
                    Log.Out($"{_cheatBuff.Name} was not active, try adding");
                   Player.Buffs.AddBuff(nameof(_cheatBuff));
                } catch
                { 
                }
            }

            List<PassiveEffect> pE1 = new List<PassiveEffect>();
            MinEffectGroup effectGroup = MinEffectController.EffectGroups[0];

            PassiveEffect newPassiveEffect = new PassiveEffect
            {
                MatchAnyTags = true,
                Modifier = valueModifierTypes,
                Type = passiveEffects,
                Values = new float[] { value }, // Adjust the values accordingly
                                                // Set other properties if needed
            };//this is just the passive effects

            var passiveEffectsList = MinEffectController.EffectGroups[0].PassiveEffects;
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

            MinEffectController.PassivesIndex.Add(passiveEffects); // adds to MinEffectController.PassivesIndex MUST DO OTHERWISE NULL REFERENCE ERROR
            //effectGroup.PassiveEffects.Add(newPassiveEffect);           // MinEffectController.MinEffectGroup.PassivesIndex __ This location just adds buffs on top if added multiple times
            MinEffectController.EffectGroups[0].PassiveEffects.Add(newPassiveEffect);           // MinEffectController.MinEffectGroup.PassivesIndex __ This location just adds buffs on top if added multiple times
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
        private static void DisplayToggleButton(PassiveEffects effect)
        {
            // Get or set the toggle state in the dictionary.
            if (!TempSettings.ContainsKey(nameof(effect)))
            {
                TempSettings[effect.ToString()] = false; // Set the initial state to false for new effects.
            }
            bool toggleState = (bool)TempSettings[effect.ToString()];

            // Display the toggle button and update the toggle state in the dictionary.
            bool buttonPressed = NewGUILayout.Button(effect.ToString(), GUILayout.MaxWidth(150));
            TempSettings[effect.ToString()] = buttonPressed;

            // If the button is pressed, set the input text field to the same string as the button text
            if (buttonPressed)
            {
                inputPassiveEffects = effect.ToString();
            }
        }




        //BUFF RELATED
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
            if (BuffManager.GetBuff(_cheatBuff.Name) == null)
            {

                Log.Out($"Buff {_cheatBuff} has ben added");

                Player.Buffs.AddBuff(_cheatBuff.Name);
                Log.Out($"Buff {_cheatBuff.Name} has ben added to{Player.Buffs.ActiveBuffs.GetInternalArray()} ");
            }
            else
            {
                Debug.LogWarning($"Buff {_cheatBuff.Name} was already added to the system");
                if (Player.Buffs.GetBuff(_cheatBuff.Name) == null)
                {

                    Player.Buffs.AddBuff(_cheatBuff.Name);
                    Log.Out($"Buff {_cheatBuff.Name} was Added to to Player again");
                }
            }
        }
        public static void AddEffectGroup()
        {
            Debug.LogWarning("adding effectGroup");
            MinEffectController.EffectGroups = new List<MinEffectGroup>
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
        public static void ClearCheatBuff()
        {
            Debug.LogWarning("Clearing CheatBuff");

            MinEffectController.EffectGroups[0].PassiveEffects.Clear();
            MinEffectController.PassivesIndex.Clear();
        }
        public static void RemoveAllBuff()
        {
            List<BuffValue> activeBuffs = Player.Buffs.ActiveBuffs;
            foreach (BuffValue buff in activeBuffs)
            {
                Player.Buffs.RemoveBuff(buff.BuffName);

                if (TempSettings.ContainsKey(buff.BuffName))
                {
                    TempSettings[buff.BuffName] = false;
                }
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
                            if (!TempSettings.ContainsKey(buffName))
                            {
                                TempSettings[buffName] = false;
                            }

                            // Use the boolean value from the _ToggleStates dictionary to determine the button's toggle state
                            bool toggleState = (bool)TempSettings[buffName];

                            // Use GUILayout.Toggle to create a toggle button for each buff name
                            // The toggle state is controlled by the _ToggleStates dictionary

                            // Use GUILayout.Toggle to create a toggle button for each buff name
                            // The toggle state is controlled by the _ToggleStates dictionary
                            bool newToggleState = GUILayout.Toggle(toggleState, buffName);

                            if (newToggleState != toggleState)
                            {
                                // If the toggle state changes, update the _ToggleStates dictionary with the new state
                                TempSettings[buffName] = newToggleState;

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
                if (entityLocalPlayer != null)
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
                                    //entityLocalPlayer.Buffs.AddBuff(buffClass.Name);
                                    entityLocalPlayer.Buffs.AddBuff(buffClass.Name, new Vector3i(), -1, false, false, 999999f);
                                    Debug.LogWarning($"{buffClass.Name} Added to player {Player.gameObject.name}");
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
        }
        public static void MaxSkill() ///////////////////////////////
        {
            if (NewSettings.ListProgressionValues.Count > 0)
            {
                foreach (ProgressionValue progressionValue in NewSettings.ListProgressionValues)
                {
                    int lvl = progressionValue.Level;
                    int max = progressionValue.ProgressionClass.MaxLevel;

                    if (lvl < max)
                    {
                        progressionValue.Level = max;
                    }
                }
            }
        }
        public static void ListProgressionValues(string search) //////////Progression Value/////////////////////
        {
            if (NewSettings.ListProgressionValues.Count > 0)
            {

                var groupedValues = NewSettings.ListProgressionValues.GroupBy(progressionValue => progressionValue.ProgressionClass.Type);
                Dictionary<ProgressionType, List<ProgressionValue>> groupedValuesDict = groupedValues.ToDictionary(g => g.Key, g => g.ToList());

                foreach (var keyValuePair in groupedValuesDict)
                {
                    ProgressionType type = keyValuePair.Key;
                    List<ProgressionValue> values = keyValuePair.Value;

                    if (!string.IsNullOrEmpty(search))
                    {
                        values = values.Where(progressionValue => progressionValue.Name.Contains(search)).ToList();
                    }

                    string progressionType = type.ToString();

                    if (!TempSettings.ContainsKey(progressionType))
                    {
                        TempSettings[progressionType] = false; // Set the initial state to false for the bool toggle
                    }


                    bool state = (bool)TempSettings[progressionType];
                    NewGUILayout.DropDownForMethods("Progression Type: " + progressionType, () =>
                    {
                        foreach (ProgressionValue progressionValue in values)
                        {
                            string id = progressionValue.Name;

                            NewGUILayout.BeginHorizontal(() =>
                            {
                                GUILayout.Label(id);
                                if (GUILayout.Button("+1", GUILayout.MaxWidth(50)))
                                {
                                    int lvl = progressionValue.Level;
                                    int max = progressionValue.ProgressionClass.MaxLevel;
                                    if (lvl < max)
                                    {
                                        progressionValue.Level++;
                                    }
                                }
                                if (GUILayout.Button("MAX", GUILayout.MaxWidth(50)))
                                {
                                    int max = progressionValue.ProgressionClass.MaxLevel;
                                    progressionValue.Level = max;
                                }
                            });



                        }
                    }, ref state);
                    TempSettings[progressionType] = state;
                }
                if (NewGUILayout.Button($"Max Skill"))
                {
                    MaxSkill();
                }

            }
        }



        //Lists Related!
        public static void ListEntityZombie() ///////////////////////////////
        {
            if (NewSettings.EntityAlive.Count > 1)
            {
                foreach (EntityAlive entityAlive in NewSettings.EntityAlive)
                {
                    if (!entityAlive || entityAlive == Player || !entityAlive.IsAlive())
                    {
                        continue;
                    }

                    //string xm1 = zombie.entityFlags.ToString();
                    //string ZM1= zombie.EntityName.ToString();
                    string entityId = entityAlive.entityId.ToString();
                    string entityName = entityAlive.EntityName;
                    string entityIdentity = entityName + entityId;
                    // Get or set the zombie's toggle state in the dictionary.
                    if (!TempSettings.ContainsKey(entityIdentity))
                    {
                        TempSettings[entityIdentity] = false; // Set the initial state to false for new zombies.
                    }

                    bool toggleState = (bool)TempSettings[entityIdentity];
                    NewGUILayout.DropDownForMethods(entityIdentity, () =>
                    {
                        NewGUILayout.BeginHorizontal(() =>
                        {
                            if (GUILayout.Button("Teleport To"))
                            {
                                // Perform teleport action for the zombie.
                                Player.TeleportToPosition(entityAlive.GetPosition());
                            }
                            if (GUILayout.Button("Kill"))
                            {

                                // Perform kill action for the zombie.
                                entityAlive.DamageEntity(new DamageSource(EnumDamageSource.Internal, EnumDamageTypes.Suicide), 99999, false, 1f);
                                TempSettings.Remove(entityIdentity);
                            }
                        });
                    }, ref toggleState);
                    TempSettings[entityIdentity] = toggleState;
                }
            }
            else
            {
                GUILayout.Label("No Entities found.");
            }
        }
        public static void ListEntityPlayer() ///////////////////////////////
        {
            if (NewSettings.EntityPlayers.Count > 1)
            {
                foreach (EntityPlayer player in NewSettings.EntityPlayers)
                {
                    if (!player || player == Player || !player.IsAlive())
                    {
                        continue;
                    }


                    string entityId = player.entityId.ToString();
                    string entityName = player.EntityName;
                    string entityIdentity = entityName + entityId;

                    //string playerName = player.EntityName.ToString()+player.entityId.ToString();
                    //string zm = player.EntityName.ToString();

                    // Get or set the zombie's toggle state in the dictionary.
                    if (!TempSettings.ContainsKey(entityIdentity))
                    {
                        TempSettings[entityIdentity] = false; // Set the initial state to false for new zombies.
                    }

                    bool toggleState = (bool)TempSettings[entityIdentity];
                    NewGUILayout.DropDownForMethods(entityIdentity, () =>
                    {
                        NewGUILayout.BeginHorizontal(() =>
                        {
                            //CGUILayout.Button("whatever", Color.yellow, Color.blue);
                            if (GUILayout.Button("Teleport"))
                            {
                                // Perform teleport action for the zombie.
                                Player.TeleportToPosition(player.GetPosition());
                            }
                            if (GUILayout.Button("Kill"))
                            {
                                // Perform kill action for the zombie.
                                player.DamageEntity(new DamageSource(EnumDamageSource.Internal, EnumDamageTypes.Suicide), 99999, false, 1f);
                                TempSettings.Remove(entityIdentity);
                            }//zombie.DamageEntity(new DamageSource(EnumDamageSource.Internal, EnumDamageTypes.Suicide), 99999, false, 1f);
                        });

                    }, ref toggleState);

                    // Update the toggle state in the dictionary.
                    TempSettings[entityIdentity] = toggleState;
                }
            }
            else
            {
                GUILayout.Label("No Players found.");
            }
        }

        //public static void ListZombie1() ///////////////////////////////
        //{
        //    if (NewSettings.EntityAlive.Count > 1)
        //    {
        //        foreach (EntityEnemy enemy in NewSettings.EntityAlive)
        //        {
        //            if (!enemy || enemy == Player || !enemy.IsAlive())
        //            {
        //                continue;
        //            }

        //            //string xm1 = zombie.entityFlags.ToString();
        //            //string ZM1= zombie.EntityName.ToString();
        //            string zombieIID = enemy.entityId.ToString();
        //            string zm = enemy.EntityName;
        //            string zmIID = zm + zombieIID;
        //            // Get or set the zombie's toggle state in the dictionary.
        //            if (!TempSettings.ContainsKey(zmIID))
        //            {
        //                TempSettings[zmIID] = false; // Set the initial state to false for new zombies.
        //            }

        //            bool toggleState = (bool)TempSettings[zmIID];
        //            NewGUILayout.DropDownForMethods(zmIID, () =>
        //            {
        //                NewGUILayout.BeginHorizontal(() =>
        //                {
        //                    if (GUILayout.Button(" Teleport "))
        //                    {
        //                        // Perform teleport action for the zombie.
        //                        Player.TeleportToPosition(enemy.GetPosition());
        //                    }
        //                    if (GUILayout.Button("Kill"))
        //                    {

        //                        // Perform kill action for the zombie.
        //                        enemy.DamageEntity(new DamageSource(EnumDamageSource.Internal, EnumDamageTypes.Suicide), 99999, false, 1f);
        //                        TempSettings.Remove(zmIID);
        //                    }
        //                });
        //            }, ref toggleState);
        //            TempSettings[zmIID] = toggleState;
        //        }
        //    }
        //    else
        //    {
        //        GUILayout.Label("No zombies found.");
        //    }
        //}
        
        //Buffs and Progression 

        
        


        #endregion


        public void OnHud()
        {

        }
        //--------------------------------------------------------------------------------------------------------
        private void Start()
        {
            SettingsInstance.CheckBoolKeyExist(nameof(SettingsBools.CHEAT_BUFF));
            SettingsInstance.CheckBoolKeyExist(nameof(SettingsBools.BUFF_CLASSES_LOADED));


            //CheckForBoolKey(nameof(_cheatBuff)); //pretty much adds the bool key to the dictionary so we do not get reference error 
            Debug.LogWarning($"Start: {nameof(Cheat)}");
        }

        private static bool CheckForBoolKey(string stringBoolKey)
        {
            #region DictionaryCheck
            // Check if the key exists in the dictionary
            if (!Settings.ContainsKey(stringBoolKey))
            {
                // Add the key with a default value of false if it does not exist
                Settings[stringBoolKey] = false;
                return true;
                //NewSettings.AddSetting(boolKey, false);
            }
            // Ensure the value associated with the key is a bool
            if (!(Settings[stringBoolKey] is bool))
            {
                Debug.LogError($"Key '{stringBoolKey}' is not a boolean.");
                return false;
            }
            #endregion
            return true;
        }
        //--------------------------------------------------------------------------------------------------------
        private void Update()
        {
            if (NewSettings.GameManager.IsQuitting)
            {
                _boolDict[nameof(SettingsBools.CHEAT_BUFF)] = false; //resets variable
                _boolDict[nameof(SettingsBools.BUFF_CLASSES_LOADED)] = false; //resets variable
                _boolDict[nameof(SettingsBools.CHEAT_BUFFS_LOADED)] = false; //resets variable
                _boolDict[nameof(SettingsBools.PROGRESSION_VALUE_LOADED)] = false; //resets variable


            }


            //loading of cheat buffs and caching progression values 
            if (NewSettings.GameManager.gameStateManager.bGameStarted == true && Player && _boolDict[nameof(SettingsBools.CHEAT_BUFF)]==false)
            {
                Debug.LogWarning($"amount of buffs before load {BuffManager.Buffs.Count}");
                InitCheatBuff();


                if (!_boolDict[nameof(SettingsBools.BUFF_CLASSES_LOADED)])
                {
                    Log.Out("Reloading buffs");
                    NewSettings.ListBuffClasses.Clear();
                    foreach (var buffClass in BuffManager.Buffs)
                    {
                        NewSettings.ListBuffClasses.Add(buffClass.Value);
                    }
                    NewSettings.ListBuffClasses.Sort((buff1, buff2) => string.Compare(buff1.Name, buff2.Name));

                    LogListBuffClass(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "load", "ListBuffClasses.txt"));
                    _boolDict[nameof(SettingsBools.BUFF_CLASSES_LOADED)] = true;
                    Debug.LogWarning($"2 = {_boolDict[nameof(SettingsBools.BUFF_CLASSES_LOADED)]}");
                }

                if (!_boolDict[nameof(SettingsBools.CHEAT_BUFFS_LOADED)])
                {
                    string CbuffRssName = "SevenDTDMono.Features.Buffs.Cbuffs.XML";

                    Log.Out($"{CbuffRssName} begin Load....");

                    NewSettings.ListCheatBuffs.Clear();
                    LoadCustomXml(CbuffRssName);
                    NewSettings.ListCheatBuffs.Sort((a, b) => string.Compare(a.Name, b.Name));
                    Debug.LogWarning($"{CbuffRssName} finished load!");
                    _boolDict[nameof(SettingsBools.CHEAT_BUFFS_LOADED)] = true;
                    Debug.LogWarning($"3 = {_boolDict[nameof(SettingsBools.CHEAT_BUFFS_LOADED)]}");
                }
                if (!_boolDict[nameof(SettingsBools.PROGRESSION_VALUE_LOADED)] && Player)
                {
                    if (NewSettings.ListProgressionValues.Count <= 0)
                    {
                        Debug.LogWarning("List empty");
                    }
                    NewSettings.ListProgressionValues.Clear();
                    foreach (KeyValuePair<int, ProgressionValue> keyValuePair in Player.Progression.GetDict())
                    {
                        ProgressionValue value = keyValuePair.Value;
                        bool flag;
                        if (value == null)
                        {
                            flag = null != null;
                        }
                        else
                        {
                            ProgressionClass progressionClass = value.ProgressionClass;
                            flag = (progressionClass != null ? progressionClass.Name : null) != null;
                        }
                        if (flag)
                        {

                            NewSettings.ListProgressionValues.Add(keyValuePair.Value);
                        }

                    }
                    NewSettings.ListProgressionValues.Sort((a, b) => string.Compare(a.Name, b.Name));
                    _boolDict[nameof(SettingsBools.PROGRESSION_VALUE_LOADED)] = true;
                    Debug.LogWarning($"4 = {_boolDict[nameof(SettingsBools.PROGRESSION_VALUE_LOADED)]}");
                }






            }
            


            if (NewSettings.GameManager.gameStateManager.bGameStarted == true && Player)
            {
                if (_boolDict[nameof(Quest.QuestState.Completed)])
                {
                    //Loop Rewards!
                    if (Player.QuestJournal.quests.Count > 0)
                    {
                        int lastIndex = Player.QuestJournal.quests.Count - 1;

                        Quest lastQuest = Player.QuestJournal.quests[lastIndex];

                        if (lastQuest.CurrentState == Quest.QuestState.Completed)
                        {
                            lastQuest.CurrentState = Quest.QuestState.ReadyForTurnIn;
                            Debug.LogWarning($" {lastQuest.ID} is ready for turn in");
                        }
                    }
                    else
                    {
                        // The list is empty, handle this case accordingly
                    }
                };
                if (_boolDict[nameof(Quest.QuestState.InProgress)])
                {
                    //finish quest!!
                    foreach (Quest quest in Player.QuestJournal.quests)
                    {


                        //QuestClass.Category == "Challenge"
                        if ((quest.Tracked == true || quest.Active == true) && quest.CurrentState == Quest.QuestState.InProgress)
                        {
                            quest.CurrentState = Quest.QuestState.ReadyForTurnIn;
                        }
                    }
                };
                if (_boolDict[nameof(EntityTrader)] && Trader!=null)
                {

                    //disables the open trader incase we leave the grounds!
                    
                    Vector3 v3Trader = Trader.traderArea.Position;
                    Vector3 v3Player = Player.position;
                    
                    const float range = 70.0f; //roughly outside of the traderArea. If we reach like way to far we will have trader=null
                    var distance = Vector3.Distance(v3Player, v3Trader);


                    //Debug.LogWarning($"Distance: {distance}");

                    if (distance > range)
                    {
                        Debug.LogWarning($"Distance: {distance}");
                        Debug.LogError($"{distance} is less then {range}, Resetting trader");
                        _boolDict[nameof(EntityTrader)] = false;
                        Trader.IsDancing = false;
                        Trader.TraderInfo.CloseTime = (ulong)21833;
                        Trader.TraderInfo.OpenTime = (ulong)4083;

                    }
                }
                if (_boolDict[nameof(SettingsBools.NO_WEAPON_BOB)]) // When noWeaponBob is active enable 
                {
                    vp_FPWeapon weapon = Player.vp_FPWeapon;

                    if (weapon)
                    {
                        weapon.BobRate = Vector4.zero;
                        weapon.ShakeAmplitude = Vector3.zero;
                        weapon.RenderingFieldOfView = 120f;
                        weapon.StepForceScale = 0f;
                    }
                }


                if (_boolDict[nameof(SettingsBools.NAME_SCRAMBLE)] && Player)
                {
                    //Log.Out($" sett name2 {SETT._nameScramble}");
                    if (Player.EntityName != null)
                    {
                        Settings[nameof(Player)] =  Player.EntityName.ToString();
                        
                        Player.SetEntityName(Extras.ScrambleString(Player.EntityName));

                    }
                }


                //////if (Input.GetKeyDown(KeyCode.O)) //infinity ammo ???
                //////{
                //////    if (!O.ELP)
                //////    {
                //////        return;
                //////    }

                //////    Inventory inventory = O.ELP.inventory;

                //////    if (inventory != null)
                //////    {
                //////        ItemActionAttack gun = inventory.GetHoldingGun();

                //////        if (gun != null)
                //////        {
                //////            gun.InfiniteAmmo = !gun.InfiniteAmmo;

                //////        }
                //////    }
                //////}//infinity ammo

                //////if (Input.GetKeyDown(KeyCode.F9)) //checks if the key is being pressed. if it does execute F9 is empty in game
                //////{
                //////    // we can put cheat here

                //////}


                ////////if ((bool)Settings["bool_CreativeMode"] || !(bool)Settings["bool_CreativeMode"]) //Toggle for ingame Creative and Debug Working like a sharm
                ////////{
                ////////    CmDm((bool)Settings["bool_CreativeMode"]);
                ////////}
                ////////if (RB["_isEditmode"] || !RB["_isEditmode"]) //Toggle for ingame Creative and Debug Working like a sharm
                ////////{
                ////////    editMode();
                ////////}



                //here should i init the cheatbuff!! it can only happen if game and player exists! 





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
        void Awake()
        {
            Debug.LogWarning($"Awake: {nameof(Cheat)}");
        }






















        private static string EscapeForCsv(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }

            // If the value contains a comma or a double quote, enclose it in double quotes and escape any existing double quotes
            if (value.Contains(',') || value.Contains('"'))
            {
                return $"\"{value.Replace("\"", "\"\"")}\"";
            }

            return value;
        }
        //public static void LogProgressionClassToFile(string filePath)
        //{
        //    if (_listProgressionValue != null && _listProgressionValue.Count > 0)
        //    {
        //        try
        //        {
        //            using (StreamWriter writer = new StreamWriter(filePath))
        //            {
        //                writer.WriteLine("name,type,sortorder");

        //                foreach (var prog in _listProgressionValue)
        //                {

        //                    string str1 = prog.Name;
        //                    string str2 = prog.ProgressionClass.Type.ToString();
        //                    string str3 = prog.ProgressionClass.ListSortOrder.ToString();


        //                    //writer.WriteLine($"{EscapeForCsv(buffClass.Name)},{buffClass.DamageType},{EscapeForCsv(buffClass.NameTag.ToString())}");
        //                    writer.WriteLine($"{EscapeForCsv(str1)},{EscapeForCsv(str2)},{EscapeForCsv(str3)}");
        //                }
        //            }

        //            Console.WriteLine("progressionclasses as been logged");
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine($"Error occurred while logging buff classes: {ex.Message}");
        //        }
        //    }

        //}
        private static void LogListBuffClass(string filePath)
        {
            if (NewSettings.ListBuffClasses != null && NewSettings.ListBuffClasses.Count > 0)
            {
                try
                {
                    using (StreamWriter writer = new StreamWriter(filePath))
                    {
                        writer.WriteLine("Buff Name,Damage Type,Description");

                        foreach (var buffClass in NewSettings.ListBuffClasses)
                        {

                            string str1 = buffClass.DamageSource.ToString();
                            string str2 = buffClass.Effects.EffectGroups[0].ToString();


                            //writer.WriteLine($"{EscapeForCsv(buffClass.Name)},{buffClass.DamageType},{EscapeForCsv(buffClass.NameTag.ToString())}");
                            writer.WriteLine($"{EscapeForCsv(buffClass.Name)},,");
                        }
                    }

                    Console.WriteLine("Buff classes have been logged to the file.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error occurred while logging buff classes: {ex.Message}");
                }
            }
        }













        //--------------------------------------------------------------------------------------------------------


        //public static EntityAlive Entity;


        //public static void Trader()
        //{
        //    if (O.Etrader != null && O.ELP)
        //    {
        //        //if (O.Etrader.aiClosestPlayer.ToString().ToLower() == O.ELP.name.ToString().ToLower())
        //        //{
        //            O.Etrader.IsDancing = true;
        //            ulong OCTime = 0;
        //            O.Etrader.TraderInfo.CloseTime = OCTime;
        //            O.Etrader.TraderInfo.OpenTime = OCTime;

        //        //}

        //    }
        //    else if (O.Etrader == null) 
        //    {

        //        RB["_EtraderOpen"] = false;
        //    }
        //}
        //public static void InstantQuestFinish()
        //{
        //    if (RB["_QuestComplete"] == true && O.ELP)
        //    {
        //        foreach (Quest quest in O.ELP.QuestJournal.quests)
        //        {


        //            //QuestClass.Category == "Challenge"
        //            if ((quest.Tracked==true||quest.Active==true) && quest.CurrentState == Quest.QuestState.InProgress) 
        //            {

        //                quest.CurrentState = Quest.QuestState.ReadyForTurnIn;
        //            }
        //        }
        //    }
        //}
        //public static void LoopLASTQuestRewards()
        //{
        //    if ((bool)Settings[nameof(Quest.QuestState.Completed)] == true && Player)
        //    {
        //        if (Player.QuestJournal.quests.Count > 0)
        //        {
        //            int lastIndex = Player.QuestJournal.quests.Count - 1;

        //            Quest lastQuest = Player.QuestJournal.quests[lastIndex];

        //            if (lastQuest.CurrentState == Quest.QuestState.Completed )
        //            {
        //                lastQuest.CurrentState = Quest.QuestState.ReadyForTurnIn;
        //                Debug.LogWarning($" {lastQuest.ID} is ready for turn in");
        //            }
        //        }
        //        else
        //        {
        //            // The list is empty, handle this case accordingly
        //        }
        //    }
        //}


    }
}


