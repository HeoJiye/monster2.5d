using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyMove : MonoBehaviour
{
    private float speed = 2f;

    public Animator animator;
    public Rigidbody physicsBody;
    public SpriteRenderer spriteRenderer;
    public Transform trans;

    private Vector3 _movement;
    
    public bool isAttacked = false;
    public int direction = 0;

    public string walk_param = "Walk";
    public string attacked_param = "Attacked";
    public string attack_param = "Attack";

    public Vector3 pos;

    void Start() {
        Invoke("Think", 3);
    }
    private void Update() {
        if(!isAttacked) Move();
    }

    void Think() {
        if (!isAttacked) pos = SetPos();

        float nextThinkTime = Random.Range(2f, 5f);
        Invoke("Think", nextThinkTime);
    }

    Vector3 SetPos() {
        direction = Random.Range(-2, 3);

        // Vertical
        float moveZ = 0;
        if (direction == -1) moveZ = 1;
        else if (direction == 1) moveZ = -1;

        // Horizontal
        float moveX = 0;
        if (direction == -2) moveX = 1;
        else if (direction == 2) moveX = -1;

        return new Vector3(moveX, 0, moveZ);
    }
    void Move() {
        // Normalize
        if(pos.x != 0) spriteRenderer.flipX = pos.x > 0;

        _movement = pos.normalized;

        animator.SetBool(walk_param, Math.Abs(_movement.sqrMagnitude) > Mathf.Epsilon);
    }

    private void FixedUpdate()
    {
        physicsBody.velocity = _movement * speed;
    }


    private void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.tag == "Skill")
            animator.SetTrigger(attacked_param);
    }
    private void OnCollisionStay(Collision collision) {
        if(collision.gameObject.tag == "Skill") {
            isAttacked = true;
            spriteRenderer.color = Color.red;
        }
    }
    private void OnCollisionExit(Collision collision) {
        if(collision.gameObject.tag == "Skill") {
            isAttacked = false;
            spriteRenderer.color = Color.white;
        }
    }

    private void OnTriggerEnter(Collider collision) {
        if(collision.gameObject.tag == "Skill")
            animator.SetTrigger(attacked_param);
    }
    private void OnTriggerStay(Collider collision) {
        if(collision.gameObject.tag == "Skill") {
            isAttacked = true;
            spriteRenderer.color = Color.red;
        }
    }
    private void OnTriggerExit(Collider collision) {
        if(collision.gameObject.tag == "Skill") {
            isAttacked = false;
            spriteRenderer.color = Color.white;
        }
    }
}
