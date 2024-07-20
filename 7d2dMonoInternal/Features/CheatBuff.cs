using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SevenDTDMono.Features
{
    public partial class Cheat
    {
        private static BuffClass _cheatBuff;
        private static MinEffectController _minEffectController;

        public static MinEffectController MinEffectController
        {
            get
            {
                if (_minEffectController == null)
                {
                    _minEffectController = new MinEffectController();
                    return _minEffectController;
                }
                return _minEffectController;
            }
        }



        private void InitCheatBuff()
        {
            if ((bool)Settings[nameof(_cheatBuff)] == false)
            {
                if (_cheatBuff == null)
                {
                    _cheatBuff = new BuffClass();
                    Debug.Log($"{_cheatBuff} as been init as a BuffClass() ");

                }
                else if (_cheatBuff != null)
                {
                    Debug.LogWarning($"{_cheatBuff.Name} begin init");


                }
                else
                {
                    Log.Out($"{_cheatBuff.Name} Has Not been init");

                }



                _cheatBuff = new BuffClass()
                {
                    Name = nameof(_cheatBuff),
                    DamageType = EnumDamageTypes.None,
                    durationMax = 9999999f,
                    Icon = "ui_game_symbol_agility",
                    ShowOnHUD = true,
                    Hidden = false,
                    IconColor = new Color(0.22f, 0.4f, 1f, 100f),
                    DisplayType = EnumEntityUINotificationDisplayMode.IconOnly,
                    LocalizedName = "Custom CheatBuff",
                    Description = "This is the buff that manages all modiefer values we add to the player,\n for every passive effect we adding we adding it to thsi Buffclass to avoid crashes and nullreferences \n " +
                                  "Also to avoid not being able to edit future values easier. \n i have not yet figured out how i can make the slider modifers realtime modifers or how to avoid passiveffects stacking  ",
                    Effects = MinEffectController

                };
                BuffManager.Buffs.Add(_cheatBuff.Name, _cheatBuff);
                Debug.LogWarning($"Buff {_cheatBuff.Name} has ben added to  "); //{_listBuffClass}
                Log.Out($"{_cheatBuff.Name} Has been initieted");

                MinEffectController.EffectGroups = new List<MinEffectGroup>
            {
                new MinEffectGroup
                {
                    OwnerTiered = true,
                    PassiveEffects = new List<PassiveEffect>
                    {
                        new PassiveEffect
                        {
                            MatchAnyTags = true,
                            Type = PassiveEffects.None,
                            Modifier = PassiveEffect.ValueModifierTypes.base_add,
                            Values = new float[] { 20}
                            //Set other properties of PassiveEffect if needed
                        }
                    },
                    PassivesIndex = new List<PassiveEffects>
                    {
                        PassiveEffects.None,
                    }
                }

            };
                MinEffectController.PassivesIndex = new HashSet<PassiveEffects>
            {
                PassiveEffects.None,
            };

                NewSettings.DictionaryBuffClassKeyCollection = BuffManager.Buffs.Keys;

                Debug.LogWarning($"{_cheatBuff.Name} finished init");

                Settings[nameof(_cheatBuff)] = true;
                int count2 = BuffManager.Buffs.Count;
                Debug.LogWarning($"amount of buffs after init {count2}");

            }
        }








    }
}
