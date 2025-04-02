using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
namespace MultiTetris.Presentation
{
    public class GuiController : MonoBehaviour
    {
        public GameObject panelPlayer1, panelPlayer2, gameOverText;
        public Text textPlayer1, textPlayer2;
        private GameManager _gm = GameManager.Instance;

        void Start()
        {
            if (_gm.players.Count == 1)
            {
                panelPlayer2.SetActive(false);
            }
            else
            {
                panelPlayer2.SetActive(true);
            }
        }
     
        void Update()
        {
            textPlayer1.text = "Name: " + _gm.players[0].playerName + "\nScore: " + _gm.players[0].score;
            if (_gm.players[0].gameOver)
            {
                textPlayer1.text += "\nGAME OVER!";
            }
            if (_gm.players.Count != 1)
            {
                textPlayer2.text = "Name: " + _gm.players[1].playerName + "\nScore: " + _gm.players[1].score;
                if (_gm.players[1].gameOver)
                {
                    textPlayer2.text += "\nGAME OVER!";
                }
            }
            bool gameOver= true;
            foreach (Player player in _gm.players)
            {
                if (player.gameOver==false)
                {
                     gameOver= false;
                    break;
                }
            }
            gameOverText.SetActive(gameOver);
        }

        public void BackToMenu()
        {
            SceneManager.LoadScene("StartScene", LoadSceneMode.Single);
        }
    }
}
