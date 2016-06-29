using UnityEngine;
using System.Collections;

[System.Serializable]
public class Steel_Sword : Item {

	public Steel_Sword()
	{
		itemSort = ItemSort.Weapons;
		itemClass = ItemClass.Weapon;
		_itemName = "Steel Sword";
		_itemLevel = 5;
		_attackSpeed = 0.05f;
		_itemPhysicalDamage = 18;
		itemElement = ItemElement.Normal;
		_itemBuyValue = 350;
		_itemSellValue = Mathf.RoundToInt(_itemBuyValue / 4);
		_itemSprite = "Sprites/Items/Sword1";
	}
}
