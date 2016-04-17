using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class BarGraph : MonoBehaviour {

    public int barSize = 15;
    public GameObject barPrefab;

    public void Start() {
        for(int i = 0; i < barSize; i++) {
            GameObject bar = Instantiate(barPrefab, new Vector3(48 + (48 * i), 48, 0), Quaternion.identity) as GameObject;
            bar.name = "Bar #" + i;
            bar.transform.parent = this.transform;
        }
    }

    public void drawGraph(Stock stock) {
        Stack<double> dupeStack = new Stack<double>(stock.GetRates());
        double[] priceList = dupeStack.ToArray();

        if (priceList.Length >= barSize) {
            int maxSize = 0;
            for (int i = priceList.Length - 1; i > priceList.Length - barSize; i--) {
                if (priceList[i] > maxSize) maxSize = (int) (Math.Ceiling(priceList[i] / 100d) * 100);
            }

            for (int i = barSize - 1; i >= 0; i--) {
                int percentageValue = (int)Math.Floor(dupeStack.Pop() / maxSize);
                this.transform.GetChild(i).GetComponent<Slider>().value = percentageValue;
            }
        }
    }
}
