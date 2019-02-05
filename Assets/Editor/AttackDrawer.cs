using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//[CustomPropertyDrawer(typeof(AttackAnimations))]
public class AttackDrawer : PropertyDrawer
{
    //public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    //{
    //    EditorGUI.BeginProperty(position, label, property);

    //    position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Keyboard), label);

    //    var indent = EditorGUI.indentLevel;
    //    EditorGUI.indentLevel = 0;

    //    var leftAttackRect = new Rect(position.x, position.y, 100, position.height);
    //    var leftDamageRect = new Rect(position.x + 170, position.y, 60, position.height);

    //    EditorGUI.PropertyField(leftAttackRect, property.FindPropertyRelative("leftAttacks"), GUIContent.none);
    //    EditorGUI.PropertyField(leftDamageRect, property.FindPropertyRelative("damage"), GUIContent.none);


    //    EditorGUI.indentLevel = indent;

    //    EditorGUI.EndProperty();
    //}

}
