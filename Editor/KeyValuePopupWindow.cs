using System;
using System.Collections.Generic;
using Kogane.Internal;
using UnityEngine;

namespace Kogane
{
    public static class KeyValuePopupWindow
    {
        public static void Open
        (
            string                                 title,
            IReadOnlyList<KeyValuePopupWindowData> textDataList,
            Action<KeyValuePopupWindowData>        onSelected
        )
        {
            KeyValuePopupWindowInstance.DataList   = textDataList;
            KeyValuePopupWindowInstance.OnSelected = onSelected;

            var window = ScriptableObject.CreateInstance<KeyValuePopupWindowInstance>();
            window.titleContent = new GUIContent( title );
            window.ShowAuxWindow();
        }
    }
}