using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEditor;
using System;

[CustomEditor (typeof(CreateItem))]
public class CreateItemEditor : Editor {

	public enum ItemSort
	{
		Weapon,
		Armor
	}

	public ItemSort itemSort;
	private string message = "";
	public string weaponName = "";
	public float attackSpeed = 1f;
	public int physicalDamage = 0;
	public int magicalDamage = 0;
	public int itemLevel = 0;
	public int buyValue = 0;
	public Item.ItemElement itemElement;
	private bool openElements = false;

	[SerializeField] private CreateItem createItem;

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		EditorGUILayout.BeginHorizontal();
	
		if (createItem == null)
		{
			createItem = (CreateItem)EditorGUILayout.ObjectField("Create Item", createItem, typeof(CreateItem), true);
		}

		if (createItem != null)
		{
			if (createItem.ShowMe)
			{
				GUILayout.Label("SHOWING");

			}else
			{
				GUILayout.Label("HIDING");

			}
		}
		EditorGUILayout.EndHorizontal();

		CreateItem createItemScript = (CreateItem)target;
		Item item = new Item();

		GUILayout.Box(message);
		if (itemSort == ItemSort.Weapon)
		{
			if (GUILayout.Button("Switch to Create Armor"))
			{
				itemSort = ItemSort.Armor;
			}
				
			GUILayout.Label("Weapon Name");
			weaponName = GUILayout.TextField(weaponName);

			GUILayout.Label("Physical Damage");
			physicalDamage = EditorGUILayout.IntField(physicalDamage);

			GUILayout.Label("Magical Damage");
			magicalDamage = EditorGUILayout.IntField(magicalDamage);

			GUILayout.Label("Weapon Attack Speed = " + attackSpeed.ToString("F2"));
			attackSpeed = GUILayout.HorizontalSlider(attackSpeed, 0.5f, 1.5f);
			//attackSpeed = GUILayout.TextField(attackSpeed);
			//attackSpeed = Regex.Replace(attackSpeed, "[^0-9]", "");

			GUILayout.Label("Weapon Requirement Level");
			itemLevel = EditorGUILayout.IntField(itemLevel);

			GUILayout.Label("Buy Value");
			buyValue = EditorGUILayout.IntField(buyValue);

			if (GUILayout.Button("Selected Element : " + itemElement))
				openElements = openElements == false ? true : false;
			
			if (openElements)
			{
				if (GUILayout.Button("Element : Normal"))
					itemElement = Item.ItemElement.Normal;
				if (GUILayout.Button("Element : Dark"))
					itemElement = Item.ItemElement.Dark;
				if (GUILayout.Button("Element : Earth"))
					itemElement = Item.ItemElement.Earth;
				if (GUILayout.Button("Element : Fire"))
					itemElement = Item.ItemElement.Fire;
				if (GUILayout.Button("Element : Frost"))
					itemElement = Item.ItemElement.Frost;
				if (GUILayout.Button("Element : Light"))
					itemElement = Item.ItemElement.Light;
				if (GUILayout.Button("Element : Lightning"))
					itemElement = Item.ItemElement.Lightning;
				if (GUILayout.Button("Element : Poison"))
					itemElement = Item.ItemElement.Poison;
				if (GUILayout.Button("Element : Silver"))
					itemElement = Item.ItemElement.Silver;
				if (GUILayout.Button("Element : Water"))
					itemElement = Item.ItemElement.Water;
			}
			if (GUILayout.Button("Create Weapon"))
			{
				if (weaponName != "" && (physicalDamage != 0 || magicalDamage != 0) && itemLevel != 0)
				{
					item.getItemName = weaponName;
					item.getItemAttackSpeed = Mathf.Floor(attackSpeed * 100) / 100;
					item.getItemLevel = itemLevel;
					item.getItemPhysicalDamage = physicalDamage;
					item.getItemMagicalDamage = magicalDamage;
					item.getItemBuyValue = buyValue;
					item.getItemSellValue = buyValue / 4;	
					item.getItemElement = itemElement;
					item.itemClass = Item.ItemClass.Weapon;
					item.getItemEquipAble = true;	

					weaponName = "";
					attackSpeed = 1f;
					itemLevel = 0;
					physicalDamage = 0;
					magicalDamage = 0;
					buyValue = 0;
					itemElement = Item.ItemElement.Normal;

					createItemScript.CreateNewWeapons(item);
					message = "";
				}else
					message = "Fill in all the fields!";
			}
		}else if (itemSort == ItemSort.Armor)
		{
			if (GUILayout.Button("Switch to Create Weapon"))
			{
				itemSort = ItemSort.Weapon;
			}
			if (GUILayout.Button("BAG"))
				Debug.Log("SUP");
		}
	}
}
