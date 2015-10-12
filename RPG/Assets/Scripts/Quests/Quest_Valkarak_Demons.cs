using UnityEngine;
using System.Collections;

[System.Serializable]
public class Quest_Valkarak_Demons : Quests {

	public Quest_Valkarak_Demons()
	{
		currentQuestDifficulity = QuestDifficulity.Hard;
		questName = "Demon's Possession";
		questID = 3;
		questObjective = "Investigate the Cursed House";
		_questAmount = 4;
		_questRewards[0] = 150;
		_questRewards[1] = 200;
		_questRewards[2] = -1;
		_questThoughts = "Evil spirits again... The cursed house is in the center of Valkarak";
		questAccepted = false;
		questReturnAble = false;
		questCompleted = false;
		questCoordinates = new Vector3 (-500,75,-75);
	}
}
