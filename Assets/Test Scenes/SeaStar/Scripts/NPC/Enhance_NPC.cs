using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enhance_NPC : NPC
{
    public GameObject Enhance_UI;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Enhance_UI = GameObject.Find("Canvas").transform.Find("Enhance_Ability").gameObject;

        Scale = transform.localScale;
        DefaultX = Scale.x;
        FlipX = -Scale.x;

        talk = OpenEngance;
    }

    void Update()
    {
        Flip();
        talkWithPlayer();
    }

    void OpenEngance()
    {
        Enhance EH = Enhance_UI.GetComponent<Enhance>();
        TestTest py = Player.GetComponent<TestTest>();
        EH.player = Player;
        EH.am = Player.GetComponent<AbilityManager>();

        if (py.ActiveAbility.AbSprite == null && py.PassiveAbility.AbSprite == null) { SpeechBubble(SpeechBubbles[0]); }
        else
        {
            Enhance_UI.SetActive(true);
            EH.ability.Clear();
            EH.ability.Add(Player.GetComponent<TestTest>().ActiveAbility);
            EH.ability.Add(Player.GetComponent<TestTest>().PassiveAbility);
        }
    }
}
