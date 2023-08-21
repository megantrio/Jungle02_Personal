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
        PlayerStats player = gameManager.GetPlayerStats(); // �÷��̾� ���� ��������
        int enemyLife = GetEnemyLifeByCar(enemyCar);
        int enemyAttack = GetEnemyAttackByCar(enemyCar);
        int enemyAgility = GetEnemyAgilityByCar(enemyCar);

        // ������ ����
        List<ItemData> usableItems = inventoryManager.GetUsableItems();
        List<ItemData> equipableItems = inventoryManager.GetEquipableItems();

        //������ ���� ��ü�� ����� �ȵ�.
        foreach (var usableItem in usableItems)
        {
            ApplyItemEffects(ref player, usableItem);
        }

        foreach (var equipableItem in equipableItems)
        {
            ApplyItemEffects(ref player, equipableItem);
        }

        // ��ø�� ���Ͽ� ������ �Ǵ�
        bool playerFirst = player.Agility >= enemyAgility;


        while (player.Life > 0 && enemyLife > 0)
        {
            if (playerFirst)
            {
                int damage = player.Attack;
                enemyLife -= damage;
                Debug.Log("Player attacks Enemy for " + damage + " damage.");
                //������ �������� �ؽ�Ʈ�� ����ϴ� ��ɵ� ������ ���� �� ����.
            }
            else
            {
                int damage = CalculateDamage(enemyAttack, player.Defense);
                player.Life -= damage;
                Debug.Log("Enemy attacks Player for " + damage + " damage.");
                //�Դ� �������� �ؽ�Ʈ�� ����ϴ� ��ɵ� ������ ���� �� ����.
            }

            playerFirst = !playerFirst;
        }
        
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && gameManager != null && gameManager.UIManager != null)
        {
            int enemyCar = gameManager.curruntCar;
            BattleStart(enemyCar);
            gameManager.ChangeState(GameState.OnBattle);
            Debug.Log("��Ʋ ��ŸƮ");
        }

    }

    //������ ���
    public int CalculateDamage(int attackerAttack, int targetDefense)
    {
        int baseDamage = attackerAttack - targetDefense;
        return Mathf.Max(baseDamage, 0);
    }

    #region ������ ��ġ ���
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

    //�� �뷱�� ��ġ ���� �ʿ�
    private int GetEnemyAttackByCar(int car)
    {
        int enemyAttack;

        if (car == 1)
        {
            enemyAttack = 20;
        }
        else
        {
            enemyAttack = 0; // �ٸ� ������ ���� �� ����
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
            enemyLife = 0; // �ٸ� ������ ���� �� ����
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
            enemyAgility = 0; // �ٸ� ������ ���� �� ����
        }

        return enemyAgility;
    }
}