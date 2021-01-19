using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingButtonBehaviour : MonoBehaviour
{
    private StatBehaviour _stat;

    private void Start()
    {
        _stat = transform.parent.Find("val").GetComponent<StatBehaviour>();
    }

    private void Train()
    {

    }
}
