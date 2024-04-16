using UnityEngine;

public abstract class Skill : ScriptableObject
{
    [Header("��Ÿ��")]
    [SerializeField]
    private float cooldown;
    private float curCooldown;

    public void OnUseSkill(Player caster)
    {
        if (IsUseabled())
        {
            CastSkill(caster);

            // ��ų ��� �� ��ٿ�
            curCooldown = cooldown;
        }
    }

    protected virtual bool IsUseabled()
    {
        return curCooldown <= 0;
    }

    public void CooldownSkill()
    {
        if (curCooldown > 0)
            curCooldown -= Time.deltaTime;
    }

    protected abstract void CastSkill(Player caster);
}