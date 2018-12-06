using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MoneyUI : MonoBehaviour {

	public Text playerMoney;

	
	void Update ()
    {
		playerMoney.text = "$" + PlayerStats.Money.ToString();
	}
}
