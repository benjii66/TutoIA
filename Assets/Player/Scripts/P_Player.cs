using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Player : MonoBehaviour
{
    [SerializeField] int health = 100;
    [SerializeField] float speed = 1;
    [SerializeField] float speedRotation = 1;
    [SerializeField] Camera camera1 = null;
    [SerializeField] Animator playerAnim = null;
    [SerializeField] Transform target = null;

    [SerializeField] float VisualRange = 20;

    Vector3 movement = Vector3.zero;
    public int Health => health;
    float rotateY = 0, rotateX = 0;
    public void SetHealth(int _health) => health = _health;

    public bool IsAtRange => Vector3.Distance(target.position, transform.position) < VisualRange;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Hit();
    }

    void Move()
	{
 
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");
        playerAnim.Play("Walk");
        transform.LookAt(target);
        movement = new Vector3(moveHorizontal, 0f, moveVertical);
        movement = movement * speed * Time.deltaTime;
        transform.position += movement;
    }

    void Hit()
	{
        if (IsAtRange)
            if (Input.GetButton("Fire1"))
                playerAnim.Play("Punch");
	}

  
}
