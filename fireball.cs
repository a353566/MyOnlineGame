using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireball : MonoBehaviour {

    void OnCollisionEnter(Collision collision)
    {
        GameObject hit = collision.gameObject;
        Health hp = hit.GetComponent<Health>();

        if (hp != null)
        {
            hp.GetComponent<PhotonView>().RPC("Getdamage", PhotonTargets.All, 10f);

        }
       // Destroy(gameObject);

    }
}
