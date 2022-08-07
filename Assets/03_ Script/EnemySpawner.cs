using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfigSO> waveConfigs; // 랜덤 wave를 사용하기 위한 그릇을 생성
    [SerializeField] float timeBetweenWaves = 0f;
    [SerializeField] bool isLooping;
    WaveConfigSO currentWave;


    void Start()
    {
        StartCoroutine(SpawnEnemiesWaves());
    }

    // 현재 사용중인 WaveConfigSO 반환
    public WaveConfigSO GetCurrentWave() { return currentWave; }


    // Enemy, Wave를 각각 인스턴스화해서 생성함
    IEnumerator SpawnEnemiesWaves()
    {
        do
        {
            foreach (WaveConfigSO wave in waveConfigs)
            {
                currentWave = wave;
                for (int i = 0; i < currentWave.GetEnemyCount(); i++)
                {
                    Instantiate(currentWave.GetEnemyPrefab(0),
                                currentWave.GetStartingWaypoint().position,
                                Quaternion.Euler(0, 0, 180),
                                transform);
                    yield return new WaitForSeconds(currentWave.GetRandomSpawnTime());
                }
                yield return new WaitForSeconds(timeBetweenWaves);
            }

        } while (isLooping);
    }

}
