using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickup : MonoBehaviour
{
    [Header("Pickup Settings")]
    [SerializeField] Transform holdArea;
    private GameObject heldobj;
    private Rigidbody heldobjRB;
    [Header("Physics Parameters")]
    [SerializeField] private float pickupRange = 5.0f;
    [SerializeField] private float pickupForce = 150.0f;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (heldobj == null)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickupRange))
                {
                    PickupObject(hit.transform.gameObject);
                }
            }
            else
            {
                DropObject();
            }
        }
        if (heldobj != null)
        {
            MoveObject();
        }
    }
      


    void MoveObject()
    {
        if (Vector3.Distance(heldobj.transform.position, holdArea.position) > 0.1f)
        {
            Vector3 moveDirection = (holdArea.position - heldobj.transform.position);
            heldobjRB.AddForce(moveDirection * pickupForce);
        } 
    }

    void PickupObject(GameObject pickObj)
    {
        if (pickObj.GetComponent<Rigidbody>())
        {
            heldobjRB = pickObj.GetComponent<Rigidbody>();
            heldobjRB.useGravity = false;
            heldobjRB.drag = 10;
            heldobjRB.constraints = RigidbodyConstraints.FreezeRotation;
            heldobjRB.transform.parent = holdArea;
            heldobj = pickObj;
        }
    }
    void DropObject()
    {            
        heldobjRB.useGravity = true;            
        heldobjRB.drag = 1;    
        heldobjRB.constraints = RigidbodyConstraints.None;    
        heldobjRB.transform.parent = null;    
        heldobj = null;     
    }
}



