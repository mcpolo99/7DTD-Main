
using SevenDTDMono.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;
using WorldGenerationEngineFinal;
using static PassiveEffect;


namespace SevenDTDMono.GuiLayoutExtended
{
   
    public partial class NewGUILayout
    {
        /// <summary>
        /// Kind of the most basic and default button, used something like this :
        /// if (NewGUILayout.Button($"Teleport To Marker ")
        /// {
        ///     Actions To Execute
        /// }
        /// </summary>
        /// <param name="label">Display Label on the button</param>
        /// <param name="options">GUI Layout Option</param>
        /// <returns></returns>
        public static bool Button(string label, params GUILayoutOption[] options)
        {
            GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
            buttonStyle.normal.textColor = Inactive;
            buttonStyle.active.textColor = Active;
            buttonStyle.hover.textColor = Hover;

            bool isClicked = GUILayout.Button(label, buttonStyle, options);
            return isClicked;
        }

        /// <summary>
        /// A Normal button WITH inputActions ()=>{} or method() that just will trigger our input Actions!
        /// </summary>
        /// <param name="label">Display Label on the button </param>
        /// <param name="onClickAction">Input actions to execute</param>
        /// <param name="options">GUI Layout Option</param>
        /// <returns></returns>
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

        /// <summary>
        ///A button to Change the apperance of the button, Pretty much a Toggle button. 
        /// 
        /// </summary>
        /// <param name="label">Display Label on the button </param>
        /// <param name="boolToggle">Boolean value to display if button has toggled on or off</param>
        /// <param name="onClickAction"> Input actions to execute </param>
        /// <param name="options">GUI Layout Option</param>
        /// <returns></returns>
        private static bool Button(string label, bool boolToggle, Action onClickAction = null, params GUILayoutOption[] options)
        {
            GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
            buttonStyle.normal.textColor = boolToggle ? Active : Inactive;
            buttonStyle.active.textColor = Active;
            buttonStyle.hover.textColor = Hover;

            bool isClicked = GUILayout.Button(label, buttonStyle, options); //will return true if button gets clicked

            if (isClicked)
            {
                //this is only executed once the button is clicked
                // Invoke additional action if provided
                onClickAction?.Invoke();
            }

            return isClicked;
        }
        /// <summary>
        /// This button can contain also a Style to customize some values.
        /// </summary>
        /// <param name="label">Display Label on the button </param>
        /// <param name="boolToggle"> Dictionary bool reference</param>
        /// <param name="style"> Uniqe style </param>
        /// <param name="onClickAction">Input actions to execute </param>
        /// <param name="options">GUI Layout Option</param>
        /// <returns></returns>
        private static bool Button(string label, bool boolToggle, GUIStyle style, Action onClickAction = null, params GUILayoutOption[] options)
        {

            style.normal.textColor = boolToggle ? Active : Inactive;
            style.active.textColor = Active;
            style.hover.textColor = Hover;


            bool isClicked = GUILayout.Button(label, style, options); //will return true if button gets clicked

            if (isClicked)
            {
                // Invoke additional action if provided
                onClickAction?.Invoke();
            }

            return isClicked;
        }
        /// <summary>
        /// This is used in only one location. For use of looping a enum list! Have not managed to make Smal Dropdown.
        /// </summary>
        /// <param name="label"></param>
        /// <param name="currentIndex"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static bool Button(string label, ref int currentIndex, params GUILayoutOption[] options)//Cycle enumlist item
        {
            // Display the button with the label and the current enum value as buttonText.
            bool isClicked = GUILayout.Button(label + " " + ((Enum)System.Enum.ToObject(typeof(ValueModifierTypes), currentIndex)).ToString(), options);

            // If the button is pressed, increment the index and loop back to the beginning if needed.
            if (isClicked)
            {
                currentIndex = (currentIndex + 1) % System.Enum.GetValues(typeof(ValueModifierTypes)).Length;
            }

            // Return the buttonPressed state.
            return isClicked;
        }


       /// <summary>
        /// Done?
        /// </summary>
        /// <param name="label"> Display Label on the button </param>
        /// <param name="boolKey">Name of Dictionary bool key</param>
        /// <param name="onClickAction">Input actions to execute </param>
        /// <param name="options">GUI Layout Option</param>
        /// <returns></returns>
        public static bool ButtonToggleDictionary(string label, string boolKey, Action onClickAction = null, params GUILayoutOption[] options)
        {
            #region Dictionary Check

            if (!SettingsInstance.CheckBoolKeyExist(boolKey))
            {
                return false;
            }
            // Retrieve the current value from dictionary
            bool toggle = _boolDict[boolKey];

            #endregion

            // Create the toggle button using the current value
            bool isClicked = NewGUILayout.Button(label, toggle, null, options); //here we do not want to pass onClickAction simply because we do not want to double execute the code

            // Toggle the value if the button is clicked BUT the issue is when the onclick action is passed to the button!! we run it twice!!
            if (isClicked)
            {
                // Toggle the bool value when the button is clicked
                _boolDict[boolKey] = !toggle;
                // Invoke additional action if provided
                onClickAction?.Invoke();
            }
            return toggle;
        }
        /// <summary>
        /// Do not remember why i have this one
        /// </summary>
        /// <param name="label">Display Label on the button </param>
        /// <param name="boolKey">Name of Dictionary bool key</param>
        /// <param name="style">GUIStyle </param>
        /// <param name="onClickAction">Input actions to execute </param>
        /// <param name="options">GUI Layout Option</param>
        /// <returns></returns>
        public static bool ButtonToggleDictionary(string label, string boolKey, GUIStyle style, Action onClickAction = null, params GUILayoutOption[] options)
        {
            #region Dictionary Check

            if (!SettingsInstance.CheckBoolKeyExist(boolKey))
            {
                return false;
            }
            // Retrieve the current value from dictionary
            bool toggle = _boolDict[boolKey];

            #endregion


            // Create the toggle button using the current value
            bool isClicked = NewGUILayout.Button(label, toggle, style, null, options); //returns true if button is clicked

            // Toggle the value if the button is clicked BUT the issue is when the onclick action is passed to the button!! we run it twice!!
            if (isClicked)
            {
                // Toggle the bool value when the button is clicked
                _boolDict[boolKey] = !toggle;

                // Invoke additional action if provided
                onClickAction?.Invoke();
            }
            return isClicked;
        }






        /// <summary>
        ///ButtonTriggerAction is for keeping a "toggle" in a dictionary in case we want to do some checks to later disable what the
        /// button has triggered! 
        /// 
        /// </summary>
        /// <param name="label">Display Label on the button </param>
        /// <param name="boolKey"> Name of Dictionary bool key</param>
        /// <param name="onClickAction">Input actions to execute </param>
        /// <param name="options">GUI Layout Option</param>
        /// <returns></returns>
        /// 
        public static bool ButtonTriggerAction(string label, string boolKey, Action onClickAction = null, params GUILayoutOption[] options)
        {
            #region Dictionary Check

            if (!SettingsInstance.CheckBoolKeyExist(boolKey))
            {
                return false;
            }
            // Retrieve the current value from dictionary
            bool toggle = _boolDict[boolKey];

            #endregion

            GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
            buttonStyle.normal.textColor = Inactive;
            buttonStyle.active.textColor = Active;
            buttonStyle.hover.textColor = Hover;

            bool isClicked = GUILayout.Button(label, buttonStyle, options);

            if (isClicked)
            {
                _boolDict[boolKey] = !toggle;
                // Invoke additional action if provided
                onClickAction?.Invoke();
            }
            return isClicked;
        }



    }
}
