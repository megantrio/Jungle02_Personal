using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

//0818

public class DoorManager : MonoBehaviour
{
    public GameObject doorChild; // 활성화할 자식 오브젝트
    public float scaleDuration = 0.5f; // 스케일 변화에 걸리는 시간
    public bool enableTriggerAfterActivation = true; // 활성화 후 트리거 활성화 여부
    private bool isPlayerColliding = false; // 플레이어와 충돌 여부
    private float scaleTimer = 0.0f; // 스케일 변화 타이머
    private Vector3 initialScale; // 초기 스케일 값


    private GameManager _gameManager;
    private InventoryManager inventoryManager;
    public GameObject battleButton;

    public TextMeshProUGUI playerDamage;
    public TextMeshProUGUI enemyDamage;
    public TextMeshProUGUI enemyLifeText;
    public GameObject playerDamageObj;
    public GameObject enemyDamageObj;
    public GameObject enemyObj;
    public GameObject enemyLifeObj;

    public int enemyLife;
    public int enemyAttack;
    public int enemyAgility;

    public TextMeshProUGUI lifeText;

    private Collider2D doorManagerCollider;

    private void Awake()
    {
        _gameManager = GameManager.Instance;
        inventoryManager = GetComponent<InventoryManager>();
    }

    private void Start()
    {
        doorManagerCollider = GetComponent<Collider2D>();
        initialScale = doorChild.transform.localScale;
        _gameManager = FindObjectOfType<GameManager>();
        
    }

    private void Update()
    {
        if (isPlayerColliding)
        {
            // 스케일 변화 타이머 업데이트
            scaleTimer += Time.deltaTime;

            // X 스케일 값 변경 (0에서 1까지 선형 보간), Y 값은 초기 스케일 값 유지
            float scaleFactor = Mathf.Clamp01(scaleTimer / scaleDuration);
            Vector3 newScale = new Vector3(scaleFactor, initialScale.y, initialScale.z);
            doorChild.transform.localScale = newScale;

            if (scaleTimer >= scaleDuration)
            {
                // 타이머 초기화 및 스케일 변화 완료 시 동작
                scaleTimer = 0.0f;
                isPlayerColliding = false;

                doorManagerCollider.isTrigger = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (_gameManager != null)
            {
                // 플레이어와 충돌 시 자식 오브젝트 활성화
                doorChild.SetActive(true);
                isPlayerColliding = true;
                _gameManager.currentCar++;
                UIManager.instance.UpdateCarText(_gameManager.currentCar);
                Debug.Log("currentCar increased: " + _gameManager.currentCar);
                Invoke("ButtonApply", 1.5f);
                enemyLifeObj.SetActive(true);
                string newText = string.Empty;
                for (int i = 0; i < enemyLife; i++)
                {
                    newText += "♥";
                    enemyLifeText.SetText(newText);
                }
            }
        }
    }

    public void BattleStart()
    {
        StartCoroutine(BattleGetIt());       
    }


    IEnumerator BattleGetIt()
    {
        
        yield return null;
        Debug.Log("전투 시작");
        battleButton.SetActive(false);
        // 민첩함 비교하여 선공권 판단
        bool playerFirst = _gameManager.PlayerAgility >= enemyAgility;
        Debug.Log("선공권 판단");

        int curruntPlayerLife = _gameManager.PlayerLife;

        while (_gameManager.PlayerLife > 0 && enemyLife > 0)
        {
            Debug.Log("적 체력 : " + enemyLife);
            Debug.Log("내 체력 : " + curruntPlayerLife);
           //Debug.Log("반복문 시작");

            if (playerFirst)
            {
                int damagef = _gameManager.PlayerAttack;
                Debug.Log("데미지f는 = " + damagef);
                enemyLife -= damagef;
                enemyDamageObj.SetActive(true);
                enemyDamage.SetText($"{damagef}");
                Debug.Log("Player attacks Enemy for " + damagef + " damage.");
                HideText();
                
            }

            else
            {
                int damage = enemyAttack - _gameManager.PlayerDefense;
                Debug.Log("데미지는 = " + damage);
                if(damage <= -1)
                {
                    damage = 0;
                }
                else
                {
                    curruntPlayerLife -= damage;
                }

                string newText = string.Empty;
                for (int i = 0; i < curruntPlayerLife; i++)
                {
                    newText += "♥";
                    lifeText.SetText(newText);
                }

                Debug.Log("현재 내 체력 : " + (curruntPlayerLife) );
               
                playerDamageObj.SetActive(true);
                playerDamage.SetText($"{damage}");
                Debug.Log("Enemy attacks Player for " + damage + " damage.");
                HideText(); 
                
            }

            playerFirst = !playerFirst;

            if (enemyLife <= 0)
            {

                Debug.Log("적이 죽었다");
                UIManager.instance.SetResultView();
                enemyObj.SetActive(false);
                string newText = string.Empty;
                for (int i = 0; i < curruntPlayerLife; i++)
                {
                    newText += "♥";
                    lifeText.SetText(newText);
                }
                
                _gameManager.PlayerLife = curruntPlayerLife;
                Debug.Log("전투 후 내 체력은 = " + _gameManager.PlayerLife);
            }

            if (curruntPlayerLife <= 0)
            {

                Debug.Log("내가 죽었다");
                UIManager.instance.SetGameViewDie();
                _gameManager.Die();
            }

        }

    }
    public void ButtonApply()
    {
        battleButton.SetActive(true);
    }

    public void HideText()
    {
        enemyDamageObj.SetActive(false);
        playerDamageObj.SetActive(false);
    }
}