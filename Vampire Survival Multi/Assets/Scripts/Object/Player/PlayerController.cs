using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // ���� ������Ʈ
    private Rigidbody2D rigid;

    // ���� ��ũ���ͺ� ������Ʈ
    private PlayerStatus status;

    // �÷��̾� ������ ���� ����
    private Vector2 position;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        status = PlayerStatus.Instance;
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        position = new Vector2(horizontalInput, verticalInput);

    }

    private void FixedUpdate()
    {
        Vector2 movement = position.normalized * status.AGI * Time.deltaTime;

        rigid.MovePosition(rigid.position + movement);
    }



}
