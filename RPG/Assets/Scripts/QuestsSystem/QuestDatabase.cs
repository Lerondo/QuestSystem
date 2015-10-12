using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuestDatabase : MonoBehaviour {
	
	public static List<Quests> questList = new List<Quests>();

	void Awake () 
	{
		questList.Add(new Quest_Isundar_Wolves());
		questList.Add(new Quest_Isundar_Revenge());
		questList.Add(new Quest_Valkarak_Infiltrate());
		questList.Add(new Quest_Valkarak_Demons());
		
		for(int i = 0; i < questList.Count; i++)
		{
			questList[i].questID = i;
		}
	}
	public static List<Quests> GetQuestsViaId(List<int> questIds)
	{
		List<Quests> newQuestList = new List<Quests>();
		for(int i = 0; i < questIds.Count;i++)
		{
			newQuestList.Add(questList[questIds[i]]);
		}
		return newQuestList;
	}
}
