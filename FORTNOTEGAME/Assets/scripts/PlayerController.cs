using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public GameObject playerCam;

    Rigidbody rb;
    float moveSpeed = 10;
    float rotationSpeed = 10;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //move
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 dir = transform.forward * vertical + transform.right * horizontal;

        dir = dir * moveSpeed;

        dir.y = rb.linearVelocity.y;

        rb.linearVelocity = dir;


        //camera
        float rotationVer = -Input.GetAxis("Mouse Y") * rotationSpeed;
        float rotationHor = Input.GetAxis("Mouse X") * rotationSpeed;

        transform.Rotate(0, rotationHor,0);
        playerCam.transform.Rotate(rotationVer, 0, 0);

    }
}
