using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class Chest : MonoBehaviour {

	public GameObject lootPanel;
	public GameObject itemSpot;
	public GameObject lootNote;
	public GameObject itemButtonPrefab;
	public GameObject itemStatsPanel;
	public Button takeGoldButton;
	public Button takeAllButton;
	public Text itemStatsText;
	public Text lootText;
	public int goldMin = 0;
	public int goldMax = 0;
	public int lootMin = 0;
	public int lootMax = 0;

	private List<Item> lootList = new List<Item>();
	private Inventory _inventory;
	private int amountOfLoot;
	private int gold;
	private bool mayOpen;
	private bool looted;

	void Start()
	{
		_inventory = GameObject.FindGameObjectWithTag(Tags.GameController).GetComponent<Inventory>();
		gold = Random.Range(goldMin,goldMax);
		amountOfLoot = Random.Range(lootMin,lootMax);
		mayOpen = false;
		looted = false;
		itemStatsPanel.SetActive(false);
		lootPanel.SetActive(false);
		lootNote.SetActive(false);
	}
	void Update()
	{
		if (mayOpen && Input.GetKeyDown(KeyCode.E))
			ShowChestInventory();
		if (itemStatsPanel.activeInHierarchy)
			itemStatsPanel.transform.position = Input.mousePosition;
	}
	private void AddItem(Item item)
	{
		GameObject button = (GameObject)Instantiate (itemButtonPrefab);
		button.name = "item" + item.getItemName;
		button.GetComponentInChildren<Text>().text = item.getItemName;
		if (item.getItemSort != Item.ItemSort.QuestItem)
		{
			button.GetComponent<Button>().onClick.AddListener
				(() =>{
					TakeItem (item, button);
				});
		}
		AddEventsToButton(button, item);
		button.transform.SetParent(itemSpot.transform);
	}
	private void TakeItem(Item item, GameObject button)
	{
		_inventory.AddItem(item);
		lootList.Remove(item);
		Destroy(button);
		CloseStats();
		looted = true;
	}
	public void ShowStats(Item item)
	{
		itemStatsPanel.SetActive(true);
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
	public void CloseStats()
	{
		itemStatsText.text = "";
		itemStatsPanel.SetActive(false);
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

		//Add OnPointerExit Event
		EventTrigger.TriggerEvent trigger2 = new EventTrigger.TriggerEvent();
		EventTrigger.Entry entry2 = new EventTrigger.Entry();
		
		trigger2.AddListener((eventData) => CloseStats());
		entry2.eventID = EventTriggerType.PointerExit;
		entry2.callback = trigger2;

		button.GetComponent<EventTrigger>().triggers.Add(entry2);

	}
	private void ShowChestInventory()
	{
		if (lootPanel.activeInHierarchy == true)
		{
			takeAllButton.onClick.RemoveAllListeners();
			takeGoldButton.onClick.RemoveAllListeners();
			DeleteItemsFromUI();
			PreventAttacking();
			lootPanel.SetActive(false);
			GameObject.FindWithTag(Tags.Player).GetComponent<MouseLook>().enabled = true;
			GameObject.FindWithTag(Tags.MainCam).GetComponent<MouseLook>().enabled = true;
		}
		else
		{
			takeAllButton.onClick.AddListener
				(() =>{
					TakeAll();
				});
			takeGoldButton.onClick.AddListener
				(() =>{
					TakeGold();
				});
			lootPanel.SetActive(true);
			PreventAttacking();
			CheckIfHasItems();
			GameObject.FindWithTag(Tags.Player).GetComponent<MouseLook>().enabled = false;
			GameObject.FindWithTag(Tags.MainCam).GetComponent<MouseLook>().enabled = false;
		}
	}
	private void PreventAttacking()
	{
		GameObject.Find("RightHand").GetComponent<PlayerCombat>().InMenu();
	}
	public void TakeAll()
	{
		looted = true;
		for (int i = 0; i < lootList.Count; i ++)
		{
			_inventory.AddItem(lootList[i]);
		}
		foreach (Transform objct in itemSpot.GetComponentInChildren<Transform>())
		{
			Destroy(objct.gameObject);
		}
		lootList.Clear();
		_inventory.ChangeGold(gold);
		gold = 0;
		lootText.text = "Chest" + "\n" + "Gold : " + gold;
	}
	public void TakeGold()
	{
		_inventory.ChangeGold(gold);
		gold = 0;
		lootText.text = "Chest" + "\n" + "Gold : " + gold;
	}
	private void CheckIfHasItems()
	{
		if (lootList.Count > 0)
		{
			for (int i = 0; i < lootList.Count; i ++)
			{
				AddItem(lootList[i]);
			}
		}else
		{
			if (!looted)
			{
				for (int i = 0; i < amountOfLoot; i ++)
				{
					Item item;
					item = ItemDatabase.itemList[Random.Range(0, ItemDatabase.itemList.Count - 1)];
					AddItem(item);
					lootList.Add(item);
				}
			}
		}
		lootText.text = "Chest" + "\n" + "Gold : " + gold;
	}
	private void DeleteItemsFromUI()
	{
		foreach (Transform item in itemSpot.GetComponentInChildren<Transform>())
		{
			Destroy(item.gameObject);
		}
	}
	void OnTriggerEnter()
	{
		mayOpen = true;
		lootNote.SetActive(true);
	}
	void OnTriggerExit()
	{
		lootNote.SetActive(false);
		if (lootPanel.activeInHierarchy)
			ShowChestInventory();
		mayOpen = false;
	}
}
