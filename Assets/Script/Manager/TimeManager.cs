using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager 
{
    private float curTime = 0;

    public void Reset()=> curTime = 0;
    public bool Timer(float second)
    {
        curTime += Time.deltaTime;
        if (curTime >= second) {
            curTime = 0;
            return true;
        }
       return false;
    }
}
