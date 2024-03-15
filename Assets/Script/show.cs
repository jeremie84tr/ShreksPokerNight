using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class show : MonoBehaviour
{
    public GameObject button;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Show(){
        button.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
