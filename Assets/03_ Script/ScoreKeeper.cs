using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    int experience;

    public int GetExperience() { return experience; }

    public void ModifyExperience(int value)
    {
        experience += value;
        Mathf.Clamp(experience, 0, 110);
        Debug.Log("experience : " + experience);
    }

    public void ResetExperience()
    {
        experience = 0;
    }
}
