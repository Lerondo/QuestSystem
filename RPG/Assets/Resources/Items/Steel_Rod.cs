using UnityEngine;
using System.Collections;

public class Steel_Rod : Item {

	public Steel_Rod()
	{
		itemClass = ItemClass.Weapon;
		_itemName = "Steel Rod";
		_itemLevel = 2;
		_itemPhysicalDamage = 10;
		_itemBuyValue = 50;
		_itemSellValue = Mathf.RoundToInt(_itemBuyValue / 4);
		_itemSprite = "Sprites/Items/Leggings1";
	}
}
