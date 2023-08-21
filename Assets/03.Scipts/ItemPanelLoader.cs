using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemPanelLoader : MonoBehaviour
{   
    public ItemData itemData;


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            UIManager.instance.ShowItemInfo(itemData);
            Debug.Log("Ãæµ¹ÇÔ");
        }

    }
}