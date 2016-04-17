using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NavManager : MonoBehaviour {

    public Image popUp;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameObject.FindGameObjectWithTag("BetPopUP") != null && GameObject.FindGameObjectWithTag("BetPopUP").active)
            {
                GameObject.FindGameObjectWithTag("BetPopUP").active = false;
            }
            else if (GameObject.FindGameObjectWithTag("BuyPanel") != null && GameObject.FindGameObjectWithTag("BuyPanel").active)
            {
                GameObject.FindGameObjectWithTag("BuyPanel").active = false;
                GameObject.FindGameObjectWithTag("OverviewPanel").active = true;
            }
        }	    
	}

    public void OpenBetPopUP()
    {
         if(GameObject.FindGameObjectWithTag("BetPopUP") != null)
            GameObject.FindGameObjectWithTag("BetPopUP").SetActive(true);
    }

}
