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
            IReadOnlyList<KeyValuePopupWindowData> dataList,
            Action<KeyValuePopupWindowData>        onSelected,
            KeyValuePopupWindowData                selectedData = default
        )
        {
            KeyValuePopupWindowInstance.DataList     = dataList;
            KeyValuePopupWindowInstance.SelectedData = selectedData;
            KeyValuePopupWindowInstance.OnSelected   = onSelected;

            var window = ScriptableObject.CreateInstance<KeyValuePopupWindowInstance>();
            window.titleContent = new( title );
            window.ShowAuxWindow();
        }
    }
}