using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TeleportActivator : MonoBehaviour {

	[SerializeField] private string _locName;
	private float textSpeed = 1f;
	private float appearColorTime;
	public Text discText;
	private bool _enabled = false;

	void Start()
	{
		discText.color = Color.clear;
	}
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == Tags.Player)
		{
			if (_enabled == false)
			{
				StartCoroutine(AreaDiscovered());
				discText.text = "You have discovered " + _locName + "!";
				GameObject.FindWithTag(Tags.GameController).GetComponent<TeleportLocations>().ActivateTeleporter(this.transform.position, _locName);
				_enabled = true;
			}
		}
	}
	IEnumerator AreaDiscovered()
	{
		appearColorTime = 0;
		while(appearColorTime < 400)
		{
			discText.color = Color.Lerp(discText.color, Color.white, Time.deltaTime * textSpeed);
			appearColorTime += textSpeed;
			if(appearColorTime > 200)
			{
				discText.color = Color.clear;
			}
			yield return new WaitForEndOfFrame();
		}
	}
}
