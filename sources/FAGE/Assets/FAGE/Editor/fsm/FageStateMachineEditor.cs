using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(FageStateMachine))]
public class FageStateMachineEditor : Editor {

	public override void OnInspectorGUI () {
		DrawDefaultInspector ();
		FageStateMachine fsm = (FageStateMachine)target;

		EditorGUILayout.LabelField("ID", fsm.id);

	}
}
