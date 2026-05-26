using UnityEngine;

public class Win : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (Cheese.gotCheese)
        {
            QuestOverlays questOverlays = GameObject.Find("QuestUI").GetComponent<QuestOverlays>();
            questOverlays.AnimateOverlay("YouWin");
        }
        else
        {
            QuestOverlays questOverlays = GameObject.Find("QuestUI").GetComponent<QuestOverlays>();
            questOverlays.AnimateOverlay("GetCheese");
        }



    }
}
