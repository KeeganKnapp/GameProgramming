using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum DinoState {
	IDLE = 0,
	ROAMING,
	CHASING,
	WALKING,
	SEARCHING
}
public class Dinosaur : MonoBehaviour
{
	[Header("References")]
	[SerializeField] Animator animator;
	[SerializeField] NavMeshAgent agent;
	[SerializeField] GameObject player;	

	[Header("Tweaks")]
	[SerializeField] float Speed;

	DinoState state = 0;

	// Start is called before the first frame update
	void Start()
	{
	}

	Vector3 GetRandomPosition() {
		Vector2 random = Random.insideUnitCircle * 100;
		Vector3 raw = new Vector3(random.x, 0f, random.y) + transform.position;

		if(NavMesh.SamplePosition(raw, out var hit, 100, NavMesh.AllAreas)) {
			return hit.position;
		}

		else return new Vector3(0f,0f,0f);
	}

	Vector3 GetPlayerPosition() {
		var playerPos = player.transform.position;
		Vector3 raw = new Vector3(playerPos.x, 0f, playerPos.z);

		if(NavMesh.SamplePosition(raw, out var hit, 100, NavMesh.AllAreas)) {
			return hit.position;
		}
		else return new Vector3(0f,0f,0f);
	}

	// Update is called once per frame
	void Update()
	{
		state = DinoState.CHASING;
		if(state == DinoState.ROAMING) {
			if(agent.remainingDistance <= agent.stoppingDistance) {
				agent.SetDestination(GetRandomPosition());
			}
		}
		else if(state == DinoState.CHASING) {
			agent.SetDestination(GetPlayerPosition());
		}

	}

}
