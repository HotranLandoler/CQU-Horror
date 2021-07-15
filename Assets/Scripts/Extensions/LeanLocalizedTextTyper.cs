using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JulienFoucher;

namespace Lean.Localization
{
	[ExecuteInEditMode]
	[DisallowMultipleComponent]
	[RequireComponent(typeof(Text))]
	[RequireComponent(typeof(TextOverTime))]
	//[HelpURL(LeanLocalization.HelpUrlPrefix + "LeanLocalizedText")]
	//[AddComponentMenu(LeanLocalization.ComponentPathPrefix + "Localized Text")]
	public class LeanLocalizedTextTyper : LeanLocalizedText
	{
		private TextOverTime _textTyper;
		//[Tooltip("If PhraseName couldn't be found, this text will be used.")]
		//public string FallbackText;

		// This gets called every time the translation needs updating
		public override void UpdateTranslation(LeanTranslation translation)
		{
			// Get the Text component attached to this GameObject
			if (_textTyper == null)
				_textTyper = GetComponent<TextOverTime>();

			// Use translation?
			if (translation != null && translation.Data is string)
			{
				_textTyper.StartEffect(LeanTranslation.FormatText((string)translation.Data, _textTyper.GetCurrText(), this, gameObject));
			}
			// Use fallback?
			else
			{
				_textTyper.StartEffect(LeanTranslation.FormatText(FallbackText, _textTyper.GetCurrText(), this, gameObject));
			}
		}

		//protected override void Awake()
		//{
		//	// Should we set FallbackText?
		//	if (string.IsNullOrEmpty(FallbackText) == true)
		//	{
		//		// Get the Text component attached to this GameObject
		//		var text = GetComponent<Text>();

		//		// Copy current text to fallback
		//		FallbackText = text.text;
		//	}
		//}
	}
}
