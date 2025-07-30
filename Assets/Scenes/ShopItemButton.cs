using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class ShopItemButton : MonoBehaviour
{
    public Item Item { get; private set; }
    public Image iconImage;    // 拖入 Prefab 裡的 Image 元件
    public Text nameText;
    //public Text priceText;
    public Button thisButton;

    public void Setup(Item item, UnityAction<Item> onClick)
    {
        Item = item;
        // 1. 把 Item 裡的 icon 填到這顆按鈕的 Image
        iconImage.sprite = item.icon;

        // 2. 名稱、價格
        nameText.text = item.itemName;
        //priceText.text = item.price.ToString();

        // 3. 點擊事件把這個 item 傳出去
        thisButton.onClick.RemoveAllListeners();
        thisButton.onClick.AddListener(() => onClick(item));
    }
}
