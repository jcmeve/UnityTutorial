using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
    public GameObject gamemanager;
    public SoundManager soundManager;
    void Awake()
    {
        if (GameManager.instance == null)
            Instantiate(gamemanager);
        //if (SoundManager.instance == null)
        //    Instantiate(soundManager);
    }
}
