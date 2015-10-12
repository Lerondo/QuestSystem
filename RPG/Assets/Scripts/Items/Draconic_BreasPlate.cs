using UnityEngine;
using System.Collections;

public class Draconic_BreasPlate : Item {

	public Draconic_BreasPlate()
	{
		itemClass = ItemClass.BreastPlate;
		_itemName = "Draconic BreastPlate";
		_itemLevel = 1;
		_itemPhysicalDefense = 30;
		_itemMagicalDefense = 25;
		_itemBuyValue = 5950;
		_itemSellValue = Mathf.RoundToInt(_itemBuyValue / 4);
		_itemSprite = "Sprites/Items/DracoPlate";
	}
}
