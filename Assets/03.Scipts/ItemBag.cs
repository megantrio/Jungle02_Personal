using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//0818

public class ItemBag : MonoBehaviour
{
    public List<GameObject> itemPrefabs = new List<GameObject>();
    public float jumpForceMin = 5f;
    public float jumpForceMax = 10f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            DropItems(); // ������ ��� �Լ� ȣ��
            gameObject.SetActive(false); // ���� ������Ʈ ��Ȱ��ȭ
        }
    }

    private void DropItems()
    {
        for (int i = 0; i < 3; i++) // ������ ������ 3�� ���
        {
            int randomIndex = Random.Range(0, itemPrefabs.Count); // ���� �ε��� ����
            GameObject itemPrefab = itemPrefabs[randomIndex]; // ������ ������ ������ ����

            Vector2 spawnPosition = transform.position + Vector3.up; // ���� ��ġ ���� (���� ��ġ���� �ణ ����)
            GameObject spawnedItem = Instantiate(itemPrefab, spawnPosition, Quaternion.identity); // ������ ����

            Rigidbody2D rb = spawnedItem.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                float xForce = Random.Range(-3f, 3f); // X ���� �� ���� ����
                float yForce = Random.Range(jumpForceMin, jumpForceMax); // Y ���� �� ���� ����
                rb.AddForce(new Vector2(xForce, yForce), ForceMode2D.Impulse); // Ƣ������� �� �߰�

             
            }
        }
    }
}
