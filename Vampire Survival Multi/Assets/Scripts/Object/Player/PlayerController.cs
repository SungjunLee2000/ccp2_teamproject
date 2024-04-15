using UnityEngine;

public class PlayerController : MonoBehaviour, IControlState
{
    [Header("�̺�Ʈ")]
    [SerializeField] private GameEvent deadEvent;
    [SerializeField] private GameEvent reviveEvent;

    // ���� ������Ʈ
    private Rigidbody2D rigid;

    // ���� ��ũ���ͺ� ������Ʈ
    private PlayerData playerData;

    // �̵� ����
    private Vector2 moveVec;

    // ��ų ����
    private Skill normalAttack;
    private Skill skill;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        playerData = LocalPlayerData.Instance.PlayerData;

        // Init Position In PlayerData
        playerData.Position = transform.position;

        // Init Skill
        ClassData classData = playerData.Class;

        normalAttack = classData.NormalAttack;
        skill = classData.ClassSkill;
    }

    private void Update()
    {
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

    private void OnEnable()
    {
        // Set Control State
        ControlContext.Instance.SetState(this);

        // Notify Revive Event
        reviveEvent.NotifyUpdate();
    }

    private void OnDisable()
    {
        // Notify Dead Event
        deadEvent.NotifyUpdate();
    }

    /***************************************************************
    * [ Ű �Է� ]
    * 
    * Ű �Է¿� ���� �ൿ ����
    ***************************************************************/

    public void OnControlKeyPressed()
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
        if (Input.GetButtonDown("Skill"))
        {
            skill.OnUseSkill();
        }
    }

    private void FixedUpdate()
    {
        // Ű �Է¿� ���� �÷��̾� ������
        Vector2 movement = moveVec.normalized * playerData.AGI * Time.deltaTime;

        rigid.MovePosition(rigid.position + movement);

        // �÷��̾� ��ǥ ����
        playerData.Position = transform.position;
    }
}
