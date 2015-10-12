using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

	public GameObject loadSaveFile;
	public GameObject optionsMenu;

	Resolution[] resolutions;
	[SerializeField] private Transform resolutionPanel;
	[SerializeField] private GameObject resolutionButtonPrefab;
	[SerializeField] private GameObject resButton;
	[SerializeField] private Toggle fullScreenToggle;

	private string[] allQualities;
	[SerializeField] private GameObject qualityButtonPrefab;
	[SerializeField] private GameObject qualityButton;	
	[SerializeField] private Transform qualityPanel;

	void Start()
	{
		loadSaveFile.SetActive(false);
		optionsMenu.SetActive(false);
		fullScreenToggle.GetComponent<Toggle>();
		GetResolutions();
		GetQualities();
	}

	void GetResolutions()
	{
		resolutions = Screen.resolutions;
		for (int i = 0; i < resolutions.Length; i++){
			GameObject button = (GameObject)Instantiate (resolutionButtonPrefab);
			button.GetComponentInChildren<Text>().text = ResToString(resolutions[i]);
			int index = i;
			button.GetComponent<Button>().onClick.AddListener(
				() => 
				{
				SetRes (index);
				}
			);
			button.transform.parent = resolutionPanel;
			resButton.GetComponent<Text>().text = ResToString(resolutions[i]);
		}
	}

	void GetQualities()
	{
		allQualities = QualitySettings.names;
		for (int i = 0; i <allQualities.Length; i++)
		{
			GameObject Quabutton = (GameObject) Instantiate (qualityButtonPrefab);
			Quabutton.GetComponentInChildren<Text>().text = allQualities[i];
			int index = i;
			Quabutton.GetComponent<Button>().onClick.AddListener(
				() =>
				{
				SetQuality(index);
				}
			);
			Quabutton.transform.parent = qualityPanel;
			qualityButton.GetComponent<Text>().text = QuaToString(allQualities[i]);
		}
	}
	void SetQuality(int index)
	{
		QualitySettings.SetQualityLevel(index, true);
		qualityButton.GetComponent<Text>().text = QuaToString(allQualities[index]);
	}
	string QuaToString(string quality)
	{
		return quality;
	}

	void SetRes(int index)
	{
		Screen.SetResolution (resolutions[index].width, resolutions[index].height, false);
		resButton.GetComponent<Text>().text = ResToString(resolutions[index]);
		SetFullScreen();
	}

	string ResToString(Resolution res)
	{
		return res.width + " x " + res.height;
	}

	public void StartNewGame()
	{
		Application.LoadLevel("Main");
	}
	public void LoadGame()
	{
		loadSaveFile.SetActive(true);
	}
	public void Options()
	{
		optionsMenu.SetActive(true);
	}
	public void ExitGame()
	{
		Application.Quit();
	}
	public void CloseCurrent()
	{
		GameObject.FindGameObjectWithTag(Tags.CloseAble).SetActive(false);
	}

	public void SetFullScreen()
	{
		if (fullScreenToggle.isOn == true)
			Screen.fullScreen = true;
		else
			Screen.fullScreen = false;
	}
}
