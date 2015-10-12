using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShopKeeper : MonoBehaviour {

	public Text welcomeText;
	private bool _canOpenShop = false;
	private Shop _shop;

	void Start()
	{
		welcomeText.text = "Welcome to my Shop Traveler!" + "\n" + "-Press 'P' To open Shop";
		welcomeText.enabled = false;
		_shop = GameObject.FindWithTag(Tags.GameController).GetComponent<Shop>();
	}
	void Update()
	{
		if (_canOpenShop == true && Input.GetKeyDown(KeyCode.P))
			_shop.ToggleShopInterface();
	}
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == Tags.Player)
		{
			welcomeText.enabled = true;
			_canOpenShop = true;
		}
	}
	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == Tags.Player)
		{
			if (GameObject.Find("ShopPanel"))
				_shop.ToggleShopInterface();
			welcomeText.enabled = false;
			_canOpenShop = false;
		}
	}
}
