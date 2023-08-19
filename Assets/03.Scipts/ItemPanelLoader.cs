using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPanelLoader : MonoBehaviour
{
    public ItemInfo itemInfo; // ������ ���� ������Ʈ�� Inspector���� �������ּ���.

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {

            Debug.Log("�浹��");
            PanelManager panelManager = FindObjectOfType<PanelManager>();
            if (panelManager != null)
            {
                panelManager.ShowItemInfo(itemInfo); // ������ ������ �гο� ǥ���ϰ� �г��� Ȱ��ȭ
            }
        }
    }
}