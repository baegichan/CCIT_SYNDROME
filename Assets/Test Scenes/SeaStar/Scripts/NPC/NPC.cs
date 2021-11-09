using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    protected GameObject Player;
    public List<GameObject> SpeechBubbles;
    public bool IsPlayer;

    protected Vector3 Scale;
    protected float DefaultX;
    protected float FlipX;

    protected void Flip()
    {
        GameObject player = Player.GetComponent<Char_Parent>().SelectChar;
        if(player.transform.position.x > transform.position.x) { Scale.x = DefaultX; }
        else { Scale.x = FlipX; }

        transform.localScale = Scale;
    }

    GameObject ActiveBubble;

    protected void SpeechBubble(GameObject Bubble)
    {
        Vector3 BubblePos = new Vector3(transform.position.x + 0.5f, transform.position.y + 1.5f, transform.position.z);

        GameObject BB = Instantiate(Bubble, BubblePos, Quaternion.identity);
        ActiveBubble = BB;
        Destroy(ActiveBubble, 1.5f);
    }

    public delegate void Talk();
    public Talk talk;

    protected void talkWithPlayer()
    {
        float Distance = Vector2.Distance(transform.position, Player.transform.position);
        if(Distance <= 2f) { IsPlayer = true; }
        else { IsPlayer = false; }

        if(IsPlayer && Input.GetKeyDown(KeyCode.F)) { talk(); }
    }
}