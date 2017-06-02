using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage {

    // 計算攻擊 並回傳是否有正確攻擊
    public static bool damage(Transform hitTransform, float damageVal) {
        // We could do a special effect at the hit location
        // DoRicochetEffectAt( hitPoint );

        Health targetHealth = hitTransform.GetComponent<Health>();

        while (targetHealth == null && hitTransform.parent) {
            hitTransform = hitTransform.parent;
            targetHealth = hitTransform.GetComponent<Health>();
        }

        // Once we reach here, hitTransform may not be the hitTransform we started with!

        // 不是 null 就對他造成傷害
        if (targetHealth != null) {
            targetHealth.GetComponent<PhotonView>().RPC("Getdamage", PhotonTargets.All, damageVal);
        } else {
            return false;
        }

        // 對手是玩家的話可以不用加這條
        /*if (targetHealth.current_HP <= 0) {
            targetHealth.GetComponent<Animator>().SetBool("Isdead", true);
        }*/

        return true;
    }
}
