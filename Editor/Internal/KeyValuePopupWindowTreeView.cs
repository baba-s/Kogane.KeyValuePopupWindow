using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.IMGUI.Controls;

namespace Kogane.Internal
{
    internal sealed class KeyValuePopupWindowTreeView : TreeView
    {
        private enum ColumnType
        {
            NUMBER,
            TEXT_ID,
            TEXT,
        }

        private readonly IReadOnlyList<KeyValuePopupWindowData> m_dataList;

        private KeyValuePopupWindowItem[] m_list;

        public Action<KeyValuePopupWindowData> OnSelected { get; set; }

        public KeyValuePopupWindowTreeView
        (
            IReadOnlyList<KeyValuePopupWindowData> dataList,
            TreeViewState                          state,
            MultiColumnHeader                      header
        ) : base( state, header )
        {
            m_dataList = dataList;

            showAlternatingRowBackgrounds = true;

            header.sortingChanged += SortItems;

            Reload();

            header.ResizeToFit();
            header.SetSorting( 0, true );
        }

        protected override float GetCustomRowHeight( int row, TreeViewItem treeViewItem )
        {
            var item      = ( KeyValuePopupWindowItem )treeViewItem;
            var lineCount = item.Text.Split( "\n" ).Length;

            return 16 * lineCount;
        }

        protected override TreeViewItem BuildRoot()
        {
            // 要素が存在しない場合、 TreeView は例外を発生する
            // そのため、要素が存在しない場合は表示しないダミーデータを追加する
            m_list = m_dataList
                    .Select( ( x, index ) => new KeyValuePopupWindowItem( index + 1, x ) )
                    .DefaultIfEmpty( new KeyValuePopupWindowItem( 0, KeyValuePopupWindowData.CreateDummy() ) )
                    .ToArray()
                ;

            var root = new TreeViewItem { depth = -1 };

            foreach ( var x in m_list )
            {
                root.AddChild( x );
            }

            return root;
        }

        protected override void RowGUI( RowGUIArgs args )
        {
            var item = ( KeyValuePopupWindowItem )args.item;

            for ( var i = 0; i < args.GetNumVisibleColumns(); i++ )
            {
                var cellRect   = args.GetCellRect( i );
                var columnType = ( ColumnType )args.GetColumn( i );

                switch ( columnType )
                {
                    case ColumnType.NUMBER:
                        EditorGUI.LabelField( cellRect, item.IdString );
                        break;

                    case ColumnType.TEXT_ID:
                        EditorGUI.LabelField( cellRect, item.TextId );
                        break;

                    case ColumnType.TEXT:
                        EditorGUI.LabelField( cellRect, item.Text );
                        break;

                    default: throw new ArgumentOutOfRangeException();
                }
            }
        }

        protected override bool DoesItemMatchSearch( TreeViewItem treeViewItem, string search )
        {
            if ( string.IsNullOrEmpty( search ) ) return true;

            var item = ( KeyValuePopupWindowItem )treeViewItem;

            return item.IsMatch( search );
        }

        private void SortItems( MultiColumnHeader header )
        {
            var sortedColumnIndex = header.sortedColumnIndex;
            var sortedColumnType  = ( ColumnType )sortedColumnIndex;
            var isSortedAscending = header.IsSortedAscending( sortedColumnIndex );

            var sortedList = sortedColumnType switch
            {
                ColumnType.NUMBER  => m_list.OrderBy( x => x.id ),
                ColumnType.TEXT_ID => m_list.OrderBy( x => x.TextId ),
                ColumnType.TEXT    => m_list.OrderBy( x => x.Text ),
                _                  => throw new ArgumentOutOfRangeException()
            };

            var reversedList = isSortedAscending
                    ? sortedList
                    : sortedList.Reverse()
                ;

            rootItem.children = reversedList
                    .Cast<TreeViewItem>()
                    .ToList()
                ;

            BuildRows( rootItem );
        }

        protected override void SingleClickedItem( int id )
        {
            var item = m_list.First( x => x.id == id );

            OnSelected?.Invoke( item.Data );
        }
    }
}