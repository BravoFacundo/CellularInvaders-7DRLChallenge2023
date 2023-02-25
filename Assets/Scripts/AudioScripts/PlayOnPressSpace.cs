using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOnPressSpace : MonoBehaviour
{
    public AK.Wwise.Event testTone;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            testTone.Post(gameObject);
        }
    }
}
