using UnityEngine;
using System.Collections;

public class Purchase {

    private string name;
    private int amount;
    private double price;

    public Purchase(string name, int amount, double price)
    {
        this.name = name;
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
        this.price -= (amount / this.amount) * this.price;
        this.amount -= amount;
    }

	public string GetName() {
		return this.name;
	}

	public int GetAmount() {
		return this.amount;
	}
}
