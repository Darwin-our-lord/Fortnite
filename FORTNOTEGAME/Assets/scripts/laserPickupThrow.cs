using UnityEngine;

public class laserPickupThrow : MonoBehaviour
{
    public bool hasLaserPointer = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hasLaserPointer)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                //throw lazerpointer
            }


        }
        else
        {

        }


    }
}
