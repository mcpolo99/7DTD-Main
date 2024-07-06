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
        #region BeginVertical
        public static void BeginVertical(GUIContent content, GUIStyle style, System.Action contentActions, params GUILayoutOption[] options)
        {
            GUILayout.BeginVertical(content, style, options);
            contentActions?.Invoke();
            GUILayout.EndVertical();
        }
        public static void BeginVertical(string _string, GUIStyle style, System.Action contentActions, params GUILayoutOption[] options)
        {
            GUILayout.BeginVertical(_string, style, options);
            contentActions?.Invoke();
            GUILayout.EndVertical();
        }
        public static void BeginVertical(GUIStyle style, System.Action contentActions, params GUILayoutOption[] options)
        {
            GUILayout.BeginVertical(style, options);
            contentActions?.Invoke();
            GUILayout.EndVertical();
        }
        public static void BeginVertical(System.Action contentActions, params GUILayoutOption[] options)
        {
            GUILayout.BeginVertical(options);
            contentActions?.Invoke();
            GUILayout.EndVertical();
        }
        public static void BeginVertical(GUIStyle style, params GUILayoutOption[] options)
        {
            GUILayout.BeginVertical(style, options);
            //contentActions?.Invoke();
            GUILayout.EndVertical();
        }
        //public static void BeginVertical(System.Action content)
        //{
        //    GUILayout.BeginVertical();
        //    content?.Invoke();
        //    GUILayout.EndVertical();
        //}


        #endregion

    }
}
