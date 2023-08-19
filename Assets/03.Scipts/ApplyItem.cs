using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyItem : MonoBehaviour
{
    private GameManager _gameManager;

    private void Awake()
    {
        _gameManager = GameManager.Instance;
    }

    public void ApplyItemEffect(ItemInfo itemInfo)
    {
        // 아이템 정보에서 필요한 값 가져와서 적용
        _gameManager.PlayerLife += itemInfo.playerLife;
        _gameManager.PlayerStamina += itemInfo.playerStamina;

        // 적 스탯에도 적용하고자 하는 정보가 있다면 여기에 추가

        // 아이템 적용 후 갱신된 값 확인
        Debug.Log($"Player Life: {_gameManager.PlayerLife}");
        Debug.Log($"Player Stamina: {_gameManager.PlayerStamina}");
        _gameManager.PlayerLife += itemInfo.playerLife;
        _gameManager.PlayerStamina += itemInfo.playerStamina;
        // 적 스탯에도 적용하고자 하는 정보가 있다면 여기에 추가
    }
}