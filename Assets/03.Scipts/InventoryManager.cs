using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance; // 싱글톤 인스턴스

    public GameObject inventoryUIPrefab; // 인벤토리 UI 프리팹을 할당
    public Transform inventorySlotParent; // 인벤토리 슬롯들을 담는 부모 Transform

    private GameObject currentInventoryUI; // 현재 열려있는 인벤토리 UI 오브젝트

    private List<GameObject> itemSprites = new List<GameObject>(); // 소지한 아이템 스프라이트들을 저장할 리스트

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void DisplayItemSprite(Sprite itemSprite, string itemName, string itemDescription)
    {
        // 아이템 스프라이트를 담을 빈 게임 오브젝트 생성
        GameObject newItemSpriteObject = new GameObject("ItemSprite");
        SpriteRenderer spriteRenderer = newItemSpriteObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = itemSprite; // 스프라이트 설정

        // 아이템 스프라이트 오브젝트를 인벤토리 슬롯에 추가
        newItemSpriteObject.transform.SetParent(inventorySlotParent, false);
        itemSprites.Add(newItemSpriteObject);

        // 아이템 정보 표시를 위해 인벤토리 UI 업데이트
        UpdateInventoryUI(itemName, itemDescription);
    }

    public void RemoveItemSprite(GameObject itemSprite)
    {
        // 아이템 스프라이트 리스트에서 제거
        itemSprites.Remove(itemSprite);
        Destroy(itemSprite);
    }

    private void UpdateInventoryUI(string itemName, string itemDescription)
    {
        // 아이템 정보를 업데이트하고 인벤토리 UI를 업데이트하는 로직을 추가해야 합니다.
        // 아래와 같이 적절한 UI 요소에 정보를 할당해주는 식으로 구현할 수 있습니다.
        // itemNameText.text = itemName;
        // itemDescriptionText.text = itemDescription;
    }

    // 기타 인벤토리 UI 관련 함수들...
}
