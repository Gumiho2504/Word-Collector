using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIController : MonoBehaviour
{

    public Button playButton;
    public Button exitButton;
    private void Start()
    {
        playButton.onClick.AddListener(() => SceneManager.LoadScene(2));
        exitButton.onClick.AddListener(Application.Quit);
    }

}
