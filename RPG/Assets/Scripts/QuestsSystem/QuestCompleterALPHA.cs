using UnityEngine;
using System.Collections;

public class QuestCompleterALPHA : MonoBehaviour {

	[SerializeField] private int _questHolder;

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == Tags.Player)
		{
			CompleteQuest();
		}
	}
	void CompleteQuest()
	{
		GameObject.FindWithTag(Tags.GameController).GetComponent<QuestJournal>().CompleteQuest(_questHolder);
	}
}
