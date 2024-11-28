using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHP;
    public float currenHP;


    private void Start()
    {
        currenHP = maxHP;
    }
    public virtual void TakeDamage(float damage)
    {
        currenHP -= damage;
        currenHP = Mathf.Max(0, currenHP);

    }
}
