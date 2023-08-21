using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private static InventoryManager instance;
    public static InventoryManager Instance => instance;

    public List<ItemData> availableItems = new List<ItemData>(); // 아이템 데이터를 담을 리스트
    private ItemData equippedItem; // 현재 장착된 아이템을 담을 변수

    #region 싱글톤
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

    }
    #endregion

    // 현재 장착된 아이템을 설정하는 메서드
    public void EquipItem(ItemData item)
    {
        equippedItem = item;
    }

    // 현재 장착된 아이템을 해제하는 메서드
    public void UnequipItem()
    {
        equippedItem = null;
    }

    // 현재 장착된 아이템을 가져오는 메서드
    public ItemData GetEquippedItem()
    {
        return equippedItem;
    }

    public List<ItemData> GetEquipableItems()
    {
        return availableItems.FindAll(item => item.ItemType == ItemType.Equipable);
    }

    public List<ItemData> GetUsableItems()
    {
        return availableItems.FindAll(item => item.ItemType == ItemType.Usable);
    }

    public void ResetItems()
    {
        availableItems.Clear();
        Debug.Log("Inventory items reset.");
    }

}