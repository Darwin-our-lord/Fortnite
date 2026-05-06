using UnityEngine;

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

            collision.gameObject.GetComponent<PlayerController>().Die();
        }
    }

}
