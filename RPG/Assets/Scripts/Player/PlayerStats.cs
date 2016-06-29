using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour {

	[SerializeField] private Text _statsText;
	[SerializeField] private Text _statsText2;
	[SerializeField] private Text _expText;
	[SerializeField] private Slider _expSlider;
	[SerializeField] private GameObject _helpPanel;

	[SerializeField] private Text _healthText;
	[SerializeField] private Text _manaText;
	[SerializeField] private Slider _healthSlider;
	[SerializeField] private Slider _manaSlider;

	private Health playerHealth;
	private Mana playerMana;

	private int _experience = 0;
	protected int playerLevel = 5;
	protected int physicalDamage = 15;
	protected int magicalDamage = 0;
	protected int elementalDamage = 0;
	protected string currentElement = "Normal";
	protected float attackSpeed = 1.00f;
	protected int criticalHitChance;
	protected int physicalDefense;
	protected int magicalDefense;

	public List<string> equipedList = new List<string>();

	void Start()
	{
		playerHealth = GameObject.FindGameObjectWithTag(Tags.Player).GetComponent<Health>();
		playerMana = GameObject.FindGameObjectWithTag(Tags.Player).GetComponent<Mana>();
		UpdateStats(true);
		UpdateStats(false);
		UpdateSliders();
		AddExperience(0);		
		GameObject.Find("RightHand").GetComponent<PlayerCombat>().GetSetDamage = (physicalDamage + magicalDamage + elementalDamage);
		//ToggleHelpPanel();
	}
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.H))
		{
			UpdateStats(true);
			UpdateStats(false);
			ToggleHelpPanel();
		}
	}
	private void ToggleHelpPanel()
	{
		if (_helpPanel.activeInHierarchy == true)
		{
			_helpPanel.SetActive(false);
			GameObject.FindWithTag(Tags.Player).GetComponent<MouseLook>().enabled = true;
			GameObject.FindWithTag(Tags.MainCam).GetComponent<MouseLook>().enabled = true;
		}
		else
		{
			_helpPanel.SetActive(true);
			GameObject.FindWithTag(Tags.Player).GetComponent<MouseLook>().enabled = false;
			GameObject.FindWithTag(Tags.MainCam).GetComponent<MouseLook>().enabled = false;
		}
	}
	public void AddExperience(int amount)
	{
		_experience += amount;
		_expSlider.value = _experience;
		if (_experience >= _expSlider.maxValue)
		{
			_experience -= Mathf.FloorToInt(_expSlider.maxValue);
			LevelUp();
		}
		UpdateStats(false);
		_expText.text = _experience.ToString() + " / " + _expSlider.maxValue;
	}
	private void LevelUp()
	{
		playerLevel += 1;
		_expSlider.maxValue = Mathf.Floor(_expSlider.maxValue * 2.5f);
		_expSlider.value = _experience;
		UpdateStats(true);
		UpdateStats(false);
	}
	public void UpdateStats(bool mainStats)
	{
		if (mainStats)
		{
			string stats = "";
			stats += "Level : " + playerLevel + "\n" + "\n";
			stats += "Physical Damage : " + physicalDamage + "\n" + "\n";
			stats += "Magical Damage : " + magicalDamage + "\n" + "\n";
			if (elementalDamage > 0)
				stats += "Elemental Damage : " + currentElement + " " + elementalDamage + "\n" + "\n";
			stats += "Attack Speed : " + attackSpeed.ToString("F2") + "\n" + "\n";
			stats += "Crit Chance : " + criticalHitChance + "%" + "\n" + "\n";
			stats += "Physical Defense : " + physicalDefense + "\n" + "\n";
			stats += "Magical Defense : " + magicalDefense;
			GameObject.Find("RightHand").GetComponent<PlayerCombat>().GetSetDamage = (physicalDamage + magicalDamage + elementalDamage);
			_statsText.text = stats;
		}else
		{
			string stats = "";
			stats += "Health : " + playerHealth.getHealth + " / " + playerHealth.getMaxHealth() + "\n" + "\n";
			stats += "Mana : " + playerMana.getMana() + " / " + playerMana.getMaxMana() + "\n" + "\n";
			stats += "Experience : " + _experience.ToString() + "/" + _expSlider.maxValue.ToString();
			_statsText2.text = stats;
			UpdateSliders();
		}
	}
	public void UpdateSliders()
	{
		_healthSlider.maxValue = playerHealth.getMaxHealth();
		_healthSlider.value = playerHealth.getHealth;
		_manaSlider.maxValue = playerMana.getMaxMana();
		_manaSlider.value = playerMana.getMana();
		_healthText.text = "Health : " + playerHealth.getHealth + " / " + playerHealth.getMaxHealth();
		_manaText.text = "Mana : " + playerMana.getMana().ToString("F0") + " / " + playerMana.getMaxMana();
	}
	public int getPlayerLevel
	{
		get { return playerLevel; }
		set { playerLevel = value; }
	}
	public float getAttackSpeed
	{
		get { return attackSpeed; }
		set { attackSpeed = value; }
	}
	public int getCritChance
	{
		get { return criticalHitChance; }
		set { criticalHitChance = value; }
	}
	public int getPhysicalDamage
	{
		get { return physicalDamage; }
		set { physicalDamage = value; }
	}
	public int getMagicalDamage
	{
		get { return magicalDamage; }
		set { magicalDamage = value; }
	}
	public int getElementalDamage
	{
		get { return elementalDamage; }
		set { elementalDamage = value; }
	}
	public int getPhysicalDefense
	{
		get { return physicalDefense; }
		set { physicalDefense = value; }
	}
	public int getMagicalDefense
	{
		get { return magicalDefense; }
		set { magicalDefense = value; }
	}
	public string getElement
	{
		get { return currentElement; }
		set { currentElement = value; }
	}
}
