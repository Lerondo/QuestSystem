using UnityEngine;
using System.Collections;

public class Mana : MonoBehaviour {

	[SerializeField] private float mana;
	private float maxMana;
	private float manaRegen = 4;
	
	void Start()
	{
		maxMana = mana;
	}
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.G))
			UseMana(30);
		if (mana < maxMana)
		{
			mana += manaRegen * Time.deltaTime;
			UpdatePlayerMana();
		}
		if (mana > maxMana)
			mana = maxMana;
	}
	public void UseMana(float manaAmount) 
	{
		if (mana - manaAmount > 0)
		{
			mana = mana - manaAmount;
			if (this.gameObject.name == "Player")
				UpdatePlayerMana();
		}
	}
	private void UpdatePlayerMana()
	{
		GameObject.FindGameObjectWithTag(Tags.GameController).GetComponent<PlayerStats>().UpdateSliders();
	}
	public float getMana()
	{
		return mana;
	}
	public float getMaxMana()
	{
		return maxMana;
	}
}
