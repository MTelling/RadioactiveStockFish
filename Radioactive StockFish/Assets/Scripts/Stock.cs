using UnityEngine;
using System.Collections;

public class Stock : MonoBehaviour {

	private Stack<double> rates;
	private String name;
	private String imgPath;
	
	public Stock (String name)
	{
		this.name = name;
		this.rates = new Stack<double> ();
	}
	
	public String GetImgPath() {
		return this.imgPath;
	}
	
	public void SetImgPath(String imgPath) {
		this.imgPath = imgPath;
	}
	
	public void SetNextRate(double rate) {
		this.rates.push (rate);
	}
	
	public Stack<double> getRates() {
		return this.rates;
	}
}
