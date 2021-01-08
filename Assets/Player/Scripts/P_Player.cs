using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Player : MonoBehaviour
{
    [SerializeField] public int health = 100;
    [SerializeField] float speed = 1;
    [SerializeField] float speedRotation = 1;
    [SerializeField] int damage = 1;
    [SerializeField] Camera camera1 = null;
    [SerializeField] Animator playerAnim = null;
    [SerializeField] IA_Brain target = null;
    [SerializeField] bool canMove = true;

    [SerializeField] float VisualRange = 20;

    Vector3 movement = Vector3.zero;
    public int Health => health;

    //float rotateY = 0, rotateX = 0;
    public void SetHealth(int _health) => health = _health;

    public bool IsAtRange => Vector3.Distance(target.transform.position, transform.position) < VisualRange;

    public bool IsDead => health >= 0;
    
    public bool CanMove => canMove = !IsDead;

    // Start is called before the first frame update
    void Start()
    {
        //playerAnim.SetBool("IsWalking", true);

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Hit();
        Die();
    }

    void Move()
	{
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");
        playerAnim.Play("Walk");
        movement = new Vector3(moveHorizontal, 0f, moveVertical);
        movement = movement * speed * Time.deltaTime;
        transform.position += movement;
    }

    void Hit()
	{
        if (IsAtRange)
            if (Input.GetButton("Fire1"))
            {
                target.Heal.health -= damage;
                playerAnim.Play("Punch");
                HurtEnnemy();
                if (target.Heal.health >= 0)
                {
                    target.Heal.health = 0;
                    Debug.Log("Is dead");
                }
            }
	}

    void HurtEnnemy()
    {
        if (target.Heal.health > 100 && target.Detection.IsDetected == false)
            target.FSM.SetBool("Is_Healing", true);
    }

    void Die()
    {
        if (health ==0) //IsDead
        {
            playerAnim.Play("Dead");
            canMove = false;
        }
    }
  
}
