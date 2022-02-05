using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    [HideInInspector] public int scoreAmount;
    private float timer;

    void Start()
    {
        if(PlayerPrefs.GetInt("scoreAmount") > 0)
        {
            scoreAmount = PlayerPrefs.GetInt("scoreAmount");
        }
    }

    void Update()
    {
        if (timer <= 1f)
        {
            timer += Time.deltaTime;
        }
        else
        {
            scoreAmount++;
            timer = 0;
        }
    }
}
