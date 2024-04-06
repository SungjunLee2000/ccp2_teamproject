﻿using UnityEngine;

public class PlayerTracker : MonoBehaviour
{
    public Transform player;

    public float speed;

    private Vector2 minPos;
    private Vector2 maxPos;

    private void Start()
    {
        // 해당 이벤트의 위치를 캐릭터에 고정
        transform.position = player.position;

        // 카메라가 이동할 맵의 범위를 파악
        mapAreaSet();
    }

    private void mapAreaSet()
    {
        MapData mapData = MapData.Instance;

        // 카메라 사이즈 계산
        Vector2 cameraSize = new Vector2(2 * Camera.main.orthographicSize * Camera.main.aspect, 2 * Camera.main.orthographicSize);

        // 카메라의 끝과 맵의 끝이 닿는 범위 계산
        Vector2 cameraMoveSize = (mapData.Size - cameraSize) / 2;

        minPos = mapData.Pivot - cameraMoveSize;
        maxPos = mapData.Pivot + cameraMoveSize;
    }

    void LateUpdate()
    {
        // 범위 밖으로 나가지 않도록 해당 이벤트의 위치를 조정
        float blockX = Mathf.Clamp(player.position.x, minPos.x, maxPos.x);
        float blockY = Mathf.Clamp(player.position.y, minPos.y, maxPos.y);

        transform.position = Vector3.Lerp(transform.position, new Vector3(blockX, blockY, -1), speed);
    }
}