using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class Inventory : MonoBehaviour {

	private int _gold;
	private int _invAmount;
	[SerializeField] private Text _goldText;
	[SerializeField] private Text _itemStatsText;
	[SerializeField] private GameObject _itemStatsPanel;
	[SerializeField] private GameObject _inventoryPanel;
	[SerializeField] private Sprite _defaultsprite;
	private Button[] allButtons = new Button[42];
	public Button[] characterButtons = new Button[13];
	public string[] characterSlotNames = new string[13];
	private int buttonCounter;
	private int _characterCounter;

	void Start()
	{
		_itemStatsPanel.SetActive(false);
		_goldText.text = "Gold : " + _gold.ToString();
		GameObject[] allItemSlots = new GameObject[42];
		for (int i = 0; i <allItemSlots.Length; i++)
		{
			buttonCounter ++;
			allItemSlots[i] = GameObject.Find("Slot" + buttonCounter);
		}
		for (int i = 0; i < allItemSlots.Length; i++)
		{
			allButtons[i] = allItemSlots[i].GetComponent<Button>();
			if (i < 1)
				AddItem(ItemDatabase.itemList[Random.Range(0,ItemDatabase.itemList.Count)]);
		}
		for (int i = 0; i < characterSlotNames.Length; i ++)
		{
			characterButtons[_characterCounter].name = characterSlotNames[_characterCounter];
			_characterCounter ++;
		}
		ToggleInventory();
	}
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.I))
			ToggleInventory();
		if (_itemStatsPanel.activeInHierarchy == true)
		    _itemStatsPanel.transform.position = Input.mousePosition;
	}
	void ToggleInventory()
	{
		if (_inventoryPanel.activeInHierarchy == true)
		{
			_inventoryPanel.SetActive(false);
			CloseStats();
			GameObject.FindWithTag(Tags.Player).GetComponent<MouseLook>().enabled = true;
		}
		else
		{
			_inventoryPanel.SetActive(true);
			GameObject.FindWithTag(Tags.Player).GetComponent<MouseLook>().enabled = false;
		}
	}
	public void RemoveItem(Item item)
	{
		AddItem(item);
		GetSlotName(item).GetComponent<Image>().sprite = _defaultsprite;
		GetSlotName(item).GetComponentInChildren<Text>().text = GetSlotName(item).name;
		GetSlotName(item).GetComponent<EventTrigger>().enabled = false;
		GetSlotName(item).GetComponent<Button>().onClick.RemoveAllListeners();
		CloseStats();
		UpdatePlayerStats(item, true);
	}
	public void WearItem(Item item, Sprite itemSprite, int slot)
	{
		if (GetSlotName(item).GetComponent<Image>().sprite == _defaultsprite 
		    && item.getItemLevel <= GetComponent<PlayerStats>().getPlayerLevel)
		{
			_invAmount -= 1;
			allButtons[slot].GetComponent<Image>().sprite = _defaultsprite;
			allButtons[slot].GetComponent<EventTrigger>().enabled = false;
			allButtons[slot].GetComponent<Button>().onClick.RemoveAllListeners();
			GetSlotName(item).GetComponent<Image>().sprite = itemSprite;
			GetSlotName(item).GetComponentInChildren<Text>().text = "";
			GetSlotName(item).GetComponent<EventTrigger>().enabled = true;
			GetSlotName(item).GetComponent<Button>().onClick.AddListener(
				() =>
				{
					GetSlotName(item).GetComponent<Button>().onClick.RemoveAllListeners();
					RemoveItem(item);
				}
			);
			AddEventsToButton(0, item, false);
			UpdatePlayerStats(item, false);
			CloseStats();
		}
	}
	private GameObject GetSlotName(Item item)
	{
		return GameObject.Find(item.getItemClass.ToString());
	}

	public void AddItem(Item item)
	{
		int slot = 0;
		for (int i = 0; i < _invAmount; i++)
		{
			if(allButtons[slot].GetComponent<Image>().sprite != _defaultsprite)
				slot ++;
		}
		Sprite itemSprite = Resources.Load(item.itemSprite, typeof(Sprite)) as Sprite;
		allButtons[slot].GetComponent<Image>().sprite = itemSprite;
		allButtons[slot].GetComponent<Button>().onClick.AddListener(
			() =>
			{
				WearItem (item, itemSprite, slot);
			}
		);
		AddEventsToButton(slot, item, true);
		_invAmount += 1;
	}

	/*
	 * Damage 15
	 * 0.75 Attack Speed
	 * DMG * ATTACK SPEED = DPS
	 * 15 * 0.75 = 11.25
	 * 
	 * Damage 22
	 * 1.20 attack Speed
	 * 22 * 1.20 = 26.4
	 * 
	*/
	public void ShowStats(int buttonSlot, Item item)
 	{
		_itemStatsPanel.SetActive(true);
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
			if (!CheckIfZero(item.getItemElementalDamage))
				stats += item.getItemElementalDamage + " " + item.getItemElement + " Damage" + "\n";
			stats += (item.getItemAttackSpeed + 1 ) + " Attack Speed" + "\n";
			stats += ((item.getItemAttackSpeed + 1 ) * item.getItemPhysicalDamage).ToString() + " DPS" + "\n";
			stats += item.getItemCritChance + " Crit Chance" + "\n";
		}else
		{
			if (!CheckIfZero(item.getItemPhysicalDefense))
				stats += item.getItemPhysicalDefense + " Physical Defense" + "\n";
			if (!CheckIfZero(item.getItemMagicalDefense))
				stats += item.getItemMagicalDefense + " Magical Defense" + "\n";
		}
		stats += "Sell Value : " + item.getItemSellValue;
		_itemStatsText.text = stats;
	}
	public bool CheckIfZero(int currentStat)
	{
		if(currentStat > 0)
		{
			return false;
		}
		return true;
	}
	public void CloseStats()
	{
		_itemStatsText.text = "";
		_itemStatsPanel.SetActive(false);
	}

	private void AddEventsToButton(int slot, Item item, bool toWear)
	{
		//Add OnPointerEnter Event
		EventTrigger.TriggerEvent trigger = new EventTrigger.TriggerEvent();
		EventTrigger.Entry entry = new EventTrigger.Entry();
		
		trigger.AddListener((eventData) => ShowStats(slot, item));
		entry.eventID = EventTriggerType.PointerEnter;
		entry.callback = trigger;
		
		//Add OnPointerExit Event
		EventTrigger.TriggerEvent trigger2 = new EventTrigger.TriggerEvent();
		EventTrigger.Entry entry2 = new EventTrigger.Entry();
		
		trigger2.AddListener((eventData) => CloseStats());
		entry2.eventID = EventTriggerType.PointerExit;
		entry2.callback = trigger2;

		if (toWear == true)
		{
			allButtons[slot].GetComponent<EventTrigger>().enabled = true;
			allButtons[slot].GetComponent<EventTrigger>().triggers.Add(entry);
			allButtons[slot].GetComponent<EventTrigger>().triggers.Add(entry2);
		}
		else
		{
			GetSlotName(item).GetComponent<EventTrigger>().triggers.Add(entry);
			GetSlotName(item).GetComponent<EventTrigger>().triggers.Add(entry2);
		}
	}

	public void ChangeGold (int amount)
	{
		_gold += amount;
		_goldText.text = "Gold : " + _gold.ToString();
	}
	private void UpdatePlayerStats(Item item, bool subract)
	{
		PlayerStats playerStats = GetComponent<PlayerStats>();
		if (subract == false)
		{
			playerStats.getCritChance += item.getItemCritChance;
			playerStats.getAttackSpeed += item.getItemAttackSpeed;
			playerStats.getPhysicalDamage += item.getItemPhysicalDamage;
			playerStats.getMagicalDamage += item.getItemMagicalDamage;
			playerStats.getElementalDamage += item.getItemElementalDamage;
			playerStats.getPhysicalDefense += item.getItemPhysicalDefense;
			playerStats.getMagicalDefense += item.getItemMagicalDefense;
			if (playerStats.getElement == "Normal")
				playerStats.getElement = item.getItemElement.ToString();
		}
		else
		{
			playerStats.getCritChance -= item.getItemCritChance;
			playerStats.getAttackSpeed -= item.getItemAttackSpeed;
			playerStats.getPhysicalDamage -= item.getItemPhysicalDamage;
			playerStats.getMagicalDamage -= item.getItemMagicalDamage;
			playerStats.getElementalDamage -= item.getItemElementalDamage;
			playerStats.getPhysicalDefense -= item.getItemPhysicalDefense;
			playerStats.getMagicalDefense -= item.getItemMagicalDefense;
			if (playerStats.getElement == item.getItemElement.ToString())
				playerStats.getElement = "Normal";
		}
		playerStats.UpdateStats();
	}
	public int getGold
	{
		get { return _gold; }
		set { _gold = value; }
	}
}