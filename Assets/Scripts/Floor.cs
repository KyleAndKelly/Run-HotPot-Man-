using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    float moveSpeed = 1.5f;
    // Start is called beore the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0,moveSpeed*Time.deltaTime,0);
        if(transform.position.y>6f){
            Destroy(gameObject);
            transform.parent.GetComponent<FloorManager>().SpwanFloor();
        }
        
    }
}
