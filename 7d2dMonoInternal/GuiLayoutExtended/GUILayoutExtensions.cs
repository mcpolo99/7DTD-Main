//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using UnityEngine;
//using UnityEditor;
//using InControl;
//using XInputDotNetPure;
//using InControl.UnityDeviceProfiles;
//using static PassiveEffect;
//using JetBrains.Annotations;
//using static Setting;
//using SevenDTDMono.GuiLayoutExtended;

//namespace SevenDTDMono.Utils
//{
   
//    public static class CGUILayout
//    {
//        private static readonly Color Active = Color.green;
//        private static readonly Color Inactive = Color.yellow;
//        private static readonly Color Hover = Color.cyan;
//        private static bool isResizing = false;
//        private static Vector2 mouseStartPos;
//        private static Rect originalWinRect;


//        public static Dictionary<string, bool> SBu = new Dictionary<string, bool>();
//        public static Dictionary<string, bool> RBu = new Dictionary<string, bool>();


//        //public static void BeginHorizontal(System.Action content, params GUILayoutOption[] options)
//        //{
//        //    GUILayout.BeginHorizontal(options);
//        //    content?.Invoke();
//        //    GUILayout.EndHorizontal();
//        //}
       



        












//        //public static Rect Window(int windowID, Rect windowRect, GUI.WindowFunction drawWindowContents, string title)
//        //{
//        //    GUILayout.BeginArea(windowRect, title, GUI.skin.window);
//        //    drawWindowContents(windowID);

//        //    // Add a resizing handle at the bottom-right corner
//        //    Rect handleRect = new Rect(windowRect.width - 20, windowRect.height - 20, 20, 20);
//        //    GUI.DrawTexture(handleRect, Texture2D.whiteTexture);

//        //    // Check if the mouse is over the handle and handle resizing logic
//        //    Event e = Event.current;
//        //    if (e.type == EventType.MouseDown && handleRect.Contains(e.mousePosition))
//        //    {
//        //        isResizing = true;
//        //        mouseStartPos = e.mousePosition;
//        //        originalWinRect = windowRect;
//        //        e.Use();
//        //    }
//        //    else if (e.type == EventType.MouseDrag && isResizing)
//        //    {
//        //        Vector2 offset = e.mousePosition - mouseStartPos;
//        //        windowRect.width = Mathf.Max(originalWinRect.width + offset.x, 100);
//        //        windowRect.height = Mathf.Max(originalWinRect.height + offset.y, 100);
//        //        e.Use();
//        //    }
//        //    else if (e.type == EventType.MouseUp && isResizing)
//        //    {
//        //        isResizing = false;
//        //        e.Use();
//        //    }

//        //    GUILayout.EndArea();
//        //    return windowRect;
//        //}


//        //public static bool FoldableMenuVertical(bool display, string label, System.Action content, params GUILayoutOption[] options)
//        //{
//        //    Color headerColor = display ? Active : Inactive;
//        //    Color hoverColor = Hover;

//        //    GUIStyle headerStyle = new GUIStyle(GUI.skin.box);
//        //    headerStyle.fontStyle = FontStyle.Bold;
//        //    headerStyle.fontSize = 12;
//        //    headerStyle.normal.textColor = headerColor;
//        //    headerStyle.hover.textColor = hoverColor;

//        //    Rect rect = GUILayoutUtility.GetRect(16f, 22f, headerStyle);
//        //    GUI.Box(rect, label, headerStyle);

//        //    Event e = Event.current;

//        //    Rect toggleRect = new Rect(rect.x + 4f, rect.y + 2f, 13f, 13f);
//        //    if (e.type == EventType.Repaint)
//        //    {
//        //        DrawFoldoutHorizontal(toggleRect, display);
//        //    }

//        //    if (e.type == EventType.MouseDown && rect.Contains(e.mousePosition))
//        //    {
//        //        display = !display;
//        //        e.Use();
//        //    }

//        //    if (display)
//        //    {
//        //        GUILayout.BeginVertical(GUI.skin.box);
//        //        content?.Invoke();
//        //        GUILayout.EndVertical();
//        //    }

//        //    return display;
//        //}



//        //public static int Toolbar(int selected, string[] texts, params GUILayoutOption[] options)
//        //{
//        //    GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
//        //    // Apply custom text colors
//        //    buttonStyle.normal.textColor = Inactive;
//        //    buttonStyle.active.textColor = Active;
//        //    buttonStyle.hover.textColor = Hover;

//        //    // Draw the toolbar using GUILayout.Toolbar
//        //    int newSelected = GUILayout.Toolbar(selected, texts, buttonStyle, options);

