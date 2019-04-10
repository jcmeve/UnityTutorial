using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MovingObject
{
    public int playerDamage;
    private Animator animator;
    private Transform target;
    private bool skipMove;
    public AudioClip enemyAttack1;
    public AudioClip enemyAttack2;
    protected override void Start()
    {
        GameManager.instance.AddEnemyToList(this);
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        base.Start();
    }


    void Update()
    {
        
    }
    protected override void AttemptMove<T>(int xdir, int ydir)
    {
        if (skipMove)
        {
            skipMove = false;
            return;
        }
        base.AttemptMove<Player>(xdir, ydir);
        skipMove = true;
    }
    public void MoveEnemy()
    {
        int xdir = 0;
        int ydir = 0;
        if (Mathf.Abs(target.position.x - transform.position.x) < float.Epsilon)
            ydir = target.position.y > transform.position.y ? 1 : -1;
        else
            xdir = target.position.x > transform.position.x ? 1 : -1;
        AttemptMove<Player>(xdir, ydir);
        
    }
    protected override void OnCantMove<T>(T component)
    {
        Player player = component as Player;
        player.LossFood(playerDamage);
        animator.SetTrigger("enemyattack");
        SoundManager.instance.RandomizeSfx(enemyAttack1, enemyAttack2);
    }
}
