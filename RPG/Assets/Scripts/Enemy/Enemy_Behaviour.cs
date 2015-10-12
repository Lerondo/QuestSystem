using UnityEngine;
using System.Collections;

public class Enemy_Behaviour : MonoBehaviour{

	[SerializeField] int questID;
	[SerializeField] int questAmount;
	[SerializeField] private int _experience;

	public void AddExperience()
	{
		GameObject.FindWithTag(Tags.GameController).GetComponent<PlayerStats>().AddExperience(_experience);
	}
	public void CheckQuestObjectiveOnDeath()
	{
		if (QuestDatabase.questList[questID].getQuestAccepted == true &&
		    QuestDatabase.questList[questID].getQuestCompleted == false)
			GameObject.FindWithTag(Tags.GameController).GetComponent<QuestTracker>().UpdateQuest(questID, questAmount);
		AddExperience();
		Destroy(this.gameObject);
	}
}

