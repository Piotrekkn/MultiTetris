using System.Collections.Generic;
using UnityEngine;
namespace MultiTetris.Presentation
{
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public List<Player> players = new List<Player>();
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }


 
  
}}
