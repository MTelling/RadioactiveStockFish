using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Stock {

	private const double LOWEST_PRICE_IN_PERCENT = 0.02;
	private const double CHANCE_OF_DEEP_DIVE = 0.7;
	private const double DEEP_DIVE_HIGH_CAP = 0.29;
	private const double DEEP_DIVE_LOW_CAP = 0.07;

	private double volatility;
	private double lowerPriceCap;

	private Stack<double> rates;
	private string stockName;
	private string imgPath;
	private System.Random rnd;
	
	public Stock (string stockName, double startRate, double volatility)
	{
		this.stockName = stockName;
		this.rates = new Stack<double> ();
		rates.Push (startRate);
		this.rnd = new System.Random ();

		this.volatility = volatility;

		this.lowerPriceCap = LOWEST_PRICE_IN_PERCENT * startRate;
	}
	
	public string GetImgPath() {
		return this.imgPath;
	}
	
	public void SetImgPath(string imgPath) {
		this.imgPath = imgPath;
	}
	
	public void SetNextRate() {
		this.rates.Push (NextPrice ());
	}
	
	public Stack<double> GetRates() {
		return this.rates;
	}

	public double NextPrice()
	{
		double change = 2 * this.volatility * rnd.NextDouble();
		double newPrice = this.rates.Peek();
		double differenceFromCap = this.lowerPriceCap / this.rates.Peek();

		if (rnd.NextDouble() * CHANCE_OF_DEEP_DIVE == 1) //We do a deep dive;
		{
			newPrice *= (1 + (rnd.NextDouble() * (DEEP_DIVE_HIGH_CAP - DEEP_DIVE_LOW_CAP)));
		}
		else if (change > this.volatility)
		{
			if (rnd.NextDouble() < differenceFromCap) //We are getting close to lower cap
			{
				newPrice += newPrice * change; //Let the stockprice rise
			} 
			else
			{
				change -= 2 * this.volatility;
				newPrice += newPrice * change;
			}
		}
		else
		{
			newPrice += newPrice * change;
		}
		return newPrice;
	}

	public string GetName() {
		return this.stockName;
	}
}
