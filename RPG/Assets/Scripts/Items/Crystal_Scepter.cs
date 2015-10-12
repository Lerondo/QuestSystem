using UnityEngine;
using System.Collections;

[System.Serializable]
public class Crystal_Scepter : Item {

	public Crystal_Scepter()
	{
		itemClass = ItemClass.Weapon;
		_itemName = "Crystal Scepter";
		_itemLevel = 12;
		_attackSpeed = -0.07f;
		_itemMagicalDamage = 10;
		_itemElementalDamage = 5;
		itemElement = ItemElement.Frost;
		_itemBuyValue = 950;
		_itemSellValue = Mathf.RoundToInt(_itemBuyValue / 4);
		_itemSprite = "Sprites/Items/Scepter1";
		_twoHanded = true;
	}
}
