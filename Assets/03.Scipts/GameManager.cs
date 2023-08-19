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

    //플레이어 기본 스탯
    public int DefaultPlayerLife = 10;
    public int DefaultPlayerStamina = 10;
    public int DefaultPlayerAttack = 10;
    public int DefaultPlayerDefense = 10;
 

    private int playerLife;
    public int PlayerLife
    {
        get { return playerLife; }
        set { playerLife = Mathf.Max(0, value); } // 음수 값 방지
    }

    private int playerStamina;
    public int PlayerStamina
    {
        get { return playerStamina; }
        set { playerStamina = Mathf.Max(0, value); } 
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
    
    private void InitializePlayerStats()
    {
        PlayerLife = DefaultPlayerLife;
        PlayerStamina = DefaultPlayerStamina;
        // 기타 초기화 작업
    }

    void Start()
    {
        UIManager = UIManager.instance;
        Inventory = new InventoryManager();
        ChangeState(GameState.Title);
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
                StartBattle();
                break;
            case GameState.Die:
                Die();
                break;
        }
    }


    public void gameStart()
    {
        curruntCar = 1;
        ChangeState(GameState.NotBattle);
        UIManager.instance.titleObj.SetActive(false);
        UIManager.SetDefaultView();
    }


    //전투 상태일때 처리할 것들
    //public void TriggerBattleDoor() => ChangeState(GameState.OnBattle);
    void StartBattle()
    {
        UIManager.SetResultView();
    }

/*    public string BattleResult()
    {

        if(remainingEnemyLife == 0)
        {
            return "이겼다...";
        }
        else
        {
            return "치명상을 입었다...";
        }
    }*/

    //죽었을때
    void Die()
    {
        UIManager?.SetGameViewDie();
    }

    public void Retry()
    {
        //템 리셋 하는 기능이 있긴 해야 할듯
        ChangeState(GameState.Title);
    }


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