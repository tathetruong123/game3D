using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource.spatialBlend = 1;
        audioSource.minDistance = 2f;
        audioSource.maxDistance = 50f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
