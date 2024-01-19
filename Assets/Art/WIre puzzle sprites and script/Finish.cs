using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    static public Finish Instance;

    public int wireCount;
    public GameObject winText;
    private int onCount = 0;


    private void Awake()
    {
        Instance = this;
    }
    public void WireChange(int points)
    {
        onCount = onCount + points;
        if (onCount == wireCount)
        {
            winText.SetActive(true);
        }
    }
}