using UnityEngine;
using System.Collections;

public class PlayerNetwork : Photon.MonoBehaviour {
    public Camera myCamera;
   // public AudioListener listner;
    public GameObject menu;
    // Use this for initialization
    void Start () {
        if(photonView.isMine)
        {
            myCamera.enabled = true;
           // listner.enabled = true;
            menu.SetActive(true);
        }
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
