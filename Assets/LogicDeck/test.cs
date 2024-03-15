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
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void LaunchGame(){
        monJeu = new Jeu(token, nbplayer);
        Jeu.Test(monJeu);
    }
    public void GiveUp(){
        monJeu.Pass(monJeu.player);
    }
    public void Follow(){
        monJeu.Bet(0);
    }
    public void Bet(){
        monJeu.Bet(displaybetvalue.value);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
