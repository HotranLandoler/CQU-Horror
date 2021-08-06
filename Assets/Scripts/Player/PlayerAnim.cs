using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAnim : MonoBehaviour
{
    private Player player;
    private Animator animator;
    private AudioSource audioSource;
    private SpriteRenderer spriteRenderer;

    private Color color;

    [SerializeField]
    private Image reloadBar;

    [Tooltip("脚步声音效")]
    [SerializeField]
    private AudioClip[] footStepSounds;

    //[Tooltip("枪声音效")]
    //[SerializeField]
    //private AudioClip[] gunSounds;

    [Tooltip("装弹音效")]
    [SerializeField]
    private AudioClip reloadSound;

    private void Awake()
    {
        player = GetComponent<Player>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        player.DirChanged += UpdateDir;
        player.MoveChanged += UpdateMove;
        //player.WeaponFired += PlayGunSound;
        player.RunChanged += UpdateRun;
        player.StartReloading += OnStartReloading;
        player.StopReloading += OnStopReloading;
    }

    private void OnDisable()
    {
        player.DirChanged -= UpdateDir;
        player.MoveChanged -= UpdateMove;
        //player.WeaponFired += PlayGunSound;
        player.RunChanged -= UpdateRun;
        player.StartReloading -= OnStartReloading;
        player.StopReloading -= OnStopReloading;
    }

    private void Update()
    {
        if (player.IsReloading)
        {
            reloadBar.fillAmount = player.reloadTimer / player.ReloadTime;
        }
    }

    // Update is called once per frame
    //void Update()
    //{
    //    if (GameManager.Instance.CurGameMode != GameMode.Gameplay)
    //        return;
    //    animator.SetBool("Move", player.move.magnitude > 0.1f);
    //    UpdateDir();
    //}

    public void PlayFootstepSound1(int dir)
    {
        //if (idx >= footStepSounds.Length)
        //throw new System.ArgumentOutOfRangeException(nameof(idx));
        if (player.direction == Direction.Dir2Vector(dir)) 
           audioSource.PlayOneShot(footStepSounds[0]);
    }

    public void PlayFootstepSound2(int dir)
    {
        if (player.direction == Direction.Dir2Vector(dir))
            audioSource.PlayOneShot(footStepSounds[1]);
    }

    private void UpdateDir()
    {
        animator.SetFloat("DirX", player.direction.x);
        animator.SetFloat("DirY", player.direction.y);
    }

    private void UpdateMove()
    {
        //animator.SetBool("Run", player.IsRunning);
        animator.SetBool("Move", player.move.magnitude > 0.1f);
    }

    private void UpdateRun()
    {
        animator.SetBool("Run", player.IsRunning);
    }

    //public void PlayGunSound()
    //{
    //    audioSource.PlayOneShot(gunSounds[0]);
    //}
    private IEnumerator DamageEffect()
    {
        yield return null;
        //not work with URP

        //color = spriteRenderer.color;
        //spriteRenderer.color = new Color(color.r, 125, 125);
        //yield return new WaitForSeconds(0.3f);
        //spriteRenderer.color = new Color(color.r, 75, 75);
        //yield return new WaitForSeconds(0.3f);
        //spriteRenderer.color = new Color(color.r, 125, 125);
        //yield return new WaitForSeconds(0.3f);
        //spriteRenderer.color = color;

        //material.color = endColor;
        //Not really sure why I need this other than to remove an errorcode
        //yield return null;
    }

    private void OnStartReloading()
    {
        audioSource.PlayOneShot(reloadSound);
        reloadBar.gameObject.SetActive(true);
    }

    private void OnStopReloading()
    {
        reloadBar.gameObject.SetActive(false);
    }
}
