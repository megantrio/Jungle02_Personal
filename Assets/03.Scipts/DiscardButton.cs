using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscardButton : MonoBehaviour
{
    public void ClosePopupUI()
    {
        // UI 캔버스 비활성화
        transform.parent.gameObject.SetActive(false);

        // 아이템 오브젝트 파괴
        Destroy(transform.parent.parent.gameObject);
    }

}