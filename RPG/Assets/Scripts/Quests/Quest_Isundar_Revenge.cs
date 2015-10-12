using UnityEngine;
using System.Collections;

[System.Serializable]
public class Quest_Isundar_Revenge : Quests {

	public Quest_Isundar_Revenge()
	{
		currentQuestDifficulity = QuestDifficulity.Legendary;
		questName = "Unleashed Fury";
		questID = 1;
		questObjective = "Kill the whole bandit camp";
		_questAmount = 3;
		_questRewards[0] = 150;
		_questRewards[1] = 1000;
		_questRewards[2] = 1;
		_questThoughts = "I thought it was located south of here, and 30 man shouldn't stop me!";
		questAccepted = false;
		questReturnAble = false;
		questCompleted = false;
		questCoordinates = new Vector3 (100,150,150);
	}
}
