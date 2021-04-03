using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerData : MonoBehaviour {

    SaveAndLoad SL;
    public DataTest neuralNetwork;


    public void saveButton()
    {
        playerDataType p = new playerDataType();
        p.w1 = (float[,])neuralNetwork.w1.Clone();
        p.w2 = (float[,])neuralNetwork.w2.Clone();
        Debug.Log("Save");
        SL.SaveData(p);
    }

    public void LoadButton()
    {
        playerDataType p = (playerDataType)SL.LoadData(typeof(playerDataType));
        neuralNetwork.w1 = (float[,])p.w1.Clone();
        neuralNetwork.w2 = (float[,])p.w2.Clone();
        Debug.Log("Load");
    }

	void Start ()
  {
    SL = GetComponent<SaveAndLoad>();
    LoadButton();
  }

  public class playerDataType
  {
    public float[,] w1 = new float[784,16];
    public float[,] w2 = new float[16,10];
  }
}
