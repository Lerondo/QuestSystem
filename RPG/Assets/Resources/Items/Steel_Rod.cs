using UnityEngine;
using System.Collections;

[System.Serializable]
public class Steel_Rod : Item {

	public Steel_Rod()
	{
		itemClass = ItemClass.Weapon;
		_itemName = "Steel Sword";
		_itemLevel = 5;
	}
}
