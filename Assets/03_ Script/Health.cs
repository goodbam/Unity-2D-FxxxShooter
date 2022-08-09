using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] bool isPlayer;
    [SerializeField] int health = 50;
    [SerializeField] ParticleSystem hitEffect;

    [SerializeField] int experience = 10;

    [SerializeField] bool applyCameraShake;

    CameraShake cameraShake;

    AudioPlayer audioPlayer;
    ScoreKeeper scoreKeeper;
    LevelManager levelManager;

    void Awake()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    public int GetHealth() { return health; } // 현재 에너지

    void OnTriggerEnter2D(Collider2D other)
    {
        // 부딪힌 오브젝트의 DamageDealer를 가져온다.
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();

        // DamageDealer가 있다면 
        if (damageDealer != null)
        {
            TakeDamage(damageDealer.GetDamage()); // 부딪힌 오브젝트의 데미지를 가져온다.
            PlayHitEffect();
            audioPlayer.PlayDamageClip();
            ShakeCamera();
            damageDealer.Hit(); // 부딪힌 오브젝트를 삭제한다.
        }
    }

    // 부딪힌 오브젝트의 데미지 만큼 나의 에너지를 감소 시키고 에너지가 0이 된다면 오브젝트를 제거한다.
    private void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (!isPlayer)
        {
            scoreKeeper.ModifyExperience(experience);
        }
        else
        {
            levelManager.LoadGameOver();
        }


        Destroy(gameObject);
    }

    void PlayHitEffect()
    {

        if (hitEffect != null)
        {
            ParticleSystem instance = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax); // 임펙트는 실행 후 삭제
        }
        else
        {
            Debug.Log("There is no particle object.");
        }
    }

    void ShakeCamera()
    {
        if (cameraShake != null && applyCameraShake)
        {
            cameraShake.Play();
        }
    }
}