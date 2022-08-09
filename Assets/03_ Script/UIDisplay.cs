using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDisplay : MonoBehaviour
{
    [Header("Experience")]
    [SerializeField] Slider expSlider;
    [SerializeField] ScoreKeeper scoreKeeper;

    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    void Start()
    {
        expSlider.maxValue = 100;
    }

    void Update()
    {
        expSlider.value = scoreKeeper.GetExperience();
    }
}
