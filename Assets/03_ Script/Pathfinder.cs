using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    EnemySpawner enemySpawner;
    WaveConfigSO waveConfig;
    List<Transform> waypoints;
    int waypointIndex = 0;

    void Awake()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    void Start()
    {
        waveConfig = enemySpawner.GetCurrentWave();
        waypoints = waveConfig.GetWaypoints(); // 모든 waypoint 오브젝트를 리스트에 담는다.
        transform.position = waypoints[waypointIndex].position; // "Pathfinder" 스크립트를 가지고 있는 객체의 위치를 waypoint 오브젝트 위치와 일치 시킨다.
    }

    void Update()
    {
        FollowPath();
    }

    private void FollowPath()
    {
        if (waypointIndex < waypoints.Count)
        {
            Vector3 targetPosition = waypoints[waypointIndex].position; // 다음 이동할 waypoint의 위치를 가져온다.
            float delta = waveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, delta); // 현재 위치에서 다음 waypoint 위치로 이동한다.
            if (transform.position == targetPosition) // waypoint에 도착했다면
            {
                waypointIndex++; // waypointIndex + 1
            }
        }
        else // 마지막 waypoint에 도착하면
        {
            Destroy(gameObject); // 자기자신 파괴
        }
    }
}
