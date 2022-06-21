using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{

    public PlayerController playerScript;

    [Range(50, 500)]
    public float sens;

    public Transform body;

    float xRot = 0f;

    public Transform leaner;
    public float zRot;
    bool isRotating;
    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }

    private void Update()
    {
        #region Camera Movement
        float rotX = Input.GetAxisRaw("Mouse X") * sens * Time.deltaTime;
        float rotY = Input.GetAxisRaw("Mouse Y") * sens * Time.deltaTime;

        xRot -= rotY;
        xRot = Mathf.Clamp(xRot, -80f, 80f);


        transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);


        body.Rotate(Vector3.up * rotX);
        #endregion

    }


}
