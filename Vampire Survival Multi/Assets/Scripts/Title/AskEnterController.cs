using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AskEnterController : MonoBehaviour
{
    //������
    public GameObject askEnterPanelPrefab;      //�� ���� Ȯ�� �г��� ������
    public GameObject roomInfoPrefab;           //�� ������ ������ ������

    //�� ���� Ȯ�� ���� Objects
    public TextMeshProUGUI tryPasswordTxt;      //�� ����� �ۼ��ϴ� ��й�ȣ

    private string publicTxt = "����";
    private string privateTxt = "�����";

    public Button[] btnsActiveByAskEnter;       //�� ���� Ȯ�� �г� ���ݱ⿡ ���� Ȱ��ȭ�� �ٲ�� ��ư��

    //�� ���� Ȯ�� �г� �ݱ� + ��ư Ȱ��ȭ (AskEnterPanel������ ���)
    public void closeAskEnterPanel()
    {
        askEnterPanelPrefab.SetActive(false);
        foreach (Button btns in btnsActiveByAskEnter)
        {
            btns.interactable = true;
        }
    }

    //�н����� Ȯ�� �� ����
    public void checkPassword()
    {
        string tryPw = tryPasswordTxt.text.Replace("\u200B", "");

        string pw = RoomInfo2.selectedPW;
        string isPrivate = RoomInfo2.selectedIsPrivateTxt;

        if (isPrivate.Equals(publicTxt))  // ������ ���
        { 
            SceneManager.LoadScene("LobbyScene");
        }
        else if (isPrivate.Equals(privateTxt) && tryPw.Equals(pw))   // ����������� ����� ��ġ�� ���
        {
            SceneManager.LoadScene("LobbyScene");
        }
        else
        {
            Debug.Log("Ʋ�� ��й�ȣ ==> ���� �Ұ���");
        }
    }

}
