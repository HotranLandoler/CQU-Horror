namespace JulienFoucher
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEditor;

	[CustomEditor(typeof(HackerTyper))]
	[CanEditMultipleObjects]
	public sealed class HackerTyperEditor : Editor
	{
		//SerializedProperty m_TargetText;
		SerializedProperty m_StartEffectOnStart;
		SerializedProperty m_StartEffectOnEnable;
		SerializedProperty m_LettersDisplayedPerKey;
		SerializedProperty m_AutoAssignUiText;
		SerializedProperty m_AutoAssignTextMesh;
		SerializedProperty m_AutoAssignTextMeshPro;
		SerializedProperty m_UiTextToModify;
		SerializedProperty m_TextMeshToModify;
		SerializedProperty m_TextMeshProToModify;
		SerializedProperty m_EventOnComplete;


		void OnEnable()
		{
			//m_TargetText = serializedObject.FindProperty("m_TargetText");
			m_StartEffectOnStart = serializedObject.FindProperty("m_StartEffectOnStart");
			m_StartEffectOnEnable = serializedObject.FindProperty("m_StartEffectOnEnable");
			m_LettersDisplayedPerKey = serializedObject.FindProperty("m_LettersDisplayedPerKey");
			m_AutoAssignUiText = serializedObject.FindProperty("m_AutoAssignUiText");
			m_AutoAssignTextMesh = serializedObject.FindProperty("m_AutoAssignTextMesh");
			m_AutoAssignTextMeshPro = serializedObject.FindProperty("m_AutoAssignTextMeshPro");
			m_UiTextToModify = serializedObject.FindProperty("m_UiTextToModify");
			m_TextMeshToModify = serializedObject.FindProperty("m_TextMeshToModify");
			m_TextMeshProToModify = serializedObject.FindProperty("m_TextMeshProToModify");
			m_EventOnComplete = serializedObject.FindProperty("m_EventOnComplete");
		}

		public override void OnInspectorGUI()
		{
			serializedObject.Update();
			HackerTyper HackerTyperScript = target as HackerTyper;

			//m_TargetText = EditorGUILayout.TextArea(TextOverTimeScript.m_TargetText, GUILayout.MaxHeight(300));
			//EditorGUILayout.PropertyField(m_TargetText);
			EditorGUILayout.LabelField("Enter target text here");
			HackerTyperScript.m_TargetText = EditorGUILayout.TextArea(HackerTyperScript.m_TargetText);

			GUILayout.Space(15);

			EditorGUILayout.PropertyField(m_LettersDisplayedPerKey);


			GUILayout.Space(15);
			EditorGUILayout.PropertyField(m_StartEffectOnStart);
			EditorGUILayout.PropertyField(m_StartEffectOnEnable);
			GUILayout.Space(15);

			EditorGUILayout.PropertyField(m_AutoAssignUiText);
			if (!HackerTyperScript.m_AutoAssignUiText)
			{
				EditorGUILayout.PropertyField(m_UiTextToModify);
			}

			EditorGUILayout.PropertyField(m_AutoAssignTextMesh);
			if (!HackerTyperScript.m_AutoAssignTextMesh)
			{
				EditorGUILayout.PropertyField(m_TextMeshToModify);
			}

			EditorGUILayout.PropertyField(m_AutoAssignTextMeshPro);
			if (!HackerTyperScript.m_AutoAssignTextMeshPro)
			{
				EditorGUILayout.PropertyField(m_TextMeshProToModify);
			}

			GUILayout.Space(15);

			EditorGUILayout.PropertyField(m_EventOnComplete);

			serializedObject.ApplyModifiedProperties();
		}
	}
}