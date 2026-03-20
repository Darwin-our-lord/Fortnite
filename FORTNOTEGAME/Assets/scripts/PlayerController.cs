using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Rigidbody rb;
    float moveSpeed = 30;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 dir = new Vector3(Input.GetAxisRaw("Horizontal"), 0,Input.GetAxisRaw("Vertical"));

        rb.linearVelocity = dir * moveSpeed * Time.deltaTime;
         

    }
}
