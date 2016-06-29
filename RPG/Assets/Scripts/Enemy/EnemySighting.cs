using UnityEngine;
using System.Collections;

public class EnemySighting : MonoBehaviour {

	private SphereCollider sphereCol;
	private float fieldOfViewAngle = 110f;
	private GameObject player;
	private bool playerInSight;
	private bool playerInRadius;

	[SerializeField] private EnemyMovement movement;

	void Start()
	{
		player = GameObject.FindGameObjectWithTag(Tags.Player);
		sphereCol = GetComponent<SphereCollider>();
	}
	void Update()
	{
		if (playerInSight)
		{
			GetComponent<Skeleton>().myTarget = player.transform;
		}
		else if (playerInRadius)
		{
			movement.SwitchState(States.SEARCHING);
			GetComponent<Skeleton>().myTarget = null;
		}
	}
	/* Notes
	// 
		// If the player has entered the trigger sphere...
			// Create a vector from the enemy to the player and store the angle between it and forward.
			// If the angle between forward and where the player is, is less than half the angle of view...
				// ... and if a raycast towards the player hits something...
					// ... and if the raycast hits the player...
						// ... the player is in sight.
						// Set the last global sighting is the players current position.
	//
	*/
	void OnTriggerStay (Collider other)
	{
		if(other.gameObject == player)
		{
			playerInSight = false;

			Vector3 direction = other.transform.position - transform.position;
			float angle = Vector3.Angle(direction, transform.forward);

			if(angle < fieldOfViewAngle * 0.5f)
			{
				RaycastHit hit;

				if(Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, sphereCol.radius))
				{
					if(hit.collider.gameObject == player)
					{
						playerInSight = true;
						playerInRadius = true;
					}
				}
			}
		}
	}
	// If the player leaves the trigger zone...
	// ... the player is not in sight.
	void OnTriggerExit (Collider other)
	{
		if(other.gameObject == player)
		{
			playerInSight = false;
			playerInRadius = false;
		}
	}
}
