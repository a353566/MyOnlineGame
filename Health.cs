using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Health : MonoBehaviour {
    public float currentHealth = 100.0f;
    public RectTransform healthbar;
    static Animator anim;
    private float healthcd = 1f;
    float cd = 0;
    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
    }
    void increasehp()
    {
        if (cd > 0)
        {
            return;
        }

        if(currentHealth<100)
        {
            currentHealth = currentHealth + 1;
        }
        
        cd = healthcd;
    }
    [PunRPC]
	public void Getdamage(float damage)
    {
        
        currentHealth = currentHealth - damage;
        if(currentHealth <= 0)
        {
            ////////dead
            currentHealth=0;
           // anim.SetBool("Isdead", true);
        }

        healthbar.sizeDelta = new Vector2(currentHealth, healthbar.sizeDelta.y);
    } 

	// Update is called once per frame
	void Update () {
        cd = cd - Time.deltaTime;
        increasehp();
        if (currentHealth <= 0)
        {
            ////////dead
            currentHealth=0;
            
            anim.SetBool("Isdead", true);

            player pl = this.GetComponent<player>();
            pl.enabled = false;
        }

        healthbar.sizeDelta = new Vector2(currentHealth, healthbar.sizeDelta.y);
      

    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(currentHealth);
        }
        else if (stream.isReading)
        {
            currentHealth = (float)stream.ReceiveNext();
        }
        healthbar.sizeDelta = new Vector2(currentHealth, healthbar.sizeDelta.y);
    }
}
