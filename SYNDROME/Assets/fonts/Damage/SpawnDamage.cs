using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnDamage : MonoBehaviour
{
    public TextMesh text;
    public Vector3 Damage_Position, Heal_Position;  //���ͺ� ������ �ߴ� ��ġ �����ϼ�
    public int damage, Heal;  //���� ���� �Ǿ��ִ��� ���� �ӽ� ������ ������

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
        //�÷��̾� ��ũ��Ʈ
        Damage_Position = this.gameObject.transform.position;
        if(this.gameObject.transform.parent.tag == "enemy")
        {
           // text.text = damage.ToString();  //�������� ���� ��ũ��Ʈ ���� �� �ش� Ŭ�������� ����Ǿ��ִ� �������� ����

           // Instantiate(text, Damage_Position, Quaternion.identity);
        }

        //���� ��ũ��Ʈ
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
