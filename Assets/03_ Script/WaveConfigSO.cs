using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = " Wave Config", fileName = "New Wave Config")]
public class WaveConfigSO : ScriptableObject
{
    [SerializeField] List<GameObject> enemyPrefabs;
    [SerializeField] Transform pathPrefab;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float timeBetweenEnemySpawns = 1f; // 적 생성 시간
    [SerializeField] float spawnTimeVarance = 0f; // 생성 시간에 더하거나 빼는 시간
    [SerializeField] float minimumSpawnTime = 0.2f; // 최소 생성 시간;

    // GameObject 인덱스를 반환
    public int GetEnemyCount() { return enemyPrefabs.Count; }

    // 리스트의 특정 Index 요소를 반환
    public GameObject GetEnemyPrefab(int index)
    {
        return enemyPrefabs[index];
    }

    // waypoint에 시작점을 반환
    public Transform GetStartingWaypoint() { return pathPrefab.GetChild(0); }

    // pathPrefab에 담긴 오브젝트를 List에 담아서 반환
    public List<Transform> GetWaypoints()
    {
        List<Transform> waypoints = new List<Transform>();
        foreach (Transform child in pathPrefab)
        {
            waypoints.Add(child);
        }
        return waypoints;
    }

    // moveSpeed 전역 변수에 담긴 값을 반환
    public float GetMoveSpeed() { return moveSpeed; }

    // 무작위 생성 시간을 반환
    public float GetRandomSpawnTime()
    {
        float spawnTime = Random.Range(timeBetweenEnemySpawns - spawnTimeVarance,
                                        timeBetweenEnemySpawns + spawnTimeVarance);

        return Mathf.Clamp(spawnTime, minimumSpawnTime, float.MaxValue);
    }


}
