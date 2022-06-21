using UnityEngine;


/// Main portal class

public class Portal : MonoBehaviour
{
    public bool isOrange;
    public Portal secondPortal;
    public Camera selfCamera;
    public Texture defaultTexture;
    private bool isReady = false;

    void Update()
    {
        if (isReady)
        {
            // Position
            Vector3 lookerPosition = secondPortal.transform.worldToLocalMatrix.MultiplyPoint3x4(Camera.main.transform.position);
            lookerPosition = new Vector3(-lookerPosition.x, lookerPosition.y, -lookerPosition.z);
            selfCamera.transform.localPosition = lookerPosition;

            // Rotation
            Quaternion difference = transform.rotation * Quaternion.Inverse(secondPortal.transform.rotation * Quaternion.Euler(0, 180, 0));
            selfCamera.transform.rotation = difference * Camera.main.transform.rotation;

            // Clipping
            selfCamera.nearClipPlane = lookerPosition.magnitude;
        }
    }


    /// Check is the second portal is available

    private void CheckSecondPortal()
    {
        if (GameObject.FindGameObjectWithTag(isOrange ? "Blue Portal" : "Orange Portal") != null)
        {
            SetCamera();
            secondPortal.SetCamera();
        }
        else
            GetComponentInChildren<MeshRenderer>().sharedMaterial.mainTexture = defaultTexture;
    }


    /// Set camera properties

    private void SetCamera()
    {
        secondPortal = GameObject.FindGameObjectWithTag(isOrange ? "Blue Portal" : "Orange Portal").GetComponent<Portal>();
        secondPortal.selfCamera.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        GetComponentInChildren<MeshRenderer>().sharedMaterial.mainTexture = secondPortal.selfCamera.targetTexture;

        isReady = true;
    }


    /// Play open portal animation

    public void Open()
    {
        GetComponent<Animator>().SetTrigger("Open");
        CheckSecondPortal();
    }

    /// Play close portal animation

    public void Close()
    {
        tag = "Untagged";
        GetComponent<Animator>().SetTrigger("Close");
        Destroy(gameObject, 0.5f);
    }

 
    /// Trigger teleporting process

    private void OnTriggerStay(Collider other)
    {
        float zPos = transform.worldToLocalMatrix.MultiplyPoint3x4(other.transform.position).z;

        other.GetComponent<Rigidbody>().useGravity = false;

        if (zPos < 0) 
            Teleport(other.transform);
    }


    /// Teleport player to the second portal
    private void Teleport(Transform obj)
    {
        if (obj != null)
        {
            Vector3 localPos = transform.worldToLocalMatrix.MultiplyPoint3x4(obj.position);
            localPos = new Vector3(-localPos.x, localPos.y, -localPos.z);
            obj.position = secondPortal.transform.localToWorldMatrix.MultiplyPoint3x4(localPos);

            Quaternion difference = secondPortal.transform.rotation * Quaternion.Inverse(transform.rotation * Quaternion.Euler(0, 180, 0));
            difference.x = 0;
            difference.z = 0;
            obj.rotation = difference * obj.rotation;

            obj.GetComponent<Rigidbody>().velocity *= -1;
            obj.GetComponent<Rigidbody>().useGravity = true;
        }
    }

    /// Set proper layer

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Beam"))
            Destroy(other.gameObject);

        other.gameObject.layer = LayerMask.NameToLayer("Teleporting");
    }


    /// Set proper layer
    private void OnTriggerExit(Collider other)
    {
        other.GetComponent<Rigidbody>().useGravity = true;
        other.gameObject.layer = LayerMask.NameToLayer("Player");
    }
}
