using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAnimation : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 20f;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(Vector3.forward * rotateSpeed * Time.fixedDeltaTime);
    }
}