//        //    // If the selected button has changed, return the new selected index
//        //    if (newSelected != selected)
//        //    {
//        //        return newSelected;
//        //    }

//        //    // Otherwise, return the original selected index
//        //    return selected;
//        //}

//        //public static int Toolbar(int selected, string[] texts, GUIStyle style, params GUILayoutOption[] options)
//        //{
//        //    style.active.textColor = Active;
//        //    style.hover.textColor = Hover;

//        //    int newSelected = GUILayout.Toolbar(selected, texts, style, options);

//        //    // If the selected button has changed, return the new selected index
//        //    if (newSelected != selected)
//        //    {
//        //        return newSelected;
//        //    }

//        //    // Otherwise, return the original selected index
//        //    return selected;
//        //}
//        //public static int Toolbar1(int selected, string[] texts, GUIStyle style, params GUILayoutOption[] options)
//        //{
//        //    //Color activeColor = Color.green;
//        //    //Color inactiveColor = Color.red;
//        //    //Color hoverColor = Color.cyan;

//        //    for (int i = 0; i < texts.Length; i++)
//        //    {
//        //        // Check if the current button is selected
//        //        if (i == selected)
//        //        {
//        //            style.normal.textColor = Active;
//        //            style.hover.textColor = Active;
//        //            style.active.textColor = Active;
//        //        }
//        //        else
//        //        {
//        //            style.normal.textColor = Inactive;
//        //            style.hover.textColor = Hover;
//        //            style.active.textColor = Inactive;
//        //        }

//        //        if (GUILayout.Button(texts[i], style, options))
//        //        {
//        //            // Button clicked, update the selected index
//        //            selected = i;
//        //        }
//        //    }

//        //    // If the selected button has changed, return the new selected index
//        //    return selected;

//        //}

//        //public static int Toolbar2(int selected, string[] texts, GUIStyle style, params GUILayoutOption[] options)
//        //{
//        //    int newSelected = selected;

//        //    for (int i = 0; i < texts.Length; i++)
//        //    {
//        //        bool isButtonActive = (i == selected);

//        //        // Determine the color based on whether the button is active or not
//        //        Color buttonColor = isButtonActive ? Active : Inactive;

//        //        // Update the button style's color
//        //        style.normal.textColor = buttonColor;
//        //        style.hover.textColor = Hover;

//        //        // Check if the button is clicked
//        //        if (GUILayout.Button(texts[i], style, options))
//        //        {
//        //            // Button clicked, update the selected index
//        //            newSelected = i;
//        //        }
//        //    }

//        //    // If the selected button has changed, return the new selected index
//        //    if (newSelected != selected)
//        //    {
//        //        return newSelected;
//        //    }

//        //    // Otherwise, return the original selected index
//        //    return selected;
//        //}
//        //private static void DrawFoldoutHorizontal(Rect position, bool expanded)
//        //{
//        //    GUIStyle foldoutStyle = new GUIStyle();
//        //    foldoutStyle.fontStyle = FontStyle.Bold;
//        //    foldoutStyle.fontSize = 12;
//        //    foldoutStyle.normal.textColor = expanded ? Active : Inactive;

//        //    GUIContent content = new GUIContent(expanded ? "\u25BC" : "\u25BA", "Click to expand/collapse");
//        //    GUI.Label(position, content, foldoutStyle);

//        //    // Draw a horizontal line to separate the header from the content
//        //    Vector2 lineStart = new Vector2(position.x + 15f, position.y + position.height);
//        //    Vector2 lineEnd = new Vector2(lineStart.x + position.width - 15f, lineStart.y);
//        //    DrawHorizontalLine(lineStart, lineEnd, Color.gray);
//        //}
//        //private static void DrawFoldoutHorizontal1(Rect position, bool display)
//        //{
//        //    // Draw a simple foldout arrow on the header
//        //    Texture2D foldoutTex = display ? Texture2D.whiteTexture : Texture2D.blackTexture;
//        //    GUI.DrawTexture(position, foldoutTex);
//        //}
//        //private static void DrawHorizontalLine(Vector2 start, Vector2 end, Color color)
//        //{
//        //    Texture2D lineTex = new Texture2D(1, 1);
//        //    lineTex.SetPixel(0, 0, color);
//        //    lineTex.Apply();

//        //    Matrix4x4 matrixBackup = GUI.matrix;
//        //    float angle = Mathf.Atan2(end.y - start.y, end.x - start.x) * 180 / Mathf.PI;
//        //    GUIUtility.RotateAroundPivot(angle, start);
//        //    GUI.DrawTexture(new Rect(start.x, start.y, (end - start).magnitude, 1), lineTex);
//        //    GUI.matrix = matrixBackup;
//        //}




//    }
//}
