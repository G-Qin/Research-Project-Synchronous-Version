using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperimentManager : MonoBehaviour
{
    public int maxTrialNum;
    public float signalRatio, noisePlayLength, signalWaitTime, feedbackTime;
    // Above are config variables
    public GameObject dataLogger;
    public Button startBtn, yesBtn, noBtn;
    public GameObject cardCover;
    public AudioSource noise, signal;
    public Image feedbackSquare;
    public Text trialNumText;
    [SerializeField]
    private int currTrialNum, response, feedbackType;
    [SerializeField]
    private float volume, noiseLength;
    private bool signalExist = false, answered = false;


    void Start(){
        feedbackType = PlayerPrefs.GetInt("FBType", 1);
        noiseLength = noise.clip.length;
        startBtn.interactable = true;
        // Hide the game part for now
        cardCover.SetActive(true);
        // Load the volume from SIAM, otherwise it will be 0.5 by default
        volume = PlayerPrefs.GetFloat("SignalVolume");
        signal.volume = volume;
        // Yes/No button cannot be accessed until noise ends
        yesBtn.interactable = false;
        noBtn.interactable = false;
        // For debug use
        UnityEngine.Debug.Log(volume);
    } 

    public void StartExperiment(){
        dataLogger.GetComponent<ExperimentDataLogger>().NewExperimentLogFile();
        startBtn.interactable = false;
        cardCover.SetActive(false);
        StartCoroutine(Experiment());
    }

    IEnumerator Experiment()
    {               
        for (currTrialNum = 1; currTrialNum <= maxTrialNum; currTrialNum++){
            answered = false;
            response = 0;
            trialNumText.text = "Trial #: " + currTrialNum;
            yesBtn.interactable = false;
            noBtn.interactable = false;
            // Determine and play signal
            float willPlay = Random.Range(0f,1f);
            if (willPlay < signalRatio) {
                signalExist = true;
                StartCoroutine(PlaySignal());
            } else {
                signalExist = false;
            }
            // Play the noise
            noise.time = Random.Range(0, noiseLength - noisePlayLength);
            noise.Play();            
            yield return new WaitForSeconds(noisePlayLength); 
            noise.Stop();
            // Enable yes/no buttons
            yesBtn.interactable = true;
            noBtn.interactable = true;
            // Wait for user response
            yield return new WaitUntil(() => answered);
            // Log the outcome of the trial
            dataLogger.GetComponent<ExperimentDataLogger>().LogTrialNumber(currTrialNum);
            dataLogger.GetComponent<ExperimentDataLogger>().LogSignal(signalExist);
            dataLogger.GetComponent<ExperimentDataLogger>().LogResponse(response);   
        }
    }

    IEnumerator PlaySignal(){
        yield return new WaitForSeconds(signalWaitTime);
        signal.Play();
    }

    public void SignalResponse(bool answerYes){
        if (answerYes){
            if (signalExist) response = 1;
            else response = 3;
        } else {
            if (signalExist) response = 2;
            else response = 4;
        }
        yesBtn.interactable = false;
        noBtn.interactable = false;
        StartCoroutine(ChangeFeedbackColor(response));
        
    }

    IEnumerator ChangeFeedbackColor(int response){
        // 1: No feedback
        // 2: Full feedback
        // 3: Signal feedback
        // 4: Yes feedback
        // 5: Correction feedback
        if (response == 1 && (feedbackType >= 2 && feedbackType <= 5)) {
            // Hit, green feedback
            feedbackSquare.color = new Color32(0, 255, 0, 255);
        } else if (response == 2 && (feedbackType == 2 || feedbackType == 3)){
            // Miss, red feedback
            feedbackSquare.color = new Color32(255, 0, 0, 255);
        } else if (response == 3 && (feedbackType == 2 || feedbackType == 4)){
            // False alarm, red feedback
            feedbackSquare.color = new Color32(255, 0, 0, 255);
        } else if (response == 4 && (feedbackType == 2 || feedbackType == 5)){
            // Correct rejection, green feedback
            feedbackSquare.color = new Color32(0, 255, 0, 255);
        }
        yield return new WaitForSeconds(feedbackTime);
        // Black
        feedbackSquare.color = new Color32(0, 0, 0, 255);
        answered = true;
    }
}
