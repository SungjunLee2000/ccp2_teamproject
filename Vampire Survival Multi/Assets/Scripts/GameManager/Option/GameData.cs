using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameData : ScriptableObject
{
    // ���� ���� ��ġ
    private const string FILE_DIRECTORY = "Assets/Resources/Option";
    private const string FILE_PATH = "Assets/Resources/Option/GameData.asset";

    private static GameData _instance;
    public static GameData Instance
    {
        get
        {
            if (_instance != null) return _instance;

            _instance = Resources.Load<GameData>("Option/GameData");

#if UNITY_EDITOR
            if (_instance == null)
            {
                // ���� ��ΰ� ���� ��� ���� ����
                if (!AssetDatabase.IsValidFolder(FILE_DIRECTORY))
                {
                    string[] folders = FILE_DIRECTORY.Split('/');
                    string currentPath = folders[0];

                    for (int i = 1; i < folders.Length; i++)
                    {
                        if (!AssetDatabase.IsValidFolder(currentPath + "/" + folders[i]))
                        {
                            AssetDatabase.CreateFolder(currentPath, folders[i]);
                        }

                        currentPath += "/" + folders[i];
                    }
                }

                // Resource.Load�� �������� ���
                _instance = AssetDatabase.LoadAssetAtPath<GameData>(FILE_PATH);

                if (_instance == null)
                {
                    _instance = CreateInstance<GameData>();
                    AssetDatabase.CreateAsset(_instance, FILE_PATH);
                }
            }
#endif
            return _instance;
        }
    }

    [Header("���� �÷��̾� ����")]
    private List<GameObject> _playerList;
    public List<GameObject> PlayerList
    {
        get { return _playerList; }
    }

    [ReadOnly]
    [SerializeField]
    private List<GameObject> _deadPlayerList;
    public List<GameObject> DeadPlayerList
    {
        get { return _deadPlayerList; }
    }

    public bool IsAllDead
    {
        get
        {
            int playerCount = _playerList.Count;
            int deadPlayerCount = _deadPlayerList.Count;

            return playerCount == deadPlayerCount;
        }
    }

    [Header("���� ����")]
    [SerializeField]
    private int _exp;
    public int Exp
    {
        get { return _exp; }
    }

    [SerializeField]
    private int _requireExp;
    public int RequireExp
    {
        get { return _requireExp; }
    }

    [SerializeField]
    private int _level;
    public int Level
    {
        get { return _level; }
    }

    [Header("�̺�Ʈ")]
    [SerializeField] private GameEvent expEvent;
    [SerializeField] private GameEvent levelUpEvent;

    public void InitData(List<GameObject> players)
    {
        InitPlayer(players);
        InitLevel();
    }

    private void InitPlayer(List<GameObject> players)
    {
        _playerList = new List<GameObject>(players);
        _deadPlayerList = new List<GameObject>();
    }

    private void InitLevel()
    {
        // Init Level
        int initLevel = 1;

        _level = initLevel;
        _exp = 0;
        _requireExp = LevelResource.Instance.GetRequireExp(initLevel);

        // ����ġ ���� �̺�Ʈ
        expEvent.NotifyUpdate();
    }

    public void AddExp(int exp)
    {
        if (_requireExp < int.MaxValue)
        {
            _exp += exp;

            // ���� �� �������� �ʿ��� ����ġ�� ����
            while (_exp >= _requireExp)
            {
                // ������
                _level++;
                _exp -= _requireExp;

                // �ʿ� ����ġ�� ����
                _requireExp = LevelResource.Instance.GetRequireExp(_level);

                // ������ �̺�Ʈ
                levelUpEvent.NotifyUpdate();
            }

            // ����ġ ���� �̺�Ʈ
            expEvent.NotifyUpdate();
        }
    }

    public void AddDeadList(GameObject player)
    {
        _deadPlayerList.Add(player);
    }

    public void ReviveAllPlayer()
    {
        _deadPlayerList.Clear();
    }
}