using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameManager : MonoBehaviour {
	
	private User user;
	private List<Stock> stocks;

	public GameObject stockPanel;
	public GameObject betPanel;
	public BarGraph barGraph;

	public GameObject currRate;
	public GameObject stockName;
	public GameObject buyBtn;
	public GameObject buyAmount;
	public GameObject amountLbl;
	public GameObject totalLbl;
	public GameObject profitLbl;

	public GameObject sellBtn;


	private int round;

	void Start() {
		init();
		round = 1;
		InvokeRepeating ("GoToNextDay", 1, 10f);
	

		BuyStock ("Amazon", 3);
		BuyStock ("Dexi.io", 5);
		BuyStock ("Microsoft", 0);
		BuyStock ("Apple", 0);
		BuyStock ("Major League Hacking", 0);

		setBetView ();
		updateUI ();

		buyBtn.GetComponent<Button>().onClick.AddListener(() => Buy());
		sellBtn.GetComponent<Button>().onClick.AddListener(() => Sell());


	}

	private void Buy() {
		Debug.Log(BuyStock (stocks [0].GetName (), Int32.Parse(buyAmount.GetComponent<InputField> ().text)));
		updateUI ();
	}

	private void Sell() {
		Debug.Log(SellStock (stocks [0].GetName (), Int32.Parse(buyAmount.GetComponent<InputField> ().text)));
		updateUI ();
	}

	public void updateStockView() {
		int i = 0;
		foreach (Purchase purchase in user.GetPurchases()) {
			stockPanel.transform.GetChild (i).GetChild (0).GetComponent<Text> ().text = purchase.GetName ();
			stockPanel.transform.GetChild (i).GetChild (1).GetComponent<Text> ().text = Math.Round (purchase.GetCurrentRate (), 2).ToString ();
			stockPanel.transform.GetChild (i).GetChild (2).GetComponent<Text> ().text = purchase.GetAmount ().ToString ();
			stockPanel.transform.GetChild (i).GetChild (3).GetComponent<Text> ().text = Math.Round (purchase.GetProfit (), 2).ToString ();

			i++;
		}
	}

	public void updateBetView() {
		int i = 0;
		foreach (Bet bet in user.GetBets()) {
			betPanel.transform.GetChild (i).GetChild (0).GetComponent<Text> ().text = bet.GetName ();
			betPanel.transform.GetChild (i).GetChild (1).GetComponent<Text> ().text = Math.Round(bet.GetExpectedAward (), 2).ToString();
			betPanel.transform.GetChild (i).GetChild (2).GetComponent<Text> ().text = Math.Round(bet.GetBetPrice (), 2).ToString();
			betPanel.transform.GetChild (i).GetChild (3).GetComponent<Text> ().text = bet.GetTime ().ToString();
			i++;
		}
	}

	public void setBetView() {
		for (int i = 0; i < 10; i++) {
			betPanel.transform.GetChild (i).GetChild (0).GetComponent<Text> ().text = "Empty";
			betPanel.transform.GetChild (i).GetChild (1).GetComponent<Text> ().text = "";
			betPanel.transform.GetChild (i).GetChild (2).GetComponent<Text> ().text = "";
			betPanel.transform.GetChild (i).GetChild (3).GetComponent<Text> ().text = "";
		}
	}


	public void updateUI() {

		updateStockView ();
		updateBetView ();


		//This parts draws to the stock view. I have no idea where or how to do this. 
		barGraph.drawGraph (stocks [0]);
		currRate.GetComponent<Text> ().text = Math.Round(stocks[0].GetRates().Peek(), 2).ToString();
		stockName.GetComponent<Text> ().text = stocks[0].GetName();
		profitLbl.GetComponent<Text> ().text = Math.Round(user.GetPurchases()[0].GetProfit(),2).ToString();
		totalLbl.GetComponent<Text> ().text = Math.Round(user.GetPurchases()[0].GetCurrentPrice(),2).ToString();
		amountLbl.GetComponent<Text> ().text = user.GetPurchases()[0].GetAmount().ToString();
	}

	public void init() {
		this.user = new User ("RadioactiveSwordFish", 5000);
		this.stocks = new List<Stock> ();

		Stock amazon = new Stock ("Amazon", 500, 0.03);
		Stock microsoft = new Stock ("Microsoft", 100, 0.0020);
		Stock dexi = new Stock ("Dexi.io", 50, 0.008);
		Stock apple = new Stock ("Apple", 100, 0.008);
		Stock mlh = new Stock ("Major League Hacking", 240, 0.008);

		this.stocks.Add (amazon);
		this.stocks.Add (dexi);
		this.stocks.Add (microsoft);
		this.stocks.Add (apple);
		this.stocks.Add (mlh);

		for (int i = 0; i < 60; i++) {
			amazon.SetNextRate ();
			microsoft.SetNextRate ();
			dexi.SetNextRate ();
			apple.SetNextRate ();
			mlh.SetNextRate ();
		}

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
			MakeBet ("Amazon", 20, stocks [0].GetRates ().Peek () - 100, 100);
			SellStock ("Dexi.io", 2);
		} else if (round % 2 == 0) {
			BuyStock ("Dexi.io", 1);
		} else if (round % 5 == 0) {
			SellStock ("Dexi.io", 10);
		}

		if (round % 10 == 0) {
			MakeBet ("Amazon", 50, stocks [0].GetRates ().Peek () - 100, 100);
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
