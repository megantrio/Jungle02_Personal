using UnityEngine;

public enum ItemType
{
    Equipable,
    Usable
}


[CreateAssetMenu(fileName = "NewItemData", menuName = "Item Data", order = 1)]

public class ItemData : ScriptableObject
{
    public string Name;
    public string Description;
    public ItemType ItemType;
    public int AddLife;
    public int AddAttack;
    public int AddDefense;
    public int AddAgility;
    
}
