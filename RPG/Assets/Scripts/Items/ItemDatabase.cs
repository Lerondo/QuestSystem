using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemDatabase : MonoBehaviour {

	public static List<Item> itemList = new List<Item>();
	
	void Awake () 
	{
		itemList.Add(new Wooden_Axe());
		itemList.Add(new Steel_Sword());
		itemList.Add(new Crystal_Scepter());
		itemList.Add(new Draconic_BreasPlate());
		itemList.Add(new Steel_Helm());
		itemList.Add(new Iron_Leggings());
		
		for(int i = 0; i < itemList.Count; i++)
		{
			itemList[i].itemID = i;
		}
	}
	public static List<Item> GetItemsViaId(List<int> itemIds)
	{
		List<Item> newItemList = new List<Item>();
		for(int i = 0; i < itemIds.Count;i++)
		{
			newItemList.Add(itemList[itemIds[i]]);
		}
		return newItemList;
	}
}
