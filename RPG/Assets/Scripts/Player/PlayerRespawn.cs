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
			if (enemy.GetComponent<NPC_Combat>().myTarget = GameObject.FindWithTag(Tags.Player))
				enemy.GetComponent<NPC_Combat>().myTarget = null;
		}
	}
	private void ActivateSpawnPoint()
	{

	}
}
