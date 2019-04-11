using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowPlayer : MonoBehaviour
{
    [Header("따라갈 대상 지정")]
    [SerializeField] protected Transform TfPlayer;

    [Header("따라갈 속도 지정")][Range(0,1)]
    [SerializeField]protected float moveSpeed;

    protected Vector3  currentForce;
    // Start is called before the first frame update
    void Start()
    {
        currentForce = TfPlayer.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, TfPlayer.position-currentForce, moveSpeed);
    }
}
