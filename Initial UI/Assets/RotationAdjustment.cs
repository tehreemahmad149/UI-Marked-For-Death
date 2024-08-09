using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationAdjustment : MonoBehaviour
{
    public Transform trasnform;

    // Update is called once per frame
    void LateUpdate()
    {
        transform.localRotation = Quaternion.Euler(0, 90, 0);
    }
}
