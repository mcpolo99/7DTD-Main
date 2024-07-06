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

        public static bool CustomDropDown(string label, System.Action content, float width, params GUILayoutOption[] options)
        {
            //for every press toggle on and off for the customdrop down
            bool toggle = false;
            GUIStyle headerStyle = new GUIStyle(GUI.skin.box);
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
            Rect headerRect = GUILayoutUtility.GetRect(width, 20f, headerStyle); // Pass the width value here
            Rect toggleRect = new Rect(headerRect.y, headerRect.x, 10f, 20f);
            GUI.Box(headerRect, label, headerStyle);
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

            if (toggle)
            {
                content?.Invoke();
            }

            return toggle;
        }

        public static bool CustomDropDown(bool toggle, string label, System.Action content, params GUILayoutOption[] options)
        {
            //for every press toggle on and off for the customdrop down

            GUIStyle headerStyle = new GUIStyle(GUI.skin.box);
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
            Rect headerRect = GUILayoutUtility.GetRect(30, 20f, headerStyle); // Pass the width value here
            Rect toggleRect = new Rect(headerRect.y, headerRect.x, 10f, 20f);
            GUI.Box(headerRect, label, headerStyle);
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

            if (toggle)
            {
                content?.Invoke();
            }

            return toggle;
        }


        public static void CustomDropDown(string label, System.Action content, ref bool toggle, params GUILayoutOption[] options)
        {
            GUIStyle headerStyle = new GUIStyle(GUI.skin.box);
            headerStyle.alignment = TextAnchor.MiddleCenter;
            headerStyle.fontSize = 15;

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

            Rect headerRect = GUILayoutUtility.GetRect(30, 20f, headerStyle); // Pass the width value here
            GUI.Box(headerRect, label, headerStyle);

            if (Event.current.type == EventType.MouseDown && headerRect.Contains(Event.current.mousePosition))
            {
                toggle = !toggle;
                Event.current.Use();
            }

            if (toggle)
            {
                content?.Invoke();
            }
        }


        public static void DropDownForMethods(string label, System.Action content, ref bool toggle, params GUILayoutOption[] options)
        {

            GUIStyle headerStyle = new GUIStyle(GUI.skin.button);
            //GUIStyle headerStyle = new GUIStyle(GUI.skin.box);
            headerStyle.alignment = TextAnchor.MiddleRight;
            headerStyle.fontSize = 15;

            if (!toggle)
            {
                headerStyle.fontStyle = FontStyle.Bold;
                //headerStyle.normal.textColor = Color.green;
            }
            else
            {
                headerStyle.fontStyle = FontStyle.BoldAndItalic;
                //headerStyle.normal.textColor = Color.yellow;
            }
            // GUIContent content1 = new GUIContent(toggle ? "\u25BC" : "\u25BA", "Click to expand/collapse");

            Rect headerRect = GUILayoutUtility.GetRect(300, 30f, headerStyle); // Pass the width value here

            //Rect headerRect = GUILayoutUtility.GetRect(content1, headerStyle); // Pass the width value here
            //Rect headerRect1 = GUILayoutUtility.GetRect(100, 10); // Pass the width value here
            float lineHeight = 10f;
            Rect lineRect = new Rect(headerRect.x, headerRect.y + (headerRect.height - lineHeight) * 0.5f, 30, lineHeight);
            DrawLine(lineRect, toggle ? Color.green : Color.yellow);
            GUI.Box(headerRect, label, headerStyle);
            if (Event.current.type == EventType.MouseDown && headerRect.Contains(Event.current.mousePosition))
            {
                toggle = !toggle;
                Event.current.Use();
            }

            if (toggle)
            {

                NewGUILayout.BeginVertical(GUI.skin.box, () =>
                {

                    content?.Invoke();

                });



            }
        }








    }
}
