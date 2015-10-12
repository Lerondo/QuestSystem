using UnityEngine;
using System.Collections;

public class Steel_Helm : Item {
	
	public Steel_Helm()
	{
		itemClass = ItemClass.Helm;
		_itemName = "Steel Helm";
		_itemLevel = 3;
		_itemPhysicalDefense = 30;
		_itemMagicalDefense = 25;
		_itemBuyValue = 120;
		_itemSellValue = Mathf.RoundToInt(_itemBuyValue / 4);
		_itemSprite = "Sprites/Items/Helm1";
	}
}
