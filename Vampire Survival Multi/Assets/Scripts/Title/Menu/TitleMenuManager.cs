using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(TitleMenuUI))]
public class TitleMenuManager : MonoBehaviourPunCallbacks
{
    // ���� ���� ����
    private string gameVersion = "1.0.0a";

    // ���� ������Ʈ
    public GameObject settingPanel;

    // UI
    private TitleMenuUI ui;

    [Header("���� ��ũ��Ʈ")]
    [SerializeField] private MultiRoomManager roomManager;

    private void Awake()
    {
        ui = GetComponent<TitleMenuUI>();
    }

    /***************************************************************
    * [ ��Ƽ ���� ]
    * 
    * ��ġ����ŷ ���� ���Ӱ� �� ��� ����
    ***************************************************************/

    public void OnClickMultiPlay()
    {
        ConnectServer();
    }

    private void ConnectServer()
    {
        PhotonNetwork.GameVersion = gameVersion;
        PhotonNetwork.ConnectUsingSettings();

        // ���� �˸�â
        ui.SetConnectiongPanel(true);
    }

    public override void OnConnectedToMaster()
    {
        // ���� �˸�â ����
        ui.SetConnectiongPanel(false);

        // ��Ƽ �� ��� ����
        roomManager.OpenMultiRoomList();
    }

    public void openSettingPanel() 
    {
        settingPanel.SetActive(true);
    }

    public void loadLobbyScene()
    {
        SceneManager.LoadScene("LobbyScene");
    }
}
