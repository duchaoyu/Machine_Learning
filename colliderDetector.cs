using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colliderDetector : MonoBehaviour
{
    public bool isCollided = false;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("target"))
        {
            isCollided = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
