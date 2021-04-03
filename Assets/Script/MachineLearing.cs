using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

public class MachineLearing : MonoBehaviour
{
  public NeuralNetwork neuralNetwork;
  public float lr,wDecayRate;
  public int pictureNum,generation;

  public Text loss,gt,nt;
  private bool st;
  private float al1,al2,al3,cost,mcost,rm;
  private int r,g;

  private float[,] w1m = new float[784,16]; //784
  private float[,] w2m = new float[16,10]; //784
  private float[] L2m = new float[16];

  private float mE = 2.718281828f;

  public void ML()
  {
    g = 1;
    r = 0;
    SL();
  }
  public void SL()
  {
    if(r< generation)
    {
      Invoke("SLs", 0.025f);
    }else
    {
      CostCalculate();
      loss.text = cost.ToString("0.000");
    }
    g+=1;
    if(g > pictureNum)
    {
      r+=1;
      g = 1;
      lr *= wDecayRate;
    }
    gt.text = r.ToString("0.000");
    nt.text = g.ToString("0.000");

  }

  public void SLs()
  {
    for(int h = 0;h < 2;h++)
    {
      neuralNetwork.Picno =h;
      neuralNetwork.Picnum=g;
      neuralNetwork.Input();
      CostCalculate();
      //ç¬