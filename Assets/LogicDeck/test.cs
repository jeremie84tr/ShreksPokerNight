using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Poker;
using Unity.VisualScripting;

public class test : MonoBehaviour
{
    public static Jeu monJeu;
    public int nbplayer;
    public int token;

    public GameObject cardSpawner;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void addCard(Card card) {
        CardCreator cardCreator = cardSpawner.GetComponent<CardCreator>();
        if (cardCreator.myCards.Count == 2){
            cardCreator.cpt = -0.5F;
            cardCreator.inPHand = false;
            cardCreator.inEHand = true;
            transform.position += new Vector3(0,5.25F,0);
            transform.Rotate(-120F,0F,0F,Space.Self);
        }
        if (cardCreator.myCards.Count == 4){
            cardCreator.cpt = -2F;
            cardCreator.inEHand = false;
            transform.position += new Vector3(0,5.25F,0);
            transform.Rotate(60F,0F,0F,Space.Self);
        }
        
        if (cardCreator.inPHand){
            transform.position = new Vector3(cardCreator.cpt, 6F, 3);
            cardCreator.cpt++;
        }
        else if (cardCreator.inEHand){
            transform.position = new Vector3(cardCreator.cpt, 6F, -3);
            cardCreator.cpt++;
        }
        else{
            transform.position = new Vector3(cardCreator.cpt, 5.25F, 0);
            cardCreator.cpt++;
        }
        cardCreator.myCards.Add(Instantiate(cardCreator.card, transform.position, transform.rotation));
        cardCreator.myCards[cardCreator.myCards.Count - 1].GetComponent<SetCardTexture>().card = card;
        cardCreator.myCards[cardCreator.myCards.Count - 1].GetComponent<SetCardTexture>().UpdateCard();
    }
    public void LaunchGame(){
        monJeu = new Jeu(token, nbplayer, addCard);
        Jeu.Test(monJeu);
        

        List<Card> allCards = new List<Card>();

        for (int i = 0; i < 2; i++)
        {
            allCards.Add(monJeu.player.hand.GetCard(i));
        }
        for (int i = 0; i < 2; i++)
        {
            allCards.Add(monJeu.AIs[0].hand.GetCard(i));
        }
        for (int i = 0; i < 3; i++)
        {
            allCards.Add(monJeu.deck.dealer[i]);
        }


        for (int i = 0; i < 7; i++)
        {
            addCard(allCards[i]);
        }
    }
    public void GiveUp(){
        monJeu.Pass(monJeu.player);
        monJeu.TourTermine();
    }
    public void Follow(){
        monJeu.Bet(0);
        monJeu.TourTermine();
    }
    public void Bet(){
        monJeu.Bet(displaybetvalue.value);
        monJeu.TourTermine();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
