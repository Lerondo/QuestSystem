using UnityEngine;
using System.Collections;

public class Iron_Leggings : Item
{
	public Iron_Leggings()
	{
		itemClass = ItemClass.Leggings;
		_itemName = "Iron Leggins";
		_itemLevel = 2;
		_itemPhysicalDefense = 5;
		_itemMagicalDefense = 2;
		_itemBuyValue = 100;
		_itemSellValue = Mathf.RoundToInt(_itemBuyValue / 4);
		_itemSprite = "Sprites/Items/Leggings1";
	}
}

