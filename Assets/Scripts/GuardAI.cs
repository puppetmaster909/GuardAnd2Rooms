using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardAI : MonoBehaviour
{
    NavMeshAgent navMeshAgent;

    public List<Waypoint> patrolPoints;

    public Transform destination;
    //private Transform searchTarget;
    private Vector3 targetLocation;

    public bool patrolWaiting;
    public float totalWaitTime = 3f;
    public float switchProbability = 0.2f;
    public float deathDistance;
    public bool playerInSight = false;

    private float chaseTimer = 0.0f;
    private int currentPatrolIndex;
    private float waitTimer;
    private float searchTimer;
    private bool traveling;
    private bool waiting;
    private bool patrolForward;
    private bool targetSet = false;
    private string currentState = "Patrol";
    private bool hasDied;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        
        if (navMeshAgent == null)
        {
            Debug.LogError("Nav mesh not attached to " + gameObject.name);
        }
        else
        {
            if (patrolPoints != null && patrolPoints.Count >= 2)
            {
                currentPatrolIndex = 0;
                SetDestination();
            }
        }
        hasDied = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, deathDistance);
    }

    // Sets the next distination from the patrol points list
    private void SetDestination()
    {
        if (destination != null)
        {
            Vector3 targetVector = patrolPoints[currentPatrolIndex].transform.position;
            navMeshAgent.SetDestination(targetVector);
            traveling = true;
        }
    }

    // Changes patrol points once the last destination has been reached
    private void ChangePatrolPoint()
    {
        if (UnityEngine.Random.Range(0f, 1f) <= switchProbability)
        {
            patrolForward = !patrolForward;
        }

        if (patrolForward)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Count;
        }
        else
        {
            if (--currentPatrolIndex < 0)
            {
                currentPatrolIndex = patrolPoints.Count - 1;
            }
        }
    }

    // Call on update to patrol to next point
    private void Patrol()
    {
        // Check if close to destination
        if (traveling && navMeshAgent.remainingDistance <= 1.0f)
        {
            traveling = false;

            // If waiting active, then wait
            if (patrolWaiting)
            {
                waiting = true;
                waitTimer = 0;
            }
            else
            {
                ChangePatrolPoint();
                SetDestination();
            }
        }

        // Instead if we're waiting
        if (waiting)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer >= totalWaitTime)
            {
                waiting = false;
                ChangePatrolPoint();
                SetDestination();
            }
        }
    }

    public void SetSearch(Transform target)
    {
        currentState = "Search";
        if (!targetSet)
        {
            targetSet = true;
            targetLocation = target.position;
        }
    }

    // Changes state and causes guard to search for target by moving towards last known location (Location of bark)
    public void Search()
    {
        if (targetSet)
        {
            navMeshAgent.SetDestination(targetLocation);

            if (navMeshAgent.remainingDistance <= 5.0f)
            {
                searchTimer += Time.deltaTime;
                if (searchTimer >= 5.0f)
                {
                    currentState = "Patrol";
                    searchTimer = 0.0f;
                    targetSet = false;
                }
            }
        }
    }

    public void SetChase(Transform playerPos)
    {
        currentState = "Chase";
        targetLocation = playerPos.position;
    }

    public void Chase()
    {
        transform.LookAt(targetLocation);
        navMeshAgent.SetDestination(targetLocation);

        if (!playerInSight)
        {
            chaseTimer += Time.deltaTime;
            if (chaseTimer >= 2.0f)
            {
                currentState = "Patrol";
                chaseTimer = 0.0f;
            }
        }
    }

    public void SetPatrol()
    {
        currentState = "Patrol";
    }

    // Update is called once per frame
    void Update()
    {
        // If statement to check current state, then call corresponding function
        if (currentState.Equals("Patrol"))
        {
            Patrol();

        } else if (currentState.Equals("Search"))
        {
            Search();      
        } else if (currentState.Equals("Chase"))
        {
            Chase();

            if (navMeshAgent.remainingDistance <= deathDistance)
            {
                if (!hasDied)
                {
                    UIManager.instance.ShowScreen("DeathScreen");
                    hasDied = true;
                }
            }
        }
    }
}