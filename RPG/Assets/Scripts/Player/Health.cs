using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Health : MonoBehaviour {
	
	[SerializeField] private int health;
	private int maxHealth;

	void Start()
	{
		maxHealth = health;
	}
	public void TakeDamage(int damage) 
	{
		Debug.Log(this.name + " Took " + damage + " Damage!");
		health = health - damage;
		if (this.gameObject.name == "Player")
		{
			UpdatePlayerHealth();
		}
		if (health <= 0)
			OnDeath();
	}
	private void UpdatePlayerHealth()
	{
		GameObject.FindGameObjectWithTag(Tags.GameController).GetComponent<PlayerStats>().UpdateStats(false);
	}
	public int getHealth
	{
		get {return health;}
		set {health = value;}
	}
	public int getMaxHealth()
	{
		return maxHealth;
	}
	private void OnDeath()
	{
		if(GetComponent<Enemy_Behaviour>())
			GetComponent<Enemy_Behaviour>().CheckQuestObjectiveOnDeath();
		else if (GetComponent<Skeleton>())
			GetComponent<Skeleton>().CheckQuestObjectiveOnDeath();
		else if (GetComponent<NPC_Behaviour>())
			Destroy(this.gameObject);
		else if (this.gameObject.name == "Player")
			GameObject.FindWithTag(Tags.GameController).GetComponent<PlayerRespawn>().Respawn();
	}
}
