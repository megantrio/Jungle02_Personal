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
        // ������ �������� �ʿ��� �� �����ͼ� ����
        _gameManager.PlayerLife += itemInfo.playerLife;
        _gameManager.PlayerStamina += itemInfo.playerStamina;

        // �� ���ȿ��� �����ϰ��� �ϴ� ������ �ִٸ� ���⿡ �߰�

        // ������ ���� �� ���ŵ� �� Ȯ��
        Debug.Log($"Player Life: {_gameManager.PlayerLife}");
        Debug.Log($"Player Stamina: {_gameManager.PlayerStamina}");
        _gameManager.PlayerLife += itemInfo.playerLife;
        _gameManager.PlayerStamina += itemInfo.playerStamina;
        // �� ���ȿ��� �����ϰ��� �ϴ� ������ �ִٸ� ���⿡ �߰�
    }
}