using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class QuestJournal : MonoBehaviour {

	[SerializeField] GameObject journalPanel;
	[SerializeField] Transform questsPanel;
	[SerializeField] GameObject questPrefabButton;
	[SerializeField] GameObject disbandQuestPanel;
	[SerializeField] GameObject questMapMarkerImagePrefab;
	[SerializeField] Transform mapPanel;

	private List<GameObject> _currentQuests = new List<GameObject>();
	private List<GameObject> _questMarkers = new List<GameObject>();

	public Text questNameText;
	public Text questDiffText;
	public Text questThoughtsText;
	public Text questObjectivesText;
	public Text questRewardsText;

	private int _selectedQuest = -1;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.J))
			ToggleJournal();
	}
	void ToggleJournal()
	{
		if (journalPanel.activeInHierarchy == true)
		{
			if (disbandQuestPanel.activeInHierarchy == true)
				disbandQuestPanel.SetActive(false);

			journalPanel.SetActive(false);
			GameObject.FindWithTag(Tags.Player).GetComponent<MouseLook>().enabled = true;
		}
		else
		{
			journalPanel.SetActive(true);
			GameObject.FindWithTag(Tags.Player).GetComponent<MouseLook>().enabled = false;
		}
	}
	public void AddQuest(string name, string diff, int id, string objective, string thoughts, int[] rewards, Vector3 mapLoc)
	{
		GameObject button = (GameObject)Instantiate (questPrefabButton);
		if (diff == "Legendary")
			button.GetComponentInChildren<Text>().color = Color.red;
		else if (diff == "Hard")
			button.GetComponentInChildren<Text>().color = Color.blue;
		else if (diff == "Medium")
			button.GetComponentInChildren<Text>().color = Color.black;
		else
			button.GetComponentInChildren<Text>().color = Color.green;
		button.name = "Quest" + id;
		_currentQuests.Add(button);
		button.GetComponentInChildren<Text>().text = name + " - " + diff;
		button.GetComponentsInChildren<Button>()[1].onClick.AddListener(()=>{DisbandQuest(id);});
		button.GetComponent<Button>().onClick.AddListener(
			() =>
			{
			SelectQuest (name ,diff, id, objective, thoughts, rewards);
		}
		);
		button.transform.SetParent(questsPanel);
		GameObject image = (GameObject)Instantiate (questMapMarkerImagePrefab);
		image.name = "Quest" + id + "Marker";
		_questMarkers.Add(image);
		image.transform.SetParent(mapPanel);
		mapLoc.y = mapLoc.z;
		image.transform.localPosition = mapLoc;
		image.transform.localScale = new Vector3(1,1,1);
		image.transform.localRotation = new Quaternion(0,0,0,0);
		AddEventsToImage(image, id);
	}

	private void AddEventsToImage(GameObject image, int id)
	{
		//Add OnPointerEnter Event
		EventTrigger.TriggerEvent trigger = new EventTrigger.TriggerEvent();
		EventTrigger.Entry entry = new EventTrigger.Entry();
		
		trigger.AddListener((eventData) => ShowQuestInfo(id));
		entry.eventID = EventTriggerType.PointerEnter;
		entry.callback = trigger;
		
		image.GetComponent<EventTrigger>().triggers.Add(entry);
		
		//Add OnPointerExit Event
		EventTrigger.TriggerEvent trigger2 = new EventTrigger.TriggerEvent();
		EventTrigger.Entry entry2 = new EventTrigger.Entry();
		
		trigger2.AddListener((eventData) => CloseQuestInfo(id));
		entry2.eventID = EventTriggerType.PointerExit;
		entry2.callback = trigger2;
		
		image.GetComponent<EventTrigger>().triggers.Add(entry2);
	}

	void SelectQuest(string qname, string diff, int id, string objective, string thoughts, int[] rewards)
	{
		questNameText.text = qname + "\n" + diff;
		if (diff == "Legendary")
			questNameText.color = Color.red;
		else
			questNameText.color = Color.black;
		if (QuestDatabase.questList[id].getAmount == 0)
		{
			questNameText.text += " - ReturnAble";
			questThoughtsText.text = "I should return to the QuestGiver";
		} else
			questThoughtsText.text = thoughts;
		questObjectivesText.text = objective + "\n" + QuestDatabase.questList[id].getAmount + " Left";
		string stats = "";
		stats += rewards[0].ToString() + " Experience" + "\n";
		stats += rewards[1].ToString() + " Gold" + "\n";
		if(rewards[2] > 0)
			stats += ItemDatabase.itemList[rewards[2]].getItemName;
		questRewardsText.text = stats;

		_selectedQuest = id;
	}
	public void UpdateQuest(int id)
	{
		if (_selectedQuest == id)
		{
			questObjectivesText.text = QuestDatabase.questList[id].questObjective + "\n" + QuestDatabase.questList[id].getAmount + " Left";
			if (QuestDatabase.questList[id].getAmount <= 0)
			{
				questNameText.text += " - ReturnAble";
				questThoughtsText.text = "I should return to the QuestGiver";
			} else
				questThoughtsText.text = QuestDatabase.questList[id].questThoughts;
		}
	}
	void DisbandQuest(int id)
	{
		_selectedQuest = id;
		disbandQuestPanel.SetActive(true);
	}
	public void AcceptDisband()
	{
		QuestDatabase.questList[_selectedQuest].getQuestAccepted = false;
		RemoveQuestFromLog(_selectedQuest);
	}
	public void CloseDisbandPanel()
	{
		disbandQuestPanel.SetActive(false);
	}
	public void CompleteQuest(int id)
	{
		if (QuestDatabase.questList[id].getQuestReturnAble == true && QuestDatabase.questList[id].getQuestCompleted == false)
		{
			GetComponent<QuestRewardSystem>().GetRewards(id);
			QuestDatabase.questList[id].getQuestCompleted = true;
			RemoveQuestFromLog(id);
		}
	}
	void RemoveQuestFromLog(int id)
	{
		Destroy(_currentQuests[id]);
		Destroy(_questMarkers[id]);
		questNameText.text = "- No Quest Selected -";
		questNameText.color = Color.black;
		questThoughtsText.text = "What should I do?";
		questObjectivesText.text = "'Row Row Row you boat!'";
		questRewardsText.text = "'Tu du du duh'";
	}
	public void ShowQuestInfo(int id)
	{
		GameObject.Find("Quest" + id + "Marker").GetComponentInChildren<Text>().enabled = true;
		GameObject.Find("Quest" + id + "Marker").GetComponentInChildren<Text>().text = QuestDatabase.questList[id].questObjective;
	}
	public void CloseQuestInfo(int id)
	{
		GameObject.Find("Quest" + id + "Marker").GetComponentInChildren<Text>().text = "";
		GameObject.Find("Quest" + id + "Marker").GetComponentInChildren<Text>().enabled = false;
	}
}
