using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [TextArea(2,3)]
    [SerializeField]
    private string[] checkPhrase;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //UIManager.Instance.ShowDialogue(checkPhrase, false);
            GameManager.Instance.StartDialogue(checkPhrase);
        }
    }
}
