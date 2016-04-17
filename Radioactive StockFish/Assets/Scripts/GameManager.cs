using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameManager : MonoBehaviour {
	
	private User user;
	private List<Stock> stocks;
	public GameObject priceLabel;
	public GameObject priceLabelTwo;
	public GameObject logLabel;
	public GameObject logLabelTwo;
	public GameObject worthLabel;
	public GameObject worthLabelTwo;
	public GameObject cashLabel;

	private int round;

	void Start() {
		init();
		round = 1;
		InvokeRepeating ("GoToNextDay", 1, 0.05f);

		BuyStock ("Amazon", 3);
		BuyStock ("Dexi.io", 5);

		updateUI ();

	}


	public void updateUI() {

		priceLabel.GetComponent<Text> ().text = stocks [0].GetRates ().Peek ().ToString ();
		priceLabelTwo.GetComponent<Text> ().text = stocks [1].GetRates ().Peek ().ToString ();

		logLabel.GetComponent<Text> ().text = user.GetPurchases () [0].GetAmount().ToString();
		logLabelTwo.GetComponent<Text> ().text = user.GetPurchases () [1].GetAmount().ToString();


		double worth = user.GetPurchases () [0].GetCurrentPrice();
		double worthTwo = user.GetPurchases () [1].GetCurrentPrice();

		worthLabel.GetComponent<Text> ().text = worth.ToString();
		worthLabelTwo.GetComponent<Text> ().text = worthTwo.ToString();

		cashLabel.GetComponent<Text> ().text = user.GetTotalCash ().ToString ();

	}

	public void init() {
		this.user = new User ("RadioactiveSwordFish", 5000);
		this.stocks = new List<Stock> ();

		Stock amazon = new Stock ("Amazon", 500, 0.03);
		Stock microsoft = new Stock ("Microsoft", 100, 0.0020);
		Stock dexi = new Stock ("Dexi.io", 50, 0.008);
		Stock apple = new Stock ("Apple", 100, 5);
		Stock mlh = new Stock ("Major League Hacking", 240, 5);

		this.stocks.Add (amazon);
		this.stocks.Add (dexi);
		this.stocks.Add (microsoft);
		this.stocks.Add (apple);
		this.stocks.Add (mlh);

	}

	//Sets the time forward a day. 
	public void GoToNextDay() {
		robot ();

		foreach (Stock stock in stocks) {
			stock.SetNextRate();
		}

		//Check the users bets. 
		user.CheckBets();

		updateUI ();
		round++;


	}

	private void robot() {
		if (round == 5) {
			MakeBet ("Amazon", 4000, stocks [0].GetRates ().Peek () - 100, 10);
			SellStock ("Dexi.io", 2);
		} else if (round % 2 == 0) {
			BuyStock ("Dexi.io", 1);
		} else if (round % 5 == 0) {
			SellStock ("Dexi.io", 10);
		}

		if (round % 10 == 0) {
			MakeBet ("Amazon", 1000, stocks [0].GetRates ().Peek () - 100, 10);
			SellStock ("Amazon", 1);
		}
		if (round % 8 == 0) {
			BuyStock ("Amazon", 1);
		}
	}

	public bool BuyStock(string stockName, int amount) {
		bool bought = false;

		foreach (Stock stock in stocks) {
			if (stock.GetName ().Equals(stockName)) {
				bought = user.Buy (stock, amount);
			}
		}

		if (!bought) {
			Debug.Log ("User failed to buy");
		}

		return bought;
	}

	public bool SellStock(string stockName, int amount) {
		bool sold = false;
		
		foreach (Stock stock in stocks) {
			if (stock.GetName ().Equals(stockName)) {
				sold = user.Sell (stock, amount);
			}
		}

		if (!sold) {
			Debug.Log ("User failed to sell");
		}
		
		return sold;
	}

	public bool MakeBet(string stockName, double betPrice, double goalRate, int time) {
		bool betMade = false;

		foreach (Stock stock in stocks) {
			if (stock.GetName ().Equals(stockName)) {
				betMade = user.MakeBet (stock, betPrice, goalRate, time);
			}
		}

		if (!betMade) {
			Debug.Log ("User failed to make bet");
		}

		return betMade;
	}
}
