﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public AudioClip chopSound1;
    public AudioClip chopSound2;
    public Sprite dmgSprite;
    public int hp = 4;

    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void DamegeWall(int loss)
    {
        SoundManager.instance.RandomizeSfx(chopSound1, chopSound2);
        spriteRenderer.sprite = dmgSprite;
        hp-=loss;
        if (hp <= 0)
        {
            gameObject.SetActive(false);
        }
    }

}
