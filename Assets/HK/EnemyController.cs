using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 3.0f; // �̵� �ӵ�

    private GameObject player; // �÷��̾� ������Ʈ

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");      
    }


    //�÷��̾���� �浹��, ������ �޼��� ȣ��. �� Ȯ�� �ʿ�
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.GetDamage(); 
            }           
        }
    }

    //�ڷ�ƾ���� �÷��̾� �߰�
    public IEnumerator ChasePlayer(Transform enemyTransform)
    {
        while (true)
        {
            if (player != null && enemyTransform != null)
            {
                Vector2 targetPosition = player.transform.position;
                Vector2 newPosition = Vector2.MoveTowards(enemyTransform.position, targetPosition, moveSpeed * Time.deltaTime);
                enemyTransform.position = newPosition;
            }

            yield return null;
        }
    }

}


