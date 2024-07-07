using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using XInputDotNetPure;

namespace SevenDTDMono.GuiLayoutExtended
{
    public partial class NewGUILayout
    {
        public static bool FoldableMenuHorizontal(bool display, string label, System.Action content, float width, params GUILayoutOption[] options)
        {

            GUIStyle headerStyle = new GUIStyle(GUI.skin.box);
            headerStyle.alignment = TextAnchor.MiddleCenter;
            headerStyle.fontSize = 15;
            if (display ? true : false)
            {
                headerStyle.fontStyle = FontStyle.Bold;
                headerStyle.normal.textColor = Color.green;
            }
            else
            {
                headerStyle.fontStyle = FontStyle.Italic;
                headerStyle.normal.textColor = Color.yellow;
            }
            Rect headerRect = GUILayoutUtility.GetRect(width, 20f, headerStyle); // Pass the width value here
            Rect toggleRect = new Rect(headerRect.y, headerRect.x, 10f, 20f);

            float lineHeight = 10f;
            Rect lineRect = new Rect(headerRect.x, headerRect.y + (headerRect.height - lineHeight) * 0.5f, 30, lineHeight);
            DrawLine(lineRect, display ? Color.green : Color.yellow);

            GUI.Box(headerRect, label, headerStyle);
            Event e = Event.current;
            if (e.type == EventType.Repaint)
            {
                //DrawFoldoutHorizontal1(toggleRect, display);
            }

            if (e.type == EventType.MouseDown && headerRect.Contains(e.mousePosition))
            {
                display = !display;
                e.Use();
            }

            if (display)
            {
                content?.Invoke();
            }

            return display;
        }

        public static bool FoldableMenuHorizontal1(string label, bool display, string boolKey, System.Action content, float width, params GUILayoutOption[] options)
        {
            #region CheckDictionary
            // Check if the key exists in the dictionary
            if (!NewSettings.Instance.SettingsDictionary.ContainsKey(boolKey))
            {
                // Add the key with a default value of false if it does not exist
                NewSettings.Instance.SettingsDictionary[boolKey] = false;
                //NewSettings.AddSetting(boolKey, false);
            }
            // Ensure the value associated with the key is a bool
            if (!(NewSettings.Instance.SettingsDictionary[boolKey] is bool))
            {
                Debug.LogError($"Key '{boolKey}' is not a boolean.");
                return false;
            }

            #endregion

            bool toggle = (bool)NewSettings.Instance.SettingsDictionary[boolKey];


            GUIStyle headerStyle = new GUIStyle(GUI.skin.box);
            headerStyle.alignment = TextAnchor.MiddleCenter;
            headerStyle.fontSize = 15;
            if (display ? true : false)
            {
                headerStyle.fontStyle = FontStyle.Bold;
                headerStyle.normal.textColor = Color.green;
            }
            else
            {
                headerStyle.fontStyle = FontStyle.Italic;
                headerStyle.normal.textColor = Color.yellow;
            }



            Rect headerRect = GUILayoutUtility.GetRect(width, 20f, headerStyle); // Pass the width value here
            //Rect toggleRect = new Rect(headerRect.y, headerRect.x, 10f, 20f);

            float lineHeight = 10f;
            Rect lineRect = new Rect(headerRect.x, headerRect.y + (headerRect.height - lineHeight) * 0.5f, 30, lineHeight);
            DrawLine(lineRect, display ? Color.green : Color.yellow);

            GUI.Box(headerRect, label, headerStyle);
            Event e = Event.current;
            if (e.type == EventType.Repaint)
            {
                //DrawFoldoutHorizontal1(toggleRect, display);
            }

            if (e.type == EventType.MouseDown && headerRect.Contains(e.mousePosition))
            {
                display = !display;
                e.Use();
            }

            if (display)
            {
                content?.Invoke();
            }

            return display;
        }

