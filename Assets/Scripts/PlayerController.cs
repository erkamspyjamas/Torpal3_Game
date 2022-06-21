using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator animator;
    CharacterController controller;
    Vector3 velocity;
    bool isGrounded;

    public Transform ground;
    public float distance = 0.3f;

    public float speed;
    public float jumpHeight;
    public float gravity;

    public float originalHeight;
    public float crouchHeight;

    public LayerMask mask;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

    }

    IEnumerator crouch2standfalse()
    {
        yield return new WaitForSeconds(0.3f); 
        animator.SetBool("crouch2stand", false);
    }

    IEnumerator stand2crouchfalse()
    {
        yield return new WaitForSeconds(0.3f);
        animator.SetBool("stand2crouch", false);
    }


    private void Update()
    {
        
        #region Movement
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 move = transform.right * horizontal + transform.forward * vertical;
        controller.Move(move * speed * Time.deltaTime);
        #region walk animation
        if (Input.GetKeyDown(KeyCode.W))
        {
            animator.SetBool("walking", true);
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            animator.SetBool("walking", false);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            animator.SetBool("walkright",true);
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            animator.SetBool("walkright",false);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            animator.SetBool("walkleft", true);
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            animator.SetBool("walkleft", false);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            animator.SetBool("walkback", true);
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            animator.SetBool("walkback", false);
        }
        #endregion

        #region run animation
        if (Input.GetKeyDown(KeyCode.D)&& Input.GetKeyDown(KeyCode.LeftShift))
        {
            animator.SetBool("runright", true);
            animator.SetBool("walkright",false);
        }
        else if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.LeftShift))
        {
            animator.SetBool("runright", false);

        }

        if (Input.GetKeyDown(KeyCode.A) && Input.GetKeyDown(KeyCode.LeftShift))
        {
            animator.SetBool("runleft", true);
            animator.SetBool("walkleft", false);

        }
        else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftShift))
        {
            animator.SetBool("runleft", false);
        }

        if (Input.GetKeyDown(KeyCode.S) && Input.GetKeyDown(KeyCode.LeftShift))
        {
            animator.SetBool("runback", true);
            animator.SetBool("walkback", false);

        }
        else if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.LeftShift))
        {
            animator.SetBool("runback", false);
        }

        #endregion

        #region crouch walk animation
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.W))
        {
            animator.SetBool("crouchwalk", true);
            
           
            animator.SetBool("walking", false) ;
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl) || Input.GetKeyUp(KeyCode.W))
        {
            animator.SetBool("crouchwalk", false);
        }

        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.D))
        {
            animator.SetBool("crouchright", true);


            animator.SetBool("walking", false);
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl) || Input.GetKeyUp(KeyCode.D))
        {
            animator.SetBool("crouchright", false);
        }

        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.A))
        {
            animator.SetBool("crouchleft", true);


            animator.SetBool("walking", false);
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl) || Input.GetKeyUp(KeyCode.A))
        {
            animator.SetBool("crouchleft", false);
        }

        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.S))
        {
            animator.SetBool("crouchback", true);


            animator.SetBool("walking", false);
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl) || Input.GetKeyUp(KeyCode.S))
        {
            animator.SetBool("crouchback", false);
        }
        #endregion


        #endregion

        #region Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) 
        {
            velocity.y += Mathf.Sqrt(jumpHeight*-3.0f*gravity);

        }

        if (Input.GetKey(KeyCode.Space))
        {
            animator.SetBool("jumping", true);
        }
        else if (Input.GetKey(KeyCode.Space) == false)
        {
            animator.SetBool("jumping", false);
        }

        #endregion

        #region Gravity
        isGrounded = Physics.CheckSphere(ground.position, distance, mask);
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity*Time.deltaTime);
        #endregion

        #region Basic Crouch

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            controller.height = crouchHeight;
            speed = 2.0f;
            jumpHeight = 0;
            gravity = -1500;
            animator.SetBool("stand2crouch", true);
            StartCoroutine(stand2crouchfalse());




        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            controller.height = originalHeight;
            speed = 5.0f;
            jumpHeight = 1;
            gravity = -9.8f;
            animator.SetBool("stand2crouch", false);
            animator.SetBool("crouch2stand", true);
            StartCoroutine(crouch2standfalse());


        }

        #endregion

        #region Basic Running

        if (Input.GetKeyDown(KeyCode.LeftShift)&&Input.GetKeyDown(KeyCode.W))
        {
            speed = 7.0f;
            animator.SetBool("running", true);
            animator.SetBool("walking", false);

        }
        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.W))
        {
            speed = 5.0f;
            animator.SetBool("running", false);

        }

        #endregion

 


        #region Shooting
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            animator.SetBool("fire",true);
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            animator.SetBool("fire", false);
        }
        #endregion




    }



}
