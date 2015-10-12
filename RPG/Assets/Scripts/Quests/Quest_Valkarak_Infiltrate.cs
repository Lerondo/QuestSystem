using UnityEngine;
using System.Collections;

[System.Serializable]
public class Quest_Valkarak_Infiltrate : Quests {

	public Quest_Valkarak_Infiltrate()
	{
		currentQuestDifficulity = QuestDifficulity.Medium;
		questName = "Brotherhood Infiltration";
		questID = 2;
		questObjective = "Infiltrate the Brotherhood of Iron and steal their ManuScripts";
		_questAmount = 5;
		_questRewards[0] = 150;
		_questRewards[1] = 2;
		_questRewards[2] = 5;
		_questThoughts = "It is believed that the location is in the South West of Valkarak";
		questAccepted = false;
		questReturnAble = false;
		questCompleted = false;
		questCoordinates = new Vector3 (600,250,250);
	}
}
