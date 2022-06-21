using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class level2palet : MonoBehaviour
{
    public GameObject door;
    public GameObject plate;


    public void Changescene(int sceneid)
    {
        SceneManager.LoadScene(sceneid);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "button")
        {
            Animator animation = other.GetComponentInChildren<Animator>();
            if (Input.GetKeyDown(KeyCode.E))
            {
                animation.SetTrigger("pressbutton");
                plate.SetActive(true);
            }
        }
        if (other.tag == "lastplate")
        {
            Animator animation = other.GetComponentInChildren<Animator>();
            animation.SetTrigger("platetrigger");
            door.GetComponent<Renderer>().material.color = Color.green;


        }



    }


    private void OnTriggerEnter(Collider other)
    {
        if (door.GetComponent<Renderer>().material.color == Color.green)
        {
            if (other.tag == "door")
            {
                Changescene(3);
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "lastplate")
        {
            Animator animation = other.GetComponentInChildren<Animator>();
            animation.SetTrigger("platestop");
            door.GetComponent<Renderer>().material.color = Color.red;

        }
    }
}
