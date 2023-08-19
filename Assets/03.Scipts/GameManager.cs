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


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
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
    public int Car
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
    public int DefaultPlayerStemina = 10;
  
    void Start()
    {
        UIManager = UIManager.instance;
        Inventory = new InventoryManager();
        ChangeState(GameState.Title);
    }


    #region 게임 상태 관련
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
        Car = 1;
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


    public void Clear() => StartCoroutine(nameof(ClearNextEventBeforeDelay));

    private bool nextStageWaitFlag;
    IEnumerator ClearNextEventBeforeDelay()
    {
        // 보상 처리 및 초기화
        nextStageWaitFlag = true;
        UIManager?.SetResultView();
        UIManager.instance.SetClearView();
        yield return new WaitForSeconds(2f); // 대기 (여운)


        if (Car <= 11)
        {
            Car++;

            ChangeState(GameState.NotBattle);
        }
        else
        {
            UIManager.instance.endingObj.SetActive(true);
            Time.timeScale = 0;
        }

    }
    #endregion

    public void Reset()
    {
        instance = null;
        Destroy(this.gameObject);
        State = GameState.Title;
    }
}
