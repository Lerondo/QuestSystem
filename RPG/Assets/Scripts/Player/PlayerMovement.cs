using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	private float _moveSpeed = 3.5f;
	private GameObject _player;
	private bool _onGround = false;

	private Rigidbody _rigidBody;
	
	void Start()
	{
		_rigidBody = gameObject.GetComponent<Rigidbody> ();
		_player = GameObject.Find (Tags.Player);
	}	
	void Update () 
	{
		if (_player != null)
			if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
				MovePlayer ();

		if (Input.GetKeyDown (KeyCode.Space))
			Jump ();
	}	
	void MovePlayer()
	{
		float x = Input.GetAxis("Horizontal") * Time.smoothDeltaTime * _moveSpeed;
		float z = Input.GetAxis("Vertical") * Time.smoothDeltaTime * _moveSpeed;
		_player.transform.Translate (x, 0, z, Space.Self);

		if (Input.GetKey (KeyCode.LeftShift))
			_moveSpeed = 35f;
		else
			_moveSpeed = 3.5f;
	}
	void Jump()
	{
		if (_onGround)
		{
			_rigidBody.AddForce(0,275,0);
		}
	}
	void OnCollisionEnter (Collision other)
	{
		if (other.gameObject.tag == Tags.Ground)
			_onGround = true;
	}
	void OnCollisionExit (Collision other)
	{
		if (other.gameObject.tag == Tags.Ground)
			_onGround = false;
	}
}
