using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoomInfo : MonoBehaviour
{
    public GameObject askEnterPanel;        //�� ���� Ȯ�� �г�
    public TextMeshProUGUI isPrivateTxt;    //�� ���� ���� �ؽ�Ʈ
    public TextMeshProUGUI passwordTxt;    //�� ��� �ؽ�Ʈ

    private string publicTxt = "����";
    public static string selectedIsPrivateTxt;
    public static string selectedPW;

    void Awake()
    {
        //�г��� ��ġ ���
        askEnterPanel.transform.position = new Vector3(0f, 0f, askEnterPanel.transform.position.z);
    }

    //�� ���� �����
    public void enterRoom()
    {
        GameObject multiSearchPanel = GameObject.Find("MultiSearchPanel");
        GameObject newAskEnterPanel = Instantiate(askEnterPanel, multiSearchPanel.transform);
        selectedIsPrivateTxt = isPrivateTxt.text;
        selectedPW = passwordTxt.text;

        if (isPrivateTxt.text.Equals(publicTxt))
        {
            Transform submitPasswordBarTransform = newAskEnterPanel.transform.Find("SubmitPasswordBar");
            if (submitPasswordBarTransform != null)
            {
                submitPasswordBarTransform.gameObject.SetActive(false);
            }
        }
    }
}
