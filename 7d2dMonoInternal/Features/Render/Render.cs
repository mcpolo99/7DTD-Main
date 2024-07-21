using UnityEngine;

using System.Collections.Generic;

namespace SevenDTDMono.Features.Render
{
    internal class Render : MonoBehaviour
    {


        #region Local settings
        private static Camera _mainCam;



        private Color _colorBlack;
        private Color _entityBoxColor;
        private Color _crossColor;
        private readonly float _crossScale = 14f;
        private readonly float _crossLineThickness = 1.75f;
        //private static Dictionary<string, object> Settings => NewSettings.Instance.SettingsDictionary; //get instance of SettingsDictionary
        private static NewSettings SettingsInstance => NewSettings.Instance;



        #endregion




        private void Start()
        {
            Debug.LogWarning($"Start: {nameof(Render)}");
            // Camera.main is a very expensive getter, so we want to do it once and cache the result.
            _mainCam = Camera.main;

            _colorBlack = new Color(0f, 0f, 0f, 120f);
            _entityBoxColor = new Color(0.42f, 0.36f, 0.90f, 1f);
            _crossColor = new Color32(30, 144, 255, 255);
        }

        private void OnGUI()
        {

            if (NewSettings.GameManager.gameStateManager.bGameStarted == false && NewSettings.EntityLocalPlayer == null)
            {
                //if game is not started and player is null return
                return;
            }
            if (Event.current.type != EventType.Repaint)
            {
                return;
            }

            if (!_mainCam)
            {
                _mainCam = Camera.main;
            }


            if (SettingsInstance.GetBoolValue(nameof(SettingsBools.CROSS_HAIR))) //crosshair Function
            {
                // Constantly redefining these vectors so that you can change your resolution and the crosshair will still be in the middle.
                Vector2 lineHorizontalStart = new Vector2(Screen.width / 2 - _crossScale, Screen.height / 2);
                Vector2 lineHorizontalEnd = new Vector2(Screen.width / 2 + _crossScale, Screen.height / 2);

                Vector2 lineVerticalStart = new Vector2(Screen.width / 2, Screen.height / 2 - _crossScale);
                Vector2 lineVerticalEnd = new Vector2(Screen.width / 2, Screen.height / 2 + _crossScale);

                RenderUtils.DrawLine(lineHorizontalStart, lineHorizontalEnd, _crossColor, _crossLineThickness);
                RenderUtils.DrawLine(lineVerticalStart, lineVerticalEnd, _crossColor, _crossLineThickness);
            }
            if (SettingsInstance.GetBoolValue(nameof(SettingsBools.FOV_CIRCLE)))
            {
                // Outline
                RenderUtils.DrawCircle(Color.black, new Vector2(Screen.width / 2, Screen.height / 2), 149f);
                RenderUtils.DrawCircle(Color.black, new Vector2(Screen.width / 2, Screen.height / 2), 151f);

                RenderUtils.DrawCircle(new Color32(30, 144, 255, 255), new Vector2(Screen.width / 2, Screen.height / 2), 150f);
            }




        }




    }
}
