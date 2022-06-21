using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonanimation : MonoBehaviour
{
    public GameObject boxSpawner;
    public GameObject spawnpoint;
    public GameObject door;
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
                Instantiate(boxSpawner,spawnpoint.transform.position,spawnpoint.transform.rotation);
            }
        }
        if (other.tag == "plate")
        {
            Animator animation = other.GetComponentInChildren<Animator>();
            animation.SetTrigger("platetrigger");
            door.GetComponent<Renderer>().material.color = Color.green;
           

        }



    }


    private void OnTriggerEnter(Collider other)
    {
        if(door.GetComponent<Renderer>().material.color == Color.green)
        {
            if (other.name == "door")
            {
                Changescene(2);
            }
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "plate")
        {
            Animator animation = other.GetComponentInChildren<Animator>();
            animation.SetTrigger("platestop");
            door.GetComponent<Renderer>().material.color = Color.red;

        }
    }

}
