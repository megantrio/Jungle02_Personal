using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//0818

public class ItemBag : MonoBehaviour
{
    private InventoryManager inventoryManager;
    public List<GameObject> itemPrefabs = new List<GameObject>();

   // private List<ItemData> equippedItems = new List<ItemData>();
    public float jumpForceMin = 5f;
    public float jumpForceMax = 10f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            DropItems(); // 아이템 드랍 함수 호출
            gameObject.SetActive(false); // 현재 오브젝트 비활성화
        }
    }

    private void DropItems()
    {
        for (int i = 0; i < 3; i++) // 랜덤한 아이템 3개 드랍
        {
            
            int randomIndex = Random.Range(0, itemPrefabs.Count);
            GameObject itemPrefab = itemPrefabs[randomIndex];
            Debug.Log("뽑음");
            Vector2 spawnPosition = transform.position + Vector3.up; // 스폰 위치 설정 (현재 위치에서 약간 위로)
            GameObject spawnedItem = Instantiate(itemPrefab, spawnPosition, Quaternion.identity); // 아이템 스폰

            Rigidbody2D rb = spawnedItem.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                float xForce = Random.Range(-3f, 3f); // X 방향 힘 랜덤 설정
                float yForce = Random.Range(jumpForceMin, jumpForceMax); // Y 방향 힘 랜덤 설정
                rb.AddForce(new Vector2(xForce, yForce), ForceMode2D.Impulse); // 튀어오르는 힘 추가
            }

        }
    }
}
