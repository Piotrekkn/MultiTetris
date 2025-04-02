
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
namespace MultiTetris.Presentation
{
public class MainMenu : MonoBehaviour
{

    [SerializeField]
    public InputField PlayerName1, PlayerName2, PlayerX1,PlayerX2,PlayerY1,PlayerY2;

    public void StartSinglePlayer()
    {
        GameManager.Instance.players.Clear();
        GameManager.Instance.players.Add(new Player(PlayerName1.text.ToString(),System.Convert.ToInt32(PlayerX1.text),System.Convert.ToInt32(PlayerY1.text)));
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }

    // Update is called once per frame
   public void StartTwoPlayers()
    {
        GameManager.Instance.players.Clear();
        GameManager.Instance.players.Add(new Player(PlayerName1.text.ToString(),System.Convert.ToInt32(PlayerX1.text),System.Convert.ToInt32(PlayerY1.text)));
        GameManager.Instance.players.Add(new Player(PlayerName2.text.ToString(),System.Convert.ToInt32(PlayerX2.text),System.Convert.ToInt32(PlayerY2.text)));
            SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }
}
}