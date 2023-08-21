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

    public List<ItemData> availableItems = new List<ItemData>(); // ������ �����͸� ���� ����Ʈ
    private List<string> equippedItemNames = new List<string>(); // ���������� �̸��� ���� ����Ʈ
    private List<ItemData> equippedItems = new List<ItemData>(); 
    private ItemData equippedItem;// ���� ������ �������� ���� ����
    private ItemData useabledItem; //����� �������� ���� ����

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


    [Header("Pocket")]
    public GameObject pocketObj; //�κ��丮
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

    // ���� ������ �������� �����ϴ� �޼���
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

            //���� �������� 3�������ۿ� �������ϴ�.�� ���� �˾�
            if (equippedItemNames.Count >= 4)
            {
                UIManager.instance.noticeObj.SetActive(true);
                Invoke("CloseNotice", 1f);
            }
        }
        //�̹� ������ �������Դϴ�. ǥ��
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

        curruntAttack.text = $"���� ���ݷ� : {modifiedLife} \n";
        curruntDefense.text = $"���� ���� : {modifiedDefense} \n";
        curruntAgility.text = $"���� ��ø�� : {modifiedAgility} \n";

        string newText = string.Empty;
        for (int i = 0; i < modifiedLife; i++)
        {
            newText += "��";
            lifeText.SetText(newText);
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