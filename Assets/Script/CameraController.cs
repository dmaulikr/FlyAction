using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform target;

    [SerializeField]
    float distanceX, distanceY, distanceZ;

    [SerializeField]
    float slerpTime = 7f;

    Transform tr;



    private void Awake()
    {
        tr = GetComponent<Transform>();
    }

    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    private void FixedUpdate()
    {
        if (target != null)
        {
            tr.position = Vector3.Slerp(tr.position, target.position + (target.forward * distanceZ) + (target.right * distanceX) + (target.up * distanceY), Time.deltaTime * slerpTime);
            tr.rotation = Quaternion.Slerp(tr.rotation,target.rotation,Time.deltaTime * slerpTime);
        }
        
    }



}
