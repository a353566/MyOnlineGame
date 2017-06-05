using UnityEngine;
using System.Collections;

public class Networkmanager : Photon.MonoBehaviour {
    public Camera standbycamera;
    GameObject myplayer;

    // Use this for initialization
    void Start () {
        PhotonNetwork.autoJoinLobby = true;
        PhotonNetwork.ConnectUsingSettings("version:0.1");
    }

	void OnJoinedLobby() {  PhotonNetwork.JoinRandomRoom(); }
    void OnPhotonRandomJoinFailed() {  PhotonNetwork.CreateRoom(null); }

    // 進入遊戲時
    void OnJoinedRoom() {
        spawnMyPlayer();
        //PhotonNetwork.Instantiate("Slime1", new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5)), SpawnPoint1.rotation, 0);
		//PhotonNetwork.Instantiate("EnemyMamagement", new Vector3(0,20,0), new Quaternion(), 0);
    }

    // 建立自己的玩家
    public void spawnMyPlayer() {
        standbycamera.enabled = false;
        Vector3 spawn = new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
        myplayer = (GameObject)PhotonNetwork.Instantiate("Mymain", spawn, new Quaternion(), 0);
        
        // 開啟血條
        ((MonoBehaviour)myplayer.GetComponent("Health")).enabled = true;

        // Camera 拉到背後
        myplayer.transform.Find("myCamera").gameObject.SetActive(true);
    }
    
    // Update is called once per frame
    void Update () {
	
	}
}
