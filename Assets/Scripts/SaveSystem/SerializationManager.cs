using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class SerializationManager<T>
    where T : class
{
    public static readonly int filesTotal = 4;
    public static readonly string SaveFolder = $"{Application.persistentDataPath}/Saves";
    //public static readonly string FileSuffix = "_save";
    public static readonly string FileExtension = ".save";

    static SerializationManager()
    {
        if (!Directory.Exists(SaveFolder))
        {
            Directory.CreateDirectory(SaveFolder);
        }
    }

    /// <summary>
    /// ��Ŀ¼�������ļ��ж�ȡ����
    /// </summary>
    /// <param name="exclude">�ų����ļ���</param>
    /// <returns>Data���飬��Ԫ��Ϊnull</returns>
    public static T[] LoadAll(int excludeName = -1)
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(SaveFolder);
        FileInfo[] files = directoryInfo.GetFiles($"*{FileExtension}");
        if (files.Length == 0) return null;
        //List<T> ts = new List<T>(filesTotal);
        T[] ts = new T[filesTotal];
        var excludeFile = $"{excludeName}{FileExtension}";
        BinaryFormatter formatter = new BinaryFormatter();
        for (int i = 0; i < ts.Length; i++)
        {
            //�����ų��ļ�(fileinfo.name������׺)
            if (i == excludeName) continue;

            var path = $"{SaveFolder}/{i.ToString()}{FileExtension}";
            if (!File.Exists(path)) continue;
            
            using FileStream file = File.OpenRead(path);
            try
            {
                T save = formatter.Deserialize(file) as T;
                //if (save != default) ts.Add(save);
                if (save != default) ts[i] = save;
            }
            catch (System.Exception e)
            {
                Debug.LogError(e.Message);
                Debug.LogError($"Failed to load file at {files[i].FullName}");
                return null;
            }
            //T item = Load(files[i].Name);
            //if (item != default) ts.Add(item);
        }
        return ts;
    }

    public static async UniTask<bool> SaveAsync(string saveName, T saveData)
    {
        Debug.Log("Saving async...");
        BinaryFormatter serializer = new();
        string path = $"{SaveFolder}/{saveName}{FileExtension}";
        Debug.Log(path);
        await using FileStream file = File.Create(path);
        try
        {
            serializer.Serialize(file, saveData);
        }
        catch (System.Exception e)
        {
            Debug.LogError(e.Message);
            Debug.LogError($"Failed to save file at {path}");
        }

        return true;
    }

    public static T Load(string saveName)
    {
        string path = $"{SaveFolder}/{saveName}{FileExtension}";
        if (!File.Exists(path))
            return default;
        BinaryFormatter formatter = new BinaryFormatter();
        using FileStream file = File.Open(path, FileMode.Open);
        try
        {
            T save = formatter.Deserialize(file) as T;
            return save;
        }
        catch (System.Exception e)
        {
            Debug.LogError(e.Message);
            Debug.LogError($"Failed to load file at {path}");
            return default;
        }
        //finally
        //{
        //    file.Close();
        //} 
    }
}
