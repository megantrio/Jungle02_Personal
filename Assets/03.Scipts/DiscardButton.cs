using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscardButton : MonoBehaviour
{
    public void ClosePopupUI()
    {
        // UI ĵ���� ��Ȱ��ȭ
        transform.parent.gameObject.SetActive(false);

        // ������ ������Ʈ �ı�
        Destroy(transform.parent.parent.gameObject);
    }

}