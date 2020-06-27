using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLoseController : MonoBehaviour
{
    private bool isProccessFinished = false;
    //private iOSHapticController hapticController;
    //private UIManager UI_Manager;

    private void Start()
    {
        //hapticController = GameObject.FindGameObjectWithTag("HapticController").GetComponent<iOSHapticController>();
        //UI_Manager = GameObject.FindGameObjectWithTag("UIController").GetComponent<UIManager>();
    }

    public void WinDriveHills()
    {
        if (!isProccessFinished)
        {
            //hapticController.TriggerImpactHeavy();
            //UI_Manager.StartWinAnimation();
        }
    }
    public void LoseDriveHills()
    {
        if (!isProccessFinished)
        {
            //hapticController.TriggerImpactHeavy();
            //UI_Manager.StartFailAnimation();
        }
    }
}
