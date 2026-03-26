using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class CatMovement : MonoBehaviour
{
    NavMeshAgent agent;
    public Vector3? follow;

    public LayerMask mask;

    GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

        if(Vector3.Distance(player.transform.position, this.gameObject.transform.position) < 5)
        {
            if (Vector3.Distance(player.transform.position, this.gameObject.transform.position) < 1.1f)
            {
                Destroy(player.gameObject);
            }

            bool hit = Physics.Raycast(gameObject.transform.position, -(gameObject.transform.position- player.transform.position).normalized, out RaycastHit ray, 5, mask);

            if (hit)
            {
                if (ray.collider.gameObject.CompareTag("Player"))
                {
                    agent.SetDestination(player.transform.position);
                    return;
                }
            }
        }
        if(follow != null)
        {
            agent.SetDestination(follow.Value);
        }
    }

    public void SetFollow(Vector3? vetor)
    {
        if(Vector3.Distance(vetor.Value, transform.position) < 1.5)
        {
            return;
        }
        follow = vetor;
    }
}
