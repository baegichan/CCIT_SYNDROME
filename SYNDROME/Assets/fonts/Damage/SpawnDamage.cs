using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnDamage : MonoBehaviour
{
    public TextMesh text;
    public Vector3 Damage_Position, Heal_Position;  //몬스터별 데미지 뜨는 위치 지정하쇼
    public int damage, Heal;  //공격 어케 되어있는지 몰라서 임시 뗌빵용 데미지

    private void Start()
    {
    
        Damage_Position = this.gameObject.transform.position;
    }
    private void Update()
    {
        //text.color= new Color(text.color.r, text.color.g, text.color.b, Mathf.Clamp(text.color.a-1,0,255));
    }
    public void setATF1(int ATF)
    {
        damage = ATF;
        text.text = string.Format("{0}", damage);
        text.color = Color.white;
        float random = Random.Range(7, 11);
        text.fontSize=(int)random;
    }
    public void setATF2(int ATF)
    {
        damage = ATF;
        text.text = string.Format("{0}", damage);
        text.color = Color.red;
        float random = Random.Range(7, 11);
        text.fontSize = (int)random;
    }
    public void setHEal(int ATF)
    {
        damage = ATF;
        text.text = string.Format("{0}", damage);
        text.color = Color.green;
        float random = Random.Range(7, 11);
        text.fontSize = (int)random;
    }
    public void damaged() {
        //플레이어 스크립트
        Damage_Position = this.gameObject.transform.position;
        if(this.gameObject.transform.parent.tag == "enemy")
        {
           // text.text = damage.ToString();  //데미지는 몬스터 스크립트 접근 후 해당 클래스에서 선언되어있는 데미지로 설정

           // Instantiate(text, Damage_Position, Quaternion.identity);
        }

        //몬스터 스크립트
        if(this.gameObject.transform.parent.tag=="Player")
        {
           // text.text = damage.ToString();
           // Instantiate(text, Damage_Position, Quaternion.identity);
        }
    }

    public void heal()
    {
        text.text = Heal.ToString();
        Instantiate(text, Heal_Position, Quaternion.identity);
    }
}
