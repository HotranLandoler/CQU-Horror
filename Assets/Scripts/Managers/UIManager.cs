using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    //private enum TipState
    //{
    //    Idle,
    //    Show,
    //}
    [Header("Dialogue")]
    [SerializeField]
    //private DialogUI dialogUI;
    private Dialogue dialogue;

    [SerializeField]
    //private DialogUI dialogUI;
    private Dialogue tip;

    [SerializeField]
    private GameObject dialogArrow;

    [Header("ScreenFX")]
    [SerializeField]
    private Image redScreen;

    [SerializeField]
    private BlackScreen blackScreen;

    [Header("HUD")]
    [SerializeField]
    private InventoryPanel bagUI;

    [SerializeField]
    private Image weaponImage;

    [SerializeField]
    private Text ammoText;

    [Header("Cursor")]
    [SerializeField]
    private Texture2D crosshair;

    [Header("Menus")]
    [SerializeField]
    private GameObject pauseMenuUI;

    [SerializeField]
    private GameObject gameOptionsUI;

    [Header("Other")]
    [SerializeField]
    private Image GameOver;

    [SerializeField]
    private Lean.Localization.LeanLocalizedText GameOverText;

    [SerializeField]
    private float redFlashTime = 0.2f;

    [SerializeField]
    private UIPanel savingImage;

    [SerializeField]
    private UIPanel savedTip;

    //物品获得提示
    [Header("ItemTip")]
    [SerializeField]
    private ItemTip itemTip;

    [SerializeField]
    private float itemTipTime = 1.5f;

    private Queue<ItemMessage> itemMessages = new Queue<ItemMessage>();

    private bool itemTipshow = false;

    private float itemTipTimer = 0;

    private Stack<IWindow> windows = new Stack<IWindow>(2);
    //private TipState tipState = TipState.Idle; 

    private void Awake()
    {
        _instance = this;
        Locator.Register<UIManager>(this);
    }

    private void Update()
    {
        //显示物品提示
        if (!itemTipshow)
        {
            if (itemMessages.Count > 0)
            {
                var message = itemMessages.Dequeue();
                itemTip.SetTip(message.itemData, message.itemNum);
                itemTip.Show();
                itemTipTimer = itemTipTime;
                //tipState = TipState.Show;
                itemTipshow = true;
            }
        }
        else
        {
            itemTipTimer -= Time.deltaTime;
            if (itemTipTimer <= 0) //计时器结束
            {
                //if (itemMessages.Count == 0) //没有message了
                //{
                //    itemTip.Hide();
                //}
                itemTip.Hide();
                itemTipshow = false;
                //tipState = TipState.Idle;
            }
        }
    }

    public void Initialize()
    {
        SetAimCursor();
        Debug.Log("UI Init.");
        ToggleEquipment(false);
        if (bagUI != null)
        {
            bagUI.ClearItemDesc();
            UpdateInventory();
        }  
        dialogue.SetTypeEndCallback(() => ToggleDialogArrow(true));
        
    }

    /// <summary>
    /// 显示对话框
    /// </summary>
    /// <param name="dialogue"></param>
    public void ShowDialogue(string phraseName, bool isUsedInTimeline = true)
    {
        //Debug.LogError("showDialog");
        if (!isUsedInTimeline)
            GameManager.Instance.SwitchGameMode(GameMode.NormalDialogue);
        ToggleDialoguePanel(true);
        dialogue.SetText(phraseName);
    }

    //public void ShowDialogue(string phrase, bool isUsedInTimeline = true)
    //{
    //    if (!isUsedInTimeline)
    //        GameManager.Instance.SwitchGameMode(GameMode.NormalDialogue);
    //    ToggleDialoguePanel(true);
    //    dialogue.SetText(phrase);
    //}

    public void ShowTip(string phraseName, bool isUsedInTimeline = true)
    {
        if (!isUsedInTimeline)
            GameManager.Instance.SwitchGameMode(GameMode.NormalDialogue);
        ToggleTip(true);
        tip.SetText(phraseName);
    }

    /// <summary>
    /// 显示提示
    /// </summary>
    /// <param name="active"></param>
    public void ToggleTip(bool active)
    {
        //if (active)
        //    dialogUI.Show();
        //else
        //    dialogUI.Fade();
        if (tip == null) return;
        if (active)
            tip.Show();
        else
            tip.Hide();
        //tip.gameObject.SetActive(active);
    }
    
    /// <summary>
    /// 隐藏对话框
    /// </summary>
    /// <param name="dialogue"></param>
    public void ToggleDialoguePanel(bool active)
    {
        if (active)
            dialogue.Show();
        else
            dialogue.Hide();

        //dialogue.gameObject.SetActive(active);
    }

    public void ToggleDialogArrow(bool active)
    {
        dialogArrow.SetActive(active);
    }

    private void SetAimCursor()
    {
        if (crosshair == null) return;
        Vector2 cursorOffset = new Vector2(crosshair.width / 2, crosshair.height / 2);
        Cursor.SetCursor(crosshair, cursorOffset, CursorMode.Auto);
    }

    public void ToggleBag(bool active)
    {
        if (active)
            bagUI.Show();
        else
            bagUI.Hide();
        //bagUI.gameObject.SetActive(active);
    }

    public void ToggleEquipment(bool active, Sprite sprite = null, int ammo = 0)
    {
        if (weaponImage == null)
            return;
        if (active)
        {
            weaponImage.sprite = sprite;
            if (ammo == -1)
                ammoText.text = string.Empty;
            else
                ammoText.text = ammo.ToString();
            weaponImage.gameObject.SetActive(true);
        }
        else
        {
            weaponImage.gameObject.SetActive(false);
        }      
    }

    public void ShowGetItemTip(Item item, int num)
    {
        itemMessages.Enqueue(new ItemMessage(item, num));
        //bagUI.gameObject.SetActive(active);
    }

    public void UpdateAmmo(Weapon gun)
    {
        ammoText.text = GameManager.Instance.inventory.GetGunAmmoLoaded(gun).ToString();
        bagUI.UpdateItem(gun);
    }

    public void FlashRed()
    {
        StartCoroutine(FlashRedScreen());
    }

    public void UpdateInventory()
    {
        bagUI.UpdateItems();
    }

    public void ScreenFadeOut()
    {
        blackScreen.FadeOut();
    }

    public void ScreenFadeIn()
    {
        blackScreen.FadeIn();
    }

    public void ScreenCrossFade()
    {
        StartCoroutine(DoScreenFadeCross());
    }

    public void TogglePauseMenu(bool active)
    {
        pauseMenuUI.SetActive(active);
        if (!active)
            ToggleOptionsInGame(false);
    }

    public void ToggleOptionsInGame(bool active)
    {
        if (active) gameOptionsUI.SetActive(true);
        else
            gameOptionsUI.SetActive(false);
    }

    public void ShowGameOver(int stat)
    {
        GameOver.gameObject.SetActive(true);
        if (stat == 0)
            GameOverText.TranslationName = "NoHp";
        else
            GameOverText.TranslationName = "NoSan";
    }

    public void ToggleSaving(bool active)
    {
        if (active)
            savingImage.Show();
        else
            savingImage.Hide();
    }

    public void ShowSavedTip()
    {
        StartCoroutine(DoShowSavedTip());       
    }

    public void AddWindow(IWindow window)
    {
        windows.Push(window);
    }

    public void RemoveWindow()
    {
        windows.Pop();
    }

    public bool CloseWindows()
    {
        if (windows.Count == 0)
            return false;
        windows.Pop().Close();
        return true;
    }

    private IEnumerator DoShowSavedTip()
    {
        savedTip.Show();
        yield return new WaitForSeconds(2);
        savedTip.Hide();
    }

    private IEnumerator FlashRedScreen()
    {
        redScreen.enabled = true;
        yield return new WaitForSeconds(redFlashTime);
        redScreen.enabled = false;
    }

    //private IEnumerator HideItemTip()
    //{
    //    yield return new WaitForSeconds(itemTipTime);
    //    itemTip.gameObject.SetActive(false);
    //}

    private IEnumerator DoScreenFadeCross()
    {
        ScreenFadeOut();
        yield return new WaitForSeconds(1);
        ScreenFadeIn();
    }
}
