using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

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
        public static float HorizontalScrollbarWithLabel(string label, string floatKey, float rightMaxValue)
        {
            NewSettings.CheckDictionaryForKey(floatKey, 0.5f);
            float Modifier = NewSettings.Get<float>(floatKey);


            GUIStyle Labelstyle = new GUIStyle(GUI.skin.label);
            Labelstyle.alignment = TextAnchor.LowerCenter;
            Labelstyle.fontSize = 13;
            Labelstyle.padding = new RectOffset(0, 0, -4, 0);

            


            GUILayout.BeginHorizontal();
            GUILayout.Label(label, Labelstyle, GUILayout.MaxWidth(100));
            //GUILayout.Label("Attacks/minute", Labelstyle, GUILayout.MaxWidth(60));
            Modifier = GUILayout.HorizontalScrollbar(Modifier, 1f, 0f, rightMaxValue);
            GUILayout.Label(Modifier.ToString("F1"), Labelstyle, GUILayout.MaxWidth(40));
            GUILayout.EndHorizontal();

            return Modifier;
        }


        //public static void BeginScrollView(Vector2 vector2, System.Action content, params GUILayoutOption[] options)
        //{

        //    vector2 = GUILayout.BeginScrollView(vector2, options);
        //    {
        //        content?.Invoke();
        //    }
        //    GUILayout.EndScrollView();
        //}


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
