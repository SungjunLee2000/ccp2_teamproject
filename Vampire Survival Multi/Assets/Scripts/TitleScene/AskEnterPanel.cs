using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AskEnterPanel : MonoBehaviour
{
    [SerializeField]
    public GameObject askEnterPanelPrefab;
    [SerializeField]
    public GameObject roomInfoPrefab;
    public TextMeshProUGUI tryPasswordTxt;
    private string publicTxt = "����";
    private string privateTxt = "�����";

    public void closeAskEnterPanel()        //�� ���� ����� �г� �ݱ�
    {
        askEnterPanelPrefab.SetActive(false);
    }

    public void checkPassword()         //�н����� Ȯ��
    {
        string tryPw = tryPasswordTxt.text.Replace("\u200B", "");
        string pw = roomInfoPrefab.transform.Find("PasswordTxt").GetComponent<TextMeshProUGUI>().text;
        string isPrivateTxt = roomInfoPrefab.transform.Find("IsPrivateImg/IsPrivateTxt").GetComponent<TextMeshProUGUI>().text;

        if (isPrivateTxt == publicTxt)
        {
            SceneManager.LoadScene("LobbyScene");
        }
        else if (isPrivateTxt.Equals(privateTxt) && tryPw.Equals(pw))
        {
            SceneManager.LoadScene("LobbyScene");
        }
        else
        {
            Debug.Log("Ʋ�� ��й�ȣ ==> ���� �Ұ���");
        }
    }
}
