using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 场所移动类
/// </summary>
public abstract class SceneTravelBase : MonoBehaviour
{
    [SerializeField]
    protected int sceneId = 4;

    [SerializeField]
    protected int roomId;

    [SerializeField]
    protected Vector2 targetDir = Vector2.down;

    protected void SceneTravel()
    {
        GameManager.Instance.roomId = roomId;
        GameManager.Instance.targetDir = targetDir;
        GameManager.Instance.LoadScene(sceneId);
    }
}
