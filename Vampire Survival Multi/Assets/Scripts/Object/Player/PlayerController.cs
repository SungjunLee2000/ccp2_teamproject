using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // ���� ������Ʈ
    private Rigidbody2D rigid;

    // ���� ��ũ���ͺ� ������Ʈ
    private LocalPlayerData status;

    // �̵� ����
    private Vector2 moveVec;

    // ��ų ����
    private Skill normalAttack;
    private Skill skill;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        status = LocalPlayerData.Instance;

        // Init Skill
        ClassData classData = status.Class;

        normalAttack = classData.NormalAttack;
        skill = classData.ClassSkill;
    }

    private void Update()
    {
        // Ű �Է� �ޱ�
        OnControlKeyPressed();

        // �⺻ ����
        normalAttack.OnUseSkill();

        // �⺻ ���� �� ��ų ��ٿ�
        CooldownSkills();
    }

    private void CooldownSkills()
    {
        normalAttack.CooldownSkill();
        skill.CooldownSkill();
    }

    /***************************************************************
    * [ Ű �Է� ]
    * 
    * Ű �Է¿� ���� �ൿ ����
    ***************************************************************/

    private void OnControlKeyPressed()
    {
        OnMoveKeyPressed();
        OnSkillKeyPressed();
    }

    private void OnMoveKeyPressed()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        moveVec = new Vector2(horizontalInput, verticalInput);
    }

    private void OnSkillKeyPressed()
    {

    }

    private void FixedUpdate()
    {
        // Ű �Է¿� ���� �÷��̾� ������
        Vector2 movement = moveVec.normalized * status.AGI * Time.deltaTime;

        rigid.MovePosition(rigid.position + movement);

        // �÷��̾� ��ǥ ����
        LocalPlayerData.Instance.Position = transform.position;
    }



}
