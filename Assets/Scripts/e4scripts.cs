using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class e4scripts : MonoBehaviour
{
    public GameObject e4b1;
    public GameObject e4b2;
    public GameObject e4b3;
    public GameObject e4b4;
    public GameObject e4plate;


    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "e4b1" && Input.GetKeyDown(KeyCode.E))
        {
            Animator animation = other.GetComponentInChildren<Animator>();
            animation.SetTrigger("pressbutton");
            e4b2.SetActive(true);

        }
        if (other.tag == "e4b2" && Input.GetKeyDown(KeyCode.E))
        {
            Animator animation = other.GetComponentInChildren<Animator>();
            animation.SetTrigger("pressbutton");
            e4b3.SetActive(true);

        }
        if (other.tag == "e4b3" && Input.GetKeyDown(KeyCode.E))
        {
            Animator animation = other.GetComponentInChildren<Animator>();
            animation.SetTrigger("pressbutton");
            e4b4.SetActive(true);

        }
        if(other.tag == "e4b4" && Input.GetKeyDown(KeyCode.E))
        {
            Animator animation = other.GetComponentInChildren<Animator>();
            animation.SetTrigger("pressbutton");
            e4plate.SetActive(true);
        }

    }
    


}
