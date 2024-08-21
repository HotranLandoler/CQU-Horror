using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public static class SaveManager
{
    public enum Type
    {
        Save,
        Load,
    }

    public static SaveData currentSave { get; set; }

    public static readonly int autoSaveName = 0;
    //[SerializeField]
    //private Type type = Type.Load; 

    public static async UniTask<bool> SaveAsync(string sceneName, bool autoSave = false, int saveId = 0)
    {
        Debug.Log("Save Started.");
        UIManager.Instance.ToggleSaving(true);
        SaveData data = GameManager.Instance.Save(sceneName);
        bool success;
        if (autoSave)
        {
            success = await SerializationManager<SaveData>.SaveAsync("0", data);
        }
        else
            success = await SerializationManager<SaveData>.SaveAsync(saveId.ToString(), data);
        UIManager.Instance.ToggleSaving(false);
        if (success) UIManager.Instance.ShowSavedTip();
        return success;
    }
}
