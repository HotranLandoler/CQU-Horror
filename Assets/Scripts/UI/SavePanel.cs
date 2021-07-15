using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePanel : UIPanel
{
    private SaveData[] saveDatas;

    private string sceneName;

    private int savingSlot = -1;

    [SerializeField]
    private ConfirmDialog confirm;

    [SerializeField]
    private SaveSlot[] saveSlots;

    private void Start()
    {
        confirm.Confirmed += ConfirmSave;
        confirm.Canceled += CancelSave;
        UpdateSlots();
        for (int j = 0; j < saveSlots.Length; j++)
        {
            saveSlots[j].Clicked += SaveSlot;
        }
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
        Show();
    }

    private async void SaveSlot(int slotId)
    {
        savingSlot = slotId;

        //提示覆盖
        if (saveDatas[slotId+1] != null)
        {   
            confirm.Show();
            return;
        }
        
        await SaveManager.SaveAsync(sceneName, false, savingSlot+1);
        UpdateSlot(savingSlot);
        EndSave();
        //UpdateSlots();
    }

    private async void ConfirmSave()
    {
        confirm.Hide();
        await SaveManager.SaveAsync(sceneName, false, savingSlot+1);
        UpdateSlot(savingSlot);
        EndSave();
    }

    private void CancelSave()
    {
        confirm.Hide();
    }

    private void EndSave()
    {
        GameManager.Instance.CurGameMode = GameMode.Gameplay;
        Hide();
    }
}
