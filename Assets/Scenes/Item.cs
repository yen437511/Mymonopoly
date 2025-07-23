using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Game/Item", order = 0)]
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
