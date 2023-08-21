using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private static InventoryManager instance;
    private GameManager _gameManager;
    public static InventoryManager Instance => instance;
    public UIManager UIManager { get; set; }

    public List<ItemData> availableItems = new List<ItemData>(); // 아이템 데이터를 담을 리스트
    private ItemData equippedItem; // 현재 장착된 아이템을 담을 변수

    #region 싱글톤
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

    // 현재 장착된 아이템을 설정하는 메서드
    public void EquipItem(ItemData item)
    {
        if (!availableItems.Contains(item))
        {
            equippedItem = item;
            availableItems.Add(item);


            //스탯 증가
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

    // 현재 장착된 아이템을 해제하는 메서드
    public void UnequipItem()
    {
        if (equippedItem != null && availableItems.Contains(equippedItem))
        {
            availableItems.Remove(equippedItem);
            equippedItem = null;
        }
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