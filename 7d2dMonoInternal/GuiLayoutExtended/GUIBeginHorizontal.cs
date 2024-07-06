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


        #region BeginHorizontal
        public static void BeginHorizontal(GUIContent content, GUIStyle style, System.Action contentActions, params GUILayoutOption[] options)
        {
            GUILayout.BeginHorizontal(content, style, options);
            contentActions?.Invoke();
            GUILayout.EndHorizontal();
        }
        public static void BeginHorizontal(string _string, GUIStyle style, System.Action contentActions, params GUILayoutOption[] options)
        {
            GUILayout.BeginHorizontal(_string, style, options);
            contentActions?.Invoke();
            GUILayout.EndHorizontal();
        }
        public static void BeginHorizontal(GUIStyle style, System.Action contentActions, params GUILayoutOption[] options)
        {
            GUILayout.BeginHorizontal(style, options);
            contentActions?.Invoke();
            GUILayout.EndHorizontal();
        }
        public static void BeginHorizontal(System.Action contentActions, params GUILayoutOption[] options)
        {
            GUILayout.BeginHorizontal(options);
            contentActions?.Invoke();
            GUILayout.EndHorizontal();
        }
        public static void BeginHorizontal(GUIStyle style, params GUILayoutOption[] options)
        {
            GUILayout.BeginHorizontal(style, options);
            //contentActions?.Invoke();
            GUILayout.EndHorizontal();
        }
        #endregion




    }
}
