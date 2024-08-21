using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    /// <summary>
    /// Show version number
    /// </summary>
    public class VersionUi : MonoBehaviour
    {
        private void Awake()
        {
            GetComponent<Text>().text = $"v{Application.version}";
        }
    }
}