using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;

public class MapBlocker : MonoBehaviour
{
    public ParticleSystem Wall;
    public ParticleSystem.MainModule Lifetime;
    public bool Downning=false;
    // Update is called once per frame
    private void Start()
    {
        Lifetime = Wall.main;
        PotalEvent Potal = transform.parent.GetComponent<PotalEvent>();
        MapLineDraw Line = Potal.GetComponent<MapLineDraw>();
        EdgeCollider2D edge= GetComponent<EdgeCollider2D>();
        //(��üũ�� - ���ũ��(2))/2 
        var shape = Wall.shape; 
        switch (Potal.PotalDirection)
        {
         
            case Potals.PotalType.L:
                shape.rotation = new Vector3(0,90,90);
                Wall.gameObject.transform.localPosition = new Vector3(0, (Mathf.Abs(Line.T_Area) + Mathf.Abs(Line.B_Area) - 2) / 2, 0);          
                shape.scale = new Vector3(Mathf.Abs(Line.T_Area)+ Mathf.Abs(Line.B_Area), 1, 0.03f);
                break;
            case Potals.PotalType.R:
                shape.rotation = new Vector3(0, -90, 90);
                Wall.gameObject.transform.localPosition = new Vector3(0, (Mathf.Abs(Line.T_Area) + Mathf.Abs(Line.B_Area) - 2) / 2, 0);

                shape.scale = new Vector3(Mathf.Abs(Line.T_Area) + Mathf.Abs(Line.B_Area), 1, 0.03f);
                break;
            case Potals.PotalType.T:
                shape.rotation = new Vector3(90, 90, 90);
                Wall.gameObject.transform.localPosition = new Vector3((Mathf.Abs(Line.L_Area) + Mathf.Abs(Line.R_Area) - 2) / 2, 0, 0);

                shape.scale = new Vector3(Mathf.Abs(Line.L_Area) + Mathf.Abs(Line.R_Area), 1, 0.03f);
                break;
            case Potals.PotalType.B:
                shape.rotation = new Vector3(-90, 90, 90);
                Wall.gameObject.transform.localPosition = new Vector3((Mathf.Abs(Line.L_Area) + Mathf.Abs(Line.R_Area)-2)/2 , 0, 0);
                shape.scale = new Vector3(Mathf.Abs(Line.L_Area) + Mathf.Abs(Line.R_Area), 1, 0.03f);
                break;

        }
        edge.points = transform.parent.GetComponent<EdgeCollider2D>().points;


    }
    public float Timer=3;
    public float currentTime=3;
    public IEnumerator StartLifeTimer()
    {
        Downning = true;
        Lifetime.startLifetime = currentTime;
        yield return null;
        currentTime= Mathf.Clamp(currentTime -= Time.deltaTime,0,Timer);
        StartCoroutine(StartLifeTimer());
    }
    private void Update()
    {
        if (MapManager.s_Instace.Map_Lock)
        {
            Lifetime.startLifetime = Timer;
            currentTime = Timer;
            Wall.gameObject.SetActive(true);
        }
        else
        {
                if(!Downning)
                {
                StartCoroutine(StartLifeTimer());
                }
            if (currentTime == 0) 
            {
             //   StopAllCoroutines();
                Wall.gameObject.SetActive(false);
            }
        }
    }
}