        public static bool DictMenuHorizonta(string label, string boolKey,  Action onClickAction , float width, params GUILayoutOption[] options)
        {
            #region CheckDictionary
            // Check if the key exists in the dictionary
            if (!Settings.ContainsKey(boolKey))
            {
                // Add the key with a default value of false if it does not exist
                Settings[boolKey] = false;
                //NewSettings.AddSetting(boolKey, false);
            }
            // Ensure the value associated with the key is a bool
            if (!(Settings[boolKey] is bool))
            {
                Debug.LogError($"Key '{boolKey}' is not a boolean.");
                return false;
            }

            #endregion
            // Retrieve the current value
            bool toggle = (bool)Settings[boolKey];

            #region Style

            GUIStyle headerStyle = new GUIStyle(GUI.skin.button);
            headerStyle.alignment = TextAnchor.MiddleCenter;
            headerStyle.fontSize = 15;
            if (toggle ? true : false)
            {
                headerStyle.fontStyle = FontStyle.Bold;
                headerStyle.normal.textColor = Color.green;
            }
            else
            {
                headerStyle.fontStyle = FontStyle.Italic;
                headerStyle.normal.textColor = Color.yellow;
            }

            #endregion
            Rect headerRect = GUILayoutUtility.GetRect(300f, 20f, headerStyle); // Pass the width value here
            float lineHeight = 10f;
            Rect lineRect = new Rect(headerRect.x, headerRect.y + (headerRect.height - lineHeight) * 0.5f, 30, lineHeight);
            DrawLine(lineRect, toggle ? Color.green : Color.yellow);
            GUI.Box(headerRect, label, headerStyle);
            GUI.Button(headerRect, label, headerStyle);





            // Create the toggle button using the current value
            bool isClicked = NewGUILayout.Button(label, (bool)Settings[boolKey], onClickAction,options);






            Event e = Event.current;
            if (e.type == EventType.Repaint)
            {
                //DrawFoldoutHorizontal1(toggleRect, display);
            }

            if (e.type == EventType.MouseDown && headerRect.Contains(e.mousePosition))
            {
                toggle = !toggle;
                e.Use();
            }


            if (isClicked)
            {
                Settings[boolKey] = !toggle; // Toggle the bool value when the button is clicked
                // Invoke additional action if provided
                //content?.Invoke();
                onClickAction?.Invoke();
            }
            return !toggle;
        }
        public static bool DictFoldMenuHorizontal(string label, string boolKey, Action onClickAction, float width, params GUILayoutOption[] options)
        {
            #region CheckDictionary
            // Check if the key exists in the dictionary
            if (!Settings.ContainsKey(boolKey))
            {
                // Add the key with a default value of false if it does not exist
                Settings[boolKey] = false;
                //NewSettings.AddSetting(boolKey, false);
            }
            // Ensure the value associated with the key is a bool
            if (!(Settings[boolKey] is bool))
            {
                Debug.LogError($"Key '{boolKey}' is not a boolean.");
                return false;
            }

            #endregion
            // Retrieve the current value
            bool toggle = (bool)Settings[boolKey];

            #region Style

            // Define header style
            GUIStyle headerStyle = new GUIStyle(GUI.skin.button);
            headerStyle.alignment = TextAnchor.MiddleCenter;
            headerStyle.fontSize = 15;

            // Adjust header style based on toggle state
            if (toggle)
            {
                headerStyle.fontStyle = FontStyle.Bold;
                headerStyle.normal.textColor = Color.green;
            }
            else
            {
                headerStyle.fontStyle = FontStyle.Italic;
                headerStyle.normal.textColor = Color.yellow;
            }


            #endregion

            // Draw header button
            Rect headerRect = GUILayoutUtility.GetRect(width, 20f, headerStyle);
            bool isClicked = GUI.Button(headerRect, label, headerStyle);
            float lineHeight = 10f;

            //draw Enabled line
            Rect lineRect = new Rect(headerRect.x, headerRect.y + (headerRect.height - lineHeight) * 0.5f, 30, lineHeight);
            DrawLine(lineRect, toggle ? Color.green : Color.yellow);

            // Handle click event
            if (isClicked)
            {
                Settings[boolKey] = !toggle; // Toggle the bool value when the button is clicked
            }

            // If toggled, draw the content
            if (toggle)
            {
                NewGUILayout.BeginVertical(GUI.skin.box, () =>
                {
                    // Invoke the provided content drawing action
                    onClickAction?.Invoke();
                });
            }

            return toggle; // Return the toggle state
        }

    }
}
