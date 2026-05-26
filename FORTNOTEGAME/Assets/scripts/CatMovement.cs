using System.Collections;
using System.Net;
using System.Threading;
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
        if (transform.position.y+1 >= player.transform.position.y || Cheese.gotCheese)
        {
            if (distance <= innerDetectPlayerRange || Cheese.gotCheese)
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

        if (chasingPlayer == true) 
        { 
            agent.ResetPath(); 
            animator.SetBool("Walking", false);
            animator.SetBool("Chasing", false);
        }
        chasingPlayer = false;

        /*if (follow != null && !chasingPlayer)
        {
            animator.SetBool("Walking", true);
            animator.SetBool("Chasing", false);
            agent.speed = walkSpeed;
        }*/
    }

    public void SetFollow(Vector3? vetor)
    {
        if (chasingPlayer) return;

        float distance = Vector3.Distance(vetor.Value, transform.position);
        bool hit = Physics.Raycast(transform.position, (vetor.Value - transform.position).normalized, out RaycastHit ray, distance-0.5f, mask);
        //Debug.Log(ray.transform.gameObject.name);
        
        if (hit) return;

        if(distance < 1.5)
        {
            animator.SetBool("Walking", false);
            animator.SetBool("Chasing", false);
            agent.speed = walkSpeed;
            return;
        }
        else
        {
            animator.SetBool("Walking", true);
            animator.SetBool("Chasing", false);
            agent.speed = walkSpeed;
        }
        follow = vetor;
        agent.SetDestination(follow.Value);
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
        
        if (collision.collider.CompareTag("Player")) 
        { 
            QuestOverlays questOverlays = GameObject.Find("QuestUI").GetComponent<QuestOverlays>(); 
            questOverlays.AnimateOverlay("YouDiedCat");

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
