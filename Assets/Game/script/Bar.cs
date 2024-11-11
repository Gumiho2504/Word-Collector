using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class Bar : MonoBehaviour
{
    public Text recieveText;
    public string recieve ="";
    public GameController gameController;
    private void Start()
    {
        recieveText.text = "Collect: " + recieve;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "char")
        {
            char c = collision.GetComponent<Text>().text[0];
            if (gameController.nextChar == c )
            {
                Handheld.Vibrate();
                gameController.sfxSource.PlayOneShot(gameController.collectClip);
                recieve += collision.gameObject.GetComponent<Text>().text;
                recieveText.text = "Collect: " + recieve;
                
                gameController.charIndex++;
              if(gameController.charIndex < gameController.collectedWord.Length){
                    gameController.nextChar = gameController.collectedWord[gameController.charIndex];
                    gameController.nextCharText.text = gameController.nextChar.ToString();
               }
              
                if(recieve == gameController.collectedWord)
                {
                    print("game win!");
                    gameController.sfxSource.PlayOneShot(gameController.trueWordsClip);
                    gameController.RandomCollectWord();
          
                    gameController.score += recieve.Length;
                    gameController.health += recieve.Length;
                    
                    recieve = "";
                    recieveText.text = "Collect: " + recieve;
                    gameController.wordCount += 1;
                    gameController.UpdateUI();

                   
                }
            }
            else
            {
                Handheld.Vibrate();
                gameController.health -= 10;
                gameController.UpdateHealth();
                gameController.sfxSource.PlayOneShot(gameController.wrongClip);

            }
            
        }else if(collision.tag == "Rock")
        {
            Handheld.Vibrate();
            gameController.sfxSource.PlayOneShot(gameController.rockClip);
            gameController.health -= 25;
            
            gameController.UpdateHealth();
            Destroy(collision.gameObject);
        }

        if (gameController.health <= 0)
        {
            gameController.sfxSource.PlayOneShot(gameController.gameOverClip);
            StartCoroutine(gameController.LoadGameOverPanel());
           

        }
        Destroy(collision.gameObject);

    }
}
