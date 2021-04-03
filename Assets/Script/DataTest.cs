using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataTest : MonoBehaviour
{
  public NeuralNetwork neuralNetwork;
  public int Classnm = 10;
  public int Picno,Picnum;
  public GameObject picture;
  public float[,] w1 = new float[784,16]; //784
  public float[,] w2 = new float[16,10]; //784

  public float[] Layer1 = new float[784];
  public float[] Layer2 = new float[16];
  public float[] Layer3 = new float[10];

  private float cost,alll,alll2;
  private float mE = 2.718281828f;
  public Text t1,t2,t3,t4,t5,t6,t7,t8,t9,t0,loss;

  void Start()
  {
    Picno = 0;
    Picnum = 1;
  }

  public void TP()
  {
    LoadWeight();
    Input();
  }

  public void NP()
  {
    Picnum+=1;
    if(Picnum > 10)
    {
      Picno+=1;
      Picnum=1;
    }
    if(Picno > 9)
    {
      Picno=0;
    }
  }

  void LoadWeight()
  {
    w1 = (float[,])neuralNetwork.w1.Clone();
    w2 = (float[,])neuralNetwork.w2.Clone();
  }

  public void Input()
  {
    for(int i = 0; i < 784; i++) //歸零
    {
      Layer1[i] = 0;
    }
    Texture2D texture = new Texture2D(28, 28);

    texture = (Texture2D)Resources.Load("mnist_test/mnist_"+Picno.ToString()+"_"+Picnum.ToString());
    texture.Apply();
    for (int py = 0; py < texture.height; py++)
    {
        for (int px = 0; px < texture.width; px++)
        {
          int i = (py*28+px);
          Layer1[i] = texture.GetPixel(px,py).grayscale;
        }
    }

    Texture2D Output = new Texture2D(28, 28);
    picture.GetComponent<Image>().material.mainTexture = Output;
    for (int py = 0; py < texture.height; py++)
      {
          for (int px = 0; px < texture.width; px++)
          {
            int i = (py*28+px);
            Color newColor = new Color(Layer1[i], Layer1[i], Layer1[i], 1f);
            Output.SetPixel(px, py, newColor);
          }
      }
    Output.Apply();
    picture.SetActive(false);
    picture.SetActive(true);
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
    Output();
  }

  void Output()
  {
    t1.text = Layer3[1].ToString("0.000");
    t2.text = Layer3[2].ToString("0.000");
    t3.text = Layer3[3].ToString("0.000");
    t4.text = Layer3[4].ToString("0.000");
    t5.text = Layer3[5].ToString("0.000");
    t6.text = Layer3[6].ToString("0.000");
    t7.text = Layer3[7].ToString("0.000");
    t8.text = Layer3[8].ToString("0.000");
    t9.text = Layer3[9].ToString("0.000");
    t0.text = Layer3[0].ToString("0.000");
    CostCalculate();
    loss.text = cost.ToString("0.000");
  }

  float Sigmoid(float value) {
    return (float) (1.0 / (1.0 + Mathf.Pow(mE, -value)));
  }
  void CostCalculate()
  {
    cost = 0;
    for(int i = 0; i < neuralNetwork.Classnm; i++)
    {
      if(i == neuralNetwork.Picno)
      cost += (neuralNetwork.Layer3[i] - 1)*(neuralNetwork.Layer3[i] - 1);
      else
      cost += neuralNetwork.Layer3[i]*neuralNetwork.Layer3[i];
    }
  }

}
