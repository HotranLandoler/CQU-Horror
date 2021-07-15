using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceBinder : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
    }

    private void OnDisable()
    {
        Locator.Reset();
    }
}
