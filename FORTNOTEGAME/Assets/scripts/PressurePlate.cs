using Unity.VisualScripting;
using UnityEngine;

public enum PressurePlateFunction
{
    None,
    door
}
public class PressurePlate : MonoBehaviour
{ 
    [SerializeField] bool onlyCat;
    [SerializeField] PressurePlateFunction plateFunction;

    [Header("DOOR")]
    [SerializeField] GameObject[] targetDoor;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.CompareTag("Cat") || other.CompareTag("Player")&&!onlyCat)
        {
            Debug.Log(other.name + "   2");
            switch (plateFunction)
            {
                case PressurePlateFunction.None:
                    Debug.LogError("A pressureplate has no function twin");
                    break;
                case PressurePlateFunction.door:
                    Debug.Log(other.name + "   3");
                    DoorFunction(true);
                    break;
            }
                
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log(other.name);
        if (other.CompareTag("Cat") || other.CompareTag("Player") && !onlyCat)
        {
            switch (plateFunction)
            {
                case PressurePlateFunction.None:
                    Debug.LogError("A pressureplate has no function twin");
                    break;
                case PressurePlateFunction.door:
                    DoorFunction(false);
                    break;
            }

        }
    }

    void DoorFunction(bool active)
    {
        Debug.Log("dorr -- "+active);
        foreach (GameObject door in targetDoor) 
        {
            door.GetComponent<Animator>().SetBool("Open", active);
        }
    }

}
