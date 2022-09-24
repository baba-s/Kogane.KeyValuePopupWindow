# Kogane Key Value Popup Window

TreeView で実装されたポップアップウィンドウ

## 使用例

```csharp
using Kogane;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public sealed class Example : MonoBehaviour
{
    public string m_text;
}

#if UNITY_EDITOR

[CustomEditor( typeof( Example ) )]
public sealed class ExampleInspector : Editor
{
    public override void OnInspectorGUI()
    {
        var textProperty = serializedObject.FindProperty( "m_text" );

        if ( GUILayout.Button( "Open" ) )
        {
            var dataArray = new KeyValuePopupWindowData[]
            {
                new( "NORMAL", "ノーマル" ),
                new( "FIRE", "ほのお" ),
                new( "WATER", "みず" ),
                new( "GRASS", "くさ" ),
            };

            KeyValuePopupWindow.Open
            (
                title: "タイプを選択",
                textDataList: dataArray,
                onSelected: data =>
                {
                    textProperty.stringValue = data.Key;
                    serializedObject.ApplyModifiedProperties();
                }
            );
        }

        EditorGUILayout.PropertyField( textProperty );
    }
}

#endif
```

![icon460](https://user-images.githubusercontent.com/6134875/192081619-117af690-f1fc-4ae8-9ed6-32d657eac2cd.gif)
