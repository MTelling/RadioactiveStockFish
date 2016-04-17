using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameManager : MonoBehaviour {
	
	private User user;
	private User ai;
	private List<Stock> stocks;

	public GameObject stockPanel;
	public GameObject betPanel;
	public GameObject buyPanel;
	public BarGraph barGraph;

	public GameObject currRate;
	public GameObject stockName;
	public GameObject buyBtn;
	public GameObject buyAmount;
	public GameObject amountLbl;
	public GameObject totalLbl;
	public GameObject profitLbl;

	public GameObject sellBtn;

	public GameObject userLbl;
	public GameObject aiLbl;

	public GameObject betAmount;
	public GameObject betTime;

    public GameObject NavManager;


	private int activeStockNum;

	private int round;

	void Start() {
		init();
		round = 1;
		activeStockNum = 1;
		InvokeRepeating ("GoToNextDay", 1, 2f);

		BuyStock ("Amazon", 3);
		BuyStock ("Dexi.io", 5);
		BuyStock ("Microsoft", 1);
		BuyStock ("Bloomberg", 2);
		BuyStock ("DotTech", 1);

		BuyStock ("Amazon", 3, true);
		BuyStock ("Dexi.io", 5, true);
		BuyStock ("Microsoft", 1, true);
		BuyStock ("Bloomberg", 2, true);
		BuyStock ("DotTech", 1, true);

        NavManager = GameObject.FindGameObjectWithTag("NavigationManager");

        setBetView ();
		updateUI ();

	}

	public void init() {
		if (PlayerPrefs.GetInt ("userCash") == 0) {
			PlayerPrefs.SetInt ("userCash", 5000);
		}
		if (PlayerPrefs.GetInt ("aiCash") == 0) {
			PlayerPrefs.SetInt ("aiCash", 5000);
		}

		//The error is here: 
		this.user = new User ("user", 5000);
		this.ai = new User ("ai", 5000);
		this.stocks = new List<Stock> ();

		Stock amazon = new Stock ("Amazon", 300, 0.01);
		Stock microsoft = new Stock ("Microsoft", 200, 0.009);
		Stock dexi = new Stock ("Dexi.io", 50, 0.035);
		Stock bloomberg = new Stock ("Bloomberg", 100, 0.015);
		Stock tech = new Stock ("DotTech", 40, 0.05);

		//Simulator constants .It's set so dexi and tech should go high and bloomberg, microsoft and amazon should be more steady and more declining. 
		amazon.SetConstants (0.2, 0.9, 0.3, 0.07);
		microsoft.SetConstants (0.2, 0.7, 0.3, 0.07);
		dexi.SetConstants (0.05, 0.4, 0.30, 0.07);
		bloomberg.SetConstants (0.2, 0.5, 0.3, 0.07);
		tech.SetConstants (0.1, 0.5, 0.3, 0.07);

		this.stocks.Add (amazon);
		this.stocks.Add (dexi);
		this.stocks.Add (microsoft);
		this.stocks.Add (bloomberg);
		this.stocks.Add (tech);

		for (int i = 0; i < 100; i++) {
			amazon.SetNextRate ();
			microsoft.SetNextRate ();
			dexi.SetNextRate ();
			bloomberg.SetNextRate ();
			tech.SetNextRate ();
		}

	}

	public void Buy() {
		BuyStock (stocks [activeStockNum].GetName (), Int32.Parse(buyAmount.GetComponent<InputField> ().text));
		updateUI ();
	}

	public void Sell() {
		SellStock (stocks [activeStockNum].GetName (), Int32.Parse(buyAmount.GetComponent<InputField> ().text));
		updateUI ();
	}

	public void Bet() {

		double amountUpDown = Double.Parse(betAmount.GetComponent<InputField> ().text);
		int actTime = Int32.Parse (betTime.GetComponent<InputField> ().text);
		MakeBet (stocks [activeStockNum].GetName (), 200, stocks [activeStockNum].GetRates().Peek() + amountUpDown, actTime);

		updateUI ();
        NavManager.GetComponent<NavManager> ().BetPopUP_object.active = false;

		Debug.Log ("Click");

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
		UpdatePanelView ();

		//Update the score board
		userLbl.GetComponent<Text> ().text = Math.Round(user.GetTotalCash(),0).ToString();
		aiLbl.GetComponent<Text> ().text = Math.Round(ai.GetTotalCash(),0).ToString();

	}

	public void OpenBuyPanel(int stocknr) {
        NavManager.GetComponent<NavManager> ().BuyPanel_object.active = true;

		this.activeStockNum = stocknr;
		updateUI ();

	}

	public void UpdatePanelView() {
		//This parts draws to the stock view. I have no idea where or how to do this. 
		barGraph.drawGraph (stocks [activeStockNum]);

		currRate.GetComponent<Text> ().text = Math.Round (stocks [activeStockNum].GetRates ().Peek (), 2).ToString ();
		stockName.GetComponent<Text> ().text = stocks [activeStockNum].GetName ();
		profitLbl.GetComponent<Text> ().text = Math.Round (user.GetPurchases () [activeStockNum].GetProfit (), 2).ToString ();
		totalLbl.GetComponent<Text> ().text = Math.Round (user.GetPurchases () [activeStockNum].GetCurrentPrice (), 2).ToString ();
		amountLbl.GetComponent<Text> ().text = user.GetPurchases () [activeStockNum].GetAmount ().ToString ();
	}




	//Sets the time forward a day. 
	public void GoToNextDay() {
		//Let the ai trade. 
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
			SellStock ("Dexi.io", 2, true);
		} else if (round % 2 == 0) {
			BuyStock ("Dexi.io", 1, true);
		} else if (round % 5 == 0) {
			SellStock ("Dexi.io", 10, true);
		}

		if (round % 10 == 0) {
			MakeBet ("Amazon", 20, stocks [0].GetRates ().Peek () - 5, 5);
			SellStock ("Amazon", 1, true);
		}
		if (round % 8 == 0) {
			BuyStock ("Amazon", 1, true);
		}

		if (round % 2 == 0) {
			BuyStock ("Bloomberg", 1, true);
		} else if (round % 5 == 0) {
			SellStock ("Bloomberg", 5, true);
		}
	}
		
	public bool BuyStock(string stockName, int amount) {
		return BuyStock (stockName, amount, false);
	}

	public bool BuyStock(string stockName, int amount, bool ai) {
		bool bought = false;

		foreach (Stock stock in stocks) {
			if (stock.GetName ().Equals(stockName)) {

				if (ai) {
					bought = this.ai.Buy (stock, amount);
				} else {
					bought = user.Buy (stock, amount);
				}
			}
		}


		return bought;
	}

	public bool SellStock(string stockName, int amount) {
		return SellStock (stockName, amount, false);
	}

	public bool SellStock(string stockName, int amount, bool ai) {
		bool sold = false;
		
		foreach (Stock stock in stocks) {
			if (stock.GetName ().Equals(stockName)) {

				if (ai) {
					sold = this.ai.Sell (stock, amount);
				} else {
					sold = user.Sell (stock, amount);
				}
			}
		}

		return sold;
	}

	public bool MakeBet(string stockName, double betPrice, double goalRate, int time) {
		return MakeBet (stockName, betPrice, goalRate, time, false);
	}

	public bool MakeBet(string stockName, double betPrice, double goalRate, int time, bool ai) {
		bool betMade = false;

		foreach (Stock stock in stocks) {
			if (stock.GetName ().Equals(stockName)) {
				if (ai) {
					betMade = this.ai.MakeBet (stock, betPrice, goalRate, time);
				} else {
					betMade = user.MakeBet (stock, betPrice, goalRate, time);
				}
			}
		}
			

		return betMade;
	}
}
