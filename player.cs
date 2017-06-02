using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class player : Photon.MonoBehaviour {
    static Animator anim;
    private CharacterController mainobj;
    public Camera mycam;
    private float speed=2.0f;
    private float rotatespeed=75.0f;
    private Rigidbody2D rb;
    private float jumpPower = 500f;
    public float attackcd = 0.5f;
    private float damage=5f;
    float cd = 0;
    
   
    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        mainobj = GetComponent<CharacterController>();
        
    }
	
	// Update is called once per frame
	void Update () {
        if (PhotonNetwork.isMasterClient) {
            Debug.Log("hi Master!");
        }
        if (!photonView.isMine)
        {
            return;
        }
        ////////name
        
        /////////////////////////////////move
        float transtion = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotatespeed;
        transtion = transtion * Time.deltaTime;
        rotation = rotation * Time.deltaTime;

        transform.Translate(0, 0, transtion);
        transform.Rotate(0, rotation, 0);
        cd = cd - Time.deltaTime;
        if(transtion!=0)
        {
            anim.SetBool("Iswalk", true);
        }
        else
        {
            anim.SetBool("Iswalk", false);
        }
        ///////////////////////////////jump
        if(Input.GetKeyDown(KeyCode.Space) )
        {
            anim.SetBool("Isjump", true);
            jump();
        }
        else
        {
            anim.SetBool("Isjump", false);
        }

        /////////////////////////////run
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            anim.SetBool("Isrun", true);
            run();
        }
        else if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            anim.SetBool("Isrun", false);
            speed = 2.0f;
        }

        // 
        if(Input.GetButtonDown("Fire1") && !anim.GetBool("Isattack1"))
        {

            anim.SetBool("Isattack", true);
            anim.SetBool("Isattack1", true);
            damage = 5f;
            attack();
        }
        else if(Input.GetButton("Fire1") && anim.GetBool("Isattack1") && !anim.GetBool("Isattack2"))
        {
            
            damage = 5f;
            attack();
            anim.SetBool("Isattack2", true);
            anim.SetBool("Isattack1", false);
        }
        else if (Input.GetButton("Fire1") && anim.GetBool("Isattack2"))
        {
            
            damage = 10f;
            attack();
            anim.SetBool("Isattack", false);
            anim.SetBool("Isattack1", false);
            anim.SetBool("Isattack2", false);
            PhotonNetwork.Instantiate("Slime1", new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5)), new Quaternion(), 0);
        }
        else
        {
            anim.SetBool("Isattack", false);
            anim.SetBool("Isattack1", false);
            anim.SetBool("Isattack2", false);
        }



    }
    void attack()
    {
        if (cd > 0)
        {
            return;
        }

        cd = attackcd;
        Debug.Log("Firing our gun!");

        Ray ray = new Ray(mycam.transform.position, mycam.transform.forward);
        Transform hitTransform;
        Vector3 hitPoint;

        hitTransform = FindClosestHitObject(ray, out hitPoint);

        if (hitTransform != null)
        {
            Debug.Log("We hit: " + hitTransform.name);

            // We could do a special effect at the hit location
            // DoRicochetEffectAt( hitPoint );

            Health h = hitTransform.GetComponent<Health>();

            while (h == null && hitTransform.parent)
            {
                hitTransform = hitTransform.parent;
                h = hitTransform.GetComponent<Health>();
            }

            // Once we reach here, hitTransform may not be the hitTransform we started with!

            if (h != null)
            {
                h.GetComponent<PhotonView>().RPC("Getdamage", PhotonTargets.All, damage);
            }

            if(h.currentHealth<=0)
            {
                h.GetComponent<Animator>().SetBool("Isdead", true);
              
            }


        }

    }

    Transform FindClosestHitObject(Ray ray, out Vector3 hitPoint)
    {

        RaycastHit[] hits = Physics.RaycastAll(ray);

        Transform closestHit = null;
        float distance = 0;
        hitPoint = Vector3.zero;

        foreach (RaycastHit hit in hits)
        {
            if (hit.transform != this.transform && (closestHit == null || hit.distance < distance))
            {
                // We have hit something that is:
                // a) not us
                // b) the first thing we hit (that is not us)
                // c) or, if not b, is at least closer than the previous closest thing

                closestHit = hit.transform;
                distance = hit.distance;
                hitPoint = hit.point;
            }
        }

        // closestHit is now either still null (i.e. we hit nothing) OR it contains the closest thing that is a valid thing to hit

        return closestHit;

    }
    void jump()
    {
       
        //rb.AddForce(Vector2.up * jumpPower);
        
    }

    void run()
    {
        speed = 5.0f;

    }
    [PunRPC]
    public void test()
    {
        PhotonNetwork.Instantiate("Slime1", new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5)), new Quaternion(), 0);

    }
}
