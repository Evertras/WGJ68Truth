using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureFinder : MonoBehaviour {

    public UnityEngine.AI.NavMeshAgent Agent;
    public float LifetimeSeconds = 2.0f;

	void Start () {
        var goal = GameObject.FindGameObjectWithTag("Goal");

        if (goal == null)
        {
            Debug.LogWarning("No goal found, destroying");
            Destroy(gameObject);
            return;
        }

        Agent.SetDestination(goal.transform.position);

        StartCoroutine(TimedDeath(LifetimeSeconds));
	}

    IEnumerator TimedDeath(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        Destroy(gameObject);
    }
	
}
