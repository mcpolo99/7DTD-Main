using SevenDTDMono.Utils;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="label"></param>
        /// <param name="content"></param>
        /// <param name="toggle"></param>
        /// <param name="options"></param>
        public static void DropDownForMethods(string label, System.Action content, ref bool toggle, params GUILayoutOption[] options)
        {
            //Background box style
            GUIStyle backgroundBoxStyle = new GUIStyle(GUI.skin.box)
            {
                padding = new RectOffset(-0, -0, -0, -0)
            };
            
            //setup style for button
            GUIStyle buttonStyle = new GUIStyle(GUI.skin.button)
            {
                alignment = TextAnchor.MiddleRight,
                fontSize = 15,
                margin = new RectOffset(0, 0, 0, 0)
            };
            
            //begin collection of items
            GUILayout.BeginVertical(backgroundBoxStyle);


            buttonStyle.fontStyle = toggle ? FontStyle.Bold : FontStyle.BoldAndItalic;
            
            Rect headerRect = GUILayoutUtility.GetRect(300, 30f, buttonStyle); // Pass the width value here

            float lineHeight = 10f;
            Rect lineRect = new Rect(headerRect.x, headerRect.y + (headerRect.height - lineHeight) * 0.5f, 30, lineHeight);
            DrawLine(lineRect, toggle ? Color.green : Color.yellow);

            GUI.Box(headerRect, label, buttonStyle);

            if (Event.current.type == EventType.MouseDown && headerRect.Contains(Event.current.mousePosition))
            {
                toggle = !toggle;
                Event.current.Use();
            }

            if (toggle)
            {
                //NewGUILayout.BeginVertical(GUI.skin.box, () =>
                //{

                content?.Invoke();

                //});
            }
            GUILayout.EndVertical();
        }


    }
}
