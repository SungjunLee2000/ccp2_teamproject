using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInfoPanel : MonoBehaviour
{
    public GameObject userMenuPanel;
    public GameObject askGiveAdminPanel;
    public GameObject askKickUserPanel;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void toggleUserMenuPanel()       //������ ���� �޴� �г� ���ݱ�
    {
        userMenuPanel.SetActive(!userMenuPanel.activeSelf);
    }
    public void openAskGiveAdminPanel()     //���� �Ѱ��ֱ� �г� ����
    {
        askGiveAdminPanel.SetActive(true);
    }
    public void openAskKickUserPanel()     //���� ���� �г� ����
    {
        askKickUserPanel.SetActive(true);
    }

}
