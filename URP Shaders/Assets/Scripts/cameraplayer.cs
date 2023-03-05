using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraplayer : MonoBehaviour
{
    public float mouseSensitivity = 100f;

    public Transform playerB;

    float XRot = 0f;

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        XRot -= mouseY;
        XRot = Mathf.Clamp(XRot, -90f, 90f);

        transform.localRotation = Quaternion.Euler(XRot, 0f, 0f);
        playerB.Rotate(Vector3.up * mouseX);

    }

}
