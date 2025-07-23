using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class ShopUI : MonoBehaviour
{
    public static ShopUI Instance { get; private set; }
    public List<Item> shopItems;                // 拖入各 Item 資產
    public GameObject shopPanel;                //放入商店Panel
    public GameObject itemButtonPrefab;         // 指向剛做好的 Prefab
    public Transform listContent;               // 拖入 Scroll View → Viewport → Content

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
                go.GetComponent<ShopItemButton>().Setup(item, OnBuy);
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
    
    void OnBuy(Item item)
    {
        // 處理購買
    }
}
