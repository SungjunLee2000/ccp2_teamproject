using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MakeRoomPanel : MonoBehaviour
{
    public GameObject makeRoomPanel;
    public ScrollRect scrollView;
    public TMP_Dropdown roomPeopleNumDropdown;
    public TMP_Dropdown privateDropdown;
    public TMP_InputField passwordInputField;
    public TMP_InputField roomNameInputField;
    public GameObject roomPrefab;
    public int maxPasswordLimit = 8;
    public int maxRoomNameLimit = 12;
    private List<GameObject> roomList = new List<GameObject>();

    void Start()
    {
        roomNameInputField.characterLimit = maxRoomNameLimit;
        roomNameInputField.onValueChanged.AddListener(delegate { OnInputFieldValueChanged(roomNameInputField, maxRoomNameLimit); });

        passwordInputField.characterLimit = maxPasswordLimit;
        passwordInputField.onValueChanged.AddListener(delegate { OnInputFieldValueChanged(passwordInputField, maxPasswordLimit); });
    }

    void Update()
    {
        
    }
    public void closeMakeRoomPanel()        //�� ����� �г� �ݱ�
    {
        makeRoomPanel.SetActive(false);
    }

    public void enableMakePassword()        //����� �� ��й�ȣ ���� Ȱ��ȭ, ������ ��й�ȣ ���� ��Ȱ��ȭ
    {
        if (privateDropdown.value == 1)
        {
            passwordInputField.interactable = true;
        }
        else
        {
            passwordInputField.interactable = false;
            passwordInputField.text = "";
        }
    }

    public void addRoomToList()
    {
        string roomPeopleNumValue = roomPeopleNumDropdown.options[roomPeopleNumDropdown.value].text;
        string privateValue = privateDropdown.options[privateDropdown.value].text;
        string password = passwordInputField.text;
        string roomName = roomNameInputField.text;

        GameObject newItem = Instantiate(roomPrefab, scrollView.content);

        newItem.transform.Find("RoomNameTxt").GetComponent<TMP_Text>().text = roomName;
        newItem.transform.Find("PasswordTxt").GetComponent<TMP_Text>().text = password;
        newItem.transform.Find("RoomPeopleNumImg/RoomPeopleNumTxt").GetComponent<TMP_Text>().text = roomPeopleNumValue;
        newItem.transform.Find("IsPrivateImg/IsPrivateTxt").GetComponent<TMP_Text>().text = privateValue;
        
        roomList.Add(newItem);
    }

    void OnInputFieldValueChanged(TMP_InputField inputField, int maxLimit)
    {
        if (inputField.text.Length > maxLimit)
        {
            inputField.text = inputField.text.Substring(0, maxLimit);
        }
    }
}
