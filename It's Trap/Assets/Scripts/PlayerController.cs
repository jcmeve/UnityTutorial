using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [Header("속도 관련 변수")]
    [SerializeField]float moveSpeed;
    [SerializeField] float jetPackSpeed;
    Rigidbody myRigid;

    public bool isJet { get; private set; }

    [Header("파티클시스템(부스터)")]
    [SerializeField] ParticleSystem ps_LeftEngine;
    [SerializeField] ParticleSystem ps_RigitEngine;

    AudioSource audioSource;

    JetEngineFuelManager theFuel;



    void Start() {
        myRigid = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        isJet = false;
        theFuel = FindObjectOfType<JetEngineFuelManager>();
    }

    // Update is called once per frame
    void Update() {
        TryMove();
        TryJet();

        
    }
    void TryMove() {
        float LR = Input.GetAxisRaw("Horizontal");
        if (LR != 0) {//A,<- = -1  이고, D,-> = 1
            Vector3 moveDir = new Vector3(0, 0, LR);
            myRigid.AddForce(moveDir * moveSpeed);
        }
    }
    void TryJet() {
        if (Input.GetKey(KeyCode.Space)&&theFuel.isFuel) {
            if (!isJet) {
                audioSource.Play();
                ps_LeftEngine.Play();
                ps_RigitEngine.Play();
                isJet = true;
            }
            myRigid.AddForce(Vector3.up * jetPackSpeed);
        }
        else {
            if (isJet) {
                audioSource.Stop();
                ps_LeftEngine.Stop();
                ps_RigitEngine.Stop();
                isJet = false;
            }
            myRigid.AddForce(Vector3.down * jetPackSpeed);
        }
    }
}
