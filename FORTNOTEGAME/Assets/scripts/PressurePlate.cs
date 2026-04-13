using Unity.VisualScripting;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{ 
    [SerializeField] bool onlyCat;
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Cat") || other.CompareTag("Player")&&!onlyCat)
        {

        }
    }
}
