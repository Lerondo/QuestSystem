using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Quests : System.Object {

	public enum QuestDifficulity
	{
		Easy,
		Medium,
		Hard,
		Legendary
	}

	public int questID;
	protected QuestDifficulity questDifficulity;
	protected string _questName;
	protected string _questThoughts;
	protected string _questObjectives;
	public int _questAmount;
	// Rewards = Experience , Gold , ItemID
	protected int[] _questRewards = new int[3];
	protected Vector3 questCoordinates;
	protected bool questAccepted;
	protected bool questReturnAble;
	protected bool questCompleted;

	public Vector3 getQuestCoordinates
	{
		get { return questCoordinates; }
		set { questCoordinates = value; }
	}
	public bool getQuestReturnAble
	{
		get { return questReturnAble; }
		set { questReturnAble = value; }
	}
	public bool getQuestAccepted
	{
		get { return questAccepted; }
		set { questAccepted = value; }
	}
	public bool getQuestCompleted
	{
		get { return questCompleted; }
		set { questCompleted = value; }
	}
	public int getAmount
	{
		get { return _questAmount; }
		set { _questAmount = value; }
	}
	public int getQuestID
	{
		get { return questID; }
		set { questID = value; }
	}
	public string questName
	{
		get { return _questName; }
		set { _questName = value; }
	}
	public string questThoughts
	{
		get { return _questThoughts; }
		set { _questThoughts = value; }
	}
	public string questObjective
	{
		get { return _questObjectives; }
		set { _questObjectives = value; }
	}
	public int[] questRewards
	{
		get { return _questRewards; }
		set { _questRewards = value; }
	}
	public QuestDifficulity currentQuestDifficulity
	{
		get { return questDifficulity; }
		set { questDifficulity = value; }
	}
}
