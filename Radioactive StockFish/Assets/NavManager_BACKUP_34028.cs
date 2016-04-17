using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NavManager : MonoBehaviour {

<<<<<<< HEAD
    public ScrollRect popUp;
	// Use this for initialization
	void Start () {
	
	}
||||||| merged common ancestors
    public Image popUp;
	// Use this for initialization
	void Start () {
	
	}
=======
    public GameObject BetPopUP_object;
    public GameObject BuyPanel_object;
    public GameObject OverviewPanel_object;
    // Use this for initialization
    void Start () {

       BetPopUP_object = GameObject.FindGameObjectWithTag("BetPopUP");
       BuyPanel_object = GameObject.FindGameObjectWithTag("BuyPanel");
       OverviewPanel_object = GameObject.FindGameObjectWithTag("OverviewPanel");
}
>>>>>>> origin/master
	
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
<<<<<<< HEAD
         if(GameObject.FindGameObjectWithTag("BetPopUP") != null)
            GameObject.FindGameObjectWithTag("BetPopUP").SetActive(true);

		popUp.enabled = true;
||||||| merged common ancestors
         if(GameObject.FindGameObjectWithTag("BetPopUP") != null)
            GameObject.FindGameObjectWithTag("BetPopUP").SetActive(true);
=======
        BetPopUP_object.active = true;
>>>>>>> origin/master
    }

}
