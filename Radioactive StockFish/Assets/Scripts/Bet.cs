using UnityEngine;
using System.Collections;
using System;

public class Bet : MonoBehaviour {

	private Stock stock;
	private double betPrice;
	private double goalRate;
	private double betRate;
	private int time;
	private int odds;

	public Bet(Stock stock, double betPrice, double betRate, double goalRate, int time) {
		this.stock = stock;
		this.betPrice = betPrice;
		this.betRate = betRate;
		this.goalRate = goalRate;
		this.time = time;
		this.odds = 2;
	}

	public bool Tick() {
		this.time -= 1;

		if (this.time == 0) {
			return true;
		} else {
			return false;
		}
	}

	public double GetAward() {
		double percentage = 1 + Math.Abs (goalRate - betRate) / betRate;
		double award = 0;
		double currentRate = stock.GetRates ().Peek ();

		if (betRate > goalRate) { //Bet was on the stock going down. 
			if (currentRate <= goalRate) {
				award = betPrice * percentage * odds;
			}
		} else { //Bet was on the stock rising.
			if (currentRate >= goalRate) {
				award = betPrice * percentage * odds;
			}
		}

		return award;
	}

}
