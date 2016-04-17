using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NavManager : MonoBehaviour {

    public GameObject BetPopUP_object;
    public GameObject BuyPanel_object;
    public GameObject OverviewPanel_object;
    // Use this for initialization
    void Start () {
/*
       BetPopUP_object = GameObject.FindGameObjectWithTag("BetPopUP");
       BuyPanel_object = GameObject.FindGameObjectWithTag("BuyPanel");
       OverviewPanel_object = GameObject.FindGameObjectWithTag("OverviewPanel");
       */
	}

	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (BetPopUP_object.active)
            {
                BetPopUP_object.active = false;
            }
            else if (BuyPanel_object.active)
            {
                BuyPanel_object.active = false;
                OverviewPanel_object.active = true;
            }
        }	    
	}

    public void OpenBetPopUP()
    {

        BetPopUP_object.active = true;

    }

}
