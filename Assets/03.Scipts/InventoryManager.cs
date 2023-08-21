using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private static InventoryManager instance;
    private GameManager _gameManager;
    public static InventoryManager Instance => instance;
    public UIManager UIManager { get; set; }

    public List<ItemData> availableItems = new List<ItemData>(); // ������ �����͸� ���� ����Ʈ
    private ItemData equippedItem; // ���� ������ �������� ���� ����

    #region �̱���
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            _gameManager = GameManager.Instance;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

    }
    #endregion

    void Start()
    {
        UIManager = UIManager.instance;
    }

    // ���� ������ �������� �����ϴ� �޼���
    public void EquipItem(ItemData item)
    {
        if (!availableItems.Contains(item))
        {
            equippedItem = item;
            availableItems.Add(item);


            //���� ����
            int totalLifeBoost = item.AddLife;
            int totalAttackBoost = item.AddAttack;
            int totalDefenseBoost = item.AddDefense;
            int totalAgilityBoost = item.AddAgility;

            PlayerStats playerStats = _gameManager.GetPlayerStats();
            playerStats.Life += totalLifeBoost;
            playerStats.Attack += totalAttackBoost;
            playerStats.Defense += totalDefenseBoost;
            playerStats.Agility += totalAgilityBoost;

            if (availableItems.Count >= 4)
            {
                UIManager.instance.noticeObj.SetActive(true);
                Invoke("CloseNotice", 1f);
            }
        }  

        if (availableItems.Contains(item))
        {
            UIManager.instance.notice2Obj.SetActive(true);
            Invoke("CloseNotice", 1f);
        }

    }


    public void CloseNotice()
    {
        UIManager.instance.noticeObj.SetActive(false);
        UIManager.instance.notice2Obj.SetActive(false);
    }

    // ���� ������ �������� �����ϴ� �޼���
    public void UnequipItem()
    {
        if (equippedItem != null && availableItems.Contains(equippedItem))
        {
            availableItems.Remove(equippedItem);
            equippedItem = null;
        }
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