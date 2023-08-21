using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearDoor : MonoBehaviour
{
    public GameObject doorChild; // 활성화할 자식 오브젝트
    public float scaleDuration = 0.5f; // 스케일 변화에 걸리는 시간
    public bool enableTriggerAfterActivation = true; // 활성화 후 트리거 활성화 여부
    private bool isPlayerColliding = false; // 플레이어와 충돌 여부
    private float scaleTimer = 0.0f; // 스케일 변화 타이머
    private Vector3 initialScale; // 초기 스케일 값

    public GameObject endingObj;
    public GameObject player;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 플레이어와 충돌 시 자식 오브젝트 활성화
            doorChild.SetActive(true);
            isPlayerColliding = true;
            endingObj.SetActive(true);
            player.SetActive(false);

        }
    }
}
