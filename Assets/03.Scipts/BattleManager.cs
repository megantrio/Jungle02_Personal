using UnityEngine;

public class BattleManager : MonoBehaviour
{
    private static BattleManager instance;
    public static BattleManager Instance => instance;

    private GameManager gameManager;

    private void Awake()
    {
        instance = this;
        gameManager = GameManager.Instance; 
    }

    // 전투 결과
    public void CalculateBattleResults(int enemyStamina, out int remainingEnemyLife, out int remainingPlayerLife)
    {
        int enemyAttack = GetEnemyAttackByCar(gameManager.currentCar);
        int playerAttack = gameManager.PlayerAttack;
        int playerStamina = gameManager.PlayerStamina;
        int playerDefense = gameManager.PlayerLife; // 방어력을 체력으로 치환

        remainingEnemyLife = CalculateRemainingEnemyLife(enemyAttack, enemyStamina);
        remainingPlayerLife = CalculateRemainingPlayerLife(playerAttack, playerStamina, playerDefense, enemyStamina);
    }

    //남은 적 체력
    private int CalculateRemainingEnemyLife(int enemyAttack, int enemyStamina)
    {
        int enemyLife = GetEnemyLifeByCar(gameManager.currentCar);
        int totalEnemyAttackDamage = enemyAttack * enemyStamina;
        return enemyLife - totalEnemyAttackDamage;
    }

    //남은 플레이어 체력
    private int CalculateRemainingPlayerLife(int playerAttack, int playerStamina, int playerDefense, int enemyStamina)
    {
        int totalPlayerAttackDamage = playerAttack * playerStamina;
        int totalReceivedDamage = (GetEnemyAttackByCar(gameManager.currentCar) - playerDefense) * enemyStamina;

        return gameManager.PlayerLife - totalReceivedDamage;
    }

    //플레이어가 받은 데미지
    private int CalculateTotalPlayerReceivedDamage(int enemyStamina)
    {
        int enemyAttack = GetEnemyAttackByCar(gameManager.currentCar);
        int playerDefense = gameManager.PlayerDefense;
        return (enemyAttack - playerDefense) * enemyStamina;
    }


    //적 밸런싱 수치 조절 필요
    private int GetEnemyAttackByCar(int car)
    {
        int enemyAttack;

        if (car == 1)
        {
            enemyAttack = 20;
        }
        else if (car == 10)
        {
            enemyAttack = 100;
        }
        else
        {
            enemyAttack = 50;
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
        else if (car == 10)
        {
            enemyLife = 100;
        }
        else
        {
            enemyLife = 50;
        }

        return enemyLife;
    }

    public int GetEnemyStaminaByCar(int car)
    {
        int enemyStamina;

        if (car == 1)
        {
            enemyStamina = 20;
        }
        else if (car == 10)
        {
            enemyStamina = 100;
        }
        else
        {
            enemyStamina = 50;
        }

        return enemyStamina;
    }
}
