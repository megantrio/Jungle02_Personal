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

    #region Object할당
    [Header("UIGroup")]
    public GameObject gameObj; //기본 UI
    public GameObject itemInfoObj; //아이템 정보창
    public GameObject resultObj; //전투 결과창
    public GameObject titleObj; //타이틀 창
    public GameObject gameOverObj; //게임 오버 창
    public GameObject endingObj; //엔딩 창   

    [Header("Default Game Info Area")]
    public TextMeshProUGUI lifeText; //생명력
    public TextMeshProUGUI curruntCarText; //현재 열차 칸
    public TextMeshProUGUI enemyLifeText;
    public GameObject emenyLifeObj;

    [Header("Item Info")]
    //public Transform itemCreatePosTr;
    public GameObject itemPrefab;
    public TextMeshProUGUI itemInfoText;

    [Header("Pocket")]
    public GameObject pocketObj; //인벤토리
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

    #region 뷰 모음 
    //기본 뷰
    public void SetDefaultView()
    {
        SetViewObject(game: true, pocket: true);
        titleObj.SetActive(false);
    }

    //타이틀 뷰
    public void SetTitleView()
    {
        SetViewObject(title: true);
    }


    //itemInfo를 띄움
    public void SetItemInfoView(ItemData itemData)
    {
        currentItemData = itemData;
        UpdateItemInfoPanelUI();
        SetViewObject(game: true, itemInfo: true);
    }

    //전투 결과 창을 띄움
    public void SetResultView()
    {
        SetViewObject(game: true, result: true);
    }

    //클리어 시 엔딩 뷰 띄움
    public void SetEndingView()
    {
        SetViewObject(ending: true);
        gameObj.SetActive(false);
        pocketObj.SetActive(false);
    }

    //죽었을 때의 뷰
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

    #region 업데이트
    private void UpdateAllText()
    {
        UpdatePlayerLife(_gameManager.DefaultPlayerLife);
        UpdateCarText(_gameManager.curruntCar);
    }

    public void UpdatePlayerLife(int LifeValue)
    {
        string newText = string.Empty;
        for (int i = 0; i < LifeValue; i++)
            newText += "♥";
        lifeText.SetText(newText);
    }

    public void UpdateCarText(int curruntCar)
    {
        curruntCarText.SetText($"{curruntCar}번째 칸");
    }

    //스테미나
    public void UpdatenemyLife(int enemyLifeValue)
    {
        string newText = string.Empty;
        for (int i = 0; i < enemyLifeValue; i++)
            newText += "♥";
        enemyLifeText.SetText(newText);
    }
    #endregion

    //전투 결과 출력
    public void ShowResult(int remainingEnemyLife, int remainingPlayerLife)
    {
       
        string resultMessage = remainingEnemyLife == 0 ? "이겼다..." : "";
        if (remainingPlayerLife == 0)
        {
            _gameManager.Die();
        }

        resultObj.SetActive(true);
        resultText.SetText(resultMessage);
    }

    #region 아이템

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

            itemInfoText.text = $"아이템 이름 : {currentItemData.Name}\n" +
                                $"아이템 설명 : {currentItemData.Description}\n" +
                                $"추가 생명 : {currentItemData.AddLife}\n" +
                                $"추가 공격력 : {currentItemData.AddAttack}\n" +
                                $"추가 방어력 : {currentItemData.AddDefense}\n" +
                                $"추가 민첩함 : {currentItemData.AddAgility}";

        }

    }

    // 아이템 장착 버튼 클릭 시
    public void EquipButtonOnClick()
    {
        if (currentItemData != null)
        {
            // 선택한 아이템 데이터를 현재 아이템으로 저장
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


    // 버리기 버튼 클릭 시
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