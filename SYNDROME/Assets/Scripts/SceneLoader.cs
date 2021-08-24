using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    // Start is called before the first frame update
    public int pre_sceneindex=0;
    public int currentScenenum=0;
    public float loadingtime;
    public string[] Scenes;
    public Animator doorani;
    public GameObject mapnamepannel;

    private void  OnTriggerStay2D(Collider2D col)
    {


        if (col.tag == "Player")
        {
            GameObject setting = GameObject.Find("Settingmanager");
            if(setting!=null&& Input.GetKey(settingmanager.GM.comunication))
            {
                moveScene();
            }
        }
    }
   
    public void moveScene()
    {
        mapnamepannel.GetComponent<Mappannel>().panneldownevent(currentScenenum);
        mapnamepannel.GetComponent<Animator>().SetTrigger("panneldowntrigger");
        Camera.main.GetComponent<Camera_movement>().MapChange(currentScenenum + 1);
    }
    public void Gameloadscene(int Sceneindex)
    {
        pre_sceneindex = currentScenenum;
        currentScenenum = Sceneindex;
        StartCoroutine(SceneLoad(Scenes[Sceneindex]));
        
    }
    public void loadscene(int Sceneindex)
    {

        
        StartCoroutine(SceneLoad(Scenes[Sceneindex]));
     if(currentScenenum==0)
        {
            doorani.SetBool("start", true); 
        }
        pre_sceneindex = currentScenenum;
        currentScenenum = Sceneindex;

    }
    IEnumerator SceneLoad(string Scenenum)
    {
        
       
        AsyncOperation operation = SceneManager.LoadSceneAsync(Scenenum);
        operation.allowSceneActivation = false;
        while(!operation.isDone)
        {
            
            
            yield return null;
           
            if ( operation.progress>=0.9f)
            {
                yield return new WaitForSeconds(loadingtime);
                operation.allowSceneActivation = true;
            }
        }

     

    }
    
}
