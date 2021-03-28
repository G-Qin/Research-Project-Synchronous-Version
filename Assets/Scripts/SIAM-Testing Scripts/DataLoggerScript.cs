using System.IO;
using System;
using System.Collections.Generic;
using UnityEngine;

public class DataLoggerScript : MonoBehaviour
{
    StreamWriter writer;
    private string SIAMLogPath = @"Logs\SIAMLogs\";
    string path;

    void Start(){
        // Create folder for the log files
        if (!Directory.Exists(SIAMLogPath)){
            Directory.CreateDirectory(SIAMLogPath);
        }
    }

    public void NewSIAMDataFile()
    {   
        // Create a new file name using current time
        string date = DateTime.Now.ToString("MM-dd-yy HH-mm-ss");
        path = SIAMLogPath + date + ".txt"; 
    }

    public void LogTrialNumber(int trialNum){
        using (writer = new StreamWriter(path, append:true)){
            writer.Write("Trial #" + trialNum.ToString() + "  ");
        }
    }

    public void LogVolume(float volume){
        using (writer = new StreamWriter(path, append:true)){
            writer.Write("Level: " + volume.ToString() + "  ");
        }
    }

    public void LogResponse(int response){
        using (writer = new StreamWriter(path, append:true)){
            if (response == 1) writer.Write("Hit\n");
            else if (response == 2) writer.Write("Miss\n");
            else if (response == 3) writer.Write("False Alarm\n");
            else if (response == 4) writer.Write("Correct rejection\n");
        }
    }

    public void LogReversal(int reversalNum){
        using (writer = new StreamWriter(path, append:true)){
            writer.Write("Reversal #" + reversalNum + "\n");
        }
    }

    public void LogAbortedProcedure(){
        using (writer = new StreamWriter(path, append:true)){
            writer.WriteLine("Aborted Procedure.");
        }
    }

    public void LogFinishedProcedure(float volume){
        using (writer = new StreamWriter(path, append:true)){
            writer.WriteLine("Finished Procedure, avg level: " + volume);
        }
    }
}
