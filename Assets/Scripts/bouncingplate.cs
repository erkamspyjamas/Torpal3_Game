using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class bouncingplate : MonoBehaviour
{
    public GameObject breakingdoor1;
    public GameObject breakingdoor2;
    public GameObject breakingdoor3;
    public GameObject exitGate;

    public void Changescene(int sceneid)
    {
        SceneManager.LoadScene(sceneid);
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "bplate")
        {
            Animator animation = other.GetComponentInChildren<Animator>();
            animation.SetTrigger("bouncing");
        }

        if (other.tag == "button2")
        {
            Animator animation = other.GetComponentInChildren<Animator>();
            animation.SetTrigger("pressbutton");
            breakingdoor3.SetActive(false);

        }
        if (exitGate.GetComponent<Renderer>().material.color == Color.green)
        {
            if (other.name == "exitGate")
            {
                Changescene(4);
                Cursor.lockState = CursorLockMode.None;

            }
        }


    }

    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "plate")
        {
            Animator animation = other.GetComponentInChildren<Animator>();
            animation.SetTrigger("platetrigger");
            breakingdoor1.SetActive(false);
            
        }

        if (other.tag == "button")
        {
            Animator animation = other.GetComponentInChildren<Animator>();
            if (Input.GetKeyDown(KeyCode.E))
            {
                animation.SetTrigger("pressbutton");
                breakingdoor2.SetActive(false);
            }
        }
        if (other.tag == "e4plate")
        {
            Animator animation = other.GetComponentInChildren<Animator>();
            animation.SetTrigger("platetrigger");
            exitGate.GetComponent<Renderer>().material.color = Color.green;
        }


    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "plate")
        {
            Animator animation = other.GetComponentInChildren<Animator>();
            animation.SetTrigger("platestop");
            breakingdoor1.SetActive(true);

        }
        if (other.tag == "e4plate")
        {
            Animator animation = other.GetComponentInChildren<Animator>();
            animation.SetTrigger("platestop");
            exitGate.GetComponent<Renderer>().material.color = Color.red;
            
        }
    }



}
