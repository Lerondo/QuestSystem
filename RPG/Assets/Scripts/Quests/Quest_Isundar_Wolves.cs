using UnityEngine;
using System.Collections;

[System.Serializable]
public class Quest_Isundar_Wolves : Quests {
	
	public Quest_Isundar_Wolves()
	{
		currentQuestDifficulity = QuestDifficulity.Easy;
		questName = "Wolf Troubles";
		questID = 0;
		questObjective = "Kill 12 Grey Wolves";
		_questAmount = 12;
		_questRewards[0] = 150;
		_questRewards[1] = 100;
		_questRewards[2] = 2;
		_questThoughts = "I should check the den north of Isundar";
		questAccepted = false;
		questReturnAble = false;
		questCompleted = false;
		questCoordinates = new Vector3 (-100,40,40);
	}
}