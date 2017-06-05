using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Health : MonoBehaviour {
    public float current_HP = 100.0f;   // 當前血量
    public RectTransform healthbar;     // 血條 (已套用 unity canvas)
    static Animator anim;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
    }

    // 定時回復血量
    private float increaseHPCD = 1f;
    private float current_increaseHPCD = 0;
    void increasehp() {
        current_increaseHPCD -= Time.deltaTime;
        if (current_increaseHPCD > 0) {
            return;
        }
        current_increaseHPCD = increaseHPCD;

        // 0~100之間才會回血
        if(0 < current_HP && current_HP < 100) {
            current_HP = current_HP + 1;
        }
    }

    // 提供給別的玩家(電腦)呼叫的攻擊函式
    [PunRPC]
	public void Getdamage(float damage) {
        current_HP -= damage;
        isDead();

        healthbar.sizeDelta = new Vector2(current_HP, healthbar.sizeDelta.y);
    }

	// Update is called once per frame
	void Update () {
        increasehp();
        if (isDead()) {
            anim.SetBool("Isdead", true);
            
            // 死了就關閉人物移動
            this.GetComponent<player>().enabled = false;
        }

        healthbar.sizeDelta = new Vector2(current_HP, healthbar.sizeDelta.y);
    }

    public bool isDead() {
        if (current_HP <= 0) {  // dead
            current_HP = 0;
            //anim.SetBool("Isdead", true);
            return true;
        }
        return false;
    }

    // 可以看到別人血量回復
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.isWriting) {
            stream.SendNext(current_HP);
        } else if (stream.isReading) {
            current_HP = (float)stream.ReceiveNext();
        }
        healthbar.sizeDelta = new Vector2(current_HP, healthbar.sizeDelta.y);
    }
}
