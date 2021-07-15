using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LoadPanel : UIPanel
{
    private SaveData[] saveDatas;

    //private AsyncOperation loadOperation;

    //[SerializeField]
    //private BlackScreen blackScreen;

    //[SerializeField]
    //private GameObject loadingIcon;

    [SerializeField]
    private SaveSlot[] saveSlots;

    public event UnityAction<int> LoadGame;

    private void Start()
    {
        UpdateSlots();
    }

    private void UpdateSlots()
    {
        saveDatas = SerializationManager<SaveData>.LoadAll();
        int i = 0;
        if (saveDatas != null)
        {
            for (; i < saveDatas.Length; i++)
            {
                saveSlots[i].SetUI(saveDatas[i]);
                saveSlots[i].Clicked += LoadSlot;
            }
        }
        for (; i < saveSlots.Length; i++)
        {
            saveSlots[i].Lock();
        }
    }

    private void LoadSlot(int slotId)
    {
        SaveData data = saveDatas[slotId];
        //blackScreen.FadeOut();
        //loadingIcon.gameObject.SetActive(true);
        SaveManager.currentSave = data;
        LoadGame?.Invoke(data.sceneId);
        //loadOperation = SceneManager.LoadSceneAsync(data.sceneId);
    }
}
