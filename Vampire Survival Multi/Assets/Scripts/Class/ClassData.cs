using UnityEngine;

[CreateAssetMenu(menuName = "Game Object/Player/Class Data", fileName = "ClassData")]
public class ClassData : ObjectData
{
    [SerializeField]
    private float _attackSpeed;
    public float AttackSpeed
    {
        get { return _attackSpeed; }
    }

    [SerializeField]
    private float _lifeSteal;
    public float LifeSteal
    {
        get { return _lifeSteal; }
    }

    [Header("���� �� ��ų")]
    [SerializeField]
    private Skill _autoAttack;
    public Skill AutoAttack
    {
        get { return _autoAttack; }
    }

    [SerializeField]
    private Skill _skill;
    public Skill ClassSkill
    {
        get { return _skill; }
    }

    [Header("Ŭ���� ����")]
    [SerializeField]
    private string _name;
    public string Name
    {
        get { return _name; }
    }
}
