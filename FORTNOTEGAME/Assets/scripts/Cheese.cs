using TMPro;
using UnityEngine;

public class Cheese : MonoBehaviour
{
    public static bool gotCheese = false;

    public void OnCollisionEnter(Collision collision)
    {

        if (collision.collider.CompareTag("Player"))
        {

            collision.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(20,2,0), ForceMode.VelocityChange);

            QuestOverlays questOverlays = GameObject.Find("QuestUI").GetComponent<QuestOverlays>();
            questOverlays.AnimateOverlay("GetOut");

            gotCheese = true;
            Destroy(this.gameObject);
        }
    }



}
