using UnityEngine;

//0818

public class DoorManager : MonoBehaviour
{
    public GameObject doorChild; // 활성화할 자식 오브젝트
    public float scaleDuration = 0.5f; // 스케일 변화에 걸리는 시간
    public bool enableTriggerAfterActivation = true; // 활성화 후 트리거 활성화 여부
    private bool isPlayerColliding = false; // 플레이어와 충돌 여부
    private float scaleTimer = 0.0f; // 스케일 변화 타이머
    private Vector3 initialScale; // 초기 스케일 값
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
            // 스케일 변화 타이머 업데이트
            scaleTimer += Time.deltaTime;

            // X 스케일 값 변경 (0에서 1까지 선형 보간), Y 값은 초기 스케일 값 유지
            float scaleFactor = Mathf.Clamp01(scaleTimer / scaleDuration);
            Vector3 newScale = new Vector3(scaleFactor, initialScale.y, initialScale.z);
            doorChild.transform.localScale = newScale;

            if (scaleTimer >= scaleDuration)
            {
                // 타이머 초기화 및 스케일 변화 완료 시 동작
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
                // 플레이어와 충돌 시 자식 오브젝트 활성화
                doorChild.SetActive(true);
                isPlayerColliding = true;
                _gameManager.currentCar++;
                UIManager.instance.UpdateCarText(_gameManager.currentCar);
                Debug.Log("currentCar increased: " + _gameManager.currentCar);
            }
        }
    }
}