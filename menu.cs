using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class menu: Photon.MonoBehaviour
{

    public GameObject menue;
    public Button startbutton;
    

    public Text ID;
    public InputField input;

    // Use this for initialization
    void Start () {
        startbutton = startbutton.GetComponent<Button>();
      
        ID = GameObject.Find("name").GetComponent<Text>();
       

        input = GetComponent<InputField>();

    }
	public void startlevel()
    {
        menue.SetActive(false);
       
    }

  

    void Update () {
        ID.text = input.text;
    }

  /* 
    }*/
}
