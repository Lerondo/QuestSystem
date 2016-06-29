using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum States
{
	PATROLLING,
	ATTACKING,
	SEARCHING
}

public class EnemyMovement : MonoBehaviour {

	public bool guard;
	public List<Transform> patrolLocations = new List<Transform>();

	private Vector3 startPos;
	private Vector3 _wanderLocation;
	private float wanderRadius = 10;
	private float cooldownToWander;
	private float wanderTime = 0;
	private NavMeshAgent _navAgent;
	private int i = 0;

	private States state;
	private States oldState;

	void Start()
	{
		_navAgent = GetComponent<NavMeshAgent>();
		startPos = this.transform.position;
	}
	void Update()
	{
		if (!GetComponent<Skeleton>().myTarget)
		{
			if (!guard)
			{
				if(Time.time > wanderTime)
					NewWanderPoint();
			}
			else
			{
				if(Time.time > wanderTime)
					NextPatrolPoint();
			}
		}
	}
	void NextPatrolPoint()
	{
		wanderTime = Time.time + cooldownToWander;
		_navAgent.ResetPath();
		i++;
		if (i >= patrolLocations.Count)
			i = 0;
		_navAgent.SetDestination(patrolLocations[i].position);
	}
	void NewWanderPoint()
	{
		cooldownToWander = Random.Range(6,9);
		wanderTime = Time.time + cooldownToWander;
		_navAgent.ResetPath();
		_wanderLocation = RandomNavSphere(startPos, wanderRadius, -1);
		_navAgent.SetDestination(_wanderLocation);
	}
	public static Vector3 RandomNavSphere(Vector3 origin, float distance, int layerMask)
	{
		Vector3 randDir = Random.insideUnitSphere * distance;
		
		randDir += origin;
		
		NavMeshHit hit;
		
		NavMesh.SamplePosition(randDir, out hit, distance, layerMask);
		return hit.position;
	}

	public void SwitchState(States stateToDo)
	{
		oldState = state;
		switch(state)
		{
		case States.PATROLLING:
			Patrolling();
			break;
		case States.SEARCHING:
			Searching();
			break;
		}
	}
	void Patrolling()
	{
		
	}
	void Searching()
	{
		
	}
}
