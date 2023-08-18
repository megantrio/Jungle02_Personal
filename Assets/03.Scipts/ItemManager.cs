using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public GameObject canvasPrefab; // UI 캔버스 프리팹

    private GameObject canvasInstance; // 생성된 UI 캔버스 인스턴스

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
            // UI 캔버스 인스턴스 생성 및 설정
            canvasInstance = Instantiate(canvasPrefab, transform.position, Quaternion.identity);
            canvasInstance.transform.SetParent(transform, false);
        }
        else
        {
            // 이미 생성된 UI 캔버스 인스턴스가 있을 경우 활성화
            canvasInstance.SetActive(true);
        }
    }
}