using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameManager : MonoBehaviour {

	private User user;
	private List<Stock> stocks;

	public GameManager() {
		this.user = new User ("RadioactiveSwordFish", 5000);
		this.stocks = new List<Stock> ();

		Stock amazon = new Stock ("Amazon", 500);
		Stock microsoft = new Stock ("Microsoft", 100);
		Stock dexi = new Stock ("Dexi.io", 50);
		Stock apple = new Stock ("Apple", 100);
		Stock mlh = new Stock ("Major League Hacking", 240);

		this.stocks.Add (amazon, microsoft, dexi, apple, mlh);
	}

	//Sets the time forward a day. 
	public void GoToNextDay() {
		foreach (Stock stock in stocks) {
			//TODO: update price of stock here. 
		}

		//Check the users bets. 
		user.CheckBets();
	}

	public bool BuyStock(string stockName, int amount) {
		bool bought = false;

		foreach (Stock stock in stocks) {
			if (stock.GetName ().Equals(stockName)) {
				user.Buy (stock, amount);
				bought = true;
			}
		}

		return bought;
	}

	public bool SellStock(string stockName, int amount) {
		bool sold = false;
		
		foreach (Stock stock in stocks) {
			if (stock.GetName ().Equals(stockName)) {
				user.Sell (stock, amount);
				sold = true;
			}
		}
		
		return sold;
	}

	public bool MakeBet(string stockName, double betPrice, double goalRate, int time) {
		bool betMade = false;

		foreach (Stock stock in stocks) {
			if (stock.GetName ().Equals(stockName)) {
				user.MakeBet (stock, betPrice, goalRate, time);
				betMade = true;
			}
		}

		return betMade;
	}
}
