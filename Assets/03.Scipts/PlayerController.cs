using UnityEngine;

//0818
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10.0f; // 이동 속도 조절 변수
    public float jumpForce = 8.0f; // 점프 힘 조절 변수
    public float cameraYOffset = 2.0f; // 카메라의 Y 위치 조절 변수
    private bool isGrounded = false; // 바닥에 닿았는지 여부

    private Rigidbody2D rb;
    private Camera mainCamera;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
    }

    private void Update()
    {
        // 화살표 좌우 키 입력 처리
        float moveHorizontal = Input.GetAxis("Horizontal");

        Vector2 moveDirection = new Vector2(moveHorizontal, 0.0f); // 상하 이동은 0으로 설정
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, rb.velocity.y); // y 속도는 그대로 유지

        // 스페이스 키로 점프 처리
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false;
        }

        // 카메라의 위치 업데이트
        Vector3 cameraPosition = mainCamera.transform.position;
        cameraPosition.x = transform.position.x;
        cameraPosition.y = transform.position.y + cameraYOffset;
        mainCamera.transform.position = cameraPosition;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}