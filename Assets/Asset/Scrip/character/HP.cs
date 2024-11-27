using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP : MonoBehaviour
{
    public float maxHP;
    public float currentHP;

    public virtual void TakeDamage(float damage)
    {
        currentHP -= damage;
        currentHP =Mathf.Max(0,currentHP);
    }



    // Start is called before the first frame update
     private void Start()
    {
        currentHP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
