namespace JulienFoucher
{
	using UnityEngine.UI;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.Events;
	using TMPro;

	public class HackerTyper : TextBehaviourBase
	{

		[Tooltip("This controls the letter count displayed per keystroke")]
		public int m_LettersDisplayedPerKey = 1;

		[Tooltip("This event is trigerred on the effect end")]
		public UnityEvent m_EventOnComplete;

		bool m_EffectEnabled = false;


		#region PUBLIC
		/// <summary>
		/// Set the text you want to be displayed
		/// </summary>
		/// <param name="text">The text you want to set</param>
		public void SetTargetText(string text)
		{
			m_TargetText = text;
		}

		/// <summary>
		/// Return true if all the text is displayed
		/// </summary>
		public bool isFinished()
		{
			return m_TargetText.Length == m_CurrentTextDisplayed.Length;
		}

		/// <summary>
		/// End the effect and display all text
		/// </summary>
		/// /// <param name="disableEffect">Default true</param>
		/// /// <param name="triggerEvent">Default true. Should we trigger the onEnd event ?</param>
		public void DisplayAllText(bool disableEffect = true, bool triggerEvent = true)
		{
			if (triggerEvent && m_EventOnComplete != null)
				m_EventOnComplete.Invoke();
			if (disableEffect)
				m_EffectEnabled = false;
			SetDisplayedText(m_TargetText);
		}

		/// <summary>
		/// Start the effect
		/// </summary>
		/// /// <param name="reset">Defaut true. Set to true if you want the text to be cleared</param>
		public void StartEffect(bool reset = true)
		{
			if (reset)
				SetDisplayedText("");
			m_EffectEnabled = true;
		}

		/// <summary>
		/// Stop the effect and clear the text
		/// </summary>
		public void Reset()
		{
			SetDisplayedText("");
			m_EffectEnabled = false;
		}
		#endregion

		#region PRIVATE
		void Start()
		{
			if (m_StartEffectOnStart && !m_EffectEnabled)
				StartEffect();
		}

		void OnEnable()
		{
			if (m_StartEffectOnEnable && !m_EffectEnabled)
				StartEffect();
		}

		void Update()
		{
			if (m_EffectEnabled && HasPressedAnyKey())
			{
				if (m_TargetText.Length > m_CurrentTextDisplayed.Length + m_LettersDisplayedPerKey)
					SetDisplayedText(m_TargetText.Remove(m_CurrentTextDisplayed.Length + m_LettersDisplayedPerKey));
				else
					DisplayAllText();
			}
		}

		bool HasPressedAnyKey()
		{
			return Input.anyKeyDown && !Input.GetMouseButtonDown(0) && !Input.GetMouseButtonDown(1);
		}


		#endregion
	}
}
