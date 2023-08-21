using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private static InventoryManager instance;
    public static InventoryManager Instance => instance;

    public List<ItemData> availableItems = new List<ItemData>(); // ������ �����͸� ���� ����Ʈ
    private ItemData equippedItem; // ���� ������ �������� ���� ����

    #region �̱���
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

    // ���� ������ �������� �����ϴ� �޼���
    public void EquipItem(ItemData item)
    {
        equippedItem = item;
    }

    // ���� ������ �������� �����ϴ� �޼���
    public void UnequipItem()
    {
        equippedItem = null;
    }

    // ���� ������ �������� �������� �޼���
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