﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YesButtonScript : MonoBehaviour
{
    public GameObject manager;
    public GameObject thisButton;

    void Start(){
        // Yes button is hidden at the start
        thisButton.SetActive(false);
    }

    public void AwakeOnSoundEnd(){
        thisButton.SetActive(true);
    }

    public void HideOnClick(){        
        thisButton.SetActive(false);
    }
}