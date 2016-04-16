using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Stock : MonoBehaviour {

	private Stack<double> rates;
	private string name;
	private string imgPath;
	
	public Stock (string name, double startRate)
	{
		this.name = name;
		this.rates = new Stack<double> ();
		rates.Push (startRate);
	}
	
	public string GetImgPath() {
		return this.imgPath;
	}
	
	public void SetImgPath(string imgPath) {
		this.imgPath = imgPath;
	}
	
	public void SetNextRate(double rate) {
		this.rates.Push (rate);
	}
	
	public Stack<double> GetRates() {
		return this.rates;
	}

    public static double NextPrice()
    {
        double output = 0;
        switch (rnd.Next(1, 11))
        {
            case 1:
                if ((double)rnd.Next(1, 11) > 8)
                {
                    output = rates.Peek() - ((double)rnd.Next(4, 19) / (double)rnd.Next(97, 101)) * rates.Peek(); // Do a deep dive!
                }
                else
                {
                    output = rates.Peek() * 1.02; // Small increase
                }
                break;
            case 2:
                if ((double)rnd.Next(1, 3) == 1)
                {
                    output = rates.Peek() + ((double)rnd.Next(2, 5) / (double)rnd.Next(97, 101)) * rates.Peek(); // Steady increase
                }
                else
                {
                    output = rates.Peek() - ((double)rnd.Next(1, 4) / (double)rnd.Next(97, 101)) * rates.Peek(); // Steady decrease
                }

                break;
            case 3:
                if ((double)rnd.Next(1, 3) == 1)
                {
                    output = rates.Peek() * 1.016;
                }
                else
                {
                    output = rates.Peek() * 0.995;
                }

                break;
            case 4:
                if ((double)rnd.Next(1, 3) == 1)
                {
                    output = rates.Peek() * 1.013;
                }
                else
                {
                    output = rates.Peek() * 0.99;
                }

                break;
            case 5:
                if ((double)rnd.Next(1, 3) == 1)
                {
                    output = rates.Peek() * 1.021;
                }
                else
                {
                    output = rates.Peek() * 0.99;
                }

                break;
            case 6:
                if ((double)rnd.Next(1, 3) == 1)
                {
                    output = rates.Peek() * 1.01;
                }
                else
                {
                    output = rates.Peek() * 0.99;
                }
                break;
            case 7:
                if ((double)rnd.Next(1, 3) == 1)
                {
                    if ((double)rnd.Next(1, 5) > 3)
                    {
                        output = rates.Peek() - ((double)rnd.Next(5, 10) / (double)rnd.Next(97, 101)) * rates.Peek(); // Steady decrease
                    }
                    else
                    {
                        output = rates.Peek() + ((double)rnd.Next(2, 6) / (double)rnd.Next(97, 101)) * rates.Peek(); // Steady increase
                    }
                }
                else
                {
                    output = rates.Peek() - ((double)rnd.Next(1, 4) / (double)rnd.Next(97, 101)) * rates.Peek(); // Steady decrease
                }

                break;
            default:
                if ((double)rnd.Next(1, 3) == 1)
                {
                    if ((double)rnd.Next(1, 6) > 3)
                    {
                        output = rates.Peek() - ((double)rnd.Next(rnd.Next(1, 4), rnd.Next(3, 8)) / (double)rnd.Next(97, 101)) * rates.Peek(); // Steady decrease
                    }
                    else
                    {
                        output = rates.Peek() + ((double)rnd.Next(rnd.Next(2, 4), rnd.Next(3, 6)) / (double)rnd.Next(97, 101)) * rates.Peek(); // Steady increase
                    }
                }
                else
                {
                    output = rates.Peek() - ((double)rnd.Next(1, 4) / (double)rnd.Next(97, 101)) * rates.Peek(); // Steady decrease
                }
                break;
        }
        return output;

    }

	public string GetName() {
		return this.name;
	}
}
