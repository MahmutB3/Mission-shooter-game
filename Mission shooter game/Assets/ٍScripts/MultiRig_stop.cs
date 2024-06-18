using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class MultiRig_stop : MonoBehaviour
{
    public MultiAimConstraint multiAimConstraint;
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
            if (multiAimConstraint != null)
            {
                multiAimConstraint.weight = 1f;
            }
        }
        else
        {
            // Sol fare tuşuna basılı değilse MultiAimConstraint pasif olsun
            if (multiAimConstraint != null)
            {
                multiAimConstraint.weight = 0f;
            }
        }
    }
}
