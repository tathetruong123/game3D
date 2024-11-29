using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damezone : MonoBehaviour
{
    public Collider damageCollider;
    public int damageAmount = 20;

    public string targetTag;
    public List<Collider> collidersTargets = new List<Collider>();

    // danh sach cac collider cua enemy
    void Start()
    {
        damageCollider.enabled = false;
        
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag(targetTag) && !collidersTargets.Contains(other))
        {
            collidersTargets.Add(other);
            var go = other.GetComponent<HP>();
            if (go != null)
            {
                go.TakeDamage(damageAmount);
            }

        }
    }
    

        // Update is called once per frame
        private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(targetTag)&& !collidersTargets.Contains(other))
            {
            collidersTargets.Add(other);
            var go = other.GetComponent<HP>();
            if( go != null)
            {
                go.TakeDamage(damageAmount);
            }
        }
    }
    public void BeginAttack()
    {
        collidersTargets.Clear();
        damageCollider.enabled = true;
    }
    public void EndAttack()
    {
        collidersTargets.Clear();
        damageCollider.enabled=false;
    }
}
