using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class BarGraph : MonoBehaviour {

    public int barCount = 15;
    public int barSize = 48;
    public GameObject barPrefab;

    public void Start() {
        for(int i = 0; i < barCount; i++) {
            GameObject bar = Instantiate(barPrefab, new Vector3(barSize + (barSize * i), barSize, 0), Quaternion.identity) as GameObject;
            bar.name = "Bar #" + i;
            bar.transform.SetParent(this.transform, true);
        }

        Stock a = new Stock("Amazon", 100, 0.03);
        for (int i = 0; i < barCount; i++) a.SetNextRate();
        this.drawGraph(a);
    }

    public void drawGraph(Stock stock) {
        Stack<double> dupeStack = new Stack<double>(stock.GetRates());
        double[] priceList = dupeStack.ToArray();

        if (priceList.Length >= barCount) {
            int maxSize = 0;
            for (int i = priceList.Length - 1; i > priceList.Length - barCount; i--) {
                if (priceList[i] > maxSize) maxSize = (int) (Math.Ceiling(priceList[i] / 100d));
                Debug.Log(Math.Ceiling((priceList[i] / 100d) * 100));
            }

            for (int i = barCount - 1; i >= 0; i--) {
                int percentageValue = (int)Math.Floor(dupeStack.Pop() / maxSize);
                this.transform.GetChild(i).GetComponent<Slider>().value = percentageValue;
                Debug.Log(percentageValue);
            }
        }
    }
}
