using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private Button resumeButton;

    [SerializeField]
    private Button optionsButton;

    [SerializeField]
    private Button exitButton;

    private void Awake()
    {
        resumeButton.onClick.AddListener(Resume);
        optionsButton.onClick.AddListener(Options);
        exitButton.onClick.AddListener(Exit);
    }

    private void Resume()
    {
        GameManager.Instance.TogglePauseMenu();
    }

    private void Options()
    {
        UIManager.Instance.ToggleOptionsInGame(true);
    }

    private void Exit()
    {
        Destroy(GameManager.Instance.gameObject);
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

}
