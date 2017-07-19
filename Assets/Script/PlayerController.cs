using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public class PlayerController : MonoBehaviour
{

    Transform tr;
    Rigidbody ri;

    [SerializeField]
    float moveSpeed = 5f, rotateSpeed =5f,  curveMax = 80f;

    [SerializeField]
    float h, v;

    void Awake()
    {
        tr = GetComponent<Transform>();
        ri = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
    }

    void FixedUpdate() {

        tr.Rotate(Vector3.left * v * curveMax);
        tr.Rotate(Vector3.up * h*curveMax);
        //tr.rotation = Quaternion.Slerp(tr.rotation, Quaternion.Euler(tr.rotation.x + v * -curveMax, tr.rotation.y + h * curveMax, 0),Time.deltaTime * rotateSpeed);
        ri.AddForce(tr.forward * moveSpeed);
    }


}
