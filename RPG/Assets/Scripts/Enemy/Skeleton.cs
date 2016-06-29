using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Skeleton : MonoBehaviour {

	#region Base
	[SerializeField] int questID;
	[SerializeField] int questAmount;
	[SerializeField] private int _experience;
	
	public void AddExperience()
	{
		GameObject.FindWithTag(Tags.GameController).GetComponent<PlayerStats>().AddExperience(_experience);
	}
	public void CheckQuestObjectiveOnDeath()
	{
		if (QuestDatabase.questList[questID].getQuestAccepted == true &&
		    QuestDatabase.questList[questID].getQuestCompleted == false)
			GameObject.FindWithTag(Tags.GameController).GetComponent<QuestTracker>().UpdateQuest(questID, questAmount);
		AddExperience();
		FallApart();
	}
	
	#endregion
	
	private List<Rigidbody> bodyParts = new List<Rigidbody>();
	public List<Vector3> bodyPartEndPositions = new List<Vector3>();
	private List<Quaternion> bodyPartEndRotations = new List<Quaternion>();
	private List<Component> scripts = new List<Component>();
	private float _despawnTime = 5;
	private float _speed = 4f;
	private float _attackCooldown = 1.0f;	
	private float _newAttackTime;
	
	[SerializeField] private int _damage;
	
	private NavMeshAgent _navAgent;
	public Transform myTarget;
	private bool _dead = false;
	private bool _revived = false;
	private bool _reviving = false;
	private bool _reviveNow = false;
	private float _timeBeforeRevive = 3f;
	private float _momentOfReviving;
	
	void Start()
	{
		_navAgent = GetComponent<NavMeshAgent>();
		_navAgent.speed = _speed;
		GetComponentsOfObjects();
	}
	private void GetComponentsOfObjects()
	{
		foreach (Rigidbody rigidbody in GetComponentsInChildren<Rigidbody>())
		{
			bodyParts.Add(rigidbody);
		}
		foreach (Component comp in GetComponentsInChildren<Component>())
		{
			if (comp.ToString().Contains("UnityEngine.BoxCollider") || comp.ToString().Contains("UnityEngine.Rigidbody"))
				scripts.Add(comp);
		}
		scripts.Add(GetComponent<Health>());
		scripts.Add(GetComponent<SphereCollider>());
		scripts.Add(GetComponent<NavMeshAgent>());
		scripts.Add(GetComponent<EnemyMovement>());
		scripts.Add(GetComponent<EnemySighting>());
		scripts.Add(this);
	}
	void Update()
	{
		if (!_dead && myTarget != null && _reviving == false)
			ChasePlayer(myTarget.transform);
		if(Input.GetKeyDown(KeyCode.K) && !_dead)
			FallApart();
		if (_reviveNow)
			ReviveSkeleton();
	}
	void ChasePlayer(Transform target)
	{
		if (Vector3.Distance(this.transform.position, target.position) < 2.5f)
		{
			Vector3 lookPos = target.position - transform.position;
			lookPos.y = 0;
			Quaternion rotation = Quaternion.LookRotation(lookPos);
			transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5);
			DealDamage(target.gameObject);
			_navAgent.Stop();
		}
		else
		{
			_navAgent.SetDestination(target.transform.position);
			_navAgent.Resume();
		}
	}
	void DealDamage(GameObject target)
	{
		if(_newAttackTime < Time.time)
		{
			if (!GameObject.Find("RightHand").GetComponent<PlayerCombat>().blocking)
				target.GetComponent<Health>().TakeDamage(_damage);
			_newAttackTime = Time.time + _attackCooldown;
		}
	}
	private void ReviveSkeleton()
	{
		for (int i = 0; i < bodyParts.Count; i ++)
		{
			bodyParts[i].GetComponent<Transform>().position = Vector3.Lerp(bodyParts[i].transform.position, bodyPartEndPositions[i], Time.deltaTime * 3);
			bodyParts[i].GetComponent<Transform>().rotation = Quaternion.Lerp(bodyParts[i].transform.rotation, bodyPartEndRotations[i], Time.deltaTime * 3);
		}
	}
	IEnumerator WaitCoroutine()
	{
		_reviving = true;
		yield return new WaitForSeconds(_timeBeforeRevive);
		_reviveNow = true;
		MakeEndPoints();
		yield return new WaitForSeconds(_timeBeforeRevive);
		foreach (Rigidbody rigidbody in bodyParts)
		{
			rigidbody.constraints = RigidbodyConstraints.FreezeAll;
			rigidbody.useGravity = true;
			rigidbody.GetComponent<BoxCollider>().enabled = true;
			GetComponent<BoxCollider>().enabled = true;
		}
		_reviveNow = false;
		_reviving = false;
		_revived = true;
		_navAgent.Resume();
	}
	private void MakeEndPoints()
	{
		foreach(Rigidbody rigidbody in bodyParts)
		{
			rigidbody.useGravity = false;
			rigidbody.GetComponent<BoxCollider>().enabled = false;
		}
	}
	private void FallApart()
	{
		GetComponent<BoxCollider>().enabled = false;
		if (!_revived)
		{
			foreach(Rigidbody rigidbody in bodyParts)
			{
				bodyPartEndPositions.Add(rigidbody.transform.position);
				bodyPartEndRotations.Add(rigidbody.transform.rotation);
			}
			_navAgent.Stop();
			foreach (Rigidbody rigidbody in bodyParts)
			{
				rigidbody.constraints = RigidbodyConstraints.None;
				rigidbody.AddExplosionForce(150, rigidbody.transform.position + new Vector3(Random.Range(-1f,1f),Random.Range(-1f,1f),Random.Range(-1f,1f)), 1);
			}
			GetComponent<Health>().getHealth = GetComponent<Health>().getMaxHealth();
			StartCoroutine(WaitCoroutine());
		}
		else
		{
			_dead = true;
			_navAgent.Stop();
			foreach (Rigidbody rigidbody in bodyParts)
			{
				rigidbody.constraints = RigidbodyConstraints.None;
				rigidbody.AddExplosionForce(150, rigidbody.transform.position + new Vector3(Random.Range(-1f,1f),Random.Range(-1f,1f),Random.Range(-1f,1f)), 1);
			}
			if (scripts.Count > 0)
			{
				foreach (Component component in scripts)
				{
					Destroy(component, _despawnTime);
				}
			}
		}
	}
//	void OnTriggerEnter(Collider other)
//	{
//		if (other.gameObject.tag == Tags.Player)
//			myTarget = other.gameObject;
//	}
	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == Tags.Player)
		{
			myTarget = null;
			_navAgent.ResetPath();
		}
	}
}
