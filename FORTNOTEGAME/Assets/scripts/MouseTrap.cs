using UnityEngine;
using System.Collections;
public class MouseTrap : MonoBehaviour
{
    [SerializeField]
    bool triggered = false;

    Animator animator;
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (triggered) return;

        if (collision.gameObject.CompareTag("Cat"))
        {
            triggered = true;
            animator.SetTrigger("trigger");
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            triggered = true;
            animator.SetTrigger("trigger");

            QuestOverlays questOverlays = GameObject.Find("QuestUI").GetComponent<QuestOverlays>();
            questOverlays.AnimateOverlay("YouDiedTrap");

            PlayerController player = collision.collider.GetComponent<PlayerController>();

            if (player != null)
            {
                StartCoroutine(WaitAndDie(player));
            }
        }
    }
    IEnumerator WaitAndDie(PlayerController player)
    {
        yield return new WaitForSecondsRealtime(2);

        if (player != null)
        {
            player.Die();
        }
    }
}
