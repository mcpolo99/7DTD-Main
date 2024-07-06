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
        public static int Toolbar4(int selected, string[] texts, GUIStyle style, params GUILayoutOption[] options)
        {
            int newSelected = selected;
            int buttonCount = texts.Length;

            GUILayout.BeginHorizontal();
            {
                for (int i = 0; i < buttonCount; i++)
                {
                    Rect rect = GUILayoutUtility.GetRect(new GUIContent(texts[i]), style, options);

                    bool isButtonActive = (i == selected);
                    Color buttonColor = isButtonActive ? Active : Inactive;
                    style.normal.textColor = buttonColor;
                    style.hover.textColor = Hover;

                    if (GUI.Button(rect, texts[i], style))
                    {
                        newSelected = i;
                    }
                }
            }
            GUILayout.EndHorizontal();
            if (newSelected != selected)
            {
                return newSelected;
            }

            return selected;
        }




    }
}
