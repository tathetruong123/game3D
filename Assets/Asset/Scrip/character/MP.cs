using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MP : MonoBehaviour
{
    public int MaxMP;
    public int CurrentMP;

    private void Start()
    {
        CurrentMP = MaxMP;
        StartCoroutine(RecoverMP());
    }
    IEnumerator RecoverMP()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            CurrentMP += 10;
            CurrentMP = Mathf.Min(MaxMP, CurrentMP);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
