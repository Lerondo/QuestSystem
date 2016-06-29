using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestGetItems : MonoBehaviour {

	public List<Item> itemlist = new List<Item>();
	public List<UnityEditor.MonoScript> items = new List<UnityEditor.MonoScript>();

	void Start ()
	{
		foreach(UnityEditor.MonoScript obj in Resources.LoadAll("Items/", typeof(UnityEditor.MonoScript)))
		{
			items.Add(obj);
			Debug.Log(obj.name + " = " + obj.GetType());
		}
	}
	void Update()
	{
		//if (Input.GetKeyDown(KeyCode.X))
			//itemlist.Add(items[0]);
	}
}
