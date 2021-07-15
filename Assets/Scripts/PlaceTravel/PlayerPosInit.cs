using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ʹ��ҳ�������ȷ���ſ�
/// </summary>
public class PlayerPosInit : MonoBehaviour
{
    [SerializeField]
    private Player player;


    /// <summary>
    /// �Ե����ϣ�ÿ��¥��������
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
