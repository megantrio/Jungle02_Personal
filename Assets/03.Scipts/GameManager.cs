using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public UIManager UIManager { get; set; }
    public InventoryManager Inventory { get; private set; }
    public int currentCar = 0;
    public GameObject player;

    public static GameManager Instance => instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            InitializePlayerStats();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

    public enum GameState
    {
        Title,
        NotBattle,
        OnBattle,
        Die
    }

    public GameState State { get; private set; }

    private int _car;
    public int curruntCar
    {
        get => _car;
        private set
        {
            _car = value;
            UIManager?.UpdateCarText(_car);
        }
    }

    //�÷��̾� �⺻ ����
    public int DefaultPlayerLife = 10;
    public int DefaultPlayerAgility = 10;
    public int DefaultPlayerAttack = 10;
    public int DefaultPlayerDefense = 10;

    // �÷��̾� �⺻ ������ �������� �Լ�
    public PlayerStats GetPlayerStats()
    {
        PlayerStats playerStats = new PlayerStats();
        playerStats.Life = DefaultPlayerLife;
        playerStats.Agility = DefaultPlayerAgility;
        playerStats.Attack = DefaultPlayerAttack;
        playerStats.Defense = DefaultPlayerDefense;
        return playerStats;
    }

    #region ������Ƽ ����
    private int playerLife;
    public int PlayerLife
    {
        get { return playerLife; }
        set { playerLife = Mathf.Max(0, value); } // ���� �� ����
    }

    private int playerAttack;
    public int PlayerAttack
    {
        get { return playerAttack; }
        set { playerAttack = Mathf.Max(0, value); }
    }

    private int playerDefense;
    public int PlayerDefense
    {
        get { return playerDefense; }
        set { playerDefense = Mathf.Max(0, value); }
    }

    #endregion

    //�ʱ�ȭ �۾�
    void Start()
    {
        UIManager = UIManager.instance;
        Inventory = new InventoryManager();
        InitializeUI();
        player.SetActive(false);
        ChangeState(GameState.Title);
        UIManager.instance.titleObj.SetActive(true);
    }

    private void InitializeUI()
    {
        UIManager.instance.gameObj.SetActive(false);
        UIManager.instance.itemInfoObj.SetActive(false);
        UIManager.instance.resultObj.SetActive(false);
        UIManager.instance.gameOverObj.SetActive(false);
        UIManager.instance.endingObj.SetActive(false);
        UIManager.instance.pocketObj.SetActive(false);
        UIManager.instance.emenyLifeObj.SetActive(false);
    }


    private void InitializePlayerStats()
    {
        PlayerLife = DefaultPlayerLife;
    }


    public void ChangeState(GameState state)
    {
        State = state;
        Debug.Log(state + "");
        switch (state)
        {
            case GameState.Title:
                break;
            case GameState.NotBattle:
                break;
            case GameState.OnBattle:
                Battle();
                break;
            case GameState.Die:
                Die();
                break;
        }
    }

    public void Die()
    {

        UIManager?.SetGameViewDie();
        player.SetActive(false);

    }


    public void gameStart()
    {
        curruntCar = 1;

        player.SetActive(true);
        ChangeState(GameState.NotBattle);
        UIManager.instance.titleObj.SetActive(false);
        UIManager.SetDefaultView();
    }

    public void Battle()
    {
        UIManager.instance.pocketObj.SetActive(false);
    }

    public void Retry()
    {
        //�� ���� �ϴ� ����� �ֱ� �ؾ� �ҵ�
        ChangeState(GameState.Title);
    }

    //��� ĭ Ŭ���� ���� ��.
    public void ending()
    {
        if(curruntCar == 11)
        {
            UIManager.instance.endingObj.SetActive(true);
        }    
    }

    public void Reset()
    {
        instance = null;
        Destroy(this.gameObject);
        State = GameState.Title;
    }

}