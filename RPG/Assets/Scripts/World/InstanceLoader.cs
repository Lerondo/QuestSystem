using UnityEngine;
using System.Collections;

public class InstanceLoader : MonoBehaviour {

	public GameObject locationToEnter;
	public GameObject currentLocation;
	public Vector3 spawnPos;

	void Start()
	{
		locationToEnter.SetActive(false);
	}
	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == Tags.Player)
		{
			if (Input.GetKeyDown(KeyCode.E))
			{
				other.gameObject.transform.position = spawnPos;
				locationToEnter.SetActive(true);
				currentLocation.SetActive(false);
			}
		}
	}
}
