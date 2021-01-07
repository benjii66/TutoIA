using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Player : MonoBehaviour
{
    [SerializeField] int health = 100;


    public int Health => health;

    public void SetHealth(int _health) => health = _health;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
