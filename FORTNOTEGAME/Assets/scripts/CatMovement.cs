using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.Universal;

public class CatMovement : MonoBehaviour
{
    [SerializeField]
    float viewConeSize = 1f;

    [SerializeField]
    float chaseSpeed = 5.5f;
    [SerializeField]
    float walkSpeed = 3.5f;

    float detectPlayerRange = 17;
    float innerDetectPlayerRange = 7.5f;


    NavMeshAgent agent;
    public Vector3? follow;

    public LayerMask mask;

    GameObject player;

    Animator animator;

    bool chasingPlayer = false;

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
        if (transform.position.y+1 >= player.transform.position.y)
        {
            if (distance <= innerDetectPlayerRange)
            {
                animator.SetBool("Walking", false);
                animator.SetBool("Chasing", true);
                agent.SetDestination(player.transform.position);
                agent.speed = chaseSpeed;

                chasingPlayer = true;
                return;
            }
            else if (distance <= detectPlayerRange)
            {
                if ((transform.forward - dirToPlayer).magnitude < viewConeSize)
                {
                    if (Physics.Raycast(transform.position, dirToPlayer, out RaycastHit ray, detectPlayerRange + 1, mask))
                    {
                        if (ray.collider.CompareTag("Player"))
                        {
                            animator.SetBool("Walking", false);
                            animator.SetBool("Chasing", true);
                            agent.speed = chaseSpeed;
                            agent.SetDestination(player.transform.position);

                            chasingPlayer = true;
                            return;
                        }
                    }
                }
            }
        }

        chasingPlayer = false;

        if (follow != null && !chasingPlayer)
        {
            agent.SetDestination(follow.Value);
            agent.speed = walkSpeed;
        }
    }

    public void SetFollow(Vector3? vetor)
    {
        if (chasingPlayer) return;

        if(Vector3.Distance(vetor.Value, transform.position) < 1.5)
        {
            animator.SetBool("Walking", false);
            animator.SetBool("Chasing", false);
            agent.speed = walkSpeed;
            return;
        }
        else
        {
            animator.SetBool("Walking", true);
            agent.speed = walkSpeed;
        }
        follow = vetor;
    }

    public void StopChaseLaser()
    {
        if (chasingPlayer) return;
        follow = null;
        //agent.SetDestination(transform.position);
        agent.ResetPath();

        animator.SetBool("Walking", false);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player")) collision.collider.GetComponent<PlayerController>().Die();
    }
}
