using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ExitPanel;
    public GameObject SettingPanel;
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
}
