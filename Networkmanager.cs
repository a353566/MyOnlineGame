using UnityEngine;
using System.Collections;

public class Networkmanager : Photon.MonoBehaviour {
    public Transform SpawnPoint1;
    public Transform SpawnPoint2;
    public Transform SpawnPoint3;
    public Transform SpawnPoint4;
    public Camera standbycamera;
    public int playercount = 0;
    GameObject myplayer;
    // Use this for initialization
    void Start () {
        PhotonNetwork.autoJoinLobby = true;
        PhotonNetwork.ConnectUsingSettings("version:0.1");
        
    }
	void OnJoinedLobby()
    {
        PhotonNetwork.JoinRandomRoom();
    }
    void OnPhotonRandomJoinFailed()
    {
        PhotonNetwork.CreateRoom(null);
    }
    void OnJoinedRoom()
    {
        spawnmyplayer();
        //PhotonNetwork.Instantiate("Slime1", new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5)), SpawnPoint1.rotation, 0);
		//PhotonNetwork.Instantiate("EnemyMamagement", new Vector3(0,20,0), new Quaternion(), 0);
    }
    public void spawnmyplayer()
    {
        standbycamera.enabled = false;
        Vector3 spawn = new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
        if (playercount%4==0)
        {
             myplayer = (GameObject)PhotonNetwork.Instantiate("Mymain", spawn, SpawnPoint1.rotation, 0);
        }
        else if(playercount%4==1)
        {
             myplayer = (GameObject)PhotonNetwork.Instantiate("Mymain", spawn, SpawnPoint2.rotation, 0);
        }
        else if (playercount%4 == 2)
        {
             myplayer = (GameObject)PhotonNetwork.Instantiate("Mymain", spawn, SpawnPoint3.rotation, 0);
        }
        else if (playercount%4 == 3)
        {
             myplayer = (GameObject)PhotonNetwork.Instantiate("Mymain", spawn, SpawnPoint4.rotation, 0);
        }

        //((MonoBehaviour)myplayer.GetComponent("player")).enabled = true;
        ((MonoBehaviour)myplayer.GetComponent("Health")).enabled = true;
       // Health hea = myplayer.GetComponent<Health>();
        //hea.enabled = true;
        myplayer.transform.Find("myCamera").gameObject.SetActive(true);
        //Animator ani = myplayer.GetComponent<Animator>();
        //ani.enabled = true;

        playercount++;

    }

    
    // Update is called once per frame
    void Update () {
	
	}
}
