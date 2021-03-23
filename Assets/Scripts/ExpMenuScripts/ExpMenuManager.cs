using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExpMenuManager : MonoBehaviour
{
    public Button expButton;
    public GameObject selectionText;
    // Start is called before the first frame update
    void Start()
    {
        expButton.interactable = false;
        selectionText.SetActive(false);
    }

    public void NoFeedback(){
        PlayerPrefs.SetInt("FBType", 1);
        selectionText.GetComponent<Text>().text = "Selected: No feedback.";
        selectionText.SetActive(true);
        expButton.interactable = true;
    }

    public void FullFeedback(){
        PlayerPrefs.SetInt("FBType", 2);
        selectionText.GetComponent<Text>().text = "Selected: Full feedback.";
        selectionText.SetActive(true);
        expButton.interactable = true;
    }

    public void SignalFeedback(){
        PlayerPrefs.SetInt("FBType", 3);
        selectionText.GetComponent<Text>().text = "Selected: Feedback for signal trials.";
        selectionText.SetActive(true);
        expButton.interactable = true;
    }

    public void YesFeedback(){
        PlayerPrefs.SetInt("FBType", 4);
        selectionText.GetComponent<Text>().text = "Selected: Feedback for yes responses.";
        selectionText.SetActive(true);
        expButton.interactable = true;
    }

    public void CorrectFeedback(){
        PlayerPrefs.SetInt("FBType", 5);
        selectionText.GetComponent<Text>().text = "Selected: Feedback for correct responses.";
        selectionText.SetActive(true);
        expButton.interactable = true;
    }

    public void LoadExperiment() {
        SceneManager.LoadScene("Experiment");
    }

    public void LoadMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }
}
