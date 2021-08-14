using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    //private int dir;
    //private GameControls controls;
    //[SerializeField]
    //private InputManager input;

    private Player player;

    private Rigidbody2D rgbody;

    //���λ��
    private Vector3 mousePos;

    //���λ�ú�����λ�����ɽǶ�
    //private float angle;

    [SerializeField]
    private int speed = 6;

    private readonly float runSpeedMod = 1.5f;

    //�Խ����ƶ��ٶ�
    private readonly float speedMod = 0.8f;

    private float trueSpeed;

    private void Awake()
    {
        //��λ�Ͳ�����
        player = GetComponent<Player>();
        rgbody = GetComponent<Rigidbody2D>();
        //interactObjs = new List<InteractObjects>(3);
        player.StopAction += StopAction;
        player.StopAim += StopAim;
    }

    private void Start()
    {
        var controls = InputManager.Controls;
        controls.GamePlay.Move.performed += UpdateMove;
        controls.GamePlay.Move.canceled += StopMove;
        controls.GamePlay.Run.started += StartRun;
        controls.GamePlay.Run.canceled += StopRun;
        controls.GamePlay.Check.started += Interact;
        controls.GamePlay.Reload.started += Reload;
    }

    private void Update()
    {
        Aim();
        Attack();
    }

    void FixedUpdate()
    {
        if (player.IsRunning)
            trueSpeed = speed * runSpeedMod;
        else
            trueSpeed = speed;
        if (player.move.x != 0 && player.move.y != 0)
            trueSpeed *= speedMod;

        trueSpeed *= player.SpeedModEffect;
        //�����ƶ�
        rgbody.velocity = player.move * trueSpeed;
    }

    private void StopAction()
    {
        StopMove();
        StopAim();
    }

    private void UpdateMove(InputAction.CallbackContext ctx)
    {
        if (GameManager.Instance.CurGameMode != GameMode.Gameplay //#|| GameManager.Instance.bag
            || GameManager.Instance.paused)
            return;
        player.SetMove(ctx.ReadValue<Vector2>());
        if (!player.IsAiming)
            player.SetDirection(player.move);
    }

    private void StopMove(InputAction.CallbackContext ctx = default)
    {
        player.SetMove(Vector2.zero);
        player.SetRun(false);
    }

    private void Interact(InputAction.CallbackContext ctx)
    {
        //Debug.Log($"InteractNum:{player.interactObjs.Count}");
        if (player.interactObjs.Count == 0)
            return;
        if (GameManager.Instance.CurGameMode != GameMode.Gameplay || GameManager.Instance.bag
            || GameManager.Instance.paused)
            return;
        StopAction();
        var obj = player.interactObjs[player.interactObjs.Count - 1];
        obj.Interact();
        //Physics.OverlapSphere(transform.position, 1.5f)[0];
    }

    private void Aim()
    {
        if (player.Equip == null)
            return;
        //��Ӧ�ڷ�Gameplayģʽ�򱳰�״̬
        if (GameManager.Instance.CurGameMode != GameMode.Gameplay || GameManager.Instance.bag
            || GameManager.Instance.paused)
        {
            return;
        }
        if (Mouse.current.rightButton.isPressed)
        {  
            
            player.IsAiming = true;
            //Cursor.visible = true;
            //ֹͣ�ܲ�
            player.SetRun(false);
            player.Equip.Show(true);
            //��ȡ���λ��
            mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Vector2 place = (mousePos - transform.position).normalized;
            player.Equip.UpdateAim(mousePos);
            //���Ƕȸ�������
            player.SetDirection(place);

        }  
        else
        {
            StopAim();
            //player.Equip.transform.rotation = Quaternion.identity;
        }
    }

    private void StopAim()
    {
        if (player.Equip == null)
            return;
        player.IsAiming = false;
        //if (GameManager.Instance.CurGameMode == GameMode.Gameplay)
        player.Equip.Show(false);
    }

    private void Attack()
    {
        if (player.IsAiming && Mouse.current.leftButton.isPressed)
        {
            player.StopReload();
            player.Equip.Attack();
            //player.WeaponFired?.Invoke();
        }
    }

    private void StartRun(InputAction.CallbackContext ctx)
    {
        if (GameManager.Instance.CurGameMode != GameMode.Gameplay || GameManager.Instance.bag
            || GameManager.Instance.paused)
            return;
        if (!player.IsAiming)
        {
            player.SetRun(true);
        }
    }

    private void StopRun(InputAction.CallbackContext ctx)
    {
        if (GameManager.Instance.CurGameMode == GameMode.Gameplay)
        {
            player.SetRun(false);
        }     
    }

    private void Reload(InputAction.CallbackContext ctx)
    {
        if (GameManager.Instance.CurGameMode == GameMode.Gameplay && !GameManager.Instance.paused)
        {
            player.Reload();
        }
    }
}
