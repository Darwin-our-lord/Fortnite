using TMPro;
using UnityEngine;

public class Cheese : MonoBehaviour
{
    public static bool gotCheese = false;

    public void OnCollisionEnter(Collision collision)
    {

        if (collision.collider.CompareTag("Player"))
        {
            gotCheese = true;
            Destroy(this.gameObject);
            
            
            
                
        }
    }



}
