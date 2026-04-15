using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public GameObject playerCam;
    [SerializeField] LayerMask layerMask;
    [SerializeField] GameObject deathUI;

    Rigidbody rb;
    float moveSpeed = 10;
    float rotationSpeed = 10;
    float jumpHeight = 0.5f;

    float pitch = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //move
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 dir = transform.forward * vertical + transform.right * horizontal;

        rb.linearVelocity = new Vector3(dir.x*moveSpeed,rb.linearVelocity.y,dir.z*moveSpeed);

        //jump
        if (Input.GetKey(KeyCode.Space))
        {
            bool ray = Physics.Raycast(gameObject.transform.position,  Vector3.down, out RaycastHit hit, 1.1f, layerMask);
            if(ray) GetComponent<Rigidbody>().AddForce(new Vector3(0, jumpHeight, 0), ForceMode.Impulse);
        }

        //camera
        float rotationVer = Input.GetAxis("Mouse Y") * rotationSpeed;
        float rotationHor = Input.GetAxis("Mouse X") * rotationSpeed;

        transform.Rotate(0, rotationHor, 0);

        pitch -= rotationVer;
        pitch = Mathf.Clamp(pitch, -80f, 90f);

        playerCam.transform.localRotation = Quaternion.Euler(pitch, 0f, 0f);


    }

    public void Die() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
