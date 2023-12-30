using System;
using UnityEditor.IMGUI.Controls;

namespace Kogane.Internal
{
    internal sealed class KeyValuePopupWindowItem : TreeViewItem
    {
        private readonly MultiColumnHeader m_header;

        public string                  IdString { get; }
        public KeyValuePopupWindowData Data     { get; }
        public string                  TextId   => Data.Key;
        public string                  Text     => Data.Value;

        public override string displayName
        {
            get
            {
                var sortedColumnIndex = m_header.sortedColumnIndex;
                var sortedColumnType  = ( ColumnType )sortedColumnIndex;

                return sortedColumnType switch
                {
                    ColumnType.NUMBER  => IdString,
                    ColumnType.TEXT_ID => TextId,
                    ColumnType.TEXT    => Text,
                    _                  => throw new ArgumentOutOfRangeException()
                };
            }
        }

        public KeyValuePopupWindowItem
        (
            int                        id,
            in KeyValuePopupWindowData data,
            MultiColumnHeader          header
        ) : base( id )
        {
            m_header = header;
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