using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public FieldOfView fieldOV;

    NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGrouded, whatIsPlayer;

    //Patroling
    Vector3 target; // Waypoints
    public Transform[] waypoint;
    int waypointIndex;

    //State
    //public float sightRange = 10;
    public bool playerIsInSight;
    //public float chaseRange = 15;
    //public bool playerIsInChaseRange;

    

    private void Awake()
    {
        fieldOV = GetComponent<FieldOfView>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }
    // Start is called before the first frame update
    void Start()
    {
        UpdateDestination();
    }

    // Update is called once per frame
    void Update()
    {
        //playerIsInSight = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        //playerIsInChaseRange = Physics.CheckSphere(transform.position, chaseRange, whatIsPlayer);
        fieldOV.FieldofViewCheck();

        if (playerIsInSight)
        {
            ChasePlayer();
        }
        else if(!playerIsInSight)
        {
            UpdateDestination();
            if (Vector3.Distance(transform.position, target) < 1)
            {
                IterateWaypointIndex();
                UpdateDestination();
            }
        }   
    }

    void UpdateDestination()
    {
        target = waypoint[waypointIndex].position; // Pega a posição do primeiro WAYPOINT
        agent.SetDestination(target); // Manda o NavmeshAgent para o nosso target
    }

    void IterateWaypointIndex()
    {
        waypointIndex++;
        if (waypointIndex == waypoint.Length)
        {
            waypointIndex = 0; // Quando chegar no ultimo ponto, volta ao primeiro
        }
    }

    public void ChasePlayer()
    {
        agent.destination = player.position;
    }
}