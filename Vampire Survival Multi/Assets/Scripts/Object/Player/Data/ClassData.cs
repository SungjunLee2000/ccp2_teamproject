using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Object/Player", fileName = "ClassData")]
public class ClassData : ObjectData
{
    [Header("�ʱ� ���")]
    [SerializeField]
    private List<ItemData> _equips;
    public List<ItemData> Equips
    {
        get { return _equips; }
    }
}
