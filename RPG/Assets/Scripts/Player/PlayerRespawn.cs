using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerRespawn : MonoBehaviour {

	private Vector3 _spawnPointPos;

	public void Respawn()
	{
		GameObject[] enemies;
		enemies = GameObject.FindGameObjectsWithTag(Tags.Enemy);
		foreach (GameObject enemy in enemies)
		{
			if (enemy.GetComponent<Enemy_Behaviour>().myTarget = GameObject.FindWithTag(Tags.Player))
				enemy.GetComponent<Enemy_Behaviour>().myTarget = null;
		}
	}
	private void ActivateSpawnPoint()
	{

	}
}
