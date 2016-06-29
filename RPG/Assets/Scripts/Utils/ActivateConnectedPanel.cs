using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ActivateConnectedPanel : MonoBehaviour {

	[SerializeField] private List<GameObject> _mainPanels = new List<GameObject>();
	[SerializeField] private List<GameObject> _subPanels = new List<GameObject>();
	
	[SerializeField] private List<Button> _mainButtons = new List<Button>();
	[SerializeField] private List<Button> _subButtons = new List<Button>();

	void Start()
	{
		LinkButtonsToPanels();
	}
	private void LinkButtonsToPanels()
	{
		int i = -1;
		foreach(Button button in _mainButtons)
		{
			i++;
			var i2 = i;
			button.onClick.AddListener(() => { ActivatePanel(i2, true);});
			button.name = button.name + i;
		}
		int j = -1;
		foreach(Button button in _subButtons)
		{
			j++;
			var j2 = j;
			button.onClick.AddListener(() => { ActivatePanel(j2, false);});	
			button.name = button.name + j;
		}
	}
	private void ActivatePanel(int value, bool isMainPanel)
	{
		if (isMainPanel == true)
		{
			for (int i = 0; i < _mainPanels.Count; i++)
			{
				if (_mainPanels[i].activeInHierarchy == true)
					_mainPanels[i].SetActive(false);
			}
			_mainPanels[value].SetActive(true);
		}else
		{
			for (int j = 0; j < _subPanels.Count; j++)
			{
				if (_subPanels[j].activeInHierarchy == true)
					_subPanels[j].SetActive(false);
			}
			_subPanels[value].SetActive(true);
		}
	}
}
