using UnityEngine;
public class ShopUI : MonoBehaviour
{
    public GameObject itemButtonPrefab;  // 指向剛做好的 Prefab
    public Transform listContent;        // 拖入 Scroll View → Viewport → Content

    void Start()
    {
        for (int i = 0; i < 21; i++)
        {
            Instantiate(itemButtonPrefab, listContent);
        }
        // 假設你有一個 List<Item> shopItems
        // foreach (var item in shopItems)
        // {
        //     var btnGO = Instantiate(itemButtonPrefab, listContent);
        //     // 設定按鈕的 icon/text/點擊事件…
        //     btnGO.GetComponent<ShopItemButton>().Setup(item);
        // }
    }
}
