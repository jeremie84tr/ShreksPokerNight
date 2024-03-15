using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Poker;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CardCreator : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject card; 
    public Deck deck;

    private List<GameObject> myCards;

    private bool inPHand = true;
    private bool inEHand = false;
    private float cpt = -0.5F; 

    void Start()
    {
        deck = new Deck();
        myCards = new List<GameObject>();
        deck.Shuffle();
        transform.Rotate(-30F,0F,0F,Space.Self);
        for (int i = 0; i < 9; i++){
            Distrib();
        }
    }

    public void Distrib(){
        if (myCards.Count < 9){
            Card myCard = deck.DrawCard();
            Debug.Log(myCards.Count);
            if (myCards.Count == 2){
                cpt = -0.5F;
                inPHand = false;
                inEHand = true;
                transform.position += new Vector3(0,5.25F,0);
                transform.Rotate(-120F,0F,0F,Space.Self);
            }
            if (myCards.Count == 4){
                cpt = -2F;
                inEHand = false;
                transform.position += new Vector3(0,5.25F,0);
                transform.Rotate(60F,0F,0F,Space.Self);
            }
            
            if (inPHand){
                transform.position = new Vector3(cpt, 6F, 3);
                cpt++;
            }
            else if (inEHand){
                transform.position = new Vector3(cpt, 6F, -3);
                cpt++;
            }
            else{
                transform.position = new Vector3(cpt, 5.25F, 0);
                cpt++;
            }
            myCards.Add(Instantiate(card, transform.position, transform.rotation));
            myCards[myCards.Count - 1].GetComponent<SetCardTexture>().card = myCard;
            myCards[myCards.Count - 1].GetComponent<SetCardTexture>().UpdateCard();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
