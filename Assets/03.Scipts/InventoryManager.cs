using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UIElements;

public class InventoryManager : MonoBehaviour
{
    private static InventoryManager instance;
    private GameManager _gameManager;
    public static InventoryManager Instance => instance;
    public UIManager UIManager { get; set; }

    public List<ItemData> availableItems = new List<ItemData>(); // 아이템 데이터를 담을 리스트
    private List<string> equippedItemNames = new List<string>(); // 장착아이템 이름을 담을 리스트
    private List<ItemData> equippedItems = new List<ItemData>(); 
    private ItemData equippedItem;// 현재 장착된 아이템을 담을 변수
    private ItemData useabledItem; //사용형 아이템을 담을 변수

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


    [Header("Pocket")]
    public GameObject pocketObj; //인벤토리
    public GameObject pocketPanelObj;
    public TextMeshProUGUI curruntEquipItem;
    public TextMeshProUGUI curruntAttack;
    public TextMeshProUGUI curruntDefense;
    public TextMeshProUGUI curruntAgility;
    public TextMeshProUGUI lifeText;


    void Start()
    {
        UIManager = UIManager.instance;
    }

    // 현재 장착된 아이템을 설정하는 메서드
    public void EquipItem(ItemData item)
    {
        PlayerStats playerStats = _gameManager.GetPlayerStats();

        if (!availableItems.Contains(item) && equippedItemNames.Count < 4)
        {
            equippedItem = item;
            useabledItem = item;
            availableItems.Add(item);
      

            if (ItemType.Equipable == item.ItemType) 
            {
                if (equippedItemNames.Count < 3)
                {
                    equippedItemNames.Add(item.Name);
                    string equippedItemsText = string.Join(", \n ", equippedItemNames);
                    curruntEquipItem.text = $"{equippedItemsText}";
                }
            }

            //장착 아이템은 3개까지밖에 못가집니다.에 대한 팝업
            if (equippedItemNames.Count >= 4)
            {
                UIManager.instance.noticeObj.SetActive(true);
                Invoke("CloseNotice", 1f);
            }
        }
        //이미 장착된 아이템입니다. 표시
        if (equippedItems.Contains(item))
        {
            UIManager.instance.notice2Obj.SetActive(true);
            Invoke("CloseNotice", 1f);
        }

        var stats = ApplyItemStats(item.AddLife, item.AddAttack, item.AddDefense, item.AddAgility);
        int modifiedLifeUsable = stats.modifiedLife;
        int modifiedAttackUsable = stats.modifiedAttack;
        int modifiedDefenseUsable = stats.modifiedDefense;
        int modifiedAgilityUsable = stats.modifiedAgility;

        UpdateUI(modifiedLifeUsable, modifiedAttackUsable, modifiedDefenseUsable, modifiedAgilityUsable);

    }

    private (int modifiedLife, int modifiedAttack, int modifiedDefense, int modifiedAgility) 
        ApplyItemStats(int lifeItemApply, int attackItemApply, int defenseItemApply, int agilityItemApply)
    {
        PlayerStats playerStats = _gameManager.GetPlayerStats();

        int modifiedLife = playerStats.Life += lifeItemApply;
        int modifiedAttack = playerStats.Attack += attackItemApply;
        int modifiedDefense = playerStats.Defense += defenseItemApply;
        int modifiedAgility = playerStats.Agility += agilityItemApply;

        return (modifiedLife, modifiedAttack, modifiedDefense, modifiedAgility);
    }

    private void UpdateUI(int modifiedLife, int modifiedAttack, int modifiedDefense, int modifiedAgility)
    {
        _gameManager.PlayerLife += modifiedLife;
        _gameManager.PlayerAttack += modifiedAttack;
        _gameManager.PlayerDefense += modifiedDefense;
        _gameManager.PlayerAgility += modifiedAgility;

        curruntAttack.text = $"현재 공격력 : {modifiedLife} \n";
        curruntDefense.text = $"현재 방어력 : {modifiedDefense} \n";
        curruntAgility.text = $"현재 민첩함 : {modifiedAgility} \n";

        string newText = string.Empty;
        for (int i = 0; i < modifiedLife; i++)
        {
            newText += "♥";
            lifeText.SetText(newText);
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