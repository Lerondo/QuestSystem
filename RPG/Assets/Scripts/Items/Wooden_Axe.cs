using UnityEngine;
using System.Collections;

[System.Serializable]
public class Wooden_Axe : Item {

	public Wooden_Axe()
	{
		itemClass = ItemClass.Weapon;
		_itemName = "Wooden Axe";
		_itemLevel = 1;
		_attackSpeed = 0.20f;
		_itemPhysicalDamage = 3;
		itemElement = ItemElement.Normal;
		_itemBuyValue = 25;
		_itemSellValue = Mathf.RoundToInt(_itemBuyValue / 4);
		_itemSprite = "Sprites/Items/Axe1";
		_twoHanded = false;
	}
}
