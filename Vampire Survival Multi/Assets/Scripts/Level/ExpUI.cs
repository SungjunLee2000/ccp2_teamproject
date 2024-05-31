using TMPro;
using UnityEngine;

public class ExpUI : MonoBehaviour
{
    [Header("���� ������Ʈ")]
    [SerializeField] private TextMeshProUGUI tmpUI;

    // ���� ������
    private GameData gameData;

    private void Start()
    {
        gameData = GameData.Instance;
    }

    public void UpdateUI()
    {
        int level = gameData.Level;
        int exp = gameData.Exp;
        int requireExp = gameData.RequireExp;

        tmpUI.text = "Exp : " + exp + " / " + requireExp;
    }
}