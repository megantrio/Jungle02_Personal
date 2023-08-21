using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;


public class UIManager : MonoBehaviour
{
    #region Singleton
    public static UIManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            _inventoryManager = InventoryManager.Instance;
            _gameManager = GameManager.Instance;
        }
        else
        {
            Destroy(gameObject); // �̹� �ʱ�ȭ�� ��� �� ������Ʈ�� �ı�
            return;
        }
    }
    #endregion

    private GameManager _gameManager;
    private InventoryManager _inventoryManager;
    private ItemData currentItemData;
    private ItemData displayedItemData;
    

    #region Object�Ҵ�
    [Header("UIGroup")]
    public GameObject gameObj; //�⺻ UI
    public GameObject itemInfoObj; //������ ����â
    public GameObject resultObj; //���� ���â
    public GameObject titleObj; //Ÿ��Ʋ â
    public GameObject gameOverObj; //���� ���� â
    public GameObject endingObj; //���� â   

    [Header("Default Game Info Area")]
    public TextMeshProUGUI lifeText; //�����
    public TextMeshProUGUI curruntCarText; //���� ���� ĭ
    public TextMeshProUGUI enemyLifeText;
    public GameObject emenyLifeObj;

    [Header("Item Info")]
    //public Transform itemCreatePosTr;
    public GameObject itemPrefab;
    public TextMeshProUGUI itemInfoText;
    public GameObject noticeObj;

    [Header("Pocket")]
    public GameObject pocketObj; //�κ��丮
    public GameObject pocketPanelObj;
    public TextMeshProUGUI curruntEquipItem;
    public TextMeshProUGUI curruntAttack;
    public TextMeshProUGUI curruntDefense;
    public TextMeshProUGUI curruntAgility;


    [Header("resultPanel")]
    public TextMeshProUGUI resultText;

    [Header("Choice")]
    public Button equipButton;
    public Button discardButton;
    #endregion

    //Start
    private void Start()
    {
        if (itemInfoObj != null)
        {
            itemInfoObj.SetActive(false);
        }
        if (pocketPanelObj != null)
        {
            pocketPanelObj.SetActive(false);
        }

    }


    #region �� ���� 
    //�⺻ ��
    public void SetDefaultView()
    {
        SetViewObject(game: true, pocket: true);
        titleObj.SetActive(false);
    }

    //Ÿ��Ʋ ��
    public void SetTitleView()
    {
        SetViewObject(title: true);
    }


    public void SetItemInfoView(ItemData itemData)
    {
        currentItemData = itemData;
        UpdateItemInfoPanelUI();
        SetViewObject(game: true, itemInfo: true);
    }

    //���� ��� â�� ���
    public void SetResultView()
    {
        SetViewObject(game: true, result: true);
    }

    //Ŭ���� �� ���� �� ���
    public void SetEndingView()
    {
        SetViewObject(ending: true);
        gameObj.SetActive(false);
        pocketObj.SetActive(false);
    }

    //�׾��� ���� ��
    public void SetGameViewDie()
    {
        SetViewObject(gameOver: true);
    }

    private void SetViewObject(bool game = false, bool itemInfo = false, bool result = false,
        bool gameOver = false, bool title = false, bool ending = false, bool pocket = false, bool enemyLife = false, bool pocketPanel = false)
    {
        gameObj.SetActive(game);
        itemInfoObj.SetActive(itemInfo);
        resultObj.SetActive(result);
        gameOverObj.SetActive(gameOver);
        endingObj.SetActive(ending);
        titleObj.gameObject.SetActive(title);
        pocketObj.gameObject.SetActive(pocket);
        emenyLifeObj.gameObject.SetActive(enemyLife);
        pocketPanelObj.gameObject.SetActive(pocketPanel);
    }
    #endregion

    #region ������Ʈ
    private void UpdateAllText()
    {
        UpdatePlayerLife(_gameManager.DefaultPlayerLife);
        UpdateCarText(_gameManager.curruntCar);
    }

    public void UpdatePlayerLife(int LifeValue)
    {
        string newText = string.Empty;
        for (int i = 0; i < LifeValue; i++)
            newText += "��";
        lifeText.SetText(newText);
    }

    public void UpdateCarText(int curruntCar)
    {
        curruntCarText.SetText($"{curruntCar}��° ĭ");
    }

    //���׹̳�
    public void UpdatenemyLife(int enemyLifeValue)
    {
        string newText = string.Empty;
        for (int i = 0; i < enemyLifeValue; i++)
            newText += "��";
        enemyLifeText.SetText(newText);
    }
    #endregion

    //���� ��� ���
    public void ShowResult(int remainingEnemyLife, int remainingPlayerLife)
    {
       
        string resultMessage = remainingEnemyLife == 0 ? "�̰��..." : "";
        if (remainingPlayerLife == 0)
        {
            _gameManager.Die();
        }

        resultObj.SetActive(true);
        resultText.SetText(resultMessage);
    }

    #region ������

    public void ShowItemInfo(ItemData itemData)
    {
        currentItemData = itemData;
        UpdateItemInfoPanelUI();
        itemInfoObj.SetActive(true);
        displayedItemData = itemData;
    }

    
    //������ �г� ������Ʈ
    private void UpdateItemInfoPanelUI()
    {
        if (itemInfoObj != null && currentItemData != null)
        {

            itemInfoText.text = $"������ �̸� : {currentItemData.Name}\n" +
                                $"������ ���� : {currentItemData.Description}\n\n" +
                                $"�߰� ���� : {currentItemData.AddLife}\n" +
                                $"�߰� ���ݷ� : {currentItemData.AddAttack}\n" +
                                $"�߰� ���� : {currentItemData.AddDefense}\n" +
                                $"�߰� ��ø�� : {currentItemData.AddAgility}";

        }

    }

    // ������ ���� ��ư Ŭ�� ��
    public void EquipButtonOnClick()
    {
        if (currentItemData != null)
        {
            // ������ ������ �����͸� ���� ���������� ����
            InventoryManager.Instance.EquipItem(currentItemData);
            itemInfoObj.SetActive(false);

            //�ֱ� ������ ������ ������ ��� �ִ� ������Ʈ�� ã�Ƽ� �ı�
            if (displayedItemData != null)
            {
                GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
                foreach (GameObject item in items)
                {
                    ItemPanelLoader itemComponent = item.GetComponent<ItemPanelLoader>();
                    if (itemComponent != null && itemComponent.itemData == displayedItemData)
                    {
                        Destroy(item);
                        break;
                    }
                }
            }

            Debug.Log("������ ����");
        }
    }


    // ������ ��ư Ŭ�� ��
    public void DiscardButtonOnClick()
    {
        if (currentItemData != null)
        {
            itemInfoObj.SetActive(false);

            if (displayedItemData != null)
            {
                GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
                foreach (GameObject item in items)
                {
                    ItemPanelLoader itemComponent = item.GetComponent<ItemPanelLoader>();
                    if (itemComponent != null && itemComponent.itemData == displayedItemData)
                    {
                        Destroy(item);
                        break;
                    }
                }
            }
            Debug.Log("������ ���� ����");
        }
    }

    public void UnequipButtonOnClick()
    {
        if (currentItemData != null)
        {
            InventoryManager.Instance.UnequipItem();
            itemInfoObj.SetActive(false);

            if (displayedItemData != null)
            {
                GameObject[] items = GameObject.FindGameObjectsWithTag("Item"); 
                foreach (GameObject item in items)
                {
                    ItemPanelLoader itemComponent = item.GetComponent<ItemPanelLoader>();
                    if (itemComponent != null && itemComponent.itemData == displayedItemData)
                    {
                        Destroy(item);
                        break;
                    }
                }
            }

            Debug.Log("������ ���� ����");
        }
    }

    public void LoadItemInfo(ItemData itemData)
    {
        currentItemData = itemData;
        UpdateItemInfoPanelUI();
        SetViewObject(game: true, itemInfo: true);
    }

    #endregion

    #region �ָӴ�


    public void UpdatePocketPanelUI()
    {
        //�߰� ��ġ �ҷ�����
        PlayerStats player = _gameManager.GetPlayerStats();
        List<ItemData> equipableItems = _inventoryManager.GetEquipableItems();
        List<ItemData> usableItems = _inventoryManager.GetUsableItems();

        Debug.Log("��ġ �ҷ���");

        //�����. �þ���� �𸣰��� �þ�Բ� ���� �ϸ� ����
        int totalLifeBoost = equipableItems.Sum(item => item.AddLife) + usableItems.Sum(item => item.AddLife);

        //�������� ���� �þ ���ݷ� ���
        int totalAttackBoost = equipableItems.Sum(item => item.AddAttack) + usableItems.Sum(item => item.AddAttack);
        int modifiedAttack = player.Attack + totalAttackBoost;

        //�������� ���� �þ ���� ���
        int totalDefenseBoost = equipableItems.Sum(item => item.AddDefense) + usableItems.Sum(item => item.AddDefense);
        int modifiedDefense = player.Defense + totalDefenseBoost;

        //�������� ���� �þ ���ݷ� ���
        int totalAgilityBoost = equipableItems.Sum(item => item.AddAgility) + usableItems.Sum(item => item.AddAgility);
        int modifiedAgility = player.Agility + totalAgilityBoost;

        if (pocketPanelObj != null && currentItemData != null)
        {
            curruntEquipItem.text = $"���������� : {currentItemData.Name}";
            curruntAttack.text = $"���ݷ� : {modifiedAttack + currentItemData.AddAttack}";
            curruntDefense.text = $"���� : {modifiedDefense + currentItemData.AddDefense}";
            curruntAgility.text = $"��ø�� : {modifiedAgility + currentItemData.AddAgility}";
        }
        else
        {
            curruntEquipItem.text = "���������� : ����";
            curruntAttack.text = $"���ݷ� : {player.Attack}";
            curruntDefense.text = $"���� : {player.Defense}";
            curruntAgility.text = $"��ø�� : {player.Agility}";
        }

    }

    public void LoadPocketPanel(ItemData itemData)
    {
        currentItemData = itemData;
        UpdatePocketPanelUI();
    }

    #endregion



    public void Retry()
    {
        _gameManager.Retry();
    }

}