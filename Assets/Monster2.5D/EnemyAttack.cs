using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public EnemyMove EM;

    private void OnTriggerStay(Collider collider) {
        if(collider.gameObject.tag == "Player") {
            Vector3 pos = EM.trans.position;
            Vector3 playerPos = collider.gameObject.transform.position;

            if(Vector3.Distance(pos, playerPos) <= 1.5f)
                EM.animator.SetTrigger(EM.attack_param);
            else
                EM.pos = playerPos - pos;
        }
    }
}