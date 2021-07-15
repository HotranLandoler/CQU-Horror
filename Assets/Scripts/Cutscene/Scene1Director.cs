using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��һĻ�Ĺ�������ʹ��
/// </summary>
public class Scene1Director : MonoBehaviour
{
    [SerializeField]
    private GameObject FireTarget;

    [SerializeField]
    private GameObject GateBlock;

    [SerializeField]
    private GameObject B5Block;

    /// <summary>
    /// ����ǹ
    /// </summary>
    public void GetPistol()
    {
        GameManager.Instance.inventory.AddItem(1);
        GameManager.Instance.Equip(1);
    }

    /// <summary>
    /// ��ǹ�߻�
    /// </summary>
    public void RandomFire()
    {
        StartCoroutine(DoRandomFire());
    }

    private IEnumerator DoRandomFire()
    {
        var dir = GameManager.Instance.player.direction;
        var Gun = GameManager.Instance.player.Equip;
        Gun.Show(true);
        //�����ǹ����
        Vector2 aim;
        if (dir.x == 0)
            aim = new Vector2(Random.Range(-1, 1), Random.Range(0, dir.y));
        else
            aim = new Vector2(Random.Range(0, dir.x), Random.Range(-1, 1));

        Gun.transform.right = aim;
        yield return new WaitForSeconds(0.2f);
        Gun.Attack();
        yield return new WaitForSeconds(1f);
        Gun.Show(false);
    }

    /// <summary>
    /// �򹷿�ǹ
    /// </summary>
    public void FireAtHound()
    {
        StartCoroutine(DoFireAtHound());
    }

    private IEnumerator DoFireAtHound()
    {
        var Gun = GameManager.Instance.player.Equip;
        Gun.Show(true);
        var aim = (FireTarget.transform.position - Gun.transform.position).normalized;
        Gun.transform.right = aim;
        yield return new WaitForSeconds(0.2f);
        Gun.Attack();
        yield return new WaitForSeconds(1f);
        Gun.Show(false);
    }

    public void StartChase()
    {
        //BGM
        GateBlock.SetActive(true);
        B5Block.SetActive(false);
    }


}
