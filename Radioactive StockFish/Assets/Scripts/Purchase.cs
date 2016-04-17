using UnityEngine;
using System.Collections;

public class Purchase {

    private Stock stock;
    private int amount;
    private double price;

    public Purchase(Stock stock, int amount, double price)
    {
        this.stock = stock;
        this.amount = amount;
        this.price = price;
    }

    public void New(int amount, double price)
    {
        this.amount += amount;
        this.price += price;
    }

    public void Sell(int amount)
    {
		this.price -= ((double)amount / (double)this.amount) * this.price;
        this.amount -= amount;
    }

	public string GetName() {
		return this.stock.GetName();
	}

	public int GetAmount() {
		return this.amount;
	}

	public double GetCurrentPrice() {
		return this.amount * this.stock.GetRates ().Peek ();
	}

	public double GetProfit() {
		return GetCurrentPrice () - this.price;
	}
}
