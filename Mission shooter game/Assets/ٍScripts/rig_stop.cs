using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class rig_stop : MonoBehaviour
{
    public Rig rig;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            // Sol fare tuşuna basılıysa MultiAimConstraint aktif olsun
            if (rig != null)
            {
                rig.weight = 1f;
            }
        }
        else
        {
            // Sol fare tuşuna basılı değilse MultiAimConstraint pasif olsun
            if (rig != null)
            {
                rig.weight = 0f;
            }
        }
    }
}
