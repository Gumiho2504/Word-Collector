using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{
    public Sprite[] rocks;
    public Text scoreText, scoreOverText;
    public Text wordCountText,wordCountOverText;
    public Text highScoreText,highScoreOverText;
    public Text nextCharText;
    public Text collectedWordText;
    public GameObject gameOverPanel;
    public Text healthText;
    public Text topWordsCountText;
    
    public GameObject letterPrefab;
    public GameObject rockPrefab;


    private string[] words = {
    "apple", "baker", "candle", "dragon", "eagle", "forest", "garden", "honey", "island", "jungle",
    "kettle", "lemon", "mountain", "nebula", "ocean", "planet", "quartz", "river", "sunset", "tiger",
    "unicorn", "valley", "whale", "xenon", "yellow", "zebra", "acorn", "blossom", "castle", "daisy",
    "ember", "feather", "glacier", "harvest", "insect", "jigsaw", "koala", "lantern", "mosaic", "nectar",
    "orange", "pirate", "quiver", "rooster", "sapphire", "thunder", "umbrella", "violet", "whisper", "xylophone",
    "yarn", "zephyr", "aurora", "boulder", "compass", "dolphin", "emerald", "fossil", "geyser", "horizon",
    "iguana", "jade", "koi", "lotus", "meteor", "nebula", "onyx", "phoenix", "quartz", "rose", "safari",
    "temple", "utopia", "vine", "warrior", "xenon", "yonder", "zenith", "artifact", "beacon", "crystal", "dune",
    "echo", "fable", "grove", "hollow", "infinity", "jungle", "kingdom", "labyrinth", "meadow", "nomad",
    "oasis", "paradise", "quest", "realm", "summit", "treasure"
};

    private string targetWord;
    public string collectedWord = "";
    public char nextChar;
    public int score = 0;
    public int health = 1000;
    public Slider healthSlider;
    public int wordCount=0;
    public GameObject topBarPosition;
    public int charIndex = 0;
    private string highscorekey = "highscore";
    private string wordCountKey = "topwordcount";
    public Button homeButton, homeInGameOverButton, playAgainButton;

    public Button soundButto;
    public AudioSource musicSource, sfxSource;
    public AudioClip rockClip, wrongClip,collectClip,trueWordsClip,gameOverClip;
    private void Start()
    {
        soundButto.onClick.AddListener(SoundAction);
        playAgainButton.onClick.AddListener(PlayAgain);
        homeButton.onClick.AddListener(HomeButton);
        homeInGameOverButton.onClick.AddListener(HomeButton);
        StartCoroutine(SpawnLatter());
        StartCoroutine(SpawnRock());
        RandomCollectWord();

        UpdateUI();
    }

    public char GetRandomLetter()
    {
        return (char)Random.Range(65, 91); // A-Z characters
    }
    
    public void RandomCollectWord()
    {
        
        collectedWord = words[Random.Range(0,words.Length)].ToUpper();
        charIndex = 0;
        nextChar = collectedWord[charIndex];
        nextCharText.text = collectedWord[charIndex].ToString();

        UpdateUI();
    }

    public void UpdateUI()
    {
        scoreText.text = "Score: " + score;
        highScoreText.text = "HighScore: " + PlayerPrefs.GetInt(highscorekey, 0);

       wordCountText.text = "words : " + wordCount;
        healthText.text = "Health: " + health;

        collectedWordText.text = "Target: " + collectedWord;
    }

    public void UpdateHealth() {
        if (health >= 1000)
        {
            health = 100;
         }else if(health <= 0)
        {
            health = 0;
        }

        healthText.text = "Health: " + health;
        healthSlider.value = (float)health / 1000;
    }

    int count = 0;
    public IEnumerator SpawnLatter()
    {
        while (1 > 0)
        {
            count += 1;
            GameObject letter = Instantiate(letterPrefab,new Vector3(Random.Range(-500,500),transform.localPosition.y,0), Quaternion.identity);
            char c = GetRandomLetter();
            if (count >= Random.Range(10, 15))
            {
                //print("in");
                c = nextChar;
                count = 0;
            }
           
            letter.GetComponent<Text>().text = c.ToString();
           
            letter.transform.SetParent(topBarPosition.transform, false);
            letter.name = c.ToString();
            yield return new WaitForSeconds(0.5f);
            
        }
       
    }

    public IEnumerator SpawnRock()
    {
        while (1 > 0)
        {
            GameObject letter = Instantiate(rockPrefab, new Vector3(Random.Range(-500, 500), transform.localPosition.y, 0), Quaternion.identity);
            letter.GetComponent<Image>().sprite = rocks[Random.Range(0, rocks.Length)];
            letter.transform.SetParent(topBarPosition.transform, false);
            
            yield return new WaitForSeconds(1.5f);
        }

    }

    public IEnumerator LoadGameOverPanel() {
        if(score > PlayerPrefs.GetInt(highscorekey, 0))
        {
            PlayerPrefs.SetInt(highscorekey, score);
        }

        if (wordCount > PlayerPrefs.GetInt(wordCountKey, 0))
        {
            PlayerPrefs.SetInt(wordCountKey, wordCount);
        }

        topWordsCountText.text = "Top words collect: "+PlayerPrefs.GetInt(wordCountKey, 0).ToString();
        highScoreOverText.text = "Highscore : " + PlayerPrefs.GetInt(highscorekey, 0);
        scoreOverText.text = "Score: " + score;
        wordCountOverText.text = "Words Count: " + wordCount;
        gameOverPanel.LeanScaleX(1f, 1f).setEaseInOutExpo();
        yield return new WaitForSeconds(1.4f);
        Time.timeScale = 0;
    }

    public void PlayAgain()
    {
       
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
    }
    public void HomeButton() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("HomeScene");
    }

    bool isSoundMute = false;
    public Sprite muteSprite, unMuteSprite;
    void SoundAction()
    {
        isSoundMute = !isSoundMute;
        soundButto.gameObject.GetComponent<Image>().sprite = isSoundMute ? muteSprite : unMuteSprite;
        if (isSoundMute)
        {
            musicSource.mute = sfxSource.mute = true;
        }
        else
        {
            musicSource.mute = sfxSource.mute = false;
        }
    }
}
