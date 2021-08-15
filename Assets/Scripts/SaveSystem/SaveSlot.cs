using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SaveSlot : MonoBehaviour
{
    private Button button;

    //private SaveData saveData;

    //private SaveManager manager;

    //[SerializeField]
    //private SaveManager.Type type = SaveManager.Type.Load;

    [SerializeField]
    private int slotId;

    [SerializeField]
    private Text sceneNameText;

    [SerializeField]
    private Text DateTimeText;

    [SerializeField]
    private Text ModeText;

    public event UnityAction<int> Clicked;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => Clicked?.Invoke(slotId));
    }

    public void SetUI(SaveData saveData)
    {  
        //this.saveData = saveData;
        if (saveData == null)
        {
            sceneNameText.text = Game.gameStrings.EmptySave;
            DateTimeText.text = string.Empty;
            ModeText.text = string.Empty;
            return;
        }
        sceneNameText.text = saveData.sceneName;
        DateTimeText.text = saveData.dateTime.ToString();
        //ModeText.text = System.Enum.GetName(typeof(Game.Difficulty), saveData.difficulty);
        ModeText.text = Game.gameStrings.GameMode[(int)saveData.difficulty];
        //if (type == SaveManager.Type.Load)
        //    loadButton.onClick.AddListener(Load);
        //else
        //    loadButton.onClick.AddListener(Save);
        button.interactable = true;
    }

    public void Lock()
    {
        button.interactable = false;
    }

    //private void Load()
    //{
    //    manager.LoadSlot(saveData);
    //}

    //private void Save()
    //{
    //    manager.SaveSlot(slotId);
    //}
}
