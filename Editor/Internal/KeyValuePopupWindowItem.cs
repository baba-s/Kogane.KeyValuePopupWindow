using System;
using UnityEditor.IMGUI.Controls;

namespace Kogane.Internal
{
    internal sealed class KeyValuePopupWindowItem : TreeViewItem
    {
        public string                  IdString { get; }
        public KeyValuePopupWindowData Data     { get; }
        public string                  TextId   => Data.Key;
        public string                  Text     => Data.Value;

        public KeyValuePopupWindowItem( int id, in KeyValuePopupWindowData data ) : base( id )
        {
            IdString = id.ToString();
            Data     = data;
        }

        public bool IsMatch( string search )
        {
            const StringComparison comparisonType = StringComparison.OrdinalIgnoreCase;

            return TextId.Contains( search, comparisonType ) ||
                   Text.Contains( search, comparisonType )
                ;
        }
    }
}