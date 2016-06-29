using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerCombat : MonoBehaviour {

	public TrailRenderer trailRenderer;
	private Animation _animation;
	private bool _mayAttack = true;
	private bool _inMenu = false;
	public bool blocking;

	private int _damage;

	void Start () 
	{
		_animation = GetComponent<Animation>();
		IgnoreCollisions();
		GetComponent<BoxCollider>().enabled = false;
		trailRenderer.enabled = false;
	}	
	void Update () 
	{
		if (!_inMenu)
		{
			if (Input.GetMouseButtonDown(0) && _mayAttack)
				Attack();
			if (Input.GetMouseButton(1))
				Block();
		}
	}
	public void InMenu()
	{
		_inMenu = _inMenu == false ? true : false;
	}
	private void IgnoreCollisions()
	{
		foreach(GameObject enemy in GameObject.FindGameObjectsWithTag(Tags.Enemy))
		{
			Physics.IgnoreCollision(GetComponent<BoxCollider>(), enemy.GetComponent<SphereCollider>());
		}
	}
	void Attack()
	{
		_mayAttack = false;
		_animation.Play();
		trailRenderer.enabled = true;
		StartCoroutine("StopAnimation", true);
		//TODO If in animation of attack : do nothing. But when blocking : cancel attack
	}
	void Block()
	{
		StartCoroutine("StopAnimation", false);
	}
	IEnumerator StopAnimation(bool attacking)
	{
		if (attacking)
		{
			yield return new WaitForSeconds(_animation.clip.length);
			//_animation.Stop();		
			trailRenderer.enabled = false;
			_mayAttack = true;
			yield return new WaitForEndOfFrame();
		}else
		{
			if (GetComponent<BoxCollider>().enabled)
				GetComponent<BoxCollider>().enabled = false;
			_animation.Play("Sword_Block");
			trailRenderer.enabled = false;
			_mayAttack = true;
			yield return new WaitForEndOfFrame();
		}
	}
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == Tags.Enemy && other.gameObject.GetComponent<Health>())
		{
			other.gameObject.GetComponent<Health>().TakeDamage(_damage);
		}
	}
	public int GetSetDamage
	{
		get { return _damage; }
		set { _damage = value; }
	}
}
