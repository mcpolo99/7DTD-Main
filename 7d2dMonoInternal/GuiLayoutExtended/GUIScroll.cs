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
        public static float HorizontalScrollbarWithLabel(string label, ref float Modifier, float rightMaxValue)
        {


            GUIStyle Labelstyle = new GUIStyle(GUI.skin.label);
            Labelstyle.alignment = TextAnchor.LowerCenter;
            Labelstyle.fontSize = 13;
            Labelstyle.padding = new RectOffset(0, 0, -4, 0);



            GUILayout.BeginHorizontal();
            GUILayout.Label(label, Labelstyle, GUILayout.MaxWidth(100));
            //GUILayout.Label("Attacks/minute", Labelstyle, GUILayout.MaxWidth(60));
            Modifier = GUILayout.HorizontalScrollbar(Modifier, 0f, 0f, rightMaxValue);
            GUILayout.Label(Modifier.ToString("F1"), Labelstyle, GUILayout.MaxWidth(40));
            GUILayout.EndHorizontal();

            return Modifier;
        }
        //public static float HorizontalScrollbarWithLabel(string label, string floatKey, float rightMaxValue)
        //{
        //    //NewSettings.CheckDictionaryForKey(floatKey, 0.5f);
        //    float Modifier = NewSettings.Get<float>(floatKey);


        //    GUIStyle Labelstyle = new GUIStyle(GUI.skin.label);
        //    Labelstyle.alignment = TextAnchor.LowerCenter;
        //    Labelstyle.fontSize = 13;
        //    Labelstyle.padding = new RectOffset(0, 0, -4, 0);




        //    GUILayout.BeginHorizontal();
        //    GUILayout.Label(label, Labelstyle, GUILayout.MaxWidth(100));
        //    //GUILayout.Label("Attacks/minute", Labelstyle, GUILayout.MaxWidth(60));
        //    Modifier = GUILayout.HorizontalScrollbar(Modifier, 1f, 0f, rightMaxValue);
        //    GUILayout.Label(Modifier.ToString("F1"), Labelstyle, GUILayout.MaxWidth(40));
        //    GUILayout.EndHorizontal();

        //    return Modifier;
        //}


        //public static void BeginScrollView(Vector2 vector2, System.Action content, params GUILayoutOption[] options)
        //{

        //    vector2 = GUILayout.BeginScrollView(vector2, options);
        //    {
        //        content?.Invoke();
        //    }
        //    GUILayout.EndScrollView();
        //}
        //public static float HorizontalScrollbarWithLabelAndButton(string label, string stringBoolKey, ref float Modifier, float rightMaxValue, Action onClickAction = null)
        //{
        //    //#region DictionaryCheck
        //    //// Check if the key exists in the dictionary
        //    //if (!Settings.ContainsKey(stringBoolKey))
        //    //{
        //    //    // Add the key with a default value of false if it does not exist
        //    //    Settings[stringBoolKey] = false;
        //    //    //NewSettings.AddSetting(boolKey, false);
        //    //}
        //    //// Ensure the value associated with the key is a bool
        //    //if (!(Settings[stringBoolKey] is bool))
        //    //{
        //    //    Debug.LogError($"Key '{stringBoolKey}' is not a boolean.");
        //    //    //return false;
        //    //}
        //    //#endregion



        //    GUIStyle Labelstyle = new GUIStyle(GUI.skin.button);
        //    Labelstyle.alignment = TextAnchor.LowerCenter;
        //    Labelstyle.fontSize = 13;
        //    Labelstyle.padding = new RectOffset(0, 0, 0, 0);
        //    Labelstyle.margin = new RectOffset(0, 0, 0, 0);

        //    //NewGUILayout.ButtonToggleDictionary("BlockDamage", nameof(PassiveEffects.BlockDamage), () =>
        //    //{
        //    //    Cheat.AddPassiveEffectToPlayer(PassiveEffects.BlockDamage, 999999f, ValueModifierTypes.base_set);
        //    //});

        //    GUILayout.BeginHorizontal();

        //    NewGUILayout.ButtonToggleDictionary(label, nameof(stringBoolKey), Labelstyle, () =>
        //    {
        //        onClickAction?.Invoke();
        //    }, GUILayout.MaxWidth(100));




        //    Modifier = GUILayout.HorizontalScrollbar(Modifier, 0f, 0f, rightMaxValue);

        //    GUILayout.Label(Modifier.ToString("F1"), Labelstyle, GUILayout.MaxWidth(40));

        //    GUILayout.EndHorizontal();

        //    return Modifier;
        //}





        /// <summary>
        /// A horizontal scroll modifier with button that will trigger onClickAction with value from scrollbar when button is pressed.
        /// </summary>
        /// <param name="label"></param>
        /// <param name="stringBoolKey"></param>
        /// <param name="Modifier"></param>
        /// <param name="rightMaxValue"></param>
        /// <param name="onClickAction"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static bool HorizontalScrollbarWithLabelAndButton1(string label, string stringBoolKey, ref float Modifier, float rightMaxValue, Action onClickAction = null, params GUILayoutOption[] options)
        {
            #region DictionaryCheck
            // Check if the key exists in the dictionary
            if (!Settings.ContainsKey(stringBoolKey))
            {
                // Add the key with a default value of false if it does not exist
                Settings[stringBoolKey] = false;
                //NewSettings.AddSetting(boolKey, false);
            }
            // Ensure the value associated with the key is a bool
            if (!(Settings[stringBoolKey] is bool))
            {
                Debug.LogError($"Key '{stringBoolKey}' is not a boolean.");
                return false;
            }
            #endregion




            GUIStyle labelStyle = new GUIStyle(GUI.skin.label)
            {
                alignment = TextAnchor.LowerCenter,
                fontSize = 13,
                padding = new RectOffset(0, 0, 0, 0),
                margin = new RectOffset(0, 0, 5, 5)
            };

            GUIStyle buttonStyle = new GUIStyle(GUI.skin.button)
            {
                alignment = TextAnchor.LowerCenter,
                fontSize = 13,
                padding = new RectOffset(0, 0, 0, 0),
                margin = new RectOffset(0, 0, 5, 5)
            };
            GUIStyle scrollStyle = new GUIStyle(GUI.skin.horizontalScrollbar)
            {
                alignment = TextAnchor.LowerCenter,
                fontSize = 13,
                padding = new RectOffset(0, 0, 0, 0),
                margin = new RectOffset(0, 0, 5, 5)
            };

            GUILayout.BeginHorizontal();

            // Create the toggle button using the current value
            bool isClicked = NewGUILayout.Button(label, (bool)Settings[stringBoolKey], buttonStyle, null, options: GUILayout.MaxWidth(100)); //returns true if button is clicked
            Modifier = GUILayout.HorizontalScrollbar(Modifier, 0f, 0f, rightMaxValue, scrollStyle);
            GUILayout.Label(Modifier.ToString("F1"), labelStyle, GUILayout.MaxWidth(50));


            // Toggle the value if the button is clicked BUT the issue is when the onclick action is passed to the button!! we run it twice!!
            if (isClicked)
            {
                // Toggle the bool value when the button is clicked
                Settings[stringBoolKey] = !(bool)Settings[stringBoolKey];

                // Invoke additional action if provided
                onClickAction?.Invoke();
            }
            GUILayout.EndHorizontal();

            return (bool)Settings[stringBoolKey];
        }









        public static void BeginScrollView(Vector2 scrollPosition, string scrollKey, Action scrollViewContent, params GUILayoutOption[] option)
        {



            // Retrieve scroll position from the dictionary
            if (Settings.ContainsKey(scrollKey) && Settings[scrollKey] is Vector2)
            {
                scrollPosition = (Vector2)Settings[scrollKey];
            }

            #region CheckDictionary
            // Check if the key exists in the dictionary
            if (!Settings.ContainsKey(scrollKey))
            {
                // Add the key with a default value of Vector.zero if it does not exist
                Settings[scrollKey] = Vector2.zero;
            }
            // Ensure the value associated with the key is a bool
            //if (!(Settings[scrollKey] is Vector2))
            //{
            //    Debug.LogError($"Key '{scrollKey}' is not a boolean.");
            //    return Vector2.zero;
            //}

            #endregion




            // Begin scroll view with the retrieved scroll position and options
            scrollPosition = GUILayout.BeginScrollView(scrollPosition, option);

            // Update scroll position in the dictionary
            Settings[scrollKey] = scrollPosition;

            // Invoke the provided content for the scroll view
            scrollViewContent?.Invoke();

            // End scroll view
            GUILayout.EndScrollView();
        }
        public static void BeginScrollView(string scrollKey, Action scrollViewContent)
        {
            // Check if the key exists in the dictionary
            if (!Settings.ContainsKey(scrollKey))
            {
                // Add the key with a default value of Vector2.zero if it does not exist
                Settings[scrollKey] = Vector2.zero;
            }

            // Retrieve scroll position from the dictionary
            Vector2 scrollPosition = (Vector2)Settings[scrollKey];

            // Begin scroll view with the retrieved scroll position and options
            scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.MinHeight(250f), GUILayout.MaxHeight(350f));

            // Update scroll position in the dictionary
            Settings[scrollKey] = scrollPosition;

            // Invoke the provided content for the scroll view
            scrollViewContent?.Invoke();

            // End scroll view
            GUILayout.EndScrollView();
        }
        public static void BeginScrollView(string scrollKey, Action scrollViewContent, params GUILayoutOption[] option)
        {
            // Check if the key exists in the dictionary
            if (!Settings.ContainsKey(scrollKey))
            {
                // Add the key with a default value of Vector2.zero if it does not exist
                Settings[scrollKey] = Vector2.zero;
            }

            // Retrieve scroll position from the dictionary
            Vector2 scrollPosition = (Vector2)Settings[scrollKey];

            // Begin scroll view with the retrieved scroll position and options
            scrollPosition = GUILayout.BeginScrollView(scrollPosition, option);

            // Update scroll position in the dictionary
            Settings[scrollKey] = scrollPosition;

            // Invoke the provided content for the scroll view
            scrollViewContent?.Invoke();

            // End scroll view
            GUILayout.EndScrollView();
        }


    }
}
