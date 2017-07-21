using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public class PlayerController : MonoBehaviour
{

    Transform tr;
    Rigidbody ri;

    [SerializeField]
    float moveSpeed = 0f, accelerSpeed = 4f, maxMoveSpeed = 7f, rotateSpeed = 5f, rotateMax = 40f;

    [SerializeField]
    Quaternion currentRotate;

    [SerializeField]
    float h, v;

    [SerializeField]
    float slerpTime = 10f;

    [SerializeField]
    Vector3 dir;

    Vector3 oldPos, currentPos;

    [SerializeField]
    Transform renderModel;

    void Awake()
    {
        tr = GetComponent<Transform>();
        ri = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //좌 우
        h = Input.GetAxis("Horizontal");
        //위 아래
        v = Input.GetAxis("Vertical");

        // 앞뒤 이동
        if (Input.GetKey(KeyCode.Q))
        {
            moveSpeed += Time.deltaTime * accelerSpeed;
            moveSpeed = Mathf.Clamp(moveSpeed, -maxMoveSpeed, maxMoveSpeed);
        }

        else if (Input.GetKey(KeyCode.E))
        {
            moveSpeed -= Time.deltaTime * accelerSpeed;
            moveSpeed = Mathf.Clamp(moveSpeed, -maxMoveSpeed, maxMoveSpeed);
        }
        else
        {
            moveSpeed = Mathf.Lerp(moveSpeed, 0, Time.deltaTime * 5f);
        }


    }

    void FixedUpdate()
    {
        dir = new Vector3(-v, h, 0);
        currentRotate = tr.rotation;

        renderModel.Rotate(Vector3.back * h * 3f * rotateSpeed * Time.deltaTime);

        if (h == 0)
        {
            renderModel.localRotation = Quaternion.Slerp(renderModel.localRotation, Quaternion.identity, Time.deltaTime * slerpTime);
        }
        else if (Mathf.Abs(renderModel.localRotation.z) > rotateMax)
        {
            Quaternion renderRotate = renderModel.localRotation;
            if (renderRotate.z < 0)
                renderRotate.z = -rotateMax;
            else
                renderRotate.z = rotateMax;

            renderModel.localRotation = renderRotate;
        }

        //if (v == 0)
        //{
        //    ResetRotate();
        //}

        tr.Rotate(dir * rotateSpeed * Time.deltaTime);
        
        ri.velocity = tr.forward * moveSpeed;

    }


    public void ResetRotate()
    {
        Quaternion rotSlerp = tr.rotation;
        //rotSlerp.x = 0;
        rotSlerp.z = 0;

        tr.rotation = Quaternion.Slerp(tr.rotation,rotSlerp,Time.deltaTime * slerpTime);
    }
}
