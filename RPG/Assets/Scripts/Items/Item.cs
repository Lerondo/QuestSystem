using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Item : System.Object {

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
	public ItemClass itemClass;
	public ItemElement itemElement;
	protected string _itemName;
	protected int _itemLevel;
	protected bool _ranged;
	protected bool _twoHanded;

	protected float _attackSpeed;
	protected int _itemBuyValue;
	protected int _itemSellValue;
	protected int _critChance;
	protected int _itemPhysicalDamage;
	protected int _itemMagicalDamage;
	protected int _itemElementalDamage;

	protected int _itemPhysicalDefense;
	protected int _itemMagicalDefense;
		
	protected string _itemSprite;
	
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
	public int getItemCritChance
	{
		get { return _critChance; }
		set { _critChance = value; }
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
	public int getItemElementalDamage
	{
		get { return _itemElementalDamage; }
		set { _itemElementalDamage = value; }
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
