using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class HumanMovementController : MonoBehaviour {
	public Location[] locations;

	private NavMeshAgent agent;

	// Use this for initialization
	void Start() {
		agent = GetComponent<NavMeshAgent>();
	}

	void Update() {
		var vel = transform.InverseTransformDirection(agent.desiredVelocity);
		Human.Animator.SetFloat("Velocity", vel.z);
	}

	public IEnumerator GoTo(string locationName) {
		if (!locations.Any(l => l.name == locationName)) {
			Debug.Log("Invalid Location: " + locationName);
			yield break;
		}

		var t = locations.Single(l => l.name == locationName).transform;
		agent.SetDestination(t.position);
		while (!ReachedDestination()) {
			yield return null;
		}
	}

	bool ReachedDestination() {
		return !agent.pathPending &&
			agent.remainingDistance <= agent.stoppingDistance &&
			(agent.hasPath || agent.velocity.sqrMagnitude == 0f);
	}

	[System.Serializable]
	public struct Location {
		public string name;
		public Transform transform;
	}
}