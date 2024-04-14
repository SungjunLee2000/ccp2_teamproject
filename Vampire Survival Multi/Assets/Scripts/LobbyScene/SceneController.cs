using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public GameObject characterSettingPanel;
    public GameObject roomSettingPanel;
    public Button[] buttons;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void openCharacterSettingPanel()     //ĳ���� �г� ����
    {
        characterSettingPanel.SetActive(true);
    }

    public void openRoomSettingPanel()      //�� ���� �г� ����
    {
        roomSettingPanel.SetActive(true);
        foreach (Button button in buttons)
        {
            button.interactable = false;
        }
    }
    public void exitScene()                 //Ÿ��Ʋ ������ ���ư���
    {
        SceneManager.LoadScene("TitleScene");
    }

    
}
