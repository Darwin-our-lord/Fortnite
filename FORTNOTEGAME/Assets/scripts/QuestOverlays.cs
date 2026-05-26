using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class QuestOverlays : MonoBehaviour
{
    public Dictionary<string, GameObject> overlays = new Dictionary<string, GameObject>();


    private void Start()
    {
        overlays.Add("GetCheese", transform.GetChild(0).gameObject);
        overlays.Add("YouDiedCat", transform.GetChild(1).gameObject);
        overlays.Add("YouDiedTrap", transform.GetChild(2).gameObject);
        overlays.Add("GetOut", transform.GetChild(3).gameObject);
        overlays.Add("YouWin", transform.GetChild(4).gameObject);

        Thread.Sleep(2300);
        AnimateOverlay("GetCheese");
    }

    public void AnimateOverlay(string name)
    {
        GameObject gameObject = overlays[name];

        if(gameObject != null) gameObject.GetComponent<Animator>().SetTrigger("Down");
    }
}
