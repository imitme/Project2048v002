using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumCell : MonoBehaviour
{
    private Text txt;
    private int _num;

    public int num
    {
        get { return _num; }
        set { _num = value; txt.text = _num.ToString(); }
    }

    public int c = 0;   //열
    public int r = 0;   //행

    private void Awake()
    {
        txt = GetComponentInChildren<Text>();
        num = 2;
    }
}