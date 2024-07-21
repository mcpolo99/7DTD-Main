using System;
using UnityEngine;


namespace SevenDTDMono.GuiLayoutExtended
{

    /// <summary>
    /// Think this is so far done. not much more to modify
    /// </summary>
    public partial class NewGUILayout
    {
        /// <summary>
        /// A Folding menu that will expand on click
        /// Using a dictionary to keep track of if it is open or not
        /// </summary>
        /// <param name="label">Display Label on the menu button </param>
        /// <param name="boolKey"> String of bool key for dictionary</param>
        /// <param name="onClickAction">Input actions to execute on expanded</param>
        /// <param name="width">The Width of the button?</param>
        /// <param name="options">GUI Layout Option</param>
        /// <returns>a toggled bool</returns>
        public static bool DictFoldMenuHorizontal(string label, string boolKey, Action onClickAction, float width, params GUILayoutOption[] options)
        {
            //dictionary check

            if (!SettingsInstance.CheckBoolKeyExist(boolKey))
            {
                return false;
            }

            // Retrieve the current value from dictionary
            bool toggle = _boolDict[boolKey];

            #region Style

            // Define Button style
            GUIStyle buttonStyle = new GUIStyle(GUI.skin.button)
            {
                alignment = TextAnchor.MiddleCenter,
                fontSize = 15,
            };
            
            // Adjust header style based on toggle state
            if (toggle)
            {
                buttonStyle.fontStyle = FontStyle.Bold;
                buttonStyle.normal.textColor = Color.green;
            }
            else
            {
                buttonStyle.fontStyle = FontStyle.Italic;
                buttonStyle.normal.textColor = Color.yellow;
            }
            buttonStyle.onActive.textColor = Color.blue; //THIS IS TEST ONLY

            #endregion

            // Draw header button
            Rect headerRect = GUILayoutUtility.GetRect(width, 20f, buttonStyle);
            bool isClicked = GUI.Button(headerRect, label, buttonStyle);
            float lineHeight = 10f;

            //draw Enabled line 
            Rect lineRect = new Rect(headerRect.x, headerRect.y + (headerRect.height - lineHeight) * 0.5f, 30, lineHeight);
            DrawLine(lineRect, toggle ? Color.green : Color.yellow); 

            // Handle click event
            if (isClicked)
            {
                _boolDict[boolKey] = !toggle; // Toggle the bool value when the button is clicked
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

        /// <summary>
        /// A Folding menu that will expand on click
        /// Using a dictionary to keep track of if it is open or not
        /// </summary>
        /// <param name="label">Display Label on the menu button </param>
        /// <param name="boolKey">String of bool key for dictionary</param>
        /// <param name="onClickAction">Input actions to execute on expanded</param>
        /// <param name="options">GUI Layout Option</param>
        /// <returns>a toggled bool</returns>
        public static bool DictFoldMenuHorizontal(string label, string boolKey, Action onClickAction, params GUILayoutOption[] options)
        {
            //dictionary check

            if (!SettingsInstance.CheckBoolKeyExist(boolKey))
            {
                return false;
            }

            // Retrieve the current value from dictionary
            bool toggle = _boolDict[boolKey];

            #region Style
            // Define button style
            GUIStyle buttonStyle = new GUIStyle(GUI.skin.button)
            {
                alignment = TextAnchor.MiddleCenter,
                fontSize = 15,
                //padding = new RectOffset(0, 0, 0, 0),
                margin = new RectOffset(0, 0, 0, 0)
            };
            // Adjust header style based on toggle state
            if (toggle)
            {
                buttonStyle.fontStyle = FontStyle.Bold;
                buttonStyle.normal.textColor = Color.green;
            }
            else
            {
                buttonStyle.fontStyle = FontStyle.Italic;
                buttonStyle.normal.textColor = Color.yellow;
            }

            //define the background in which all the controls is being displayed

            GUIStyle backgroundBoxStyle = new GUIStyle(GUI.skin.box)
            {
                padding = new RectOffset(-0, -0, -0, -0)
            };
            



            #endregion

            NewGUILayout.BeginVertical(backgroundBoxStyle, () =>
            {

                // Get rect of current location
                Rect buttonRect = GUILayoutUtility.GetRect(1, 30f, buttonStyle);
                //draw Button for toggle open 
                bool isClicked = GUI.Button(buttonRect, label, buttonStyle); //creates a button based on the buttonRect and buttonStyle still it keeps track of toggled value..


                //Draw line to display current toggle state
                float lineHeight = 10f;
                Rect lineRect = new Rect(buttonRect.x, buttonRect.y + (buttonRect.height - lineHeight) * 0.5f, 30, lineHeight);
                DrawLine(lineRect, toggle ? Color.green : Color.yellow);

                NewGUILayout.BeginVertical(() =>
                {
                    // Handle click event
                    if (isClicked)
                    {

                        //when button is clicked (isClicked) we set dictionary value to !toggle
                        _boolDict[boolKey] = !toggle; 
                    }

                    //if toggle = true we invoke onClickAction
                    if (toggle)
                    {
                        onClickAction?.Invoke();
                    }

                });

            }, options);
            return toggle; // Return the toggle state

        }
    }
}


/*


 /// <summary>
        /// A foldable menu that will expand on click
        /// Using a dictionary to keep track of if it is open or not
        /// </summary>
        /// <param name="label">Display Label on the button </param>
        /// <param name="boolKey"> String of bool key for dictionary (SHOULD NEVER BE 2 EQUAL!)</param>
        /// <param name="onClickAction">Input actions to execute </param>
        /// <param name="width">The Width of the button?</param>
        /// <param name="options">GUI Layout Option</param>
        /// <returns></returns>
        public static bool DictFoldMenuHorizontalTest(string label, string boolKey, Action onClickAction, float width, params GUILayoutOption[] options)
        {
            #region CheckDictionary for bools in BoolDictionary
            // Check if the key exists in the dictionary
            if (!_boolDict.ContainsKey(boolKey))
            {
                // Add the key with a default value of false if it does not exist
                _boolDict[boolKey] = false;
                //NewSettings.AddSetting(boolKey, false);
            }
            #endregion
            // Retrieve the current value
            bool toggle = _boolDict[boolKey];

            #region Style


            GUIStyle boxGuiStyle = new GUIStyle(GUI.skin.box)
            {
                //defBoxStyle.border = new RectOffset(-2,-2,-2,-2);
                padding = new RectOffset(0, 0, 0, 0),
                contentOffset = Vector2.zero
            };
            //boxGuiStyle.border = new RectOffset(0, 0, 0, 0);


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


            NewGUILayout.BeginVertical(boxGuiStyle, () =>
            {
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
                    _boolDict[boolKey] = !toggle; // Toggle the bool value when the button is clicked
                }

                // If toggled, draw the content
                if (toggle)
                {
                    onClickAction?.Invoke();
                    //NewGUILayout.BeginVertical(GUI.skin.box, () =>
                    //{
                    //    // Invoke the provided content drawing action
                    //    onClickAction?.Invoke();
                    //});
                }
            });
            return toggle; // Return the toggle state
        }
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




 
 */