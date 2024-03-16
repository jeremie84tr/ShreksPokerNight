using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hide : MonoBehaviour
{
    public GameObject button;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Hide(){
        button.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
