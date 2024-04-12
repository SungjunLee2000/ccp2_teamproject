﻿using UnityEngine;
public enum PanelState
{
    InPlayer,
    InOther,
    Nobody
}
public class PlayerPanelUI : MonoBehaviour
{
    [Header("참조 오브젝트")]
    [SerializeField] private GameObject moveButton;
    [SerializeField] private GameObject addButton;
    [SerializeField] private GameObject removeButton;
    [Space]
    [SerializeField] private GameObject playerIcon;
    [SerializeField] private GameObject otherIcon;

    // 패널 상태
    private PanelState _currentState = PanelState.Nobody;
    public PanelState State
    {
        get { return _currentState; }
    }

    public void OnPanelEnter()
    {
        _currentState = PanelState.InPlayer;

        UpdateInPlayerUI();
    }

    public void OnPanelLeave()
    {
        if (_currentState == PanelState.InPlayer)
        {
            _currentState = PanelState.Nobody;

            UpdateInNobodyUI();
        }
    }

    public void OnAddPlayer()
    {
        UpdateInOtherUI();
    }

    public void OnRemovePlayer()
    {
        UpdateInNobodyUI();
    }

    private void UpdateInPlayerUI()
    {
        // Set Icon
        playerIcon.SetActive(true);
        otherIcon.SetActive(false);

        // Set Button
        moveButton.SetActive(false);
        addButton.SetActive(false);
        removeButton.SetActive(false);
    }

    private void UpdateInOtherUI()
    {
        // Set Icon
        playerIcon.SetActive(false);
        otherIcon.SetActive(true);

        // Set Button
        moveButton.SetActive(false);
        addButton.SetActive(false);
        removeButton.SetActive(true);
    }

    private void UpdateInNobodyUI()
    {
        // Set Icon
        playerIcon.SetActive(false);
        otherIcon.SetActive(false);

        // Set Button
        moveButton.SetActive(true);
        addButton.SetActive(true);
        removeButton.SetActive(false);
    }
}