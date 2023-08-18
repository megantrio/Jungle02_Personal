using UnityEngine;

//0818
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10.0f; // �̵� �ӵ� ���� ����
    public float jumpForce = 8.0f; // ���� �� ���� ����
    public float cameraYOffset = 2.0f; // ī�޶��� Y ��ġ ���� ����
    private bool isGrounded = false; // �ٴڿ� ��Ҵ��� ����

    private Rigidbody2D rb;
    private Camera mainCamera;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
    }

    private void Update()
    {
        // ȭ��ǥ �¿� Ű �Է� ó��
        float moveHorizontal = Input.GetAxis("Horizontal");

        Vector2 moveDirection = new Vector2(moveHorizontal, 0.0f); // ���� �̵��� 0���� ����
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, rb.velocity.y); // y �ӵ��� �״�� ����

        // �����̽� Ű�� ���� ó��
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false;
        }

        // ī�޶��� ��ġ ������Ʈ
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