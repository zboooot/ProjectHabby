using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CarAI : MonoBehaviour
{
    private NavMeshAgent agent;
    private Vector3 randomDestination;
    public float roamRadius = 20.0f;
    public float roamInterval = 5.0f;
    private Transform player;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        InvokeRepeating("SetRandomDestination", 0, roamInterval);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void SetRandomDestination()
    {
        randomDestination = Random.insideUnitSphere * roamRadius;
        randomDestination += transform.position;
        NavMeshHit hit;

        if (NavMesh.SamplePosition(randomDestination, out hit, roamRadius, NavMesh.AllAreas))
        {
            agent.SetDestination(player.position);
        }
    }
}
