using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    public List<ItemType> item = new List<ItemType>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (item[0] != ItemType.Ignore)
            {
                //�κ��丮�� ����ֱ�
                GameManager.instance.Inventory.AddItem(item[0]);

                gameObject.SetActive(false);
            }
        }
    }
}
