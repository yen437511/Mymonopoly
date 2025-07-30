using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagUI : MonoBehaviour
{
    public static BagUI Instance { get; private set; }
    public List<Item> bagItems;                // 玩家的背包
    public GameObject bagPanel;                // 放入商店Panel
    public GameObject itemButtonPrefab;         // 指向剛做好的 Prefab
    public Transform listContent;               // 拖入 Scroll View → Viewport → Content
    public Image itemImage;                     // 詳細資訊圖片
    public Text itemNameText;                   // 詳細資訊物品名稱
    public Text itemInfoText;                   // 詳細資訊物品資訊
    public Button exitButton;                   // 離開按鈕
    public Button buyButton;                    // 購買按鈕
    public Item tempItem;                       // 存放使用者點擊的物品



    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        bagPanel.SetActive(false);
        buyButton.interactable = false;
        foreach (var item in bagItems)
        {
            var go = Instantiate(itemButtonPrefab, listContent);
            go.GetComponent<BagItemButton>().Setup(item, OnClick);
        }

        // RemoveAllListeners 可避免重複綁定
        exitButton.onClick.RemoveAllListeners();
        exitButton.onClick.AddListener(Hide);
        //buyButton.onClick.RemoveAllListeners();
        //buyButton.onClick.AddListener(Buy);
        // 假設你有一個 List<Item> shopItems
        // foreach (var item in shopItems)
        // {
        //     var btnGO = Instantiate(itemButtonPrefab, listContent);
        //     // 設定按鈕的 icon/text/點擊事件…
        //     btnGO.GetComponent<ShopItemButton>().Setup(item);
        // }
    }

    public void Show()
    {
        bagPanel.SetActive(true);
        Dice.Instance.isDisabled = true;
        Bag.Instance.isDisabled = true;
    }

    public void Hide()
    {
        bagPanel.SetActive(false);
        Dice.Instance.isDisabled = false;
        Bag.Instance.isDisabled = false;
    }

    void OnClick(Item item)
    {
        tempItem = item;
        buyButton.interactable = true;
        itemImage.color = new Color(1f, 1f, 1f, 1f);
        itemImage.sprite = item.icon;
        itemNameText.text = item.itemName;
        itemInfoText.text = "價格：" + item.price.ToString() + "\n攻擊力：" + item.attack.ToString() + "\n防禦力：" + item.defense.ToString();
    }
}
