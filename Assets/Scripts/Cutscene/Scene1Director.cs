using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 第一幕的过场动画使用
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
    /// 捡到手枪
    /// </summary>
    public void GetPistol()
    {
        GameManager.Instance.inventory.AddItem(1);
        GameManager.Instance.Equip(1);
    }

    /// <summary>
    /// 手枪走火
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
        //随机开枪方向
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
    /// 向狗开枪
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
