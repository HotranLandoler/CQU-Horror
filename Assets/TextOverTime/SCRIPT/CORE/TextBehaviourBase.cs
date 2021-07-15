namespace JulienFoucher
{

	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.UI;
	using TMPro;

	public class TextBehaviourBase : MonoBehaviour
	{
		[TextArea]
		public string m_TargetText = "";

		[Tooltip("Tick this if you want to start the effect when the GameObject is started")]
		public bool m_StartEffectOnStart = true;
		[Tooltip("Tick this if you want to start the effect when the GameObject is enabled")]
		public bool m_StartEffectOnEnable = false;
		[Tooltip("Tick this if you want the effect to be reseted when the GameObject is enabled (stop the effect, clear the text)")]
		public bool m_ResetTextOnEnable = false;
		[Tooltip("Tick this if you want to automatically setup a target for the text display by searching on this GameObject for a Text component")]
		public bool m_AutoAssignUiText = true;
		[Tooltip("Tick this if you want to automatically setup a target for the text display by searching on this GameObject for a TextMesh component")]
		public bool m_AutoAssignTextMesh = true;
		[Tooltip("Tick this if you want to automatically setup a target for the text display by searching on this GameObject for a TextMeshPro component")]
		public bool m_AutoAssignTextMeshPro = true;

		[Tooltip("Drop here the gameobject that contains the Text component where you want the text to be displayed")]
		public Text m_UiTextToModify;
		[Tooltip("Drop here the gameobject that contains the TextMesh component where you want the text to be displayed")]
		public TextMesh m_TextMeshToModify;
		[Tooltip("Drop here the gameobject that contains the TextMeshPro component where you want the text to be displayed")]
		public TMP_Text m_TextMeshProToModify;

		protected string m_CurrentTextDisplayed = "";

		protected void SetDisplayedText(string TextToDisplay)
		{
			m_CurrentTextDisplayed = TextToDisplay;
			if (m_UiTextToModify != null)
				m_UiTextToModify.text = TextToDisplay;
			if (m_TextMeshToModify != null)
				m_TextMeshToModify.text = TextToDisplay;
			if (m_TextMeshProToModify != null)
				m_TextMeshProToModify.text = TextToDisplay;
		}

		void Awake()
		{
			if (m_AutoAssignUiText && m_UiTextToModify == null)
				m_UiTextToModify = GetComponent<Text>();
			if (m_AutoAssignTextMesh && m_TextMeshToModify == null)
				m_TextMeshToModify = GetComponent<TextMesh>();
			if (m_AutoAssignTextMeshPro && m_TextMeshProToModify == null)
				m_TextMeshProToModify = GetComponent<TMP_Text>();
		}
	}
}