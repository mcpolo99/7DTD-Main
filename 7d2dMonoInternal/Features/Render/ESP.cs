using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;


namespace SevenDTDMono.Features.Render

{
    public class ESP : MonoBehaviour {
        
        #region Defenitions
        public static Camera mainCam;

        private Color _blackCol;
        private Color _entityBoxCol;
        private Color _crossHairCol;

        private readonly float _crossHairScale = 14f;
        private readonly float _lineThickness = 1.75f;

        private static EntityPlayerLocal Player => NewSettings.EntityLocalPlayer;
        private static NewSettings SettingsInstance => NewSettings.Instance;
        private static Dictionary<string, bool> _boolDict = SettingsInstance.GetChildDictionary<bool>(nameof(Dictionaries.BOOL_DICTIONARY));
        private static Dictionary<string, object> Settings => NewSettings.Instance.SettingsDictionary; //get instance of SettingsDictionary

        #endregion

        private void Start() 
        {

            Debug.LogWarning($"Start: {nameof(ESP)}");



            #region local settings start
            // Camera.main is a very expensive getter, so we want to do it once and cache the result.
            mainCam = Camera.main;

            _blackCol = new Color(0f, 0f, 0f, 120f);
            _entityBoxCol = new Color(0.42f, 0.36f, 0.90f, 1f);
            _crossHairCol = new Color32(30, 144, 255, 255);
            #endregion
        }

        private void Update() 
        {
            if (NewSettings.GameManager.gameStateManager.bGameStarted==false && Player==null)
            {
                //if game is not started and player is null return
                return;
            }

            if (_boolDict[nameof(SettingsBools.ZOMBIE_CORNER_BOX)]) 
            {
                _boolDict[nameof(SettingsBools.ZOMBIE_BOX)] = false;
            } 
            else if (SettingsInstance.GetBoolValue(nameof(SettingsBools.ZOMBIE_BOX)) && SettingsInstance.GetBoolValue(nameof(SettingsBools.ZOMBIE_CORNER_BOX))) 
            {
                _boolDict[nameof(SettingsBools.ZOMBIE_CORNER_BOX)] = false;
            }

            if (SettingsInstance.GetBoolValue(nameof(SettingsBools.PLAYER_CORNER_BOX))) 
            {
                _boolDict[nameof(SettingsBools.PLAYER_BOX)] = false;
            } 
            else if (_boolDict[nameof(SettingsBools.PLAYER_CORNER_BOX)]  && _boolDict[nameof(SettingsBools.PLAYER_BOX)]) 
            {
                _boolDict[nameof(SettingsBools.PLAYER_CORNER_BOX)] = false; ;
            }

            if (Player)
            {
                Player.weaponCrossHairAlpha = _boolDict[nameof(SettingsBools.CROSS_HAIR)] ? 0f : 255f;
            }
        }

