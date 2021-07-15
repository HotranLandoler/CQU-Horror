using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class B5Gate : SceneTravelBase
{
    [SerializeField]
    private AudioClip openSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            
            AudioManager.Instance.PlaySound(openSound);
            SceneTravel();
        }
    }
}
