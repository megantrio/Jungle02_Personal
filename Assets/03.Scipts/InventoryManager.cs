using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance; // �̱��� �ν��Ͻ�

    public GameObject inventoryUIPrefab; // �κ��丮 UI �������� �Ҵ�
    public Transform inventorySlotParent; // �κ��丮 ���Ե��� ��� �θ� Transform

    private GameObject currentInventoryUI; // ���� �����ִ� �κ��丮 UI ������Ʈ

    private List<GameObject> itemSprites = new List<GameObject>(); // ������ ������ ��������Ʈ���� ������ ����Ʈ

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
        // ������ ��������Ʈ�� ���� �� ���� ������Ʈ ����
        GameObject newItemSpriteObject = new GameObject("ItemSprite");
        SpriteRenderer spriteRenderer = newItemSpriteObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = itemSprite; // ��������Ʈ ����

        // ������ ��������Ʈ ������Ʈ�� �κ��丮 ���Կ� �߰�
        newItemSpriteObject.transform.SetParent(inventorySlotParent, false);
        itemSprites.Add(newItemSpriteObject);

        // ������ ���� ǥ�ø� ���� �κ��丮 UI ������Ʈ
        UpdateInventoryUI(itemName, itemDescription);
    }

    public void RemoveItemSprite(GameObject itemSprite)
    {
        // ������ ��������Ʈ ����Ʈ���� ����
        itemSprites.Remove(itemSprite);
        Destroy(itemSprite);
    }

    private void UpdateInventoryUI(string itemName, string itemDescription)
    {
        // ������ ������ ������Ʈ�ϰ� �κ��丮 UI�� ������Ʈ�ϴ� ������ �߰��ؾ� �մϴ�.
        // �Ʒ��� ���� ������ UI ��ҿ� ������ �Ҵ����ִ� ������ ������ �� �ֽ��ϴ�.
        // itemNameText.text = itemName;
        // itemDescriptionText.text = itemDescription;
    }

    // ��Ÿ �κ��丮 UI ���� �Լ���...
}
