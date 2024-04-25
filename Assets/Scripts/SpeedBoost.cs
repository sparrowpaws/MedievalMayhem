using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    public int BoostAmount = 5;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Boost();
        }
    }
    public void Boost()
    {
        Debug.Log("the speed has been boosted");
    }
}
