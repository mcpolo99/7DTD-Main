
using SevenDTDMono.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;
using WorldGenerationEngineFinal;
using static PassiveEffect;
using static Setting;


namespace SevenDTDMono.GuiLayoutExtended
{
   
    public partial class NewGUILayout
    {

        public static bool ButtonBoolToggle(string buttonText, bool boolValue)
        {
            //Debug.LogWarning($"ButtonBoolToggle {boolValue}");
            // Set the GUI color based on the boolean value
            GUI.color = boolValue ? Color.green : Color.white;

            // Create a button with the given text
            if (GUILayout.Button(buttonText))
            {
                // Toggle the boolean value when the button is clicked
                boolValue = !boolValue;
            }

            // Reset the GUI color to default
            GUI.color = Color.white;

            //Debug.LogWarning($"ButtonBoolToggle {boolValue}");
            // Return the updated boolean value
            return boolValue;
        }

        public static bool BoolButton(string label, string propertyName, Action onClickAction = null, params GUILayoutOption[] options)
        {
            Type settingsType = typeof(NewSettings);
            var property = settingsType.GetProperty(propertyName);

            if (property == null || property.PropertyType != typeof(bool))
            {
                Debug.LogError($"Property '{propertyName}' not found or is not of type bool in Settings.");
                return false;
            }

            bool currentValue = (bool)property.GetValue(NewSettings.Instance);

            // Create the toggle button using the current value
            bool isClicked = Button(label, currentValue , onClickAction, options);

            // Toggle the property value if the button is clicked
            if (isClicked)
            {
                bool newValue = !currentValue;
                property.SetValue(NewSettings.Instance, newValue);
                currentValue = newValue;

                // Invoke additional action if provided
                onClickAction?.Invoke();
            }

            return currentValue;
        }


        //public static bool Button(string label, bool toggle, Action onClickAction = null, params GUILayoutOption[] options)
        //{
        //    GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
        //    buttonStyle.normal.textColor = toggle ? Active : Inactive;
        //    buttonStyle.active.textColor = Active;
        //    buttonStyle.hover.textColor = Hover;

        //    //GUI.color = toggle ? Color.green : Color.white;



        //    bool isClicked = GUILayout.Button(label, buttonStyle, options);

        //    if (isClicked)
        //    {
        //        toggle = !toggle; // Toggle the bool value when the button is clicked

        //        if (onClickAction != null)
        //        {
        //            onClickAction.Invoke();
        //        }
        //    }

        //    return isClicked;
        //}
        //public static bool Button(string label, Action onClickAction, params GUILayoutOption[] options)
        //{
        //    GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
        //    buttonStyle.normal.textColor = Inactive;
        //    buttonStyle.active.textColor = Active;
        //    buttonStyle.hover.textColor = Hover;

        //    bool isClicked = GUILayout.Button(label, buttonStyle, options);

        //    if (isClicked && onClickAction != null)
        //    {
        //        onClickAction.Invoke();
        //    }

        //    return isClicked;
        //}

