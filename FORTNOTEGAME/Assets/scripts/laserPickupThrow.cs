using UnityEditor.Rendering;
using UnityEngine;

public class laserPickupThrow : MonoBehaviour
{
    public bool hasLaserPointer = true;

    public GameObject laserPoint;
    public GameObject LaserGameOBJ;
    public GameObject laserPrefab;
    public LayerMask rayCastLayerMask;
    public LayerMask collisonLayerMask;

    // Update is called once per frame
    void Update()
    {
        if (hasLaserPointer)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Destroy(LaserGameOBJ);
                LaserGameOBJ=Instantiate(laserPrefab,laserPoint.transform.position, transform.rotation);
                LaserGameOBJ.GetComponent<Rigidbody>().linearVelocity = transform.forward * 4;
                
                hasLaserPointer =false;
            }
        }
        else
        {
            bool hit = Physics.Raycast(transform.position, transform.forward, out RaycastHit ray,3,rayCastLayerMask);

            if (Vector3.Distance(ray.point, LaserGameOBJ.transform.position) < 2)
            {
                LaserGameOBJ.transform.GetChild(0).gameObject.SetActive(true);
                LaserGameOBJ.transform.GetChild(0).transform.LookAt(transform.position);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    Destroy(LaserGameOBJ);
                    LaserGameOBJ=Instantiate(laserPrefab, laserPoint.transform);
                    LaserGameOBJ.GetComponent<Rigidbody>().useGravity = false;
                    LaserGameOBJ.GetComponent<Rigidbody>().isKinematic = true;
                    LaserGameOBJ.GetComponent<Rigidbody>().excludeLayers = collisonLayerMask;
                    hasLaserPointer = true;
                }
            }
            else
            {
                LaserGameOBJ.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }
}
