/*
 using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MultiGameController : MonoBehaviour
{
    public GameObject multiPanel;           //��Ƽ ���� �г�
    public GameObject makeRoomPanel;        //�� ����� �г�
    public GameObject askEnterPanel;        //�� ���� Ȯ�� �г�

    public Button[] btnsActiveByMakeRoom;       //�� ����� �г� ���ݱ⿡ ���� Ȱ��ȭ�� �ٲ�� ��ư��
    public Button[] btnsActiveByAskEnter;       //�� ���� Ȯ�� �г� ���ݱ⿡ ���� Ȱ��ȭ�� �ٲ�� ��ư��

    //��Ƽ ���� �гο� �����ϴ� Object��
    public TMP_InputField roomSearchBar;        //�� �˻� ��
    public ScrollRect scrollView;               //�� ��� ������ ��ũ��
    public Button searchByNameBtn;              //�̸����� �� ã�� ��ư
    public Button searchByCodeBtn;              //�ڵ�� �� ã�� ��ư
    public TextMeshProUGUI placeHolder;        //placeHolder

    //�� ���� Ȯ�� �гο� �����ϴ� Object��
    public TMP_Dropdown roomPeopleNumDropdown;  //�� �ο��� ���� ��Ӵٿ�
    public TMP_Dropdown privateDropdown;        //�� �������� ��Ӵٿ�
    public TMP_InputField passwordInputField;   //�� ��й�ȣ �Է¹�
    public TMP_InputField roomNameInputField;   //�� �̸� �Է¹� 

    public int maxPasswordLimit = 8;            //�н����� �� ����
    public int maxRoomNameLimit = 12;           //�� �̸� ���ڼ� ����

    private string initialRoomPeopleNumValue;       //�⺻ �� �ο���(2��)
    private string initialPrivateValue;             //�⺻ �� ��������(����)
    private string initialPassword;                 //�⺻ �� ��й�ȣ("")
    private string initialRoomName;                 //�⺻ �� �̸�("")

    //������
    public GameObject roomPrefab;               //�� ���� �� �Է� ������

    //RoomData ����Ʈ(������ ��� ����Ʈ)
    [SerializeField]
    private List<RoomData> roomDatas;

    //������ �� Ÿ��(��Ƽ �г� ���½� �ٷ� ����Ʈ�� ������ ���)
    public enum RoomType { Room1, Room2 };

    void Start()
    {
        for (int i = 0; i < roomDatas.Count; i++)
        {
            var room = makeRoom((RoomType)i);
        }

        initialRoomPeopleNumValue = roomPeopleNumDropdown.options[roomPeopleNumDropdown.value].text;
        initialPrivateValue = privateDropdown.options[privateDropdown.value].text;
        initialPassword = passwordInputField.text;
        initialRoomName = roomNameInputField.text;

        roomNameInputField.characterLimit = maxRoomNameLimit;
        roomNameInputField.onValueChanged.AddListener(delegate { OnInputFieldValueChanged(roomNameInputField, maxRoomNameLimit); });

        passwordInputField.characterLimit = maxPasswordLimit;
        passwordInputField.onValueChanged.AddListener(delegate { OnInputFieldValueChanged(passwordInputField, maxPasswordLimit); });
    }

    void Update()
    {
        enableMakePassword();
        setSearchBarState();
    }

    //�� ����� �г� ���� + ��ư ��Ȱ��ȭ
    public void openMakeRoomPanel() 
    {
        makeRoomPanel.SetActive(true);
        foreach (Button btns in btnsActiveByMakeRoom)
        {
            btns.interactable = false;
        }
        roomSearchBar.interactable = false;
    }

    //�� ���� Ȯ�� �г� ���� + ��ư ��Ȱ��ȭ (RoomInfo������ ���)xxxx
    public void openAskEnterPanel() 
    {
        askEnterPanel.SetActive(true);
        foreach (Button btns in btnsActiveByAskEnter)
        {
            btns.interactable = false;
        }
    }

    //��Ƽ ���� �г� �ݱ�
    public void closeMultiPanel() 
    {
        multiPanel.SetActive(false);
    }

    //�� ����� �г� �ݱ� + ��ư Ȱ��ȭ + �ۼ��� �� ������ �ʱ�ȭ
    public void closeMakeRoomPanel() 
    {
        makeRoomPanel.SetActive(false);
        foreach (Button btns in btnsActiveByMakeRoom)
        {
            btns.interactable = true;
        }
        roomSearchBar.interactable = true;

        roomPeopleNumDropdown.value = roomPeopleNumDropdown.options.FindIndex(option => option.text == initialRoomPeopleNumValue);
        privateDropdown.value = privateDropdown.options.FindIndex(option => option.text == initialPrivateValue);
        passwordInputField.text = initialPassword;
        roomNameInputField.text = initialRoomName;
    }

    //�� �˻� �� �ý�Ʈ ����
    public void deleteTxt()
    {
        roomSearchBar.text = "";
    }

    //inputField ���� ���ڼ� ����
    void OnInputFieldValueChanged(TMP_InputField inputField, int maxLimit)
    {
        if (inputField.text.Length > maxLimit)
        {
            inputField.text = inputField.text.Substring(0, maxLimit);
        }
    }

    //����Ʈ�� �� �߰� �� �� ����+ ���ÿ� �г� �ݱ�
    public void addRoomToList()
    {
        string roomPeopleNumValue = roomPeopleNumDropdown.options[roomPeopleNumDropdown.value].text;
        string privateValue = privateDropdown.options[privateDropdown.value].text;
        string password = passwordInputField.text;
        string roomName = roomNameInputField.text;
        string roomCode = Guid.NewGuid().ToString("N").Substring(0, 12);

        if (roomName.Equals("") || (privateValue.Equals("�����") && password.Equals("")))
        {
            Debug.Log("�ʼ��Է¶� ����!");
        }
        else
        {
            int maxPeopleNum = int.Parse(roomPeopleNumValue);
            bool isPrivate = privateValue.Equals("�����");

            RoomData newRoomData = new RoomData(roomName, password, maxPeopleNum, privateValue.Equals("�����"),roomCode);
            roomDatas.Add(newRoomData);

            // ���� ������ RoomType�� �߰�
            RoomType roomType = (RoomType)roomDatas.Count - 1;
            makeRoom(roomType);
            closeMakeRoomPanel();
        }
    }

    //������ roomType�� �ִ� ����� ����Ʈ�� ���̰� �Ͽ� ���� ����
    public Room2 makeRoom(RoomType type)
    {
        var newRoom = Instantiate(roomPrefab, scrollView.content).GetComponent<Room2>();
        newRoom.roomData = roomDatas[(int)type];
        newRoom.name = newRoom.roomData.RoomName;
        newRoom.transform.Find("RoomNameTxt").GetComponent<TMP_Text>().text = newRoom.roomData.RoomName;
        newRoom.transform.Find("PasswordTxt").GetComponent<TMP_Text>().text = newRoom.roomData.Password;
        newRoom.transform.Find("RoomCodeTxt").GetComponent<TMP_Text>().text = newRoom.roomData.RoomCode;
        newRoom.transform.Find("RoomPeopleNumImg/RoomPeopleNumTxt").GetComponent<TMP_Text>().text = newRoom.roomData.MaxPeopleNum.ToString() + "��";
        if (newRoom.roomData.IsPrivate)
        {
            newRoom.transform.Find("IsPrivateImg/IsPrivateTxt").GetComponent<TMP_Text>().text = "�����";
        }
        else
        {
            newRoom.transform.Find("IsPrivateImg/IsPrivateTxt").GetComponent<TMP_Text>().text = "����";
        }

        return newRoom;
    }

    //����� �� ��й�ȣ ���� Ȱ��ȭ, ������ ��й�ȣ ���� ��Ȱ��ȭ(�� �����)
    public void enableMakePassword()        
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

    //�� �˻� - �̸� �˻��� �ڵ� �˻����� ������
    public void searchRoom()
    {
        string searchText = roomSearchBar.text;
        RectTransform content = scrollView.content;

        if(placeHolder.text == "�� �̸� �˻�...")
        {
            foreach (Transform room in content)
            {
                // �� child gameobject�� �̸��� �����ɴϴ�.
                string roomName = room.Find("RoomNameTxt").GetComponent<TMP_Text>().text;
                room.gameObject.SetActive(searchText.Equals(roomName));
            }
        }
        else if (placeHolder.text == "�� �ڵ� �˻�...")
        {
            foreach (Transform room in content)
            {
                // �� child gameobject�� �̸��� �����ɴϴ�.
                string roomCode = room.Find("RoomCodeTxt").GetComponent<TMP_Text>().text;
                room.gameObject.SetActive(searchText.Equals(roomCode));
            }
        }
        else
        {
            Debug.Log("�˻� ����");
        }

    }

    //�� �̸����� �˻� / placeholder ���� / �̸� ��ư ��Ȱ��ȭ, �ڵ� ��ư Ȱ��ȭ
    public void changeSearchTypeName()
    {
        //searchByNameBtn.interactable = false;
        //searchByCodeBtn.interactable = true;
        placeHolder.text = "�� �̸� �˻�...";
    }

    //�� �̸����� �˻� / placeholder ���� / �̸� ��ư ��Ȱ��ȭ, �ڵ� ��ư Ȱ��ȭ
    public void changeSearchTypeCode()
    {
        //searchByNameBtn.interactable = true;
        //searchByCodeBtn.interactable = false;
        placeHolder.text = "�� �ڵ� �˻�...";
    }

    //�� �˻��� ���� ����
    private void setSearchBarState()
    {
        if(makeRoomPanel.activeSelf)
        {
            roomSearchBar.interactable = false;
        }
        else
        {
            roomSearchBar.interactable = true;
        }
    }

}

 */