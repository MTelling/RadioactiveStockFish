using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Stock : MonoBehaviour {

	private Stack<double> rates;
	private string name;
	private string imgPath;
	
	public Stock (string name)
	{
		this.name = name;
		this.rates = new Stack<double> ();
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

	public string GetName() {
		return this.name;
	}
}
