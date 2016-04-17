using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class User {

	private List<Purchase> purchases;
	private List<Bet> bets;
	private string userName;
	private double cash;

	public User(string userName, double cash) {
		this.userName = userName;
		this.cash = cash;
		this.purchases = new List<Purchase> ();
		this.bets = new List<Bet> ();
	}

	//Returns false if the amount is too much. 
	public bool Buy(Stock stock, int amount)  {
		double stockPrice = stock.GetRates ().Peek();
		double buyPrice = stockPrice * amount;

		//If the user hasn't got enough money. Return false.
		if (buyPrice > this.cash) {
			return false;
		} 

		bool stockInInventory = false;
		//If the user has enough money buy the stock. 
		foreach (Purchase purchase in purchases) {
			if(purchase.GetName().Equals(stock.GetName())) {
				purchase.New(amount, buyPrice);
				stockInInventory = true;
			}
		}

		//If the user didn't have the stock already, add it to the list of stocks. 
		if (!stockInInventory) {
			this.purchases.Add (new Purchase(stock, amount, buyPrice));
		}

		this.cash -= buyPrice;
		PlayerPrefs.SetInt (this.userName + "Cash", (int)this.cash);


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
					PlayerPrefs.SetInt (this.userName + "Cash", (int)this.cash);

					isSold = true;
				}
			}
		}

		return isSold;
	}

	public bool MakeBet(Stock stock, double betPrice, double goalRate, int time) {
		bool isBetMade = false;

		if (bets.ToArray().Length < 10) {
				
			if (betPrice <= this.cash) {
				this.bets.Add (new Bet (stock, betPrice, stock.GetRates ().Peek (), goalRate, time));
				isBetMade = true;

				this.cash -= betPrice;
				PlayerPrefs.SetInt (this.userName + "Cash", (int)this.cash);

			}
		}

		return isBetMade;
	}

	public void CheckBets() {
		List<Bet> doneBets = new List<Bet> ();
		foreach (Bet bet in bets) {
			if (bet.Tick()) { //This is true if the time is up for the bet. 
				this.cash += bet.GetAward();
				PlayerPrefs.SetInt (this.userName + "Cash", (int)this.cash);

				doneBets.Add (bet);
			}
		}

		//Remove all the bets that are done. 
		foreach (Bet bet in doneBets) {
			this.bets.Remove (bet);
		}
	}

	public List<Purchase> GetPurchases() {
		return this.purchases;
	}

	public List<Bet> GetBets() {
		return this.bets;
	}

	public double GetCash(){
		return this.cash;
	}

	public double GetTotalCash() {
		double sum = 0;
		foreach (Purchase purchase in purchases) {
			sum += purchase.GetCurrentPrice();
		}

		sum += this.cash;
		return sum;
	}

}
