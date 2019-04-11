using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GunController gunController;
    [Header("피격 이펙트")]
    [SerializeField] GameObject go_RicochetEffect;
    [Header("총알 데미지")]
    [SerializeField] int damage;
    [Header("피격 효과음")]
    [SerializeField] string soundRicochet;

    private void Awake() {
        gunController = FindObjectOfType<GunController>();
    }
    private void OnCollisionEnter(Collision collision) {
        ContactPoint contactPoint = collision.contacts[0];
        var clone = Instantiate(go_RicochetEffect, contactPoint.point, Quaternion.LookRotation(contactPoint.normal));
        Destroy(clone,0.5f);
        SoundManager.instance.PlaySE(soundRicochet);
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        gunController.PushToBullets(gameObject);
        //Destroy(gameObject);
    }
}
