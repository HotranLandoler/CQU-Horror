using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance
    {
        get { return _instance; }
    }

    /// <summary>
    /// 游戏内部变量
    /// </summary>
    public GameVariables gameVariables;

	public GameMode CurGameMode = GameMode.Gameplay;

    private Player _player;

    public Player player
    {
        get
        {
            if (_player == null)
                _player = GameObject.FindGameObjectWithTag("Player")?.GetComponent<Player>();
            return _player;
        }
    }

	public event UnityAction<int> HpChanged;

	public event UnityAction<int> SanChanged;

    public Item[] ItemData;

    //public Dictionary<Item, int> ItemNums = new Dictionary<Item, int>();

    //public Dictionary<Weapon, int> gunAmmos = new Dictionary<Weapon, int>();

    public Inventory inventory;

    public GameObject[] WeaponPrefabs;

    public GameObject bloodPrefab;

    public int MaxHp { get; private set; }

    public readonly int maxHp_Norm = 100;

    public readonly int maxHp_Hard = 60;

    public readonly int maxSan = 100;

    public bool bag = false;

    public bool paused = false;

    public int roomId = -1;

    public Vector2 targetDir = Vector2.down;

    //private bool isBagEnabled = false;

    private bool enemyDetected = false;

    private float sanTimer = 0;

    private readonly float sanTime = 1;

    public Item[] startItems;

    private string[] dialogs;
    private int dialogIndex = 0;

    public event UnityAction DialogueEnded;
    //private AudioSource effectAudioSource;

    //[SerializeField]
    //private AudioClip heartBeat;

    private int hp;
    /// <summary>
    /// 玩家生命值
    /// </summary>
    public int Hp
    {
        get
        {
            return hp;
        }
        set
        {
            if (value > MaxHp)
            {
                value = MaxHp;
            }
            if (value <= 0)
            {
                value = 0;
                GameOver(0);
            }
            hp = value;
            //HP改变时UI血条改变
            //Debug.Log(value);
            HpChanged?.Invoke(value);
        }
    }

    private int san;
    /// <summary>
    /// 玩家San值
    /// </summary>
    public int Sanity
    {
        get
        {
            return san;
        }
        set
        {
            if (value > maxSan)
            {
                value = maxSan;
            }
            if (value <= 0)
            {
                value = 0;
                GameOver(1);
            }
            san = value;
            SanChanged?.Invoke(value);
        }
    }

    private Weapon EquippedWeaponData;

    private PlayableDirector activeDirector;

    private void Awake()
    {
        if (_instance == null)
        {
            //First run, set the instance
            _instance = this;
            DontDestroyOnLoad(gameObject);
            //初始化放在此处防止多次添加监听
            //Initialize();
            InitFromData(SaveManager.currentSave);
            SaveManager.currentSave = null;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else if (_instance != this)
        {
            Destroy(this);
            ////Instance is not the same as the one we have, destroy old one, and reset to newest one
            //Destroy(instance.gameObject);
            //instance = this;
            //DontDestroyOnLoad(gameObject);
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    //private void Start()
    //{
    //}

    private void Update()
    {
        if (CurGameMode != GameMode.Gameplay)
            return;
        //有敌人时持续减少San值
        if (sanTimer > 0)
            sanTimer -= Time.deltaTime;
        if (sanTimer <= 0 && enemyDetected)
        {
            Sanity -= 1;
            sanTimer = sanTime;
        }
        //#简单自动回血
    }

    //每个场景开始
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //foreach (var item in ItemNums)
        //{
        //    Debug.Log(item.Key.Name);
        //}
        Debug.Log("Scene Loaded");
        enemyDetected = false;
        _player = null;
        if (player != null)
            player.DamageTaken += () => UIManager.Instance.FlashRed();
        HpChanged?.Invoke(hp);
        SanChanged?.Invoke(san);
        UIManager.Instance.Initialize();
        Equip(EquippedWeaponData, false);
    }

    private void InitFromData(SaveData data)
    {
        //初始化物品
        inventory = new Inventory(data);
        if (data == null)
        {
            gameVariables = new GameVariables();
            Initialize();
            return;
        }
        Game.difficulty = data.difficulty;
        InitMaxHp();
        gameVariables = new GameVariables(data.gameFlags);
        //for (int i = 0; i < data.itemNums.Length; i++)
        //{
        //    if (data.itemNums[i] > 0)
        //        ItemNums.Add(GetItemData(i), data.itemNums[i]);
        //    if (data.gunAmmos[i] > 0)
        //        gunAmmos.Add(GetItemData(i) as Weapon, data.gunAmmos[i]);
        //}
        Hp = data.Hp;
        Sanity = data.Sanity;
        if (data.EquipId > 0)
            EquippedWeaponData = ItemData[data.EquipId] as Weapon;
        //Equip(data.EquipId, false);
        player.transform.position = new Vector2(data.playerPosX, data.playerPosY);
        player.SetDirection(data.playerDir);
        UIManager.Instance.UpdateInventory();
    }

    public SaveData Save(string sceneName)
    {
        SaveData data = new SaveData()
        {
            sceneId = SceneManager.GetActiveScene().buildIndex,
            sceneName = sceneName,
            difficulty = Game.difficulty,
            dateTime = System.DateTime.Now,
            gameFlags = gameVariables.GetFlagArray(),
            //itemNums = new int[ItemData.Length],
            itemNums = inventory.SaveItems(),
            //gunAmmos = new int[ItemData.Length],
            gunAmmos = inventory.SaveGunAmmo(),
            Hp = Hp,
            Sanity = Sanity,
            EquipId = player.Equip == null ? -1 : player.Equip.data.Id,
            playerPosX = player.transform.position.x,
            playerPosY = player.transform.position.y,
            playerDir = Direction.NormalDirInt(player.direction)
        };
        //foreach (var pair in ItemNums)
        //{
        //    data.itemNums[pair.Key.Id] = pair.Value;
        //}
        //foreach (var pair in gunAmmos)
        //{
        //    data.gunAmmos[pair.Key.Id] = pair.Value;
        //}
        return data;
    }


    public void ChangeHp(int change)
    {
        Hp += change;
    }

    public void ChangeSanity(int change)
    {
        Sanity += change;
    }    

    //触发一次
    /// <summary>
    /// 初始化数值
    /// </summary>
    public void Initialize()
    {
        //if (player != null)
        //player.DamageTaken += () => UIManager.Instance.FlashRed();
        InitMaxHp();
        Hp = MaxHp;
        Sanity = maxSan; 
    }
    
    private void InitMaxHp()
    {
        if (Game.difficulty == Game.Difficulty.Hard)
            MaxHp = maxHp_Hard;
        else MaxHp = maxHp_Norm;
    }

    public Item GetItemData(int id)
    {
        if (id >= GameManager.Instance.ItemData.Length || id < 0)
            Debug.LogError("Item id out of range");
        return GameManager.Instance.ItemData[id];
    }

    /// <summary>
    /// 添加初始物品
    /// </summary>
    public void AddStartItems()
    {
        //isBagEnabled = true;
        foreach (var item in startItems)
        {
            inventory.AddItem(item);
        }
    }

    /// <summary>
    /// 切换暂停菜单
    /// </summary>
    public void TogglePauseMenu()
    {
        UIManager.Instance.TogglePauseMenu(!paused);
        if (paused)
        {
            Time.timeScale = 1;
            paused = false;
        }
        else
        {
            Time.timeScale = 0;
            paused = true;
        }
    } 

    //Called by the TimeMachine Clip (of type Pause)
    public void PauseTimeline(PlayableDirector whichOne)
	{
        if (whichOne == null) return;
		activeDirector = whichOne;
		activeDirector.playableGraph.GetRootPlayable(0).SetSpeed(0d);
		CurGameMode = GameMode.DialogueMoment; //InputManager will be waiting for a spacebar to resume
		//UIManager.Instance.ToggleDialogArrow(true);
	}

	//Called by the InputManager
	public void ResumeTimeline()
	{
		UIManager.Instance.ToggleDialogArrow(false);
		UIManager.Instance.ToggleDialoguePanel(false);
        UIManager.Instance.ToggleTip(false);
        activeDirector.playableGraph.GetRootPlayable(0).SetSpeed(1d);
		CurGameMode = GameMode.Timeline;
	}

    /// <summary>
    /// 切换游戏模式至过场动画
    /// </summary>
    public void OnCgStarted()
    {
        player.StopMove();
        player.Equip?.Show(false);
        CurGameMode = GameMode.Timeline;
    }

    public void OnCgFinished()
    {
        player.DirChanged?.Invoke();
        CurGameMode = GameMode.Gameplay;
    }

    /// <summary>
    /// 切换游戏模式
    /// </summary>
    /// <param name="mode"></param>
    public void SwitchGameMode(GameMode mode)
    {
		CurGameMode = mode;
    }

    /// <summary>
    /// 打开/关闭背包
    /// </summary>
    public void ToggleBag()
    {
        if (paused) return;
        if (CurGameMode == GameMode.Gameplay)
        {
            if (bag == false)
            {
                //停止移动
                player.SetRun(false);
                //player.StopAction?.Invoke(); //#
            }
            UIManager.Instance.ToggleBag(!bag);
            bag = !bag;
            AudioManager.Instance.PlayBagSound();
            //SwitchGameMode(GameMode.Items);
        }
    }

    public void UseItem(Item item)
    {
        switch (item.itemType)
        {
            case ItemType.AddHp:
                {
                    Hp += item.val;
                    inventory.RemoveItem(item);
                }
                break;
            case ItemType.AddSan:
                {
                    Sanity += item.val;
                    inventory.RemoveItem(item);
                }
                break;
            case ItemType.Bullet:
                break;
            case ItemType.Weapon:
                {
                    var weapon = item as Weapon;
                    Equip(weapon);
                }
                break;
            case ItemType.Other:
                {
                    SpecialItem(item.Id);
                }
                break;
            default:
                break;
        }
    }

    public void Equip(int weaponId, bool playsound = true)
    {
        //if (weaponId >= WeaponPrefabs.Length)
        //    Debug.LogError("Weapon Id not valid");
        if (weaponId < 0) return;
        if (ItemData[weaponId].itemType != ItemType.Weapon)
            Debug.LogError("Not a weapon");
        Equip(ItemData[weaponId] as Weapon, playsound);
    }

    public void Equip(Weapon weapon, bool playsound = true)
    {
        if (weapon == null)
            return;
        if (playsound)
            AudioManager.Instance.PlayEquipSound();
        if (player.Equip != null)
        {
            Destroy(player.Equip.gameObject);
            if (player.Equip.data == weapon)
            {
                //解除装备
                UIManager.Instance.ToggleEquipment(false);
                EquippedWeaponData = null;
                return;
            }
        }
        WeaponObject weaponObj = default;
        switch (weapon.weaponType)
        {
            case WeaponType.Gun:
                {
                    switch (weapon.Id)
                    {
                        case 1:
                            weaponObj = Instantiate(WeaponPrefabs[0], player.transform).GetComponent<Gun>();
                            break;
                        case 4:
                            weaponObj = Instantiate(WeaponPrefabs[2], player.transform).GetComponent<Gun>();
                            break;
                        case 10:
                            weaponObj = Instantiate(WeaponPrefabs[3], player.transform).GetComponent<Gun>();
                            break;
                    }
                }
                break;
            case WeaponType.Melee:
                weaponObj = Instantiate(WeaponPrefabs[1], player.transform).GetComponent<Melee>(); //#
                break;
            case WeaponType.Magic:
                weaponObj = Instantiate(WeaponPrefabs[0], player.transform).GetComponent<WeaponObject>(); //#
                break;
            default:
                break;
        }
        if (weaponObj == null)
        {
            Debug.LogError("weaponType not valid");
            return;
        }
        weaponObj.Show(false);
        weaponObj.Setup(weapon);
        player.Equip = weaponObj;
        EquippedWeaponData = weapon;
        Debug.Log("Equip Init.");
        UIManager.Instance.ToggleEquipment(true, weapon.itemSprite, weaponObj.GetVal());
        Debug.Log(weaponObj.data.Name);
    }

    public void SpecialItem(int id)
    {
        switch (id)
        {
            case 0: //委托信
                {
                    StartDialogue(Game.gameStrings.UseSpecialItem[id]);
                    inventory.RemoveItem(ItemData[id]);
                    //获得空白的信
                    inventory.AddItem(9);
                    break;
                }
            default:
                break;
        }
    }

    public void OnEnemyDetected(bool detect)
    {
        //if (CurGameMode != GameMode.Gameplay && CurGameMode != GameMode.Items)
        //    return;
        if (detect)
        {
            if (!enemyDetected)
            {
                //播放心跳声
                AudioManager.Instance.PlayHeartBeat(true);
            }    
        }
        else
            AudioManager.Instance.PlayHeartBeat(false);
        enemyDetected = detect;
        
        //StartCoroutine(effectAudioSource.FadeOut(1));

    }

    public void MovePlayer(Vector3 position, Vector2 dir)
    {
        StartCoroutine(DoMovePlayer(position, dir));
    }

    public void LoadScene(int sceneId)
    {
        player.StopAction?.Invoke();
        CurGameMode = GameMode.Timeline;
        StartCoroutine(DoLoadSceneFade(sceneId));
    }

    private IEnumerator DoMovePlayer(Vector3 position, Vector2 dir)
    {
        UIManager.Instance.ScreenFadeOut();
        CurGameMode = GameMode.Timeline;
        player.StopAction?.Invoke();
        yield return new WaitForSeconds(1);
        player.transform.position = position;
        player.SetDirection(dir);
        UIManager.Instance.ScreenFadeIn();
        CurGameMode = GameMode.Gameplay;
    }

    private IEnumerator DoLoadSceneFade(int sceneId)
    {
        AsyncOperation opr = SceneManager.LoadSceneAsync(sceneId);
        opr.allowSceneActivation = false;
        UIManager.Instance.ScreenFadeOut();
        yield return new WaitForSeconds(1);
        opr.allowSceneActivation = true;
        yield return opr;
        //while (!opr.isDone)
        //{
        //    yield return null;
        //}
        //SceneManager.LoadScene(sceneId);
        CurGameMode = GameMode.Gameplay;
    }

    private void GameOver(int hpZero)
    {
        if (player != null) player.Die();
        CurGameMode = GameMode.Timeline;
        UIManager.Instance.ShowGameOver(hpZero);
        StartCoroutine(ReturnToTitle());
    }

    private IEnumerator ReturnToTitle()
    {
        yield return new WaitForSeconds(1);
        UIManager.Instance.ScreenFadeOut();
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// 按下确定键
    /// </summary>
    public void OnAccept()
    {
        if (paused) return;
        if (CurGameMode == GameMode.DialogueMoment)
        {
            ResumeTimeline();
        }
        else if (CurGameMode == GameMode.NormalDialogue)
        {
            NextDialogue();
            //UIManager.Instance.ToggleDialoguePanel(false);
            //SwitchGameMode(GameMode.Gameplay);
        }
    }

    public void StartDialogue(params string[] dialogs)
    {
        if (dialogs.Length == 0)
        {
            Debug.LogError("Start a empty dialogue");
            return;
        }
        this.dialogs = dialogs;
        dialogIndex = 0;
        UIManager.Instance.ShowDialogue(dialogs[dialogIndex], false);
    }

    private void NextDialogue()
    {
        dialogIndex++;
        if (dialogs == null || dialogIndex >= dialogs.Length)
        {
            //结束对话
            if (CurGameMode == GameMode.NormalDialogue)
            {
                UIManager.Instance.ToggleDialoguePanel(false);
                SwitchGameMode(GameMode.Gameplay);
                DialogueEnded?.Invoke();
            }
            return;
        }
        UIManager.Instance.ShowDialogue(dialogs[dialogIndex], false);
    }
}


public enum GameMode
{
	/// <summary>
	/// 游戏中
	/// </summary>
	Gameplay,
	/// <summary>
	/// 过场动画
	/// </summary>
	Timeline,
	/// <summary>
	/// 对话等待中
	/// </summary>
	DialogueMoment,
	/// <summary>
	/// 一般的场景对话
	/// </summary>
	NormalDialogue,
    /// <summary>
    /// 背包
    /// </summary>
    //Items,
}