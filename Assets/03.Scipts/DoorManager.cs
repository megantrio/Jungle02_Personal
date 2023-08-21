using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

//0818

public class DoorManager : MonoBehaviour
{
    public GameObject doorChild; // Ȱ��ȭ�� �ڽ� ������Ʈ
    public float scaleDuration = 0.5f; // ������ ��ȭ�� �ɸ��� �ð�
    public bool enableTriggerAfterActivation = true; // Ȱ��ȭ �� Ʈ���� Ȱ��ȭ ����
    private bool isPlayerColliding = false; // �÷��̾�� �浹 ����
    private float scaleTimer = 0.0f; // ������ ��ȭ Ÿ�̸�
    private Vector3 initialScale; // �ʱ� ������ ��


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
            // ������ ��ȭ Ÿ�̸� ������Ʈ
            scaleTimer += Time.deltaTime;

            // X ������ �� ���� (0���� 1���� ���� ����), Y ���� �ʱ� ������ �� ����
            float scaleFactor = Mathf.Clamp01(scaleTimer / scaleDuration);
            Vector3 newScale = new Vector3(scaleFactor, initialScale.y, initialScale.z);
            doorChild.transform.localScale = newScale;

            if (scaleTimer >= scaleDuration)
            {
                // Ÿ�̸� �ʱ�ȭ �� ������ ��ȭ �Ϸ� �� ����
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
                // �÷��̾�� �浹 �� �ڽ� ������Ʈ Ȱ��ȭ
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
                    newText += "��";
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
        Debug.Log("���� ����");
        battleButton.SetActive(false);
        // ��ø�� ���Ͽ� ������ �Ǵ�
        bool playerFirst = _gameManager.PlayerAgility >= enemyAgility;
        Debug.Log("������ �Ǵ�");

        int curruntPlayerLife = _gameManager.PlayerLife;

        while (_gameManager.PlayerLife > 0 && enemyLife > 0)
        {
            Debug.Log("�� ü�� : " + enemyLife);
            Debug.Log("�� ü�� : " + curruntPlayerLife);
           //Debug.Log("�ݺ��� ����");

            if (playerFirst)
            {
                int damagef = _gameManager.PlayerAttack;
                Debug.Log("������f�� = " + damagef);
                enemyLife -= damagef;
                enemyDamageObj.SetActive(true);
                enemyDamage.SetText($"{damagef}");
                Debug.Log("Player attacks Enemy for " + damagef + " damage.");
                HideText();
                
            }

            else
            {
                int damage = enemyAttack - _gameManager.PlayerDefense;
                Debug.Log("�������� = " + damage);
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
                    newText += "��";
                    lifeText.SetText(newText);
                }

                Debug.Log("���� �� ü�� : " + (curruntPlayerLife) );
               
                playerDamageObj.SetActive(true);
                playerDamage.SetText($"{damage}");
                Debug.Log("Enemy attacks Player for " + damage + " damage.");
                HideText(); 
                
            }

            playerFirst = !playerFirst;

            if (enemyLife <= 0)
            {

                Debug.Log("���� �׾���");
                UIManager.instance.SetResultView();
                enemyObj.SetActive(false);
                string newText = string.Empty;
                for (int i = 0; i < curruntPlayerLife; i++)
                {
                    newText += "��";
                    lifeText.SetText(newText);
                }
                
                _gameManager.PlayerLife = curruntPlayerLife;
                Debug.Log("���� �� �� ü���� = " + _gameManager.PlayerLife);
            }

            if (curruntPlayerLife <= 0)
            {

                Debug.Log("���� �׾���");
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