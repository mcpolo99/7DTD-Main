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

        private static readonly Color Active = Color.green;
        private static readonly Color Inactive = Color.yellow;
        private static readonly Color Hover = Color.cyan;
        private static Dictionary<string, object> Settings => NewSettings.Instance.SettingsDictionary;
        public static void DrawLine(Rect rect, Color color)
        {
            GUI.color = color;
            GUI.DrawTexture(rect, Texture2D.whiteTexture);
            GUI.color = Color.white;
        }


    }
}
