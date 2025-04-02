using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using System;

namespace MultiTetris.Presentation
{
    public class Grid : MonoBehaviour
    {
        [SerializeField]
        public GameObject block;
        private GameManager _gm = GameManager.Instance;
        private List<Player> players;
        private GameObject[] gridPositions;
        private float offset = 7f;
        private Color frameColor = new Color(0.11373f, 0.20784f, 0.34118f);
        private Color[] colorArray =
        {
        new Color(1f, 0.34902f, 0.36863f),
        new Color(1f, 0.79216f, 0.22745f),
        new Color(0.54118f, 0.78824f, 0.14902f),
        new Color(0.09804f, 0.50980f, 0.76863f),
        new Color(0.41569f, 0.29804f, 0.57647f)
        };
        private InputController inputController = new InputController();

        void Start()
        {
            inputController.Start();
            players = _gm.players;
            gridPositions = new GameObject[players.Count];
            CreateBorder();
            CreateGrid();
            SpaceOutGrid();
            foreach (Player player in players)
            {
                player.gridControler.SpawnNext();
            }
        }

        void Update()
        {
            inputController.ManageInput();
            RefreshGrid();
        }

        private void CreateBorder()
        {
            int index = 0;
            foreach (Player player in players)
            {
                //parentgameobject             
                gridPositions[index] = Instantiate(new GameObject(), new Vector2(transform.position.x, transform.position.y), transform.rotation, transform);
                //bottom
                player.frameArray[0] = Instantiate(block, new Vector2(transform.position.x - 0.5f + player.x / 2, transform.position.y - 1), transform.rotation, transform.GetChild(index));
                player.frameArray[0].transform.localScale = new Vector3(player.x + 2, 1, 1);
                //top
                player.frameArray[1] = Instantiate(block, new Vector2(transform.position.x - 0.5f + player.x / 2, transform.position.y + player.y), transform.rotation, transform.GetChild(index));
                player.frameArray[1].transform.localScale = new Vector3(player.x + 2, 1, 1);
                //left
                player.frameArray[2] = Instantiate(block, new Vector2(transform.position.x - 1, transform.position.y + player.y / 2), transform.rotation, transform.GetChild(index));
                player.frameArray[2].transform.localScale = new Vector3(1, player.y + 1, 1);
                //right
                player.frameArray[3] = Instantiate(block, new Vector2(transform.position.x + player.x, transform.position.y + player.y / 2), transform.rotation, transform.GetChild(index));
                player.frameArray[3].transform.localScale = new Vector3(1, player.y + 1, 1);
                //color the frame
                foreach (GameObject frame in player.frameArray)
                {
                    frame.transform.GetChild(0).GetComponent<Renderer>().material.color = frameColor;
                }
                index++;
            }
        }
        private void CreateGrid()
        {
            int index = 0;
            foreach (Player player in players)
            {
                player.gridControler.ClearGrid();
                for (int i = 0; i < player.x; i++)
                {
                    for (int j = 0; j < player.y; j++)
                    {
                        player.blockArray[i, j] = Instantiate(block, new Vector2(transform.position.x + i, transform.position.y + j), transform.rotation, transform.GetChild(index));
                    }
                }
                index++;
            }

        }
        private void SpaceOutGrid()
        {
            if (players.Count == 1)
            {
                gridPositions[0].transform.position = new Vector2(gridPositions[0].transform.position.x, gridPositions[0].transform.position.y);
            }
            else if (players.Count == 2)
            {
                gridPositions[0].transform.position = new Vector2(gridPositions[0].transform.position.x - offset, gridPositions[0].transform.position.y);
                gridPositions[1].transform.position = new Vector2(gridPositions[1].transform.position.x + offset + 2, gridPositions[1].transform.position.y);
            }
        }
        private void RefreshGrid()
        {
            foreach (Player player in players)
            {
                int[,] dataArray = player.gridControler.GetDataArray();
                for (int i = 0; i < player.x; i++)
                {
                    for (int j = 0; j < player.y; j++)
                    {
                        if (dataArray[i, j] == 0)
                            player.blockArray[i, j].SetActive(false);
                        else if (dataArray[i, j] > 0)
                        {
                            player.blockArray[i, j].SetActive(true);
                            player.blockArray[i, j].transform.GetChild(0).GetComponent<Renderer>().material.color = colorArray[dataArray[i, j] - 1];
                        }
                        else
                        {
                            player.blockArray[i, j].SetActive(true);
                            player.blockArray[i, j].transform.GetChild(0).GetComponent<Renderer>().material.color = Color.black;

                        }
                    }
                }
                //check for full lines and add score
                if (!player.gameOver)
                {
                    int score = player.gridControler.CheckForLines();
                    if (score > 0)
                        player.score += (int)Math.Pow(10, score);

                    player.gameOver = player.gridControler.gameOver;
                }
            }
        }
    }

}