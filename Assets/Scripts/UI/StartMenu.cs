using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cinemachine;
using UnityEngine.EventSystems;

public class StartMenu : MonoBehaviour
{
    private AudioSource audioSource;

    //[SerializeField]
    //private Lean.Localization.LeanLocalization localizer;

    [SerializeField]
    private BlackScreen blackScreen;

    [Header("Base")]
    [SerializeField]
    private Button startButton;

    [SerializeField]
    private Button loadButton;

    [SerializeField]
    private Button optionsButton;

    [SerializeField]
    private Button exitButton;

    private bool loadUI = false;

    private bool optionUI = false;

    [SerializeField]
    private GameObject loadingScreen;

    [Header("Start")]
    [SerializeField]
    private GameObject RayBlocker;

    [SerializeField]
    private ModeSelect ModeSelectPanel;

    [Header("Load")]
    [SerializeField]
    private LoadPanel loadPanel;

    [Header("Options")]
    [SerializeField]
    private GameObject optionsPanel;

    [SerializeField]
    private Toggle cnToggle;

    [SerializeField]
    private Toggle enToggle;


    // Start is called before the first frame update
    void Awake()
    {
        cnToggle.onValueChanged.AddListener(LangOptionChanged);
        enToggle.onValueChanged.AddListener(LangOptionChanged);
        startButton.onClick.AddListener(StartClicked);
        loadButton.onClick.AddListener(Load);
        optionsButton.onClick.AddListener(Options);
        exitButton.onClick.AddListener(Quit);        
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        Init();
        EventSystem.current.SetSelectedGameObject(startButton.gameObject);
    }

    private void Init()
    {
        //Lean.Localization.LeanLocalization.UpdateTranslations();
        //if (localizer.CurrentLanguage.Equals("English"))
        //{
        //    enToggle.isOn = true;
        //}
        //else
        //    cnToggle.isOn = true;
        ModeSelectPanel.GameStart += StartGame;
        loadPanel.LoadGame += (scene) => StartCoroutine(LoadScene(scene));
    }

    private void StartClicked()
    {
        RayBlocker.SetActive(true);
        ModeSelectPanel.Show();
        //StartCoroutine(LoadScene(1));
    }

    private void StartGame(Game.Difficulty difficulty)
    {
        Game.difficulty = difficulty;
        StartCoroutine(LoadScene(1));
    }

    private void Load()
    {
        if (optionUI)
        {
            TogglePanel(optionsPanel, false);
            optionUI = false;
        }
        if (loadUI)
        {
            loadPanel.Hide();
            loadUI = false;
        }
        else
        {
            loadUI = true;
            loadPanel.Show();
        }
        
        //cam2.Priority = 15;
    }

    private void Options()
    {
        if (loadUI)
        {
            loadPanel.Hide();
            loadUI = false;
        }
        if (optionUI)
        {
            TogglePanel(optionsPanel, false);
            optionUI = false;
        }
        else
        {
            optionUI = true;
            TogglePanel(optionsPanel, true);
        }
        
        //cam3.Priority = 15;
    }

    private void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void TogglePanel(GameObject panel, bool active)
    {
        if (active)
        {
            panel.GetComponent<Animator>().Play("Bag_Open");
            panel.GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
        else
        {
            panel.GetComponent<Animator>().Play("Bag_Close");
            panel.GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
    }



    private void LangOptionChanged(bool active)
    {
        //if (active)
        //{
        //    if (!cnToggle.isOn)
        //        localizer.SetCurrentLanguage("English");
        //    else
        //        localizer.SetCurrentLanguage("Chinese");
        //}
    }

    private IEnumerator LoadScene(int sceneId)
    {
        loadingScreen.SetActive(true);
        blackScreen.FadeOut();
        yield return audioSource.FadeOut(1);
        SceneManager.LoadScene(sceneId);
    }
}
