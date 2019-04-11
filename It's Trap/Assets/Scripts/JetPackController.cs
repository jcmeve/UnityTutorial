using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetPackController : FlowPlayer
{
    // Start is called before the first frame update
    [Header("Jet엔진 회전속도")][Range(0, 1)]
    [SerializeField] float spinSpeed;
    void Start()
    {
        currentForce = TfPlayer.position - transform.position;     
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetAxisRaw("Horizontal") > 0) {
            transform.position = Vector3.Lerp(transform.position, TfPlayer.position - currentForce, moveSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), spinSpeed);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0) {
            transform.position = Vector3.Lerp(transform.position, TfPlayer.position -
                new Vector3(currentForce.x, currentForce.y, -currentForce.z), moveSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(-100, 0, 0), spinSpeed);
        }
        else if (Input.GetAxisRaw("Horizontal") == 0) {
            transform.position = Vector3.Lerp(transform.position, TfPlayer.position -
               new Vector3(currentForce.x, currentForce.y, 0), moveSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(-56, 0, 0), spinSpeed);
        }
    }
}
