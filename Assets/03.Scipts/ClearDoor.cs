using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearDoor : MonoBehaviour
{
    public GameObject doorChild; // Ȱ��ȭ�� �ڽ� ������Ʈ
    public float scaleDuration = 0.5f; // ������ ��ȭ�� �ɸ��� �ð�
    public bool enableTriggerAfterActivation = true; // Ȱ��ȭ �� Ʈ���� Ȱ��ȭ ����
    private bool isPlayerColliding = false; // �÷��̾�� �浹 ����
    private float scaleTimer = 0.0f; // ������ ��ȭ Ÿ�̸�
    private Vector3 initialScale; // �ʱ� ������ ��

    public GameObject endingObj;
    public GameObject player;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // �÷��̾�� �浹 �� �ڽ� ������Ʈ Ȱ��ȭ
            doorChild.SetActive(true);
            isPlayerColliding = true;
            endingObj.SetActive(true);
            player.SetActive(false);

        }
    }
}