        //buttons with colorchange
        public static bool Button(ref bool buttonState, string label, Color active, Color inactive, Color hover, params GUILayoutOption[] options)
        {
            GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
            // Check if the button is currently active or inactive
            Color textColor = buttonState ? active : inactive;
            buttonStyle.normal.textColor = textColor;
            buttonStyle.hover.textColor = hover;
            buttonStyle.active.textColor = textColor;

            // Detect if the button is clicked
            bool isClicked = GUILayout.Button(label, buttonStyle, options);

            // Toggle the button state on each click // the buttonstate kan be used to toogle a bool on/off
            if (isClicked)
            {
                buttonState = !buttonState;
            }

            return isClicked;
        }
        public static bool Button(ref bool buttonState, string label, Color active, Color inactive, params GUILayoutOption[] options)
        {
            GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
            // Check if the button is currently active or inactive
            Color textColor = buttonState ? active : inactive;
            buttonStyle.normal.textColor = textColor;
            buttonStyle.hover.textColor = Hover;
            buttonStyle.active.textColor = textColor;

            // Detect if the button is clicked
            bool isClicked = GUILayout.Button(label, buttonStyle, options);

            // Toggle the button state on each click
            if (isClicked)
            {
                buttonState = !buttonState;
            }

            return isClicked;
        }
        public static bool Button(ref bool buttonState, string label, Color hover, params GUILayoutOption[] options)
        {
            GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
            // Check if the button is currently active or inactive
            Color textColor = buttonState ? Active : Inactive;
            buttonStyle.normal.textColor = textColor;
            buttonStyle.hover.textColor = hover;
            buttonStyle.active.textColor = textColor;

            // Detect if the button is clicked
            bool isClicked = GUILayout.Button(label, buttonStyle, options);

            // Toggle the button state on each click
            if (isClicked)
            {
                buttonState = !buttonState;
            }

            return isClicked;
        }
        public static bool Button(ref bool buttonState, string label, params GUILayoutOption[] options)
        {
            GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
            // Check if the button is currently active or inactive
            Color textColor = buttonState ? Active : Inactive;
            buttonStyle.normal.textColor = textColor;
            buttonStyle.hover.textColor = Hover;
            buttonStyle.active.textColor = textColor;

            // Detect if the button is clicked
            bool isClicked = GUILayout.Button(label, buttonStyle, options);

            // Toggle the button state on each click
            if (isClicked)
            {
                buttonState = !buttonState;
            }

            return isClicked;
        }

        //button as a trigger button, no toggle color just live  color
        public static bool Button(string label, Color inactive, Color active, Color hover, params GUILayoutOption[] options)
        {
            GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
            buttonStyle.normal.textColor = inactive;
            buttonStyle.active.textColor = active;
            buttonStyle.hover.textColor = hover;

            bool isClicked = GUILayout.Button(label, buttonStyle, options);
            return isClicked;
        }
        public static bool Button(string label, Color inactive, Color active, params GUILayoutOption[] options)
        {
            GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
            buttonStyle.normal.textColor = inactive;
            buttonStyle.active.textColor = active;
            buttonStyle.hover.textColor = Hover;

            bool isClicked = GUILayout.Button(label, buttonStyle, options);
            return isClicked;
        }
        public static bool Button(string label, params GUILayoutOption[] options)
        {
            GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
            buttonStyle.normal.textColor = Inactive;
            buttonStyle.active.textColor = Active;
            buttonStyle.hover.textColor = Hover;

            bool isClicked = GUILayout.Button(label, buttonStyle, options);
            return isClicked;
        }

        public static bool Button(string label, Action onClickAction, params GUILayoutOption[] options)
        {
            GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
            buttonStyle.normal.textColor = Inactive;
            buttonStyle.active.textColor = Active;
            buttonStyle.hover.textColor = Hover;

            bool isClicked = GUILayout.Button(label, buttonStyle, options);

            if (isClicked && onClickAction != null)
            {
                onClickAction.Invoke();
            }

            return isClicked;
        }
        public static bool Button(string label, Action onClickAction, ref bool toggle, params GUILayoutOption[] options)
        {
            GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
            buttonStyle.normal.textColor = toggle ? Active : Inactive;
            buttonStyle.active.textColor = Active;
            buttonStyle.hover.textColor = Hover;

            bool isClicked = GUILayout.Button(label, buttonStyle, options);

            if (isClicked && onClickAction != null)
            {
                onClickAction.Invoke();
                toggle = !toggle; // Toggle the bool value when the button is clicked
            }

            return isClicked;
        }
        public static bool Button(string label, ref bool toggle, Action onClickAction = null, params GUILayoutOption[] options)
        {
            GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
            buttonStyle.normal.textColor = toggle ? Active : Inactive;
            buttonStyle.active.textColor = Active;
            buttonStyle.hover.textColor = Hover;

            bool isClicked = GUILayout.Button(label, buttonStyle, options);

            if (isClicked)
            {
                toggle = !toggle; // Toggle the bool value when the button is clicked

                if (onClickAction != null)
                {
                    onClickAction.Invoke();
                }
            }

            return isClicked;
        }
        public static bool Button(string label, bool toggle, Action onClickAction = null, params GUILayoutOption[] options)
        {
            GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
            buttonStyle.normal.textColor = toggle ? Active : Inactive;
            buttonStyle.active.textColor = Active;
            buttonStyle.hover.textColor = Hover;

            bool isClicked = GUILayout.Button(label, buttonStyle, options);

            if (isClicked)
            {
                toggle = !toggle; // Toggle the bool value when the button is clicked

                if (onClickAction != null)
                {
                    onClickAction.Invoke();
                }
            }

            return isClicked;
        }

        public static bool SButton(string label, string boolKey, Action onClickAction = null, params GUILayoutOption[] options)
        {
            if (!SB.ContainsKey(boolKey))
            {
                SB[boolKey] = false;
            }
            bool toggle = SB.ContainsKey(boolKey) ? SB[boolKey] : false;
            bool isClicked = NewGUILayout.Button(label, SB[boolKey], onClickAction, options);
            if (isClicked)
            {
                SB[boolKey] = !toggle; // Toggle the bool value when the button is clicked
            }
            return SB[boolKey];
        }
        public static bool RButton(string label, string boolKey, Action onClickAction = null, params GUILayoutOption[] options)
        {

            bool toggle = RB.ContainsKey(boolKey) ? RB[boolKey] : false;
            bool isClicked = NewGUILayout.Button(label, RB[boolKey], onClickAction, options);
            if (isClicked)
            {
                RB[boolKey] = !toggle; // Toggle the bool value when the button is clicked
            }
            return RB[boolKey];
        }
        
        
        
        
        
        public static bool DictButton(string label, string boolKey, Action onClickAction = null, params GUILayoutOption[] options)
        {
            #region DictionaryCheck
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

            // Retrieve the current value
            bool toggle = (bool)NewSettings.Instance.SettingsDictionary[boolKey];

            // Create the toggle button using the current value
            bool isClicked = NewGUILayout.Button(label, (bool)NewSettings.Instance.SettingsDictionary[boolKey], onClickAction, options);

            // Toggle the value if the button is clicked
            if (isClicked)
            {
                NewSettings.Instance.SettingsDictionary[boolKey] = !toggle; // Toggle the bool value when the button is clicked
                // Invoke additional action if provided
                onClickAction?.Invoke();
            }
            return !toggle;
        }

        public static bool ButtonTrigger(string label, string boolKey, Action onClickAction = null, params GUILayoutOption[] options)
        {
            #region DictionaryCheck
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

            // Retrieve the current value
            bool toggle = (bool)NewSettings.Instance.SettingsDictionary[boolKey];

            // Create the toggle button using the current value
            bool isClicked = NewGUILayout.Button(label, (bool)NewSettings.Instance.SettingsDictionary[boolKey], onClickAction, options);

            // Toggle the value if the button is clicked
            if (isClicked)
            {
                NewSettings.Instance.SettingsDictionary[boolKey] = !toggle; // Toggle the bool value when the button is clicked
                // Invoke additional action if provided
                onClickAction?.Invoke();
            }
            return !toggle;
        }
        public static bool ButtonToggle(string label, string boolKey, Action onTrueAction , Action onFalseAction , params GUILayoutOption[] options)
        {
            //#region DictCheck
            //// Check if the key exists in the dictionary
            //if (!NewSettings.Instance.SettingsDictionary.ContainsKey(boolKey))
            //{
            //    // Add the key with a default value of false if it does not exist
            //    NewSettings.Instance.SettingsDictionary[boolKey] = false;
            //}

            //// Ensure the value associated with the key is a bool
            //if (!(NewSettings.Instance.SettingsDictionary[boolKey] is bool))
            //{
            //    Debug.LogError($"Key '{boolKey}' is not a boolean.");
            //    return false;
            //}
            //#endregion

            NewSettings.CheckDictionaryForKey(boolKey,false);
           // CheckDictionaryForKey(boolKey,false);


            // Retrieve the current value
            bool toggle = (bool)NewSettings.Instance.SettingsDictionary[boolKey];

            


            // Create the toggle button using the current value
            bool isClicked = NewGUILayout.Button(label, toggle, null, options);

            // Toggle the value if the button is clicked
            if (isClicked)
            {
                bool newToggle = !toggle;
                NewSettings.Instance.SettingsDictionary[boolKey] = newToggle; // Toggle the bool value when the button is clicked

                // Invoke the appropriate action based on the new toggle value
                if (newToggle)
                {
                    onTrueAction?.Invoke();
                }
                else
                {
                    onFalseAction?.Invoke();
                }
            }
            return !toggle;
        }













        public static bool Button(string label, Dictionary<string, bool> boolDictionary, string key, Action onClickAction = null, params GUILayoutOption[] options)
        {
            GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
            buttonStyle.normal.textColor = boolDictionary[key] ? Active : Inactive;
            buttonStyle.active.textColor = Active;
            buttonStyle.hover.textColor = Hover;

            bool isClicked = GUILayout.Button(label, buttonStyle, options);

            if (isClicked)
            {
                boolDictionary[key] = !boolDictionary[key]; // Toggle the bool value when the button is clicked

                if (onClickAction != null)
                {
                    onClickAction.Invoke();
                }
            }

            return isClicked;
        }

        public static bool Button(string label, ref int currentIndex, params GUILayoutOption[] buttonOptions)//Cycle enumlist item
        {
            // Display the button with the label and the current enum value as buttonText.
            bool buttonPressed = GUILayout.Button(label + " " + ((Enum)System.Enum.ToObject(typeof(ValueModifierTypes), currentIndex)).ToString(), buttonOptions);

            // If the button is pressed, increment the index and loop back to the beginning if needed.
            if (buttonPressed)
            {
                currentIndex = (currentIndex + 1) % System.Enum.GetValues(typeof(ValueModifierTypes)).Length;
            }

            // Return the buttonPressed state.
            return buttonPressed;
        }
        public static void Button(ref Dictionary<string, bool> buttonStates, string label, System.Action onClickAction = null, params GUILayoutOption[] options)
        {
            // Check if the button label exists in the dictionary, and if not, add it with the default value.
            if (!buttonStates.ContainsKey(label))
            {
                buttonStates[label] = false; // Set the default value for the new button.
            }

            GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
            buttonStyle.normal.textColor = buttonStates[label] ? Color.green : Color.red; // Set different colors for the toggle state
            buttonStyle.active.textColor = Color.green;
            buttonStyle.hover.textColor = Color.green;

            bool isClicked = GUILayout.Button(label, buttonStyle, options);

            if (isClicked)
            {
                buttonStates[label] = !buttonStates[label]; // Toggle the bool value when the button is clicked

                if (onClickAction != null)
                {
                    onClickAction.Invoke();
                }
            }
        }




        public static bool Button1(string label, Action[] onClickActions, params GUILayoutOption[] options)
        {
            GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
            buttonStyle.normal.textColor = Inactive;
            buttonStyle.active.textColor = Active;
            buttonStyle.hover.textColor = Hover;

            bool isClicked = GUILayout.Button(label, buttonStyle, options);

            if (isClicked && onClickActions != null)
            {
                foreach (var onClickAction in onClickActions)
                {
                    onClickAction?.Invoke();
                }
            }

            return isClicked;
        }

        public static bool SButton(string label, string abv, string boolKey, Action onClickAction = null, params GUILayoutOption[] options)
        {
            if (!SB.ContainsKey(boolKey))
            {
                SB[boolKey] = false;
            }
            bool toggle = SB.ContainsKey(boolKey) ? SB[boolKey] : false;
            bool isClicked = NewGUILayout.Button(label, SB[boolKey], onClickAction, options);
            if (isClicked)
            {
                SB[boolKey] = !toggle; // Toggle the bool value when the button is clicked
            }
            return SB[boolKey];
        }
    }
}
