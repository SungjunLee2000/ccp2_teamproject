using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public enum RoomType { Room1, Room2 };

public class MultiSearchPanel : MonoBehaviour
{
    public GameObject makeRoomPanel;
    public GameObject multiSearchPanel;
    public ScrollRect scrollView;
    [SerializeField]
    public GameObject roomPrefab;
    [SerializeField]
    private List<RoomData> roomDatas;

    public TMP_InputField roomSearchBar;
    public Button multiExitBtn;
    public Button txtDeleteBtn;
    public Button makeRoomBtn;

    void Start()
    {
        for (int i = 0; i < roomDatas.Count; i++)
        {
            var room = MakeRoom((RoomType)i);
        }

    }
    public void openMakeRoomPanel()     //�� ����� �г� ����
    {
        makeRoomPanel.SetActive(true);
        multiExitBtn.interactable = false;
        txtDeleteBtn.interactable = false;
        makeRoomBtn.interactable = false;
        roomSearchBar.interactable = false;
    }
    public void closeMultiSearchPanel()     //��Ƽ �� ã�� �г� �ݱ�
    {
        multiSearchPanel.SetActive(false);
    }
    public Room MakeRoom(RoomType type)
    {
        var newRoom = Instantiate(roomPrefab, scrollView.content).GetComponent<Room>();
        newRoom.roomData = roomDatas[(int)type];
        newRoom.name = newRoom.roomData.RoomName;
        newRoom.transform.Find("RoomNameTxt").GetComponent<TMP_Text>().text = newRoom.roomData.RoomName;
        newRoom.transform.Find("PasswordTxt").GetComponent<TMP_Text>().text = newRoom.roomData.Password;
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
}
