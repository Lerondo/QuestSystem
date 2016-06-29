using UnityEngine;
using System.Collections;
using System;

public class CreateItem : MonoBehaviour {

	[SerializeField] private bool toShow;

	public bool ShowMe{get {return toShow;}}

	public void CreateNewWeapons(Item item)
	{
		Debug.Log("Name " + item.getItemName);
		Debug.Log("Attack Speed " + item.getItemAttackSpeed);
		Debug.Log("buy value " + item.getItemBuyValue);
		Debug.Log("Item Level " + item.getItemLevel);
		Debug.Log("Physical Damage " + item.getItemPhysicalDamage);
		Debug.Log("Magical Damage " + item.getItemMagicalDamage);
		Debug.Log("Element " + item.getItemElement);
	}
	public void CreateNewArmor(Item item)
	{
		
	}
}
