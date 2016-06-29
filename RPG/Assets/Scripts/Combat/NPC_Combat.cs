using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPC_Combat : MonoBehaviour {

	[SerializeField] private bool _canFight;
	[SerializeField] private int _damage;
	[SerializeField] private float _attackCooldown = 1.0f;
	[SerializeField] private float _speed;
	private float _newAttackTime;
	private NavMeshAgent _navAgent;
	public GameObject myTarget;
	public List<GameObject> enemies = new List<GameObject>();
	private Vector3 _originalPos;
	private GameObject[] _allies; 
	
	void Start()
	{
		_originalPos = this.transform.position;
		_navAgent = GetComponent<NavMeshAgent>();
		_navAgent.speed = _speed;
	}
	void Update()
	{
		if (myTarget != null)
			ChaseTarget();
		else if (enemies.Count > 0)
		{
			if (enemies[0] == null)
			{
				enemies.RemoveAt(0);
				if (enemies.Count > 0)
					myTarget = enemies[0];
			}
		}			
		else if (Vector3.Distance(transform.position, _originalPos) > 4f)
			ReturnToOriginalPosition();
	}
	void OnTriggerEnter(Collider other)
	{
		if (this.gameObject.tag == Tags.Enemy)
		{
			if (other.gameObject.tag == Tags.NPC || other.gameObject.tag == Tags.Player)
			{
				_allies = GameObject.FindGameObjectsWithTag(Tags.Enemy);
				RallyAllies();
				if (myTarget == null)				
				{
					myTarget = other.gameObject;
					if (!enemies.Contains(myTarget))
						enemies.Add(other.gameObject);
				}
				else if (!enemies.Contains(other.gameObject))
					enemies.Add(other.gameObject);
			}
		}
		if (this.gameObject.tag == Tags.NPC)
		{
			if (other.gameObject.tag == Tags.Enemy)
			{
				_allies = GameObject.FindGameObjectsWithTag(Tags.NPC);
				RallyAllies();
				if (myTarget == null)
				{
					myTarget = other.gameObject;
					if (!enemies.Contains(myTarget))
						enemies.Add(other.gameObject);
				}
				else if (!enemies.Contains(other.gameObject))
					enemies.Add(other.gameObject);
			}
		}
	}
	void RallyAllies()
	{
		foreach (GameObject ally in _allies)
		{
			if(Vector3.Distance(ally.transform.position, this.transform.position) < 7.5f)
			{
				if (ally.GetComponent<NPC_Combat>().myTarget == null)
					ally.GetComponent<NPC_Combat>().myTarget = myTarget;
			}
		}
	}
	void OnTriggerExit(Collider other)
	{
		if (this.gameObject.tag == Tags.NPC)
		{
			if (other.gameObject.tag == Tags.Enemy)
			{
				enemies.Remove(other.gameObject);
				if (other.gameObject == myTarget)
					myTarget = null;
			}
		}
		if (this.gameObject.tag == Tags.Enemy)
		{
			if (other.gameObject.tag == Tags.NPC || other.gameObject.tag == Tags.Player)
			{
				enemies.Remove(other.gameObject);
				if (other.gameObject == myTarget)
					myTarget = null;
			}
		}
	}
	private void ChaseTarget()
	{
		if (Vector3.Distance(myTarget.transform.position, this.gameObject.transform.position) <= 4)
		{
			_navAgent.Stop();
			DealDamage(myTarget);
		}else
		{
			_navAgent.Resume();
			_navAgent.SetDestination(myTarget.transform.position);
		}
	}
	private void DealDamage(GameObject target)
	{
		if (_newAttackTime <= Time.time)
		{
			if (target.GetComponent<Health>().getHealth < _damage)
				myTarget = null;
			target.GetComponent<Health>().TakeDamage(_damage);
			_newAttackTime = Time.time + _attackCooldown;
		}
	}
	private void ReturnToOriginalPosition()
	{
		_navAgent.Resume();
		_navAgent.SetDestination(_originalPos);
	}
}
