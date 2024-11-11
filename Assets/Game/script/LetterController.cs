using UnityEngine;
using UnityEngine.UI;

public class LetterController : MonoBehaviour
{

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Destroy(collision.gameObject);
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.gameObject.name == "bottomBar")
        //{
        Destroy(collision.gameObject);
        //}
       
    }
}
