using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSetter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Set default signal volume
        if (!PlayerPrefs.HasKey("SignalVolume")){
            PlayerPrefs.SetFloat("SignalVolume", 0.5f);
        }
        // Disable the experiment button if no SIAM has been run
        if (!PlayerPrefs.HasKey("SIAMDone")){
            PlayerPrefs.SetInt("SIAMDone", 0);
        }
    }
}
