using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerControls : MonoBehaviour
{
    public float speed;
    public float jumpSpeed;
    public float rotateSpeed;
    public float gravity;

    public GameObject portalGun;

    private Vector3 moveDirection = Vector3.zero;

    public Transform playerCamera;

    private Rigidbody body;

    private bool isJumped;
    public float fallMultiplier = 2.5f;
    public float jumpMultiplier = 2f;

    private float xRotation = 0;
   
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        playerCamera = GetComponentInChildren<Camera>().transform;
        body = GetComponent<Rigidbody>();

    }

    void Update()
    {

        float mouseX = Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * rotateSpeed * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        transform.Rotate(Vector3.up * mouseX);

        moveDirection = new Vector3(Input.GetAxis("Horizontal") * speed, moveDirection.y, Input.GetAxis("Vertical") * speed);
        moveDirection = transform.TransformDirection(moveDirection);

        transform.position += moveDirection * Time.deltaTime;

        if (Input.GetButton("Jump") && !isJumped)
        {
            body.velocity += new Vector3(0, jumpSpeed, 0);
            isJumped = true;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 15;
        }
        else
        {
            speed = 10;
        }


        // Make jumps more realistic
        if (body.velocity.y < 0 && isJumped)
            body.velocity += Vector3.up * Physics.gravity.y * fallMultiplier * Time.deltaTime;
        else if (body.velocity.y > 0 && !Input.GetButton("Jump"))
            body.velocity += Vector3.up * Physics.gravity.y * jumpMultiplier * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        isJumped = false;
    }
}
