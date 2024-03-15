using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Poker;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class SetCardTexture : MonoBehaviour
{
    // Start is called before the first frame update
    public Card card;
    public float speed = 10.0F;

    void Start()
    {

    }

    public void UpdateCard() {
        Texture2D texture = new Texture2D(2,2);
        ImageConversion.LoadImage(texture,File.ReadAllBytes("Assets/Textures/CardTextures/card_" + card.GetName() + ".png"));

        Material material = new Material(GetComponent<Renderer>().sharedMaterial);

        material.mainTexture = texture;

        GetComponent<Renderer>().sharedMaterial = material;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
