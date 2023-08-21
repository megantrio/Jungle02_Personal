using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using static GameManager;

public class BattleManager : MonoBehaviour
{
    public GameObject BattleWall;
    private static BattleManager instance;
    public static BattleManager Instance => instance;

    private GameManager gameManager;
    private InventoryManager inventoryManager;

    private void Awake()
    {
        instance = this;
        gameManager = GameManager.Instance;
        inventoryManager = GetComponent<InventoryManager>();
    }


    public void BattleStart(int enemyCar)
    {
        PlayerStats player = gameManager.GetPlayerStats(); // 플레이어 스탯 가져오기
        int enemyLife = GetEnemyLifeByCar(enemyCar);
        int enemyAttack = GetEnemyAttackByCar(enemyCar);
        int enemyAgility = GetEnemyAgilityByCar(enemyCar);

        // 아이템 적용
        List<ItemData> usableItems = inventoryManager.GetUsableItems();
        List<ItemData> equipableItems = inventoryManager.GetEquipableItems();

        foreach (var usableItem in usableItems)
        {
            ApplyItemEffects(ref player, usableItem);
        }

        foreach (var equipableItem in equipableItems)
        {
            ApplyItemEffects(ref player, equipableItem);
        }

        // 민첩함 비교하여 선공권 판단
        bool playerFirst = player.Agility >= enemyAgility;

        while (player.Life > 0 && enemyLife > 0)
        {
            if (playerFirst)
            {
                int damage = player.Attack;
                enemyLife -= damage;
                Debug.Log("Player attacks Enemy for " + damage + " damage.");
                //입히는 데미지를 텍스트로 출력하는 기능도 넣으면 좋을 것 같음.
            }
            else
            {
                int damage = CalculateDamage(enemyAttack, player.Defense);
                player.Life -= damage;
                Debug.Log("Enemy attacks Player for " + damage + " damage.");
                //입는 데미지를 텍스트로 출력하는 기능도 넣으면 좋을 것 같음.
            }

            playerFirst = !playerFirst;
        }

    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && gameManager != null && gameManager.UIManager != null)
        {
            //int enemyCar = gameManager.curruntCar;
            //BattleStart(enemyCar);
            gameManager.ChangeState(GameState.OnBattle);
            Debug.Log("배틀 스타트");
        }

    }

    //데미지 계산
    public int CalculateDamage(int attackerAttack, int targetDefense)
    {
        int baseDamage = attackerAttack - targetDefense;
        return Mathf.Max(baseDamage, 0);
    }

    #region 아이템 수치 계산
    public void ApplyItemEffects(ref PlayerStats player, ItemData item)
    {
        player.Life += item.AddLife;
        player.Attack += item.AddAttack;
        player.Defense += item.AddDefense;
        player.Agility += item.AddAgility;
    }

    public void RemoveItemEffects(ref PlayerStats player, ItemData item)
    {
        player.Life -= item.AddLife;
        player.Attack -= item.AddAttack;
        player.Defense -= item.AddDefense;
        player.Agility -= item.AddAgility;
    }

    #endregion

    //적 밸런싱 수치 조절 필요
    private int GetEnemyAttackByCar(int car)
    {
        int enemyAttack;

        if (car == 1)
        {
            enemyAttack = 20;
        }
        else
        {
            enemyAttack = 0; // 다른 차량에 대한 값 설정
        }

        return enemyAttack;
    }

    private int GetEnemyLifeByCar(int car)
    {
        int enemyLife;

        if (car == 1)
        {
            enemyLife = 20;
        }
        else
        {
            enemyLife = 0; // 다른 차량에 대한 값 설정
        }

        return enemyLife;
    }

    public int GetEnemyAgilityByCar(int car)
    {
        int enemyAgility;

        if (car == 1)
        {
            enemyAgility = 20;
        }
        else
        {
            enemyAgility = 0; // 다른 차량에 대한 값 설정
        }

        return enemyAgility;
    }
}
