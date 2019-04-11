using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("따라갈 플레이어")]
    [SerializeField] Transform tf_Player;
    [Header("따라갈 속도")]
    [Range(0,1)][SerializeField] float chaseSpeed;

    float camNormalXPos;
    [Header("부스터시 멀어질 거리")]
    [SerializeField] float camJetXPos;
    float camCurrentxPos;
    // Start is called before the first frame update
    PlayerController thePlayer;
    void Start()
    {
        camNormalXPos = transform.position.x;
        camCurrentxPos = camNormalXPos;
        thePlayer = tf_Player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update() {
        if (thePlayer.isJet) {
            camCurrentxPos = camJetXPos;
        }
        else {
            camCurrentxPos = camNormalXPos;
        }
        Vector3 movePos = Vector3.Lerp(transform.position, new  Vector3(camCurrentxPos,tf_Player.position.y,tf_Player.position.z), chaseSpeed);
        //        Vector3 movePos = Vector3.Lerp(transform.position, tf_Player.position, chaseSpeed);
        transform.position = movePos;//new Vector3(camCurrentxPos, movePos.y, movePos.z);

    }
}
