using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class User : MonoBehaviour {

	private List<Purchase> purchases;
	private List<Bet> bets;
	private string name;
	private double cash;

	public User(string name, double cash) {
		this.name = name;
		this.cash = cash;
		this.purchases = new LinkedList<Purchase> ();
	}

	//Returns false if the amount is too much. 
	public bool Buy(Stock stock, int amount)  {
		double stockPrice = stock.GetRates ().Peek();
		string stockName = stock.GetName ();
		double buyPrice = stockPrice * amount;

		//If the user hasn't got enough money. Return false.
		if (buyPrice > this.cash) {
			return false;
		} 

		bool stockInInventory = false;
		//If the user has enough money buy the stock. 
		foreach (Purchase purchase in purchases) {
			if(purchase.GetName().Equals(stockName)) {
				purchase.New(amount, buyPrice);
				stockInInventory = true;
			}
		}

		//If the user didn't have the stock already, add it to the list of stocks. 
		if (!stockInInventory) {
			this.purchases.Add (new Purchase(stock.GetName (), amount, buyPrice));
		}

		this.cash -= buyPrice;

		return true;
	}

	public bool Sell(Stock stock, int amount) {
		double stockPrice = stock.GetRates ().Peek ();
		string stockName = stock.GetName ();

		double sellPrice = stockPrice * amount;
		bool isSold = false;
		foreach (Purchase purchase in purchases) {
			if(purchase.GetName().Equals(stockName)) {
				//Only sell if you have more than the amount chosen.
				if (purchase.GetAmount() >= amount) {
					purchase.Sell(amount);
					this.cash += sellPrice;
					isSold = true;
				}
			}
		}

		return isSold;
	}

	public bool MakeBet(Stock stock, int betPrice, double goalRate, int time) {
		bool isBetMade = false;
		if (betPrice <= this.cash) {
			this.bets.Add (new Bet(stock.GetName(), betPrice, stock.GetRates().Peek (), goalRate, time));
			isBetMade = true;
		}

		return isBetMade;
	}

	public bool CheckBets() {
		List<Bet> doneBets = new List<Bet> ();
		foreach (Bet bet in bets) {
			if (bet.Tick()) { //This is true if the time is up for the bet. 
				this.cash += bet.GetAward();
				doneBets.Add (bet);
			}
		}

		//Remove all the bets that are done. 
		foreach (Bet bet in doneBets) {
			this.bets.Remove (bet);
		}
	}


}
