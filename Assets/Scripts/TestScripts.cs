using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScripts : MonoBehaviour
{
    //SO_Building[] buildingsFound = Resources.LoadAll<SO_Building>("Buildings");

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("Pressed P");
        }
    }
}
