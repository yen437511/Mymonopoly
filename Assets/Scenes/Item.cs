using UnityEngine;

[CreateAssetMenu(menuName = "Game/Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public int price;
    public int sellPrice;
    public string itemType;
    public int attack;
    public int defense;
}
