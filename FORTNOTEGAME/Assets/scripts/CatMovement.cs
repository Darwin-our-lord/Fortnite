using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class CatMovement : MonoBehaviour
{
    NavMeshAgent agent;
    public Vector3? follow;

    public LayerMask mask;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
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
