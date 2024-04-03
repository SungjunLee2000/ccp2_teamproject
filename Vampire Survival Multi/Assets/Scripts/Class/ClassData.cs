using UnityEngine;

[CreateAssetMenu(menuName = "Game Object/Player/Class Data", fileName = "ClassData")]
public class ClassData : ObjectData
{
    [Header("���� �� ��ų")]
    [SerializeField]
    private Skill _normalAttack;
    public Skill NormalAttack
    {
        get { return _normalAttack; }
    }

    [SerializeField]
    private Skill _skill;
    public Skill ClassSkill
    {
        get { return _skill; }
    }
}
