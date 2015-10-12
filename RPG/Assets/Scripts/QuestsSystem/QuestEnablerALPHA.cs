using UnityEngine;
using System.Collections;

public class QuestEnablerALPHA : MonoBehaviour {

	private int count = 0;

	void OnTriggerEnter(Collider other)
	{
		if (count < 4)
		{
			QuestDatabase.questList[count].getQuestAccepted = true;
			GameObject.FindWithTag(Tags.GameController)
				.GetComponent<QuestJournal>().AddQuest(
					QuestDatabase.questList[count].questName,
					QuestDatabase.questList[count].currentQuestDifficulity.ToString(),
					QuestDatabase.questList[count].getQuestID,
					QuestDatabase.questList[count].questObjective,
					QuestDatabase.questList[count].questThoughts,
					QuestDatabase.questList[count].questRewards,
					QuestDatabase.questList[count].getQuestCoordinates
					);
			count = count + 1;
		}
	}
}
