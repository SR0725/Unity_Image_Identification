using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine;
using System.IO;
using System.Text;

public class NeuralNetwork : MonoBehaviour
{
  public int Classnm = 10;
  public int Picno,Picnum;
  public float alll,alll2;
  public float[,] w1 = new float[784,16]; //784
  public float[,] w2 = new float[16,10]; //784

  public float[] Layer1 = new float[784];
  public float[] Layer2 = new float[16];
  public float[] Layer3 = new float[10];

  public PlayerData playerData;
  public MachineLearing machineLearing;

  public void wRandom()
  {
    for(int i = 0; i < 784; i++)
    {
      for(int a = 0; a < 10; a++)
      {
        w1[i,a] = Random.Range(-0.01f,0.01f);
      }
    }
    for(int i = 0; i < 16; i++)
    {
      for(int a = 0; a < 10; a++)
      {
        w2[i,a] = Random.Range(-0.01f,0.01f);
      }
    }
  }

  public void Input()
  {
    for(int i = 0; i < 784; i++) //歸零
    {
      Layer1[i] = 0;
    }
    //ReadPicture to Layer1
    Texture2D texture = new Texture2D(28, 28);

    texture = (Texture2D)Resources.Load("mnist_train/mnist_"+Picno.ToString()+"_"+Picnum.ToString());
    texture.Apply();
    for (int py = 0; py < texture.height; py++)
    {
        for (int px = 0; px < texture.width; px++)
        {
          int i = (py*28+px);
          Layer1[i] = texture.GetPixel(px,py).grayscale;
        }
    }
    NNLayer2();
  }

  void NNLayer2()
    {
      for(int i = 0; i < 16; i++) //歸零
      {
        Layer2[i] = 0;
      }
      for(int i = 0; i < 784; i++) //權重函數加成
      {
        for(int a = 0; a < 16; a++)
        {
          float la;
          la = (Layer1[i] * w1[i,a]);
          Layer2[a] += (la);
        }
      }
      for(int i = 0; i < 16; i++) //激活函數加成
      {
        if(Layer2[i] < 0)
        {
          Layer2[i] = 0;
        }
      }
      NNLayer3();
  }

  void NNLayer3()
  {
    for(int i = 0; i < 10; i++) //歸零
    {
      Layer3[i] = 0;
    }
    for(int i = 0; i < 16; i++) //權重函數加成
    {
      for(int a = 0; a < 10; a++)
      {
        float la;
        la = (Layer2[i] * w2[i,a]);
        Layer3[a] += (la);
      }
    }
    for(int i = 0; i < 10; i++)
    {
      if(Layer3[i]<0)
      Layer3[i] = 0;
    }
    //////////////////////////////////////////
    alll = 0;

    for(int i = 0; i < Classnm; i++) //歸零
    {
      alll += Layer3[i];
    }
    for(int i = 0; i < Classnm; i++) //歸零
    {
      if(Layer3[i]!=0)
      Layer3[i] = Layer3[i]/alll;
    }
  }
}
