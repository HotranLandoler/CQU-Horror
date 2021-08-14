using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SavePanel : UIPanel
{
    private SaveData[] saveDatas;

    private string sceneName;

    private int savingSlot = -1;

    [SerializeField]
    private ConfirmDialog confirm;

    [SerializeField]
    private SaveSlot[] saveSlots;

    [SerializeField]
    private Button exitButton;

    private void Start()
    {
        confirm.Confirmed += ConfirmSave;
        //confirm.Canceled += CancelSave;
        UpdateSlots();
        for (int j = 0; j < saveSlots.Length; j++)
        {
            saveSlots[j].Clicked += SaveSlot;
        }
        exitButton.onClick.AddListener(() => UIManager.Instance.CloseWindows());
    }

    private void UpdateSlots()
    {
        saveDatas = SerializationManager<SaveData>.LoadAll(SaveManager.autoSaveName); 
        if (saveDatas != null)
        {
            for (int i = 0; i < saveSlots.Length; i++)
            {
                saveSlots[i].SetUI(saveDatas[i+1]);             
            }
        }
        //for (; i < saveSlots.Length; i++)
        //{
        //    saveSlots[i].Lock();
        //}
    }

    private void UpdateSlot(int id)
    {
        //0号栏位对应1号存档（排除了自动存档）

        saveDatas[id+1] = SerializationManager<SaveData>.Load((id+1).ToString());
        saveSlots[id].SetUI(saveDatas[id+1]);
    }

    public void StartSave(string sceneName)
    {
        GameManager.Instance.CurGameMode = GameMode.Timeline;
        this.sceneName = sceneName;
        Open();
    }

    private async void SaveSlot(int slotId)
    {
        savingSlot = slotId;

        //提示覆盖
        if (saveDatas[slotId+1] != null)
        {   
            confirm.Open();
            return;
        }
        
        await SaveManager.SaveAsync(sceneName, false, savingSlot+1);
        AudioManager.Instance.PlaySaveGameSound();
        UpdateSlot(savingSlot);
        Close();
        //UpdateSlots();
    }

    private async void ConfirmSave()
    {
        confirm.Close();
        await SaveManager.SaveAsync(sceneName, false, savingSlot+1);
        AudioManager.Instance.PlaySaveGameSound();
        UpdateSlot(savingSlot);
        Close();
    }

    //private void CancelSave()
    //{
    //    confirm.Hide();
    //}

    private void EndSave()
    {
        GameManager.Instance.CurGameMode = GameMode.Gameplay;
        //Close();
    }

    public override void Open()
    {
        base.Open();
        UIManager.Instance.AddWindow(this);
    }

    public override void Close()
    {
        EndSave();
        base.Close();
    }
}