        private void OnGUI() 
        {

            // Run once per frame.
            if (Event.current.type != EventType.Repaint) {
                //do not really know what this is. 
                return;
            }

            if (!mainCam) {
                mainCam = Camera.main;
            }

            if (NewSettings.GameManager.gameStateManager.bGameStarted == false && Player == null)
            {
                //if game is not started and player is null return
                return;
            }


          
            if (NewSettings.EntityAlive.Count > 0 && (_boolDict[nameof(SettingsBools.ZOMBIE_NAME)] || _boolDict[nameof(SettingsBools.ZOMBIE_CORNER_BOX)] || _boolDict[nameof(SettingsBools.ZOMBIE_BOX)] || (bool)_boolDict[nameof(SettingsBools.ZOMBIE_HEALTH)]) ) 
            {

                foreach (EntityAlive zombie in NewSettings.EntityAlive) 
                {
                    if (!zombie || !zombie.IsAlive()) {
                        continue;
                    }

                    Vector3 w2s = mainCam.WorldToScreenPoint(zombie.transform.position);

                    if (RenderUtils.IsOnScreen(w2s)) {
                        Vector3 w2sHead = mainCam.WorldToScreenPoint(zombie.emodel.GetHeadTransform().position);
                        
                        float height = Mathf.Abs(w2sHead.y - w2s.y);
                        float x = w2s.x - height * 0.3f /* Shift the box to the left a bit. */;
                        float y = Screen.height - w2sHead.y;


                        if (_boolDict[nameof(SettingsBools.ZOMBIE_NAME)])
                        {
                            RenderUtils.DrawString(new Vector2(w2s.x, Screen.height - w2s.y + 8f/* Extra spacing below the box esp. */),
                                zombie.EntityName.Replace("zombie", "Zombie_"), Color.red, true, 12, FontStyle.Normal);
                        }

                        if (_boolDict[nameof(SettingsBools.ZOMBIE_BOX)]) 
                        {
                            RenderUtils.OutlineBox(new Vector2(x - 1f, y - 1f), new Vector2((height / 2f) + 2f, height + 2f), _blackCol);
                            RenderUtils.OutlineBox(new Vector2(x, y), new Vector2(height / 2f, height), _entityBoxCol);
                            RenderUtils.OutlineBox(new Vector2(x + 1f, y + 1f), new Vector2((height / 2f) - 2f, height - 2f), _blackCol);
                        }
                        else if (_boolDict[nameof(SettingsBools.ZOMBIE_CORNER_BOX)]) 
                        {
                            RenderUtils.CornerBox(new Vector2(w2sHead.x, y ), height / 2f, height, 2f, _entityBoxCol, true);
                        }
                        
                        if (_boolDict[nameof(SettingsBools.ZOMBIE_HEALTH)]) {
                            float health = zombie.Health;
                            int maxHealth = zombie.GetMaxHealth();
                            float percentage = health / maxHealth;
                            float barHeight = height * percentage;

                            Color barColour = RenderUtils.GetHealthColour(health, maxHealth);

                            RenderUtils.RectFilled(x - 5f, y, 4f, height, _blackCol);
                            RenderUtils.RectFilled(x - 4f, y + height - barHeight - 1f, 2f, barHeight, barColour);
                        }
                    }
                }
            }//some zombie check 

            if (NewSettings.EntityPlayers.Count > 1 && (_boolDict[nameof(SettingsBools.PLAYER_NAME)] || _boolDict[nameof(SettingsBools.PLAYER_BOX)] || _boolDict[nameof(SettingsBools.PLAYER_HEALTH)])) {
                foreach (EntityPlayer player in NewSettings.EntityPlayers) {
                    if (!player || player == Player || !player.IsAlive()) {
                        continue;
                    }

                    Vector3 w2s = mainCam.WorldToScreenPoint(player.transform.position);

                    if (RenderUtils.IsOnScreen(w2s)) {
                        Vector3 w2sHead = mainCam.WorldToScreenPoint(player.emodel.GetHeadTransform().position);

                        float height = Mathf.Abs(w2sHead.y - w2s.y);
                        float x = w2s.x - height * 0.3f ;
                        float y = Screen.height - w2sHead.y;

                        if (_boolDict[nameof(SettingsBools.PLAYER_BOX)]) {
                            RenderUtils.OutlineBox(new Vector2(x - 1f, y - 1f), new Vector2((height / 2f) + 2f, height + 2f), _blackCol);
                            RenderUtils.OutlineBox(new Vector2(x, y), new Vector2(height / 2f, height), _entityBoxCol);
                            RenderUtils.OutlineBox(new Vector2(x + 1f, y + 1f), new Vector2((height / 2f) - 2f, height - 2f), _blackCol);
                        }
                        else if (_boolDict[nameof(SettingsBools.PLAYER_CORNER_BOX)]) {
                            RenderUtils.CornerBox(new Vector2(w2sHead.x, y), height / 2f, height, 2f, _entityBoxCol, true);
                        }

                        if (_boolDict[nameof(SettingsBools.PLAYER_NAME)]) {
                            RenderUtils.DrawString(new Vector2(w2s.x, Screen.height - w2s.y + 8f),
                            player.EntityName, Color.red, true, 12, FontStyle.Normal);
                        }

                        if (_boolDict[nameof(SettingsBools.PLAYER_HEALTH)]) {
                            float health = player.Health;
                            int maxHealth = player.GetMaxHealth();
                            float percentage = health / maxHealth;
                            float barHeight = height * percentage;

                            Color barColour = RenderUtils.GetHealthColour(health, maxHealth);

                            RenderUtils.RectFilled(x - 5f, y, 4f, height, _blackCol);
                            RenderUtils.RectFilled(x - 4f, y + height - barHeight - 1f, 2f, barHeight, barColour);
                        }
                    }
                }
            }//some Player check
        }


    }
}
