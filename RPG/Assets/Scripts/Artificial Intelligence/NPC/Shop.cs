using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class Shop : MonoBehaviour {

	[SerializeField] private GameObject _shopPanel;
	[SerializeField] private List<Transform> _itemParentObjects = new List<Transform>();
	public GameObject itemButtonPrefab;
	public Text itemStatsText;
	public Text goldText;

	private int _shopAmount;
	private Inventory _inventory;

	void Start()
	{
		_inventory = GetComponent<Inventory>();
		ToggleShopInterface();
	}
	void Update()
	{		
		if (Input.GetKeyDown(KeyCode.N))
			AddItem(ItemDatabase.itemList[Random.Range(0, ItemDatabase.itemList.Count - 1)]);
	}
	public void ToggleShopInterface()
	{
		if (_shopPanel.activeInHierarchy == true)
		{
			_shopPanel.SetActive(false);
			GameObject.FindWithTag(Tags.Player).GetComponent<MouseLook>().enabled = true;
			GameObject.FindWithTag(Tags.MainCam).GetComponent<MouseLook>().enabled = true;
		}
		else
		{
			_shopPanel.SetActive(true);
			goldText.text = "Your Gold : " + _inventory.getGold;
			GameObject.FindWithTag(Tags.Player).GetComponent<MouseLook>().enabled = false;
			GameObject.FindWithTag(Tags.MainCam).GetComponent<MouseLook>().enabled = false;
		}
	}
	public void AddItem(Item newItem)
	{
		Item item;
		item = newItem;
		GameObject button = (GameObject)Instantiate (itemButtonPrefab);
		int i = 0;
		if (item.itemSort == Item.ItemSort.Weapons)
			i = 0;
		else if (item.itemSort == Item.ItemSort.Armor)
			i = 1;
		else if (item.itemSort == Item.ItemSort.Aid)
			i = 2;
		else if (item.itemSort == Item.ItemSort.Misc)
			i = 3;
		
		button.name = "item" + item.getItemName;
		button.GetComponentInChildren<Text>().text = item.getItemName;
		if (item.getItemSort != Item.ItemSort.QuestItem)
		{
			button.GetComponent<Button>().onClick.AddListener
				(() =>{
					BuyItem (item, button);
				});
		}
		AddEventsToButton(button, item);
		button.transform.SetParent(_itemParentObjects[i]);
	}
	public void BuyItem(Item item, GameObject button)
	{
		if (_inventory.getGold > item.getItemBuyValue)
		{
			_inventory.AddItem(item);
			_inventory.ChangeGold(-item.getItemBuyValue);
			goldText.text = "Your Gold : " + _inventory.getGold;
			Destroy(button);
		}
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
		stats += "Buy Value : " + item.getItemBuyValue;
		itemStatsText.text = stats;
	}
}
