using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public GameObject canvasPrefab; // UI ĵ���� ������

    private GameObject canvasInstance; // ������ UI ĵ���� �ν��Ͻ�

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ShowItemInfo();
        }
    }

    private void ShowItemInfo()
    {
        if (canvasInstance == null)
        {
            // UI ĵ���� �ν��Ͻ� ���� �� ����
            canvasInstance = Instantiate(canvasPrefab, transform.position, Quaternion.identity);
            canvasInstance.transform.SetParent(transform, false);
        }
        else
        {
            // �̹� ������ UI ĵ���� �ν��Ͻ��� ���� ��� Ȱ��ȭ
            canvasInstance.SetActive(true);
        }
    }
}