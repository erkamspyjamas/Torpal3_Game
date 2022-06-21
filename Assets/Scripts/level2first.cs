using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level2first : MonoBehaviour
{

    public GameObject plate2;
    public GameObject plate;
    public GameObject destroydoor;
    

    
    private void OnTriggerStay(Collider other)
    {

        if (other.tag == "plate")
        {
            Animator animation = other.GetComponentInChildren<Animator>();
            animation.SetTrigger("platetrigger");
            Destroy(destroydoor);
        }

        if (other.tag == "plate2")
        {
            Animator animation = other.GetComponentInChildren<Animator>();
            animation.SetTrigger("platetrigger");
            plate.SetActive(true);
        }



    }

    private void OnTriggerExit(Collider other)
    {
        StartCoroutine(platetimer());
        IEnumerator platetimer()
        {
            yield return new WaitForSeconds(2);
            if (other.tag == "plate2")
            {
                Animator animation = other.GetComponentInChildren<Animator>();
                animation.SetTrigger("platestop");
                plate.SetActive(false);
            }
        }

    }
}
