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
    public static UIManager instance; // UIManager Ŭ������ �ν��Ͻ��� �����ϱ� ���� ���� ����

    private void Awake()
    {
        _gameManager = GameManager.instance; // GameManager Ŭ������ �ν��Ͻ��� ������
        _gameManager.UIManager = this; // GameManager�� ���� UIManager �ν��Ͻ��� ���

        if (instance == null) // UIManager�� �ν��Ͻ��� �����Ǿ����� ���� ���
        {
            instance = this; // ���� UIManager �ν��Ͻ��� instance�� �Ҵ�
        }
        else // �̹� �ٸ� UIManager �ν��Ͻ��� �����ϴ� ���
        {
            Destroy(this); // ���� UIManager �ν��Ͻ��� �ı��Ͽ� �ߺ� ������ ����
        }

    }
    #endregion

    private GameManager _gameManager;

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
    public TextMeshProUGUI steminaText; // ü��ĭ

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

    //itemInfo�� ���
    public void SetItemInfoView()
    {
        SetViewObject(game: true, itemInfo: true);
    }

    //���� ��� â�� ���
    public void SetResultView()
    {
        SetViewObject(game: true, result: true);
    }

    //ĭ Ŭ�����
    public void SetClearView()
    {
        carClearText.SetText($"���� ����� ������ {_gameManager.Car} ��° ĭ�� �Ѿ �� �ֽ��ϴ�.");
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
            newText += "��";
        lifeText.SetText(newText);
    }

    public void UpdateCarText(int CarIdx)
    {
        curruntCarText.SetText($"{CarIdx}��° ĭ");
    }

    //���׹̳�
    public void UpdatePlayerStemina(int SteminaValue)
    {
        string newText = string.Empty;
        for (int i = 0; i < SteminaValue; i++)
            newText += "��";
        steminaText.SetText(newText);
    }


/*    /// <summary>
    /// �ش� ������Ʈ�� Ȥ�ö� �������� ����ִ� ��쿡 ȣ����� �� ��. ȣ��ο��� �� ���� �� ������ϵ��� ó����
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