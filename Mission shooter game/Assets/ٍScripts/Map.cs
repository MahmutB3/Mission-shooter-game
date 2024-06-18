using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public GameObject objectToToggle;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M) && objectToToggle != null)
        {
            objectToToggle.SetActive(!objectToToggle.activeSelf);
        }
    }
}
