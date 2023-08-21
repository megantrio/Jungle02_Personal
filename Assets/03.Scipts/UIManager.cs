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
        }
        else
        {
            Destroy(gameObject); // 이미 초기화된 경우 이 오브젝트를 파괴
            return;
        }
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

    //Start
    private void Start()
    {
        if (itemInfoObj != null)
        {
            itemInfoObj.SetActive(false);
        }

        UpdatePocketPanelUI();
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

    //아이템 패널 업데이트
    private void UpdateItemInfoPanelUI()
    {
        if (itemInfoObj != null && currentItemData != null)
        {

            itemInfoText.text = $"아이템 이름 : {currentItemData.Name}\n" +
                                $"아이템 설명 : {currentItemData.Description}\n\n" +
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
            InventoryManager.Instance.EquipItem(currentItemData);
            itemInfoObj.SetActive(false);

            //최근 열람한 아이템 정보를 담고 있는 오브젝트를 찾아서 파괴
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

            Debug.Log("아이템 장착");
        }
    }


    // 버리기 버튼 클릭 시
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

            Debug.Log("아이템 장착 해제");
        }
    }

    #endregion

    public void ShowPocket()
    { 
        if (currentItemData != null)
        {
            //인벤토리 매니저에서 현재 장착 아이템만 불러오는 기능
        }


            
    }


    private void UpdatePocketPanelUI()
    {
        if (pocketObj != null && currentItemData != null)
        {
            curruntEquipItem.text = $"장착아이템 : {currentItemData.Name}\n";

            //추가 수치 불러오기
            PlayerStats player = _gameManager.GetPlayerStats();
            List<ItemData> equipableItems = _inventoryManager.GetEquipableItems();
            List<ItemData> usableItems = _inventoryManager.GetUsableItems();

            //생명력. 늘어날지는 모르겠음 늘어나게끔 구현 하면 좋음
            int totalLifeBoost = equipableItems.Sum(item => item.AddLife) + usableItems.Sum(item => item.AddLife);

            //아이템을 통해 늘어난 공격력 계산
            int totalAttackBoost = equipableItems.Sum(item => item.AddAttack) + usableItems.Sum(item => item.AddAttack);
            int modifiedAttack = player.Attack + totalAttackBoost;

            //아이템을 통해 늘어난 방어력 계산
            int totalDefenseBoost = equipableItems.Sum(item => item.AddDefense) + usableItems.Sum(item => item.AddDefense);
            int modifiedDefense = player.Defense + totalDefenseBoost;

            //아이템을 통해 늘어난 공격력 계산
            int totalAgilityBoost = equipableItems.Sum(item => item.AddAgility) + usableItems.Sum(item => item.AddAgility);
            int modifiedAgility = player.Agility + totalAgilityBoost;

            //현재 수치  출력
            curruntAttack.text = $"공격력 : {modifiedAttack}\n";
            curruntDefense.text = $"방어력 : {modifiedDefense}\n";
            curruntAgility.text = $"민첩함 : {modifiedAgility}\n";
        }

    }
    #region 주머니



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