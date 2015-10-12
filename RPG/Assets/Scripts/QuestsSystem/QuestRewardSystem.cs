using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuestRewardSystem : MonoBehaviour {

	public void GetRewards(int id)
	{
		GetComponent<PlayerStats>().AddExperience(QuestDatabase.questList[id].questRewards[0]);
		GetComponent<Inventory>().ChangeGold(QuestDatabase.questList[id].questRewards[1]);
		if (QuestDatabase.questList[id].questRewards[2] > 0)
			GetComponent<Inventory>().AddItem(ItemDatabase.itemList[QuestDatabase.questList[id].questRewards[2]]);	                      
	}
}
