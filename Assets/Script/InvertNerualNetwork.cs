using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvertNerualNetwork : MonoBehaviour
{
  public NeuralNetwork neuralNetwork;
  public int Classnm = 10;
  public int Picno,Picnum,Non;
  public GameObject picture;
  public float[,] w1 = new float[784,16]; //784
  public float[,] w2 = new float[16,10]; //784

  private float[] Layer1 = new float[784];
  private float[] Layer2 = new float[16];
  public float[] Layer3 = new float[10];

  private float cost,alll,alll2;

  void Start()
  {
    Picno = 0;
    Picnum = 1;
  }

  public void TP()
  {
    LoadWeight();
    NNLayer2();
  }

  void LoadWeight()
  {
    w1 = (float[,])neuralNetwork.w1.Clone();
    w2 = (float[,])neuralNetwork.w2.Clone();
  }

  void NNLayer2()
  {
    for(int i = 0; i < 784; i++) //歸零
    {
      Layer1[i] = 0;
    }
    for(int i = 0; i < 16; i++) //歸零
    {
      Layer2[i] = 0;
    }
    for(int i = 0; i < 10; i++) //歸零
    {
      Layer3[i] = 0;
    }
    Layer3[Non] = 1;
    for(int i = 0; i < 16; i++) //權重函數加成
    {
      for(int a = 0; a < 10; a++)
      {
        float la;
        la = (Layer3[a] * (w2[i,a]));
        Layer2[i] += (la);
      }
    }
    for(int i = 0; i < 16; i++) //激活函數加成
    {
      if(Layer2[i]<0)
      Layer2[i] = 0;
    }
    for(int i = 0; i < 784; i++) //權重函數加成
    {
      for(int a = 0; a < 16; a++)
      {
        float la;
        la = (Layer2[a] * (w1[i,a]));
        Layer1[i] += (la);
      }
    }
    for(int i = 0; i < 10; i++) //激活函數加成
    {
      if(Layer1[i]<0)
      Layer1[i] = 0;
    }
    Input();
  }

  public void Input()
  {

    Texture2D Output = new Texture2D(28, 28);
    picture.GetComponent<Image>().material.mainTexture = Output;
    for (int py = 0; py < 28; py++)
      {
          for (int px = 0; px < 28; px++)
          {
            int i = (py*28+px);
            Color newColor = new Color(Layer1[i], Layer1[i], Layer1[i], 1f);
            Output.SetPixel(px, py, newColor);
          }
      }
    Output.Apply();
    picture.SetActive(false);
    picture.SetActive(true);
  }

}
