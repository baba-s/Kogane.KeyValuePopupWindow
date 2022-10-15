using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace Kogane.Internal
{
    internal sealed class KeyValuePopupWindowHeader : MultiColumnHeader
    {
        public KeyValuePopupWindowHeader( MultiColumnHeaderState state ) : base( state )
        {
            const int width = 32;

            var columns = new MultiColumnHeaderState.Column[]
            {
                new()
                {
                    width               = width,
                    minWidth            = width,
                    maxWidth            = width,
                    headerContent       = new( "No." ),
                    headerTextAlignment = TextAlignment.Center,
                },
                new()
                {
                    headerContent       = new( "Key" ),
                    headerTextAlignment = TextAlignment.Center,
                },
                new()
                {
                    headerContent       = new( "Value" ),
                    headerTextAlignment = TextAlignment.Center,
                },
            };

            this.state = new( columns );
        }
    }
}