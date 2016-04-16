using UnityEngine;
using System.Collections;

public class Purchase {

    private Stock name;
    private int amount;
    private double price;

    public Purchase(Stock name, int amount, double price)
    {
        this.name = name;
        this.amount = amount;
        this.price = price;
    }

    public void buy(int amount, double price)
    {
        this.amount += amount;
        this.price += price;
    }

    public void sell(int amount)
    {
        this.price -= (amount / this.amount) * this.price;
        this.amount -= amount;
    }
}
