using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour {

	[SerializeField] private Text _statsText;
	[SerializeField] private Text _expText;
	[SerializeField] private Slider _expSlider;
	[SerializeField] private GameObject _helpPanel;

	private int _experience = 0;
	protected int playerLevel = 5;
	protected int physicalDamage;
	protected int magicalDamage;
	protected int elementalDamage;
	protected string currentElement = "Normal";
	protected float attackSpeed = 1.00f;
	protected int criticalHitChance;
	protected int physicalDefense;
	protected int magicalDefense;

	void Start()
	{
		UpdateStats();
		AddExperience(0);
		ToggleHelpPanel();
	}
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.H))
			ToggleHelpPanel();
	}
	private void ToggleHelpPanel()
	{
		if (_helpPanel.activeInHierarchy == true)
		{
			_helpPanel.SetActive(false);
			GameObject.FindWithTag(Tags.Player).GetComponent<MouseLook>().enabled = true;
		}
		else
		{
			_helpPanel.SetActive(true);
			GameObject.FindWithTag(Tags.Player).GetComponent<MouseLook>().enabled = false;
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
		_expText.text = _experience.ToString() + " / " + _expSlider.maxValue;
	}
	private void LevelUp()
	{
		playerLevel += 1;
		UpdateStats();
		_expSlider.maxValue = Mathf.Floor(_expSlider.maxValue * 2.5f);
		_expSlider.value = _experience;
	}
	public void UpdateStats()
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
		_statsText.text = stats;
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
