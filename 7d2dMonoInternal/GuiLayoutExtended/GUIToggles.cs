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

        public static bool Toggle(bool value, string label, params GUILayoutOption[] options)
        {
            // Use the provided colors for the toggle
            GUIStyle toggleStyle = new GUIStyle(GUI.skin.toggle);
            toggleStyle.normal.textColor = Inactive;                      //WHEN OFF 
            toggleStyle.onNormal.textColor = Active;                    //WHEN ON 
            toggleStyle.active.textColor = Active;                       //OFF TO ON When pressing/holding 
            toggleStyle.onActive.textColor = Inactive;                     // ON TO OFF
            toggleStyle.hover.textColor = Hover;                     //OFF on offstate
            toggleStyle.onHover.textColor = Hover;                   //ON  on onState
            value = GUILayout.Toggle(value, label, toggleStyle, GUILayout.Width(120));

            return value;
        }
        public static bool Toggle(bool value, string label, Color active, Color inactive, params GUILayoutOption[] options)
        {
            // Use the provided colors for the toggle
            GUIStyle toggleStyle = new GUIStyle(GUI.skin.toggle);
            toggleStyle.normal.textColor = inactive;                      //WHEN OFF 
            toggleStyle.onNormal.textColor = active;                    //WHEN ON 
            toggleStyle.active.textColor = active;                       //OFF TO ON When pressing/holding 
            toggleStyle.onActive.textColor = inactive;                     // ON TO OFF
            toggleStyle.hover.textColor = Hover;                     //OFF
            toggleStyle.onHover.textColor = Hover;                   //ON    
            value = GUILayout.Toggle(value, label, toggleStyle, GUILayout.Width(120));

            return value;
        }
        public static bool Toggle(bool value, string label, Color active, Color inactive, Color hover, params GUILayoutOption[] options)
        {
            GUIStyle toggleStyle = new GUIStyle(GUI.skin.toggle);
            //toggleStyle.onFocused.textColor = activeColor;
            toggleStyle.normal.textColor = inactive;                      //WHEN OFF 
            toggleStyle.onNormal.textColor = active;                    //WHEN ON 
            toggleStyle.active.textColor = active;                       //OFF TO ON When pressing/holding 
            toggleStyle.onActive.textColor = inactive;                     // ON TO OFF
            toggleStyle.hover.textColor = hover;                     //OFF
            toggleStyle.onHover.textColor = hover;                   //ON
            value = GUILayout.Toggle(value, label, toggleStyle, GUILayout.Width(120));

            return value;
        }
        public static bool Toggle(bool value, string label, Color hover, params GUILayoutOption[] options)
        {

            GUIStyle toggleStyle = new GUIStyle(GUI.skin.toggle);
            //toggleStyle.onFocused.textColor = activeColor;
            toggleStyle.normal.textColor = Inactive;                      //WHEN OFF 
            toggleStyle.onNormal.textColor = Active;                    //WHEN ON 
            toggleStyle.active.textColor = Active;                       //OFF TO ON When pressing/holding 
            toggleStyle.onActive.textColor = Inactive;                     // ON TO OFF
            toggleStyle.hover.textColor = hover;                     //OFF
            toggleStyle.onHover.textColor = hover;                   //ON
            value = GUILayout.Toggle(value, label, toggleStyle, GUILayout.Width(120));

            return value;
        }





    }
}
