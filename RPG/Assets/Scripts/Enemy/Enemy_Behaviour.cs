using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy_Behaviour : MonoBehaviour{

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
	private List<Component> scripts = new List<Component>();
	private float _despawnTime = 5;
	private float _speed = 4f;
	private float _attackCooldown = 1.0f;	
	private float _newAttackTime;

	[SerializeField] private int _damage;

	private NavMeshAgent _navAgent;
	public GameObject myTarget;
	private bool _dead = false;

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
	}
	void Update()
	{
		if (!_dead && myTarget != null)
			ChasePlayer(myTarget);
		if(Input.GetKeyDown(KeyCode.L) && !_dead)
			FallApart();
	}
	void ChasePlayer(GameObject target)
	{
		if (Vector3.Distance(this.transform.position, target.transform.position) < 3.5f)
		{
			Vector3 lookPos = target.transform.position - transform.position;
			lookPos.y = 0;
			Quaternion rotation = Quaternion.LookRotation(lookPos);
			transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5);
			DealDamage(target);
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
			target.GetComponent<Health>().TakeDamage(_damage);
			_newAttackTime = Time.time + _attackCooldown;
		}
	}
	private void FallApart()
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
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == Tags.Player)
			myTarget = other.gameObject;
	}
	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == Tags.Player)
		{
			myTarget = null;
			_navAgent.ResetPath();
		}
	}
}

