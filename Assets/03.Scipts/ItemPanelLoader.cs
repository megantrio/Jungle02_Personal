using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPanelLoader : MonoBehaviour
{
    public ItemInfo itemInfo; // 아이템 정보 컴포넌트를 Inspector에서 연결해주세요.

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {

            Debug.Log("충돌함");
            PanelManager panelManager = FindObjectOfType<PanelManager>();
            if (panelManager != null)
            {
                panelManager.ShowItemInfo(itemInfo); // 아이템 정보를 패널에 표시하고 패널을 활성화
            }
        }
    }
}