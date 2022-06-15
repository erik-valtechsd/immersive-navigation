using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDisablePanel : MonoBehaviour
{
    float timestamp = 0f;
    // Start is called before the first frame update
    void Start()
    {
         timestamp = Time.time+12;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time>timestamp){
            this.gameObject.SetActive(false);
        }
    }
}
