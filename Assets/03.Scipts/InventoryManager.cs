using System.Collections.Generic;
using UnityEngine;
using ItemEnums;

public class InventoryManager
{
    private List<EquipItemType> equipItems;
    private List<UseItemType> useItems;

    public InventoryManager()
    {
        equipItems = new List<EquipItemType>();
        useItems = new List<UseItemType>();
    }

    public void EquipItem(EquipItemType type)
    {
        equipItems.Add(type);
        Debug.Log($"Equipped {type}.");
    }

    public void UseItem(UseItemType type)
    {
        useItems.Add(type);
        Debug.Log($"Used {type}.");
    }

    public void ResetUsedItems()
    {
        useItems.Clear();
        Debug.Log("Used items reset.");
    }

    public void ResetItems()
    {
        equipItems.Clear();
        useItems.Clear();
        UIManager.instance.ResetItem();
        Debug.Log("Inventory items reset.");
    }

}