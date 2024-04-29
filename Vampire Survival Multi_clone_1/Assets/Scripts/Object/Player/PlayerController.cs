using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerController : MonoBehaviour, IControlState
{
    [Header("�̺�Ʈ")]
    [SerializeField] private GameEvent deadEvent;
    [SerializeField] private GameEvent reviveEvent;

    // ���� ������Ʈ
    private Rigidbody2D rigid;
    private Player player;

    // ���� ��ũ���ͺ� ������Ʈ
    private PlayerData playerData;
    private ClassData classData;

    // �̵� ����
    private Vector2 moveVec;

    // ��ų ����
    private Skill autoAttack;
    private float attackCooldown;
    private Skill skill;
    private float skillCooldown;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
        playerData = LocalPlayerData.Instance.PlayerData;

        // Init Position In PlayerData
        playerData.Position = transform.position;

        // Init Skill & Normal Attack
        InitSkill();
    }

    private void InitSkill()
    {
        autoAttack = classData.PassiveSkill;
        attackCooldown = 0;

        skill = classData.ActiveSkill;
        skillCooldown = 0;
    }

    private void Update()
    {
        // �⺻ ����
        OnNormalAttack();

        // �⺻ ���� �� ��ų ��ٿ�
        CooldownSkills();
    }

    private void OnNormalAttack()
    {
        if (attackCooldown <= 0)
        {
            autoAttack.UseSkill(player);

            // ���� �� ��ٿ� ����
            attackCooldown = autoAttack.Cooldown;
        }
    }

    private void CooldownSkills()
    {
        attackCooldown -= Time.deltaTime;
        skillCooldown -= Time.deltaTime;
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
        if (WaveData.Instance.IsRunning)
        {
            // Notify Dead Event
            deadEvent.NotifyUpdate();
        }
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
            if (skillCooldown <= 0)
            {
                skill.UseSkill(player);

                // ��ų ��� �� ��ٿ� ����
                skillCooldown = skill.Cooldown;
            }
        }
    }

    private void FixedUpdate()
    {
        // Ű �Է¿� ���� �÷��̾� ������
        Vector2 movement = moveVec.normalized * playerData.MoveSpeed * Time.deltaTime;

        rigid.MovePosition(rigid.position + movement);

        // �÷��̾� ��ǥ ����
        playerData.Position = transform.position;
    }
}
