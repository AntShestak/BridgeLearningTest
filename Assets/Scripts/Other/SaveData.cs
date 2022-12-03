using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace BridgeLearningTest
{
    public class SaveData : MonoBehaviour
    {
       
        public void SaveScoreData(int score, int objects, int seconds)
        {
            string path = Application.persistentDataPath + "/Scores.json";

            //check for old data
            Scores scores;
            if (File.Exists(path))
            {
                string read = File.ReadAllText(path);
                scores = JsonUtility.FromJson<Scores>(read);
            }
            else
                scores = new Scores();

            //setup new entry
            ScoreData data = new ScoreData();
            data.score = score;
            data.collectedObjects = objects;
            data.secondsTaken = seconds;
            scores.scores.Add(data);
                

            //save data
            string dataJson = JsonUtility.ToJson(scores);
            File.WriteAllText(path,dataJson);
        }

        
    }

    [System.Serializable]
    public class Scores
    {
        public List<ScoreData> scores = new List<ScoreData>();
    }

    [System.Serializable]
    public class ScoreData
    {
        public int score;
        public int collectedObjects;
        public int secondsTaken;
    }
}