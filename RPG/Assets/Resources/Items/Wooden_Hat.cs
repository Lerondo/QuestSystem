using UnityEngine;
using System.Collections;

public class Wooden_Hat : Item {
	
	public Wooden_Hat()
	{
		itemClass = ItemClass.Helm;
		_itemName = "Wooden Hat";
		_itemLevel = 1;
		_itemPhysicalDefense = 2;
		_itemMagicalDefense = 1;
		_itemBuyValue = 10;
		_itemSellValue = Mathf.RoundToInt(_itemBuyValue / 4);
		_itemSprite = "Sprites/Items/Leggings1";
	}
}
