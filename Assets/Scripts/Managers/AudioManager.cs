using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    private AudioSource _as;

    [SerializeField]
    private AudioClip bagSound;

    [SerializeField]
    private AudioClip[] pickItem;

    [SerializeField]
    private AudioClip storeItem;

    [SerializeField]
    private AudioClip equip;

    [SerializeField]
    private AudioClip heartBeat;

    [SerializeField]
    private AudioClip bulletShell;

    [SerializeField]
    private AudioClip gunNoAmmo;

    [Header("UI")]
    [SerializeField]
    private AudioClip skillShop;

    [SerializeField]
    private AudioClip skillUnlocked;

    [SerializeField]
    private AudioClip learnSkillFailed;

    [SerializeField]
    private AudioClip closeItemBox;

    [SerializeField]
    private AudioClip startGame;

    [SerializeField]
    private AudioClip saveGame;

    // Start is called before the first frame update
    void Awake()
    {
        _instance = this;
        _as = GetComponent<AudioSource>();
    }

    public void PlayHeartBeat(bool play)
    {
        if (play)
        {
            if (_as.clip == heartBeat && _as.isPlaying) return;
            _as.clip = heartBeat;
            _as.Play();
            //StartCoroutine(effectAudioSource.FadeIn(1, 1));
        }
        else
            _as.Stop();
    }

    public void PlayBagSound()
    {
        _as.PlayOneShot(bagSound);
    }

    public void PlaySound(AudioClip clip)
    {
        _as.PlayOneShot(clip);
    }

    public void PlayBulletShellSound()
    {
        _as.PlayOneShot(bulletShell);
    }

    public void PlayNoAmmoSound()
    {
        _as.PlayOneShot(gunNoAmmo);
    }

    public void PlayPickItemSound(Item item)
    {
        if (item.itemType == ItemType.Weapon)
        {
            if ((item as Weapon).weaponType == WeaponType.Gun)
                _as.PlayOneShot(pickItem[1]);
        }
        else _as.PlayOneShot(pickItem[0]);
    }

    public void PlayStoreItemSound()
    {
        _as.PlayOneShot(storeItem);
    }

    public void PlayEquipSound()
    {
        _as.PlayOneShot(equip);
    }

    public void PlaySkillUnlockSound()
    {
        _as.PlayOneShot(skillUnlocked);
    }

    public void PlayLearnSkillFailedSound()
    {
        _as.PlayOneShot(learnSkillFailed);
    }

    public void PlayStartGameSound()
        => _as.PlayOneShot(startGame);

    public void PlaySaveGameSound()
        => _as.PlayOneShot(saveGame);

    public void PlayOpenShopSound()
        => _as.PlayOneShot(skillShop);

    public void PlayCloseBoxSound()
        => _as.PlayOneShot(closeItemBox);
}
