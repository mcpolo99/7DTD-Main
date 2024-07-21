using SevenDTDMono.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static PassiveEffect;

namespace SevenDTDMono.GuiLayoutExtended
{
    public partial class NewGUILayout
    {
        /// <summary>
        /// Scroll View
        /// </summary>
        /// <param name="scrollKey">Name of the vector2 key in Dictionary</param>
        /// <param name="scrollViewContent"></param>
        public static void BeginScrollView(string scrollKey, Action scrollViewContent)
        {
            // Check if the key exists in the dictionary
            if (SettingsInstance.CheckVector2ZeroExist(scrollKey))
            {
                // Retrieve scroll position from the dictionary
                Vector2 scrollPosition = SettingsInstance.GetVector2(scrollKey);

                // Begin scroll view with the retrieved scroll position and options
                scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.MinHeight(250f), GUILayout.MaxHeight(350f));

                // Update scroll position in the dictionary
                SettingsInstance.SetVector2(scrollKey, scrollPosition);

                // Invoke the provided content for the scroll view
                scrollViewContent?.Invoke();

                // End scroll view
                GUILayout.EndScrollView();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="scrollKey">Name of the vector2 key in Dictionary</param>
        /// <param name="scrollViewContent">Content within the scroll view</param>
        /// <param name="option">GUI Layout Option</param>
        public static void BeginScrollView(string scrollKey, Action scrollViewContent, params GUILayoutOption[] option)
        {


            if (SettingsInstance.CheckVector2ZeroExist(scrollKey))
            {  // Retrieve scroll position from the dictionary
                Vector2 scrollPosition = SettingsInstance.GetVector2(scrollKey);
                // Begin scroll view with the retrieved scroll position and options
                scrollPosition = GUILayout.BeginScrollView(scrollPosition, option);
                // Update scroll position in the dictionary
                SettingsInstance.SetVector2(scrollKey, scrollPosition);
                // Invoke the provided content for the scroll view
                scrollViewContent?.Invoke();

                // End scroll view
                GUILayout.EndScrollView();
            }
        }
        /// <summary>
        /// Horizontal scrollbar.
        /// When button is pressed: execute onClickAction
        /// When slider is moved modify reference float value.
        /// </summary>
        /// <param name="label"></param>
        /// <param name="boolKey"></param>
        /// <param name="modifier"></param>
        /// <param name="rightMaxValue"></param>
        /// <param name="onClickAction"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static bool HorizontalScrollbarWithLabelAndButton(string label, string boolKey, ref float modifier, float rightMaxValue, Action onClickAction = null, params GUILayoutOption[] options)
        {
            #region Dictionary Check

            if (!SettingsInstance.CheckBoolKeyExist(boolKey))
            {
                return false;
            }
            // Retrieve the current value from dictionary
            bool toggle = _boolDict[boolKey];

            #endregion
            #region Styles

            GUIStyle buttonStyle = new GUIStyle(GUI.skin.button)
            {

                alignment = TextAnchor.MiddleCenter,
                fontSize = 13,
                padding = new RectOffset(0, 0, 0, 0), //changes posision of text in button.
                margin = new RectOffset(0, 0, 0, 0), //changes margin to next element here
                fontStyle = toggle ? FontStyle.Bold : FontStyle.Normal,
                active = new GUIStyleState()
                {
                    textColor = toggle ? Color.blue : Color.magenta
                }

            };
            buttonStyle.normal.textColor = toggle ? Color.green : Color.yellow;
            GUIStyle scrollStyle = new GUIStyle(GUI.skin.horizontalScrollbar)
            {
                alignment = TextAnchor.MiddleCenter,
                fontSize = 13,
                padding = new RectOffset(0, 0, 0, 0),
                margin = new RectOffset(20, 0, 0, 0)
            };
            GUIStyle labelStyle = new GUIStyle(GUI.skin.label)
            {
                alignment = TextAnchor.MiddleCenter,
                fontSize = 13,
                padding = new RectOffset(0, 0, 0, 0),
                margin = new RectOffset(0, 0, 0, 0),
                stretchWidth = true
            };


            #endregion

            GUILayout.BeginHorizontal();
            // Create button with rect size
            Rect buttonRect = GUILayoutUtility.GetRect(0, 15f, buttonStyle, GUILayout.MaxWidth(130));
            bool isClicked1 = GUI.Button(buttonRect, label, buttonStyle);

            modifier = GUILayout.HorizontalScrollbar(modifier, 0f, 0f, rightMaxValue, scrollStyle);
            GUILayout.Label(modifier.ToString("F1"), labelStyle, GUILayout.MaxWidth(50));

            if (isClicked1)
            {
                //in here we write if we want to execute once
                _boolDict[boolKey] = !toggle; // Toggle the bool value when the button is clicked
                onClickAction?.Invoke();
            }
            if (toggle)
            {
                //in here we can write if we want to update each frame
            }
            GUILayout.EndHorizontal();

            return isClicked1;
        }


    }
}