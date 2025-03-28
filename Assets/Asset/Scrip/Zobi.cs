using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zobi : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject[] targets;

    void Update()
    {
        targets = GameObject.FindGameObjectsWithTag("Player");
        if (targets.Length == 0) return;

        GameObject target = null;
        float minDistanceSqr = Mathf.Infinity;

        foreach (var t in targets)
        {
            float distanceSqr = (t.transform.position - transform.position).sqrMagnitude;
            if (distanceSqr < minDistanceSqr)
            {
                minDistanceSqr = distanceSqr;
                target = t;
            }
        }

        if (target != null && agent != null)
            agent.SetDestination(target.transform.position);
    }
}