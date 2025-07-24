using System.Collections.Generic;
using System.Linq;
//using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;
public class ShopUI : MonoBehaviour
{
    public static ShopUI Instance { get; private set; }
    public List<Item> shopItems;                // 拖入各 Item 資產
    public GameObject shopPanel;                // 放入商店Panel
    public GameObject itemButtonPrefab;         // 指向剛做好的 Prefab
    public Transform listContent;               // 拖入 Scroll View → Viewport → Content
    public Image itemImage;                     // 詳細資訊圖片
    public Text itemNameText;                   // 詳細資訊物品名稱
    public Text itemInfoText;                   // 詳細資訊物品資訊

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        shopPanel.SetActive(false);
        for (int i = 0; i < 4; i++)
        {
            foreach (var item in shopItems)
            {
                var go = Instantiate(itemButtonPrefab, listContent);
                go.GetComponent<ShopItemButton>().Setup(item, OnClick);
            }
        }
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
        shopPanel.SetActive(true);
    }

    public void Hide()
    {
        shopPanel.SetActive(false);
    }

    void OnClick(Item item)
    {
        itemImage.color = new Color(1f, 1f, 1f, 1f);
        itemImage.sprite = item.icon;
        itemNameText.text = item.itemName;
        itemInfoText.text = item.attack.ToString();
    }
}
