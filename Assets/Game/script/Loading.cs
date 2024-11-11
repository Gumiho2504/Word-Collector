using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Loading : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Image load;
    IEnumerator Start()
    {
        float amout = 0f;
        while(amout <= 100)
        {
            amout += Random.Range(10, 20) * Time.deltaTime;
            load.fillAmount = amout / 100;
            yield return null;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
