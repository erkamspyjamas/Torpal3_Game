using UnityEngine;

public class PortalGun : MonoBehaviour
{
    public GameObject orangePortalPrefab;
    public GameObject bluePortalPrefab;

    public Material orangeColorMat;
    public Material blueColorMat;

    public AudioClip shootClip;
    public AudioClip resetClip;

    public GameObject beamPrefab;

    public LayerMask portalLayers;

    public bool isOrangeAvailable = true;
    public bool isBlueAvailable = true;

    public MeshRenderer gunColorfulPart;

    public Animator animator;

    private AudioSource audioSource;
 

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            animator.SetTrigger("Reset");
            audioSource.PlayOneShot(resetClip);
            RemovePortals(true, true);
        }

        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f));
            RaycastHit hit;
            bool isPortalAvailable = false;

            if (Physics.Raycast(ray, out hit))
            {
                if (portalLayers == (portalLayers | (1 << hit.collider.gameObject.layer)))
                    isPortalAvailable = true;
                    
                animator.SetTrigger("Shoot");
                audioSource.PlayOneShot(shootClip);

                if (Input.GetMouseButtonDown(0) && isBlueAvailable)
                {
                    if (isPortalAvailable)
                    {
                        RemovePortals(true, false);
                        CreatePortal(bluePortalPrefab, hit.point, hit.normal);

                        gunColorfulPart.material = blueColorMat;
                    }

                    CreateBeam(hit.point, blueColorMat);
                }
                else if(Input.GetMouseButtonDown(1) && isOrangeAvailable)
                {
                    if (isPortalAvailable)
                    {
                        RemovePortals(false, true);
                        CreatePortal(orangePortalPrefab, hit.point, hit.normal);

                        gunColorfulPart.material = orangeColorMat;
                    }

                    CreateBeam(hit.point, orangeColorMat);
                }
            }
        }
    }
    

    /// Create portal at the ray hit point
    /// <param name="prefab"></param>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    private void CreatePortal(GameObject prefab, Vector3 position, Vector3 rotation)
    {
        var portal = Instantiate(prefab, position, Quaternion.LookRotation(rotation));
        portal.transform.position += portal.transform.forward * 0.6f;
        portal.GetComponent<Portal>().Open();
    }

    /// Remove all portals from the level
    /// <param name="blue"></param>
    /// <param name="orange"></param>
    private void RemovePortals(bool blue, bool orange)
    {
        if (blue)
        {
            if (GameObject.FindGameObjectWithTag("Blue Portal") != null)
                GameObject.FindGameObjectWithTag("Blue Portal").GetComponent<Portal>().Close();
        }

        if (orange)
        {
            if (GameObject.FindGameObjectWithTag("Orange Portal") != null)
                GameObject.FindGameObjectWithTag("Orange Portal").GetComponent<Portal>().Close();
        }
    }


    /// Create a beam that flies from the gun
    /// <param name="direction"></param>
    /// <param name="color"></param>
    private void CreateBeam(Vector3 direction, Material color)
    {
        var beam = Instantiate(beamPrefab, transform.position + transform.forward, Quaternion.identity);
        beam.GetComponent<MeshRenderer>().material = color;
        beam.GetComponent<TrailRenderer>().material = color;
        beam.transform.LookAt(direction);
        beam.GetComponent<Rigidbody>().AddForce(beam.transform.forward * 200, ForceMode.Impulse);
    }
}
