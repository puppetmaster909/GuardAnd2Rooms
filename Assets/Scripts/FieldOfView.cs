using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float viewRaidus;
    [Range(0, 360)]
    public float viewAngle;

    public LayerMask playerMask;
    public LayerMask obstacleMask;

    private bool playerSeen = false;

    GuardAI guardInstance;

    [HideInInspector]
    public List<Transform> visiblePlayers = new List<Transform>();

    void Start()
    {
        StartCoroutine("FindPlayersWithDelay", .2f);
        guardInstance = GetComponent<GuardAI>();
    }

    IEnumerator FindPlayersWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisiblePlayer();
        }
    }

    void FindVisiblePlayer()
    {
        visiblePlayers.Clear();

        Collider[] playersInViewRaidus = Physics.OverlapSphere(transform.position, viewRaidus, playerMask);

        for (int i = 0; i < playersInViewRaidus.Length; i++)
        {
            Transform player = playersInViewRaidus[i].transform;
            Vector3 dirToPlayer = (player.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToPlayer) < viewAngle / 2)
            {
                float dstToPlayer = Vector3.Distance(transform.position, player.position);

                if (!Physics.Raycast(transform.position, dirToPlayer, dstToPlayer, obstacleMask))
                {
                    visiblePlayers.Add(player);
                    // Set bool playerSeen to true
                    playerSeen = true;

                    // Call SetChase function
                    if (guardInstance)
                    {
                        guardInstance.SetChase(player);
                        guardInstance.playerInSight = true;
                    }

                }
            }
        }

        // At this point the array has been looped through. If visiblePlayers is empty and playerSeen == true, start countdown
        if (visiblePlayers.Count == 0 && playerSeen)
        {
            guardInstance.playerInSight = false;
        }
    }

    public Vector3 dirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
