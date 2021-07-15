namespace JulienFoucher
{
	using UnityEngine;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine.UI;
	using UnityEngine.Events;
	using TMPro;

	public class TextOverTime : TextBehaviourBase
	{


		[Tooltip("Tick this if you want to display the cursor during apparition effect")]
		public bool m_EnableCursor = false;
		[Tooltip("Set the cursor string here")]
		public string m_Cursor = "_";               //show only if m_EnableCursor or m_CursorBlinkOnEnd
		[Tooltip("Tick this if you want the cursor to blink on end")]
		public bool m_CursorBlinkOnEnd = true;
		[Tooltip("Number of cursor blink per second")]
		public float m_CursorBlinkRate = 0.5f;      //show only if m_CursorBlinkOnEnd
		[Tooltip("Set this if you want the effect to pause for x seconds on a line feed")]
		public float m_PauseTimeOnLineFeed = 0;
		[Tooltip("Set this if you want the effect to blink cursor while waiting on a line feed")]
		public bool m_CursorBlinkOnLineEnd = true;  //show only if m_PauseTimeOnLineFeed > 0
		[Tooltip("Set this if you want the spaces to be instantly displayed")]
		public bool m_InstantDisplaySpace = true;
		[Tooltip("Set this if you want the tab to be instantly displayed")]
		public bool m_InstantDisplayTab = true;




		[Tooltip("Set the effect speed")]
		public float m_DelayBetweenLetters = 0.25f;
		[Tooltip("Set the number of letter before the correct one is displayed")]
		public int m_InterLettersCount = 4;

		[Tooltip("Tick this if you want to set a specific character set to be displayed during the effect instead of random characters")]
		public bool m_UseSpecificCharacters = false;
		[Tooltip("Tick this if you want the specific characters to be displayed in a specific order. Else it will display it randomly")]
		public bool m_FollowSpecificCharactersOrder = false;    //show only if m_UseSpecificCharacters
		[Tooltip("Set the specific characters displayed during the effect here")]
		public char[] m_PossibleChar;                           //show only if m_UseSpecificCharacters

		[Tooltip("This event is trigerred on the effect start")]
		public UnityEvent m_EventOnStart;
		[Tooltip("This event is trigerred on the effect end")]
		public UnityEvent m_EventOnEnd;



		bool m_TextFullyDisplayed = false;
		bool m_EffectEnabled = false;
		//float m_DelayErrorCorrection = 0;
		float m_LastTimeDisplayedLetter = 0;
		int m_CurrentIndex = 0;
		int m_InterIndex = 0;

		string m_CurrentTextToDisplay = "";
		//char[] m_InterText = new char[1] { '#' };
		private Coroutine m_MainCR;
		private Coroutine m_CursorBlinkCR;

		#region PUBLIC

		/// <summary>
		/// Display all the target text. End the effect
		/// </summary>
		/// /// <param name="disableEffect">Default true</param>
		/// /// <param name="triggerEvent">Default true. Should we trigger the "EventOnEnd" ?</param>
		public void DisplayAllTargetText(bool disableEffect = true, bool triggerEvent = true)
		{
			if (triggerEvent && m_EventOnEnd != null)
				m_EventOnEnd.Invoke();
			m_CurrentTextToDisplay = m_TargetText;
			SetDisplayedText(m_TargetText);
			m_TextFullyDisplayed = true;
			if (disableEffect)
			{
				m_EffectEnabled = false;
				if (m_MainCR != null)
					StopCoroutine(m_MainCR);
				if (m_CursorBlinkOnEnd)
					StartBlinkCursor();
			}
		}

		/// <summary>
		/// Return true if the text is fully displayed
		/// </summary>
		public bool IsFullDisplayed()
		{
			return m_TextFullyDisplayed;
		}

		/// <summary>
		/// Start the effect
		/// </summary>
		public void StartEffect()
		{
			DisplayText();
		}

		/// <summary>
		/// Start the effect and set its target text
		/// </summary>
		/// /// /// <param name="targetText">The target text you want to set</param>
		public void StartEffect(string targetText)
		{
			m_TargetText = targetText;
			DisplayText();
		}

		/// <summary>
		/// Start the effect
		/// </summary>
		/// /// <param name="textToDisplay">The text you want to be displayed</param>
		/// /// <param name="clearTextToDisplay"></param>
		public void DisplayText(string textToDisplay, bool clearTextDisplay = true)
		{
			if (m_EventOnStart != null)
				m_EventOnStart.Invoke();
			if (clearTextDisplay)
				ClearText(true);
			textToDisplay = textToDisplay.Replace("\\n", "\n");
			m_TargetText = textToDisplay;
			m_EffectEnabled = true;
			m_MainCR = StartCoroutine(CRAnimateText());
		}

		/// <summary>
		/// Enable the blinking cursor effect
		/// </summary>
		public void StartBlinkCursor()
		{
			StopBlinkCursor(true);
			m_CursorBlinkCR = StartCoroutine(CRBlinkCursor());
		}

		/// <summary>
		/// Stop the cursor blinking effect
		/// </summary>
		/// /// <param name="removeCursor">True by default, set it to false if you want the cursor to stay in its current state</param>
		public void StopBlinkCursor(bool removeCursor = true)
		{
			if (removeCursor)
			{
				SetDisplayedText(m_TargetText);
			}

			if (m_CursorBlinkCR != null)
				StopCoroutine(m_CursorBlinkCR);
		}

		/// <summary>
		/// Stop the effect and clear the text
		/// </summary>
		public void Reset()
		{
			ClearText();
		}

		/// <summary>
		/// Set the target text (the text that is displayed during the effect)
		/// </summary>
		/// /// <param name="text">The text you want to be displayed during the effect</param>
		public void SetTargetText(string text)
		{
			m_TargetText = text;
		}

		/// <summary>
		/// Clear the text displayed
		/// </summary>
		/// /// <param name="disableEffect">Default is true. If set to true, the effect will be stopped</param>
		public void ClearText(bool disableEffect = true)
		{
			SetDisplayedText("");
			m_CurrentTextDisplayed = "";
			m_CurrentTextToDisplay = "";
			m_TextFullyDisplayed = false;

			if (disableEffect)
			{
				m_EffectEnabled = false;
				if (m_CursorBlinkCR != null)
					StopBlinkCursor(false);
				if (m_MainCR != null)
					StopCoroutine(m_MainCR);
			}
			m_CurrentIndex = 0;
			m_InterIndex = 0;
		}

		#endregion

		#region PRIVATE

		private IEnumerator CRAnimateText()
		{
			float _timeError = 0;
			float _trueDelay = m_DelayBetweenLetters / (float)m_InterLettersCount;
			int _interCount = 0;

			while (m_CurrentIndex < m_TargetText.Length)
			{
				//m_InterIndex = 0;
				_interCount = 0;
				while (m_CurrentIndex < m_TargetText.Length && ((m_InstantDisplaySpace && m_TargetText[m_CurrentIndex] == ' ') || (m_InstantDisplayTab && m_TargetText[m_CurrentIndex] == '	')))
				{
					m_CurrentTextToDisplay += m_TargetText[m_CurrentIndex++];
				}
				while (_interCount < m_InterLettersCount)
				{
					_interCount++;
					if (m_UseSpecificCharacters)
					{
						if (m_FollowSpecificCharactersOrder)
						{
							if (m_PossibleChar.Length > 0)
							{
								if (m_InterIndex >= m_PossibleChar.Length)
								{
									m_InterIndex = 0;
								}
								m_CurrentTextToDisplay += m_PossibleChar[m_InterIndex];
							}
						}
						else
						{
							m_CurrentTextToDisplay += m_PossibleChar[Random.Range(0, m_PossibleChar.Length)];
						}
					}
					else
					{
						m_CurrentTextToDisplay += (char)Random.Range(33, 126);
					}
					m_InterIndex++;
					if (m_EnableCursor)
					{
						m_CurrentTextToDisplay += m_Cursor;
					}
					SetDisplayedText(m_CurrentTextToDisplay);

					if (_timeError <= _trueDelay)
					{
						m_LastTimeDisplayedLetter = Time.timeSinceLevelLoad;
						yield return new WaitForSeconds(_trueDelay);
						_timeError = (Time.timeSinceLevelLoad - m_LastTimeDisplayedLetter);
					}
					_timeError -= _trueDelay;


					if (m_CurrentIndex < m_CurrentTextToDisplay.Length)
					{
						m_CurrentTextToDisplay = m_CurrentTextToDisplay.Remove(m_CurrentIndex);
					}
				}
				SetDisplayedText(m_CurrentTextToDisplay);
				if (m_CurrentIndex < m_TargetText.Length && (m_TargetText[m_CurrentIndex] == (char)10 || m_TargetText[m_CurrentIndex] == (char)13))    //LF or CR
				{
					if (m_CursorBlinkOnLineEnd)
						StartBlinkCursor();
					yield return new WaitForSeconds(m_PauseTimeOnLineFeed);
					if (m_CursorBlinkOnLineEnd)
						StopBlinkCursor();
					while (m_TargetText[m_CurrentIndex] == (char)10 || m_TargetText[m_CurrentIndex] == (char)13)
					{
						m_CurrentTextToDisplay += m_TargetText[m_CurrentIndex++];
					}
				}
				if(m_TargetText.Length >= m_CurrentIndex + 1)
					m_CurrentTextToDisplay += m_TargetText[m_CurrentIndex++];
			}
			DisplayAllTargetText();
		}

		private IEnumerator CRBlinkCursor()
		{
			//bool _cursorDisplayed = false;
			while (true)
			{
				if (m_CurrentTextDisplayed.Length > m_CurrentTextToDisplay.Length)
				{
					SetDisplayedText(m_CurrentTextDisplayed.Remove(m_CurrentTextToDisplay.Length));
				}
				else
				{
					SetDisplayedText(m_CurrentTextToDisplay + m_Cursor);
				}
				yield return new WaitForSeconds(1f / m_CursorBlinkRate);
			}
		}

		private void OnEnable()
		{
			if (m_ResetTextOnEnable)
				ClearText();
			if (m_StartEffectOnEnable && !m_EffectEnabled)
				DisplayText();
		}


		private void Start()
		{
			if (m_StartEffectOnStart)
				DisplayText();
		}

		private void DisplayText(bool clearTextDisplay = true)
		{
			DisplayText(m_TargetText, clearTextDisplay);
		}

		#endregion
	}
}
