using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    #region Singleton
    public static UIManager instance; // UIManager 클래스의 인스턴스를 저장하기 위한 정적 변수

    private void Awake()
    {
        _gameManager = GameManager.instance; // GameManager 클래스의 인스턴스를 가져옴
        _gameManager.UIManager = this; // GameManager에 현재 UIManager 인스턴스를 등록

        if (instance == null) // UIManager의 인스턴스가 생성되어있지 않은 경우
        {
            instance = this; // 현재 UIManager 인스턴스를 instance에 할당
        }
        else // 이미 다른 UIManager 인스턴스가 존재하는 경우
        {
            Destroy(this); // 현재 UIManager 인스턴스를 파괴하여 중복 생성을 방지
        }

    }
    #endregion

    private GameManager _gameManager;

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
    public TextMeshProUGUI steminaText; // 체력칸

    [Header("Item Info")]
    //public Transform itemCreatePosTr;
    public GameObject itemPrefab;

    [Header("resultPanel")]
    public TextMeshProUGUI resultText;
    public TextMeshProUGUI attackNumText;
    public TextMeshProUGUI damageText;
    public TextMeshProUGUI blockText;
    public TextMeshProUGUI decreaseText;

    [Header("Choice")]
    public Button equipButton;
    public Button discardButton;

    [Header("Clear")]
    public TextMeshProUGUI carClearText;


    #region Method by GameState

    public void SetDefaultView()
    {
        SetViewObject(game: true);
    }

    //itemInfo를 띄움
    public void SetItemInfoView()
    {
        SetViewObject(game: true, itemInfo: true);
    }

    //전투 결과 창을 띄움
    public void SetResultView()
    {
        SetViewObject(game: true, result: true);
    }

    //칸 클리어시
    public void SetClearView()
    {
        carClearText.SetText($"이제 당신은 열차의 {_gameManager.Car} 번째 칸을 넘어설 수 있습니다.");
    }

    public void SetEndingView()
    {
        SetViewObject(ending: true);
    }

    public void SetGameViewDie()
    {
        SetViewObject(gameOver: true);
    }

    private void SetViewObject(bool game = false, bool itemInfo = false, bool result = false, bool gameOver = false, bool title = false, bool ending = false)
    {
        gameObj.SetActive(game);
        itemInfoObj.SetActive(itemInfo);
        resultObj.SetActive(result);
        gameOverObj.SetActive(gameOver);
        endingObj.SetActive(ending);
        titleObj.gameObject.SetActive(titleObj);

    }
    #endregion


    private void UpdateAllText()
    {
        UpdatePlayerLife(_gameManager.DefaultPlayerLife);
        UpdateCarText(_gameManager.Car);
        UpdatePlayerStemina(_gameManager.DefaultPlayerStemina);
    }

    public void UpdatePlayerLife(int LifeValue)
    {
        string newText = string.Empty;
        for (int i = 0; i < LifeValue; i++)
            newText += "♥";
        lifeText.SetText(newText);
    }

    public void UpdateCarText(int CarIdx)
    {
        curruntCarText.SetText($"{CarIdx}번째 칸");
    }

    //스테미나
    public void UpdatePlayerStemina(int SteminaValue)
    {
        string newText = string.Empty;
        for (int i = 0; i < SteminaValue; i++)
            newText += "★";
        steminaText.SetText(newText);
    }


/*    /// <summary>
    /// 해당 오브젝트는 혹시라도 아이템이 비어있는 경우에 호출되지 않 음. 호출부에서 빈 값일 때 수행안하도록 처리됨
    /// </summary>
    public void RemoveItemOnView()
    {
        var obj = itemCreatePosTr.GetChild(0).gameObject;
        Destroy(obj);
    }*/

    public void LoadItemInfo()
    {
        itemInfoObj.SetActive(true);
    }

 /*   public void GameRestart()
    {
        _gameManager.Reset();
    }*/


    public void Retry()
    {
        _gameManager.Retry();
    }

    public void ResetItem()
    {
        Destroy(this.gameObject);
    }
}