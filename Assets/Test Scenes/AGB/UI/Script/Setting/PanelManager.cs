using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ExitPanel;
    public GameObject SettingPanel;
    public GameObject HomePanel;
    public Text BeforeText;
    public Text AftterText;

    #region ExitPanel
    public void CheckExitBtn(bool isExit)
    {
        if (isExit)
        {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;

            #else
                 Application.Quit(); //어플리케이션 종료
    
            #endif
        }
        else
        {
            ExitPanel.SetActive(false);
        }
    }

    public void ShowExitPanelBtn()
    {
        ExitPanel.SetActive(true);
    }

    #endregion

    #region SettingPanel
    public void ShowSettingBtn(bool isShow)
    {   
        
        SettingPanel.SetActive(isShow);
        
    }

    public void ApplyBtn()
    {
        
    }
    #endregion
    #region GoHome
    public void ShowHomeBtn(bool isShow)
    { 
        int fog = Convert.ToInt32(AbyssManager.abyss.Darkfog * 0.1f);
        if (isShow)
        {
            BeforeText.text = Convert.ToString(AbyssManager.abyss.Darkfog);
            AftterText.text = Convert.ToString(fog);

        }
        HomePanel.SetActive(isShow);
       
    }
    #endregion
}
