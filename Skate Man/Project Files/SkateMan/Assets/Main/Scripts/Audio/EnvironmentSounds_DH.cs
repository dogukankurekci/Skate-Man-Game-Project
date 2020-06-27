using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentSounds_DH : MonoBehaviour
{
    void impactPlay()
    {
        FindObjectOfType<AudioManager_DH>().Play("impact");
    }
    void impactStop()
    {
        FindObjectOfType<AudioManager_DH>().Stop("impact");
    }
}
