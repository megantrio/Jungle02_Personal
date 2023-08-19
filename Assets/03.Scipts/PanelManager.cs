using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    public GameObject itemInfoPanel;
    private ItemInfo currentItemInfo;

    private void Start()
    {
        // ���� �� �г� ��Ȱ��ȭ
        if (itemInfoPanel != null)
        {
            itemInfoPanel.SetActive(false);
        }
    }

    public void ShowItemInfo(ItemInfo itemInfo)
    {
        currentItemInfo = itemInfo;
        UpdatePanelUI();
        itemInfoPanel.SetActive(true);
    }

    public void HideItemInfoPanel()
    {
        itemInfoPanel.SetActive(false);
        currentItemInfo = null;
    }

    private void UpdatePanelUI()
    {
        if (itemInfoPanel != null && currentItemInfo != null)
        {
            Text itemNameText = itemInfoPanel.transform.Find("itemNameText").GetComponent<Text>();
            Text itemDescriptionText = itemInfoPanel.transform.Find("itemDescriptionText").GetComponent<Text>();

            if (itemNameText != null)
            {
                itemNameText.text = currentItemInfo.itemName;
            }

            if (itemDescriptionText != null)
            {
                itemDescriptionText.text = currentItemInfo.itemDescription;
            }
        }
    }

    public void DiscardButton()
    {
        // ������ ��ư Ŭ�� �� ȣ��� �Լ�
        if (currentItemInfo != null)
        {
            // ������ �ı� ���� ���� ����
            Destroy(currentItemInfo.gameObject);
            HideItemInfoPanel();
        }
    }
}
