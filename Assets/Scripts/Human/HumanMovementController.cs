using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class HumanMovementController : MonoBehaviour {
	public Location[] locations;

	private NavMeshAgent agent;

	void Start() {
		agent = GetComponent<NavMeshAgent>();
	}

	void Update() {
		var vel = transform.InverseTransformDirection(agent.velocity);
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
		//transform.rotation = t.rotation;
		yield return RotateTowards(t.rotation);
	}

	public void SetActive(bool active) {
		agent.isStopped = !active;
	}

	public IEnumerator RotateTowards(Quaternion rot) {
		Quaternion orig = transform.rotation;
		float lerp = 0f;
		while (lerp < 1f) {
			lerp += Time.deltaTime * 5;
			transform.rotation = Quaternion.Slerp(orig, rot, lerp);
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