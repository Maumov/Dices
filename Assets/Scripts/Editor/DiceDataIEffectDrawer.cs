using UnityEditor;
using System;
using System.Linq;
using System.Collections.Generic;

[CustomEditor( typeof( DiceScriptableObject ) )]
public class DiceDataIEffectDrawer : Editor
{
    private List<Type> effectTypes;
    private string[] effectNames;
    private int selectedIndex;

    private void OnEnable()
    {
        /*
        // Find all IEffect subclasses
        effectTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany( assembly => assembly.GetTypes() )
            .Where( type => type.IsSubclassOf( typeof( DiceEffect ) ) && !type.IsAbstract )
            .ToList();

        // Prepare names for dropdown
        effectNames = effectTypes.Select( t => t.Name ).ToArray();

        // Load current selection from ScriptableObject
        DiceScriptableObject data = ( DiceScriptableObject ) target;
        if ( !string.IsNullOrEmpty( data.Effect ) )
        {
            Type currentType = Type.GetType( data.Effect );
            selectedIndex = effectTypes.IndexOf( currentType );
        }
        */
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); // Draw default fields
        /*
        DiceScriptableObject data = ( DiceScriptableObject ) target;

        EditorGUILayout.LabelField( "Effect Type", EditorStyles.boldLabel );

        // Dropdown for selecting effect type
        int newIndex = EditorGUILayout.Popup( "Effect", selectedIndex, effectNames );

        if ( newIndex != selectedIndex )
        {
            selectedIndex = newIndex;
            data.SetEffectType( effectTypes[ selectedIndex ] );
            EditorUtility.SetDirty( data ); // Mark as dirty to save changes
        }
        */
    }
}
