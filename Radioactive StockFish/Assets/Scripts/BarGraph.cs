using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class BarGraph : MonoBehaviour {

    public int barCount = 20;
    public int barSize = 30;
    public GameObject barPrefab;

    public void Start() {
        for(int i = 0; i < barCount; i++) {
            GameObject bar = Instantiate(barPrefab, new Vector3(barSize + (barSize * i), barSize, 0), Quaternion.identity) as GameObject;
            bar.name = "Bar #" + i;
            bar.transform.SetParent(this.transform, true);

        }

       
    }

    public void drawGraph(Stock stock) {
        Stack<double> dupeStack = new Stack<double>(stock.GetRates());
        double[] priceList = dupeStack.ToArray();

        if (priceList.Length >= barCount) {
            int maxSize = 0;

            for (int i = priceList.Length - 1; i > priceList.Length - barCount; i--) {
                if (priceList[i] > maxSize) maxSize = (int) (Math.Ceiling(priceList[i] / 100d));
            }

			for (int i = 1; i < barCount + 1; i++) {
				int percentageValue = (int)Math.Floor(priceList[priceList.Length - i] / maxSize);
                this.transform.GetChild(barCount - i).GetComponent<Slider>().value = percentageValue;
            }
        }
        
    }
}
