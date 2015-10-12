using UnityEngine;
using System.Collections;

public class QuestTracker : MonoBehaviour {

	// Kill 12 wolves, kill 1 wolf with T, in QuestLog Track number of Wolves Left

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.R) && QuestDatabase.questList[0].getQuestAccepted == true)
			GameObject.FindWithTag(Tags.Enemy).GetComponent<Enemy_Behaviour>().CheckQuestObjectiveOnDeath();
		if (Input.GetKeyDown(KeyCode.T))
			UpdateQuest(1,2);
		if (Input.GetKeyDown(KeyCode.Y))
			UpdateQuest(2,1);
		if (Input.GetKeyDown(KeyCode.U))
			UpdateQuest(3,1);
	}	
	public void UpdateQuest(int id, int amount)
	{
		if (QuestDatabase.questList[id].getAmount > 0)
		{
			QuestDatabase.questList[id].getAmount -= amount;
			if (QuestDatabase.questList[id].getAmount <= 0)
			{
				QuestDatabase.questList[id].getQuestReturnAble = true;
				QuestDatabase.questList[id]._questAmount = 0;
			}
			GetComponent<QuestJournal>().UpdateQuest(id);
		}
	}
}
