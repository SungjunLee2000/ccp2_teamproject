using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private ClassData classData;

    // ���� ������Ʈ
    private Rigidbody2D rigid;

    // �÷��̾� ������ ���� ����
    private Vector2 position;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        position = new Vector2(horizontalInput, verticalInput);

    }

    void FixedUpdate()
    {
        Vector2 movement = position.normalized * classData.Speed * Time.deltaTime;

        rigid.MovePosition(rigid.position + movement);
    }



}
