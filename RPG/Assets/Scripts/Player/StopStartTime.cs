using UnityEngine;
using System.Collections;

public class StopStartTime : MonoBehaviour {
	void Start()
	{
		ChangeTime ();
	}
	public void ChangeTime()
	{
		if (Time.timeScale == 0)
		{
			Time.timeScale = 1;
			GameObject.FindGameObjectWithTag(Tags.MainCam).GetComponent<Crosshair>().enabled = true;
		}else
		{
			Time.timeScale = 0;
			GameObject.FindGameObjectWithTag(Tags.MainCam).GetComponent<Crosshair>().enabled = false;
		}
	}
}
