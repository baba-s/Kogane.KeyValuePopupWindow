using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace Kogane.Internal
{
    internal sealed class KeyValuePopupWindowInstance : EditorWindow
    {
        private const string SEARCH_STRING_STATE_KEY = "KeyValuePopupWindow_SearchString";

        public static IReadOnlyList<KeyValuePopupWindowData> DataList     { private get; set; }
        public static KeyValuePopupWindowData                SelectedData { private get; set; }
        public static Action<KeyValuePopupWindowData>        OnSelected   { private get; set; }

        private KeyValuePopupWindowHeader   m_header;
        private SearchField                 m_searchField;
        private KeyValuePopupWindowTreeView m_keyValuePopupWindowTreeView;

        private void OnEnable()
        {
            var state = new TreeViewState();

            m_header = new( null );

            m_keyValuePopupWindowTreeView = new( DataList, SelectedData, state, m_header )
            {
                searchString = SessionState.GetString( SEARCH_STRING_STATE_KEY, string.Empty ),
                OnSelected = textData =>
                {
                    OnSelected?.Invoke( textData );
                    Close();
                }
            };

            m_searchField                         =  new();
            m_searchField.downOrUpArrowKeyPressed += m_keyValuePopupWindowTreeView.SetFocusAndEnsureSelectedItem;
        }

        private void OnDisable()
        {
            DataList   = null;
            OnSelected = null;
        }

        private void OnGUI()
        {
            DrawSearchField();
            DrawTreeView();
        }

        private void DrawSearchField()
        {
            using var scope = new EditorGUI.ChangeCheckScope();

            var searchString = m_searchField.OnToolbarGUI( m_keyValuePopupWindowTreeView.searchString );

            if ( !scope.changed ) return;

            SessionState.SetString( SEARCH_STRING_STATE_KEY, searchString );
            m_keyValuePopupWindowTreeView.searchString = searchString;
        }

        private void DrawTreeView()
        {
            var singleLineHeight = EditorGUIUtility.singleLineHeight;

            var rect = new Rect
            {
                x      = 0,
                y      = singleLineHeight + 1,
                width  = position.width,
                height = position.height - singleLineHeight - 1
            };

            m_keyValuePopupWindowTreeView.OnGUI( rect );
        }
    }
}