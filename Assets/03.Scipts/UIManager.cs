using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UIManager : MonoBehaviour
{
    #region Singleton
    public static UIManager instance;

    private void Awake()
    {
        instance = this;
        _inventoryManager = new InventoryManager();
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

    [Header("Pocket")]
    public GameObject pocketObj; //�κ��丮
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

    private void Start()
    {
        if (itemInfoObj != null)
        {
            itemInfoObj.SetActive(false);
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


    //itemInfo�� ���
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
        bool gameOver = false, bool title = false, bool ending = false, bool pocket = false, bool enemyLife = false)
    {
        gameObj.SetActive(game);
        itemInfoObj.SetActive(itemInfo);
        resultObj.SetActive(result);
        gameOverObj.SetActive(gameOver);
        endingObj.SetActive(ending);
        titleObj.gameObject.SetActive(title);
        pocketObj.gameObject.SetActive(pocket);
        emenyLifeObj.SetActive(enemyLife);
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

    public void HideItemInfoPanel()
    {
        itemInfoObj.SetActive(false);
        currentItemData = null;
    }

    private void UpdateItemInfoPanelUI()
    {
        if (itemInfoObj != null && currentItemData != null)
        {

            itemInfoText.text = $"������ �̸� : {currentItemData.Name}\n" +
                                $"������ ���� : {currentItemData.Description}\n" +
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
            _inventoryManager.EquipItem(currentItemData);
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
        }
    }


    // ������ ��ư Ŭ�� ��
    public void UnequipButtonOnClick()
    {
        if (currentItemData != null)
        {
            _inventoryManager.UnequipItem();
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
        }
    }

    #endregion

    public void LoadItemInfo(ItemData itemData)
    {
        currentItemData = itemData;
        UpdateItemInfoPanelUI();
        SetViewObject(game: true, itemInfo: true);
    }


    public void Retry()
    {
        _gameManager.Retry();
    }

}