using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Health : MonoBehaviour {
	
	[SerializeField] int health;
	
	public void TakeDamage(int damage) 
	{
		health = health - damage;
		if (health <= 0)
			OnDeath();
	}
	public int getHealth()
	{
		return health;
	}
	private void OnDeath()
	{
		if(GetComponent<Enemy_Behaviour>())
			GetComponent<Enemy_Behaviour>().CheckQuestObjectiveOnDeath();
		else if (GetComponent<NPC_Behaviour>())
			Destroy(this.gameObject);
		else
			GameObject.FindWithTag(Tags.GameController).GetComponent<PlayerRespawn>().Respawn();
	}
}
