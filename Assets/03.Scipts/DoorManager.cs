using UnityEngine;

//0818

public class DoorManager : MonoBehaviour
{
    public GameObject doorChild; // Ȱ��ȭ�� �ڽ� ������Ʈ
    public float scaleDuration = 0.5f; // ������ ��ȭ�� �ɸ��� �ð�
    public bool enableTriggerAfterActivation = true; // Ȱ��ȭ �� Ʈ���� Ȱ��ȭ ����
    private bool isPlayerColliding = false; // �÷��̾�� �浹 ����
    private float scaleTimer = 0.0f; // ������ ��ȭ Ÿ�̸�
    private Vector3 initialScale; // �ʱ� ������ ��
    private GameManager _gameManager;

    private Collider2D doorManagerCollider;

    private void Start()
    {
        doorManagerCollider = GetComponent<Collider2D>();
        initialScale = doorChild.transform.localScale;
        _gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if (isPlayerColliding)
        {
            // ������ ��ȭ Ÿ�̸� ������Ʈ
            scaleTimer += Time.deltaTime;

            // X ������ �� ���� (0���� 1���� ���� ����), Y ���� �ʱ� ������ �� ����
            float scaleFactor = Mathf.Clamp01(scaleTimer / scaleDuration);
            Vector3 newScale = new Vector3(scaleFactor, initialScale.y, initialScale.z);
            doorChild.transform.localScale = newScale;

            if (scaleTimer >= scaleDuration)
            {
                // Ÿ�̸� �ʱ�ȭ �� ������ ��ȭ �Ϸ� �� ����
                scaleTimer = 0.0f;
                isPlayerColliding = false;

                doorManagerCollider.isTrigger = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(_gameManager != null)
            {
                // �÷��̾�� �浹 �� �ڽ� ������Ʈ Ȱ��ȭ
                doorChild.SetActive(true);
                isPlayerColliding = true;
                _gameManager.currentCar++;
                UIManager.instance.UpdateCarText(_gameManager.currentCar);
                Debug.Log("currentCar increased: " + _gameManager.currentCar);
            }
        }
    }
}