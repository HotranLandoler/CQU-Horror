using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 使玩家出现在正确的门口
/// </summary>
public class PlayerPosInit : MonoBehaviour
{
    [SerializeField]
    private Player player;


    /// <summary>
    /// 自底向上，每层楼从右向左
    /// </summary>
    [Header("Entrance/Exit")]
    [SerializeField]
    private Transform[] Pos;



    // Start is called before the first frame update
    void Awake()
    {
        if (GameManager.Instance.roomId != -1)
        {
            player.transform.position = Pos[GameManager.Instance.roomId].position;
            player.SetDirection(GameManager.Instance.targetDir);
            GameManager.Instance.roomId = -1;
        }
    }
}
