using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class CatMovement : MonoBehaviour
{
    [SerializeField]
    float viewConeSize = 0.8f;

    float detectPlayerRange = 7;

    NavMeshAgent agent;
    public Vector3? follow;

    public LayerMask mask;

    GameObject player;

    Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        player = GameObject.FindWithTag("Player");

        animator = transform.GetChild(0).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dirToPlayer = (player.transform.position - transform.position).normalized;
        float distance = Vector3.Distance(player.transform.position, transform.position);

        if (distance <= detectPlayerRange)
        {
            if ((transform.forward-dirToPlayer).magnitude < viewConeSize)
            {
                if (Physics.Raycast(transform.position, dirToPlayer, out RaycastHit ray, detectPlayerRange + 1, mask))
                {
                    if (ray.collider.CompareTag("Player"))
                    {
                        agent.SetDestination(player.transform.position);
                    }
                    else if (follow != null)
                    {
                        agent.SetDestination(follow.Value);
                    }
                }
            }
        }
        else if (follow != null)
        {
            agent.SetDestination(follow.Value);
        }
    }

    public void SetFollow(Vector3? vetor)
    {
        if(Vector3.Distance(vetor.Value, transform.position) < 1.5)
        {
            animator.SetBool("Walking", false);
            return;
        }
        else
        {
            animator.SetBool("Walking", true);
        }
        follow = vetor;
    }
}
