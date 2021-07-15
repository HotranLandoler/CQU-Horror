namespace JulienFoucher
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEditor;

	[CustomEditor(typeof(TextOverTime))]
	[CanEditMultipleObjects]
	public sealed class TextOverTimeEditor : Editor
	{
		SerializedProperty m_StartEffectOnStart;
		SerializedProperty m_StartEffectOnEnable;
		SerializedProperty m_ResetTextOnEnable;
		SerializedProperty m_AutoAssignUiText;
		SerializedProperty m_AutoAssignTextMesh;
		SerializedProperty m_AutoAssignTextMeshPro;
		SerializedProperty m_UiTextToModify;
		SerializedProperty m_TextMeshToModify;
		SerializedProperty m_TextMeshProToModify;
		SerializedProperty m_EnableCursor;
		SerializedProperty m_Cursor;
		SerializedProperty m_CursorBlinkOnEnd;
		SerializedProperty m_CursorBlinkRate;
		SerializedProperty m_PauseTimeOnLineFeed;
		SerializedProperty m_CursorBlinkOnLineEnd;
		SerializedProperty m_InstantDisplaySpace;
		SerializedProperty m_InstantDisplayTab;
		//SerializedProperty m_TargetText;

		SerializedProperty m_DelayBetweenLetters;
		SerializedProperty m_InterLettersCount;

		SerializedProperty m_UseSpecificCharacters;
		SerializedProperty m_FollowSpecificCharactersOrder;
		SerializedProperty m_PossibleChar;
		SerializedProperty m_EventOnStart;
		SerializedProperty m_EventOnEnd;


		void OnEnable()
		{
			m_StartEffectOnStart = serializedObject.FindProperty("m_StartEffectOnStart");
			m_StartEffectOnEnable = serializedObject.FindProperty("m_StartEffectOnEnable");
			m_ResetTextOnEnable = serializedObject.FindProperty("m_ResetTextOnEnable");
			m_AutoAssignUiText = serializedObject.FindProperty("m_AutoAssignUiText");
			m_AutoAssignTextMesh = serializedObject.FindProperty("m_AutoAssignTextMesh");
			m_AutoAssignTextMeshPro = serializedObject.FindProperty("m_AutoAssignTextMeshPro");
			m_UiTextToModify = serializedObject.FindProperty("m_UiTextToModify");
			m_TextMeshToModify = serializedObject.FindProperty("m_TextMeshToModify");
			m_TextMeshProToModify = serializedObject.FindProperty("m_TextMeshProToModify");

			m_EnableCursor = serializedObject.FindProperty("m_EnableCursor");
			m_Cursor = serializedObject.FindProperty("m_Cursor");
			m_CursorBlinkOnEnd = serializedObject.FindProperty("m_CursorBlinkOnEnd");
			m_CursorBlinkRate = serializedObject.FindProperty("m_CursorBlinkRate");
			m_PauseTimeOnLineFeed = serializedObject.FindProperty("m_PauseTimeOnLineFeed");
			m_CursorBlinkOnLineEnd = serializedObject.FindProperty("m_CursorBlinkOnLineEnd");
			m_InstantDisplaySpace = serializedObject.FindProperty("m_InstantDisplaySpace");
			m_InstantDisplayTab = serializedObject.FindProperty("m_InstantDisplayTab");
			//m_TargetText = serializedObject.FindProperty("m_TargetText");

			m_DelayBetweenLetters = serializedObject.FindProperty("m_DelayBetweenLetters");
			m_InterLettersCount = serializedObject.FindProperty("m_InterLettersCount");

			m_UseSpecificCharacters = serializedObject.FindProperty("m_UseSpecificCharacters");
			m_FollowSpecificCharactersOrder = serializedObject.FindProperty("m_FollowSpecificCharactersOrder");
			m_PossibleChar = serializedObject.FindProperty("m_PossibleChar");
			m_EventOnStart = serializedObject.FindProperty("m_EventOnStart");
			m_EventOnEnd = serializedObject.FindProperty("m_EventOnEnd");
		}

		public override void OnInspectorGUI()
		{
			serializedObject.Update();
			TextOverTime TextOverTimeScript = target as TextOverTime;

			//m_TargetText = EditorGUILayout.TextArea(TextOverTimeScript.m_TargetText, GUILayout.MaxHeight(300));
			//EditorGUILayout.PropertyField(m_TargetText);
			EditorGUILayout.LabelField("Enter target text here");
			TextOverTimeScript.m_TargetText = EditorGUILayout.TextArea(TextOverTimeScript.m_TargetText, GUILayout.MaxWidth(400));

			GUILayout.Space(15);

			EditorGUILayout.PropertyField(m_StartEffectOnStart);
			EditorGUILayout.PropertyField(m_StartEffectOnEnable);
			EditorGUILayout.PropertyField(m_ResetTextOnEnable);

			GUILayout.Space(15);

			EditorGUILayout.PropertyField(m_AutoAssignUiText);
			if (!TextOverTimeScript.m_AutoAssignUiText)
			{
				EditorGUILayout.PropertyField(m_UiTextToModify);
			}

			EditorGUILayout.PropertyField(m_AutoAssignTextMesh);
			if (!TextOverTimeScript.m_AutoAssignTextMesh)
			{
				EditorGUILayout.PropertyField(m_TextMeshToModify);
			}

			EditorGUILayout.PropertyField(m_AutoAssignTextMeshPro);
			if (!TextOverTimeScript.m_AutoAssignTextMeshPro)
			{
				EditorGUILayout.PropertyField(m_TextMeshProToModify);
			}

			GUILayout.Space(15);

			EditorGUILayout.PropertyField(m_EnableCursor);
			EditorGUILayout.PropertyField(m_CursorBlinkOnEnd);
			if (TextOverTimeScript.m_EnableCursor || TextOverTimeScript.m_CursorBlinkOnEnd)
			{
				EditorGUILayout.PropertyField(m_Cursor);
			}
			if (TextOverTimeScript.m_CursorBlinkOnEnd)
			{
				EditorGUILayout.PropertyField(m_CursorBlinkRate);
			}

			GUILayout.Space(15);

			EditorGUILayout.PropertyField(m_PauseTimeOnLineFeed);
			if (TextOverTimeScript.m_PauseTimeOnLineFeed > 0)
			{
				EditorGUILayout.PropertyField(m_CursorBlinkOnLineEnd);
			}

			GUILayout.Space(15);

			EditorGUILayout.PropertyField(m_InstantDisplaySpace);
			EditorGUILayout.PropertyField(m_InstantDisplayTab);

			EditorGUILayout.PropertyField(m_DelayBetweenLetters);
			EditorGUILayout.PropertyField(m_InterLettersCount);

			GUILayout.Space(15);

			EditorGUILayout.PropertyField(m_UseSpecificCharacters);
			if (TextOverTimeScript.m_UseSpecificCharacters)
			{
				EditorGUILayout.PropertyField(m_FollowSpecificCharactersOrder);
				EditorGUILayout.PropertyField(m_PossibleChar, true);
			}

			GUILayout.Space(15);

			EditorGUILayout.PropertyField(m_EventOnStart);
			EditorGUILayout.PropertyField(m_EventOnEnd);

			serializedObject.ApplyModifiedProperties();
		}
	}
}
