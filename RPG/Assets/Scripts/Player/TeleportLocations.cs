using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TeleportLocations : MonoBehaviour{

	public List<Vector3> teleportPositions = new List<Vector3>();
	
	[SerializeField] private GameObject mapPanel;
	[SerializeField] private GameObject tpButtonPrefab;
	
	public void AddTeleportPoint(Vector3 locationPos)
	{
		teleportPositions.Add(locationPos);
	}
	private void ToggleMap()
	{
		if (mapPanel.activeInHierarchy)
			mapPanel.SetActive(false);
		else
			mapPanel.SetActive(true);
	}
	public void ActivateTeleporter(Vector3 pos, string tpName)
	{
		AddTeleportPoint(pos);
		MakeTeleporterButton((teleportPositions.Count -1), tpName, pos);
	}
	private void MakeTeleporterButton(int id, string tpName, Vector3 pos)
	{
		GameObject tpButton = (GameObject)Instantiate (tpButtonPrefab);
		tpButton.GetComponentInChildren<Text>().text = tpName;
		tpButton.GetComponent<Button>().onClick.AddListener(
			() =>
			{
			Teleport (id);
		}
		);
		tpButton.transform.parent = mapPanel.transform;
		pos.y = pos.z;
		tpButton.transform.localPosition = pos;
		tpButton.transform.localScale = new Vector3(1,1,1);
		tpButton.transform.localRotation = new Quaternion(0,0,0,0);
	}
	public void Teleport(int id)
	{
		GameObject.FindWithTag(Tags.Player).transform.position = teleportPositions[id];
		ToggleMap();
	}
}

