using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class displaybetvalue : MonoBehaviour
{
    // Start is called before the first frame update
    public static int value;
    public TMP_Text text;
    void Start()
    {
        value = 0;
    }

    // Update is called once per frame
    public void Up(){
        value++;
    }
    public void Down(){
        if (value > 0){
            value--;
        }
    }

    void Update()
    {
        text.text = "" + value; 
    }
}
