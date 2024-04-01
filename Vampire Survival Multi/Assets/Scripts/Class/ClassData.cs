using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Object/Player", fileName = "ClassData")]
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

    [Header("�ʱ� ���")]
    [SerializeField]
    private List<ItemData> _equips;
    public List<ItemData> Equips
    {
        get { return _equips; }
    }
}
