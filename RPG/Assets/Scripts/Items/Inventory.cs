using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class Inventory : MonoBehaviour {

	private int _gold = 0;
	[SerializeField] private Text _goldText;
	[SerializeField] private GameObject _inventoryPanel;
	[SerializeField] private List<Transform> _itemParentObjects = new List<Transform>();
	public GameObject itemButtonPrefab;
	public Text itemStatsText;

	private PlayerStats _playerStats;

	void Start()
	{
		_playerStats = GetComponent<PlayerStats>();
		_goldText.text = "Gold : " + _gold.ToString();
		_inventoryPanel.SetActive(false);
	}
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.I))
			ToggleInventory();
		if (Input.GetKeyDown(KeyCode.N))
			AddItem(ItemDatabase.itemList[Random.Range(0, ItemDatabase.itemList.Count - 1)]);
		
		if (Input.GetKeyDown(KeyCode.B))
			ChangeGold(100);
	}
	void ToggleInventory()
	{
		if (_inventoryPanel.activeInHierarchy == true)
		{
			_inventoryPanel.SetActive(false);
			PreventAttacking();
			GameObject.FindWithTag(Tags.Player).GetComponent<MouseLook>().enabled = true;
			GameObject.FindWithTag(Tags.MainCam).GetComponent<MouseLook>().enabled = true;
		}
		else
		{
			_inventoryPanel.SetActive(true);
			PreventAttacking();
			GameObject.FindWithTag(Tags.Player).GetComponent<MouseLook>().enabled = false;
			GameObject.FindWithTag(Tags.MainCam).GetComponent<MouseLook>().enabled = false;
		}
	}	
	private void PreventAttacking()
	{
		GameObject.Find("RightHand").GetComponent<PlayerCombat>().InMenu();
	}
	public void AddItem(Item newItem)
	{
		Item item;
		item = newItem;
		GameObject button = (GameObject)Instantiate (itemButtonPrefab);
		int i = 0;
		//item.itemSort = Item.ItemSort.Weapons; // DELETE LATER
		if (item.itemSort == Item.ItemSort.Weapons)
			i = 0;
		else if (item.itemSort == Item.ItemSort.Armor)
			i = 1;
		else if (item.itemSort == Item.ItemSort.Aid)
			i = 2;
		else if (item.itemSort == Item.ItemSort.Misc)
			i = 3;
		else if (item.itemSort == Item.ItemSort.QuestItem)
			i = 4;

		button.name = "item" + item.getItemName;
		button.GetComponentInChildren<Text>().text = item.getItemName;
		if (item.getItemSort != Item.ItemSort.QuestItem)
		{
			button.GetComponent<Button>().onClick.AddListener
			(() =>{
				EquipItem (item, button);
			});
		}
		AddEventsToButton(button, item);
		button.transform.SetParent(_itemParentObjects[i]);
	}
	public void EquipItem(Item item, GameObject button)
	{
		if (item.getItemEquipAble && item.getItemLevel <= _playerStats.getPlayerLevel && !_playerStats.equipedList.Contains(item.getItemClass.ToString()))
		{
			_playerStats.equipedList.Add(item.getItemClass.ToString());
			button.GetComponentInChildren<Text>().text = "Equiped : " + item.getItemName;
			button.GetComponentInChildren<Text>().color = Color.gray;
			item.getItemEquiped = true;
			UpdatePlayerStats(item, false);
			button.GetComponent<Button>().onClick.RemoveAllListeners();
			button.GetComponent<Button>().onClick.AddListener(
			() =>{
				UnEquipItem (item, button);
			});
		}
	}
	public void UnEquipItem(Item item, GameObject button)
	{
		_playerStats.equipedList.Remove(item.getItemClass.ToString());
		button.GetComponentInChildren<Text>().text = item.getItemName;
		button.GetComponentInChildren<Text>().color = Color.black;
		item.getItemEquiped = false;
		UpdatePlayerStats(item, true);
		button.GetComponent<Button>().onClick.RemoveAllListeners();
		button.GetComponent<Button>().onClick.AddListener(
			() =>{
			EquipItem (item, button);
		});
	}
	public void ShowStats(Item item)
 	{
		string stats = "";
		stats += item.itemClass + "\n";
		stats += item.getItemName + "\n";
		stats += "Level : " + item.getItemLevel + " Required" + "\n";
		if (item.itemClass == Item.ItemClass.Weapon || item.itemClass == Item.ItemClass.OffHand)
		{
			if (!CheckIfZero(item.getItemPhysicalDamage))
				stats += item.getItemPhysicalDamage + " Physical Damage" + "\n";
			if (!CheckIfZero(item.getItemMagicalDamage))
				stats += item.getItemMagicalDamage + " Magical Damage" + "\n";
			stats += (item.getItemAttackSpeed + 1 ) + " Attack Speed" + "\n";
			stats += ((item.getItemAttackSpeed + 1 ) * item.getItemPhysicalDamage).ToString() + " DPS" + "\n";
		}else
		{
			if (!CheckIfZero(item.getItemPhysicalDefense))
				stats += item.getItemPhysicalDefense + " Physical Defense" + "\n";
			if (!CheckIfZero(item.getItemMagicalDefense))
				stats += item.getItemMagicalDefense + " Magical Defense" + "\n";
		}
		stats += "Sell Value : " + item.getItemSellValue;
		itemStatsText.text = stats;
	}
	public bool CheckIfZero(int currentStat)
	{
		if(currentStat > 0)
			return false;
		return true;	
	}
	private void AddEventsToButton(GameObject button, Item item)
	{
		//Add OnPointerEnter Event
		EventTrigger.TriggerEvent trigger = new EventTrigger.TriggerEvent();
		EventTrigger.Entry entry = new EventTrigger.Entry();
		
		trigger.AddListener((eventData) => ShowStats(item));
		entry.eventID = EventTriggerType.PointerEnter;
		entry.callback = trigger;

		button.GetComponent<EventTrigger>().triggers.Add(entry);
		/*
		//Add OnPointerExit Event
		EventTrigger.TriggerEvent trigger2 = new EventTrigger.TriggerEvent();
		EventTrigger.Entry entry2 = new EventTrigger.Entry();
		
		//trigger2.AddListener((eventData) => CloseStats());
		entry2.eventID = EventTriggerType.PointerExit;
		entry2.callback = trigger2;
		*/
	}

	public void ChangeGold (int amount)
	{
		_gold += amount;
		_goldText.text = "Gold : " + _gold.ToString();
	}
	private void UpdatePlayerStats(Item item, bool subract)
	{
		if (subract == false)
		{
			_playerStats.getAttackSpeed += item.getItemAttackSpeed;
			_playerStats.getPhysicalDamage += item.getItemPhysicalDamage;
			_playerStats.getMagicalDamage += item.getItemMagicalDamage;
			_playerStats.getPhysicalDefense += item.getItemPhysicalDefense;
			_playerStats.getMagicalDefense += item.getItemMagicalDefense;
			if (_playerStats.getElement == "Normal")
				_playerStats.getElement = item.getItemElement.ToString();
		}
		else
		{
			_playerStats.getAttackSpeed -= item.getItemAttackSpeed;
			_playerStats.getPhysicalDamage -= item.getItemPhysicalDamage;
			_playerStats.getMagicalDamage -= item.getItemMagicalDamage;
			_playerStats.getPhysicalDefense -= item.getItemPhysicalDefense;
			_playerStats.getMagicalDefense -= item.getItemMagicalDefense;
			if (_playerStats.getElement == item.getItemElement.ToString())
				_playerStats.getElement = "Normal";
		}
		_playerStats.UpdateStats(true);
	}
	public int getGold
	{
		get { return _gold; }
		set { _gold = value; }
	}
}