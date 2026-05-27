using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (Cheese.gotCheese)
        {
            QuestOverlays questOverlays = GameObject.Find("QuestUI").GetComponent<QuestOverlays>();
            questOverlays.AnimateOverlay("YouWin");
            StartCoroutine(Inum());
        }
        else
        {
            QuestOverlays questOverlays = GameObject.Find("QuestUI").GetComponent<QuestOverlays>();
            questOverlays.AnimateOverlay("GetCheese");
        }
    }

    IEnumerator Inum()
    {
        yield return new WaitForSecondsRealtime(2);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(0);
    }

}
