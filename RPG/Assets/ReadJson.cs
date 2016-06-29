using UnityEngine;
using System.Collections;
using System.IO;
using LitJson;

public class ReadJson : MonoBehaviour {

	private string jsonString;
	private JsonData itemData;

	void Start () 
	{
		jsonString = File.ReadAllText(Application.dataPath + "/Resources/Items/Items.json");
		itemData = JsonMapper.ToObject(jsonString);

		for (int i = 0; i < itemData["Weapons"].Count; i++)
		{
			Item item = new Item();
			//item.itemID = itemData["Weapons"][i]["id"].ToString();
			item.getItemSort = Item.ItemSort.Weapons;
			item.getItemName = itemData["Weapons"][i]["name"].ToString();
			//item.getItemClass = itemData["Weapons"][i]["itemClass"].ToString();
			//item.getItemPhysicalDamage = itemData["Weapons"][i]["damage"].ToString();
		}
	}

	/*
	JsonData GetItem(string name, string type)
	{
		for (int i = 0; i < itemData[type].Count; i++)
		{
			if (itemData[type][i]["name"].ToString() == name)
				return itemData[type][i];
		}
		return null;
	}
	*/
}
