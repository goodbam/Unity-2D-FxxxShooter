using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f; // 발사체 속도
    [SerializeField] float projectileLifetime = 5f; // 발사체 지속 시간
    [SerializeField] float baseFiringRate = 0.2f; // 발사체 발사 간격

    [Header("AI")]
    [SerializeField] bool useAI; // 적의 자동 공격 유무를 확인
    [SerializeField] float firingRateVariance = 0f; // 발사 속도 간격
    [SerializeField] float minimumFirngRate = 0.1f; // 최소 발사율

    [HideInInspector] public bool isFiring;

    Coroutine firingCoroutine;

    void Start()
    {
        if (useAI)
        {
            isFiring = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        Fire();
    }

    void Fire()
    {
        if (isFiring && firingCoroutine == null) // 조이스틱 눌러져 있는 상태라면 
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        else if (!isFiring && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject instance = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

            Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = transform.up * projectileSpeed;
            }
            Destroy(instance, projectileLifetime);

            float timeToNextProjectile = Random.Range(baseFiringRate - firingRateVariance,
                                                        baseFiringRate + firingRateVariance);
            timeToNextProjectile = Mathf.Clamp(timeToNextProjectile, minimumFirngRate, float.MaxValue);

            yield return new WaitForSeconds(timeToNextProjectile);
        }
    }

}
