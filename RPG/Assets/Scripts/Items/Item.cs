using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Item : System.Object {

	public enum ItemSort
	{
		Weapons,
		Armor,
		Aid,
		Misc,
		QuestItem
	}
	public enum ItemClass
	{
		Weapon,
		OffHand,
		Helm,
		BreastPlate,
		Leggings,
		Boots,
		Gloves,
		ShoulderPads,
		Cape,
		RingR,
		RingL,
		Necklace,
		Bracers
	}
	public enum ItemElement
	{
		Normal,
		Silver,
		Fire,
		Water,
		Frost,
		Earth,
		Lightning,
		Light,
		Dark,
		Poison
	}
	
	public int itemID;
	public ItemSort itemSort;
	public ItemClass itemClass;
	public ItemElement itemElement;
	protected bool _equiped;
	protected bool _equipAble;
	protected string _itemName;
	protected int _itemLevel;
	protected bool _ranged;

	protected float _attackSpeed;
	protected int _itemBuyValue;
	protected int _itemSellValue;
	protected int _itemPhysicalDamage;
	protected int _itemMagicalDamage;

	protected int _itemPhysicalDefense;
	protected int _itemMagicalDefense;
		
	protected string _itemSprite;

	public ItemSort getItemSort
	{
		get { return itemSort; }
		set { itemSort = value; }
	}
	public ItemClass getItemClass
	{
		get { return itemClass; }
		set { itemClass = value; }
	}
	public ItemElement getItemElement
	{
		get { return itemElement; }
		set { itemElement = value; }
	}
	public bool getItemEquiped
	{
		get { return _equiped; }
		set { _equiped = value; }
	}
	public bool getItemEquipAble
	{
		get { return _equipAble; }
		set { _equipAble = value; }
	}
	public string getItemName
	{
		get { return _itemName; }
		set { _itemName = value; }
	}
	public int getItemLevel
	{
		get { return _itemLevel; }
		set { _itemLevel = value; }
	}
	public float getItemAttackSpeed
	{
		get { return _attackSpeed; }
		set { _attackSpeed = value; }
	}
	public int getItemBuyValue
	{
		get { return _itemBuyValue; }
		set { _itemBuyValue = value; }
	}
	public int getItemSellValue
	{
		get { return _itemSellValue; }
		set { _itemSellValue = value; }
	}
	public int getItemPhysicalDamage
	{
		get { return _itemPhysicalDamage; }
		set { _itemPhysicalDamage = value; }
	}
	public int getItemMagicalDamage
	{
		get { return _itemMagicalDamage; }
		set { _itemMagicalDamage = value; }
	}
	public int getItemPhysicalDefense
	{
		get { return _itemPhysicalDefense; }
		set { _itemPhysicalDefense = value; }
	}
	public int getItemMagicalDefense
	{
		get { return _itemMagicalDefense; }
		set { _itemMagicalDefense = value; }
	}
	public string itemSprite
	{
		get { return _itemSprite; }
		set { _itemSprite = value; }
	}
}
