using System;
using UnityEngine;


namespace SevenDTDMono.GuiLayoutExtended
{

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