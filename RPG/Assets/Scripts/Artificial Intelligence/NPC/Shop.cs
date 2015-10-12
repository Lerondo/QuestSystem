using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class Shop : MonoBehaviour {

	[SerializeField] private Text _itemStatsText;
	[SerializeField] private Text _playerStatsText;
	[SerializeField] private GameObject _itemStatsPanel;
	[SerializeField] private GameObject _shopPanel;
	[SerializeField] private Sprite _defaultSprite;
	private Button[] allButtons = new Button[48];
	private int buttonCounter;
	private int _shopAmount;

	void Start()
	{
		SetPlayerStatsText();
		GameObject[] allShopSlots = new GameObject[48];
		for (int i = 0; i <allShopSlots.Length; i++)
		{
			buttonCounter ++;
			allShopSlots[i] = GameObject.Find("Slot" + buttonCounter);
		}
		for (int i = 0; i < allShopSlots.Length; i++)
		{
			allButtons[i] = allShopSlots[i].GetComponent<Button>();
			if (i < 25)
				AddItem(ItemDatabase.itemList[Random.Range(0,ItemDatabase.itemList.Count)]);
		}
		ToggleShopInterface();
	}
	void Update()
	{
		if (_itemStatsPanel.activeInHierarchy == true)
			_itemStatsPanel.transform.position = Input.mousePosition;
	}
	public void ToggleShopInterface()
	{
		if (_shopPanel.activeInHierarchy == true)
		{
			_shopPanel.SetActive(false);
			CloseStats();
			GameObject.FindWithTag(Tags.Player).GetComponent<MouseLook>().enabled = true;
		}
		else
		{
			_shopPanel.SetActive(true);
			SetPlayerStatsText();
			GameObject.FindWithTag(Tags.Player).GetComponent<MouseLook>().enabled = false;
		}
	}
	public void AddItem(Item item)
	{
		int slot = 0;
		for (int i = 0; i < _shopAmount; i++)
		{
			if(allButtons[slot].GetComponent<Image>().sprite != _defaultSprite)
				slot ++;
		}
		Sprite itemSprite = Resources.Load(item.itemSprite, typeof(Sprite)) as Sprite;
		allButtons[slot].GetComponent<Image>().sprite = itemSprite;
		allButtons[slot].GetComponent<Button>().onClick.AddListener(
			() =>
			{
			BuyItem (item, slot);
		}
		);
		AddEventsToButton(slot, item);
		_shopAmount += 1;
	}
	private void BuyItem(Item item, int slot)
	{
		if (GetComponent<Inventory>().getGold >= item.getItemBuyValue)
		{
			allButtons[slot].GetComponent<Image>().sprite = _defaultSprite;
			allButtons[slot].GetComponent<EventTrigger>().enabled = false;
			allButtons[slot].GetComponent<Button>().onClick.RemoveAllListeners();
			GetComponent<Inventory>().ChangeGold(-item.getItemBuyValue);
			SetPlayerStatsText();
			CloseStats();
			GetComponent<Inventory>().AddItem(item);
		}
	}
	private void AddEventsToButton(int slot, Item item)
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

		allButtons[slot].GetComponent<EventTrigger>().enabled = true;
		allButtons[slot].GetComponent<EventTrigger>().triggers.Add(entry);
		allButtons[slot].GetComponent<EventTrigger>().triggers.Add(entry2);
	}
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
			stats += item.getItemAttackSpeed + " Attack Speed" + "\n";
			stats += item.getItemCritChance + " Crit Chance" + "\n";
		}else
		{
			if (!CheckIfZero(item.getItemPhysicalDefense))
				stats += item.getItemPhysicalDefense + " Physical Defense" + "\n";
			if (!CheckIfZero(item.getItemMagicalDefense))
				stats += item.getItemMagicalDefense + " Magical Defense" + "\n";
		}
		stats += "Buy Value : " + item.getItemBuyValue;
		_itemStatsText.text = stats;
	}
	public void CloseStats()
	{
		_itemStatsText.text = "";
		_itemStatsPanel.SetActive(false);
	}
	public bool CheckIfZero(int currentStat)
	{
		if(currentStat > 0)
		{
			return false;
		}
		return true;
	}
	private void SetPlayerStatsText()
	{
		string stats = "";
		stats += "Level : " + GetComponent<PlayerStats>().getPlayerLevel + "\n" + "\n";
		stats += "Gold : " + GetComponent<Inventory>().getGold;
		_playerStatsText.text = stats;
	}
}
