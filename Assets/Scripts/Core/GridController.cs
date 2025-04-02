using System;
using UnityEngine;
using Random = UnityEngine.Random;
namespace MultiTetris.Core
{
    public class GridControler
    {
        public GameObject block;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        int x = 10;
        int y = 20;
        int[,] dataArray;
        public bool gameOver = false;
        public GridControler(int x, int y)
        {
            this.x = x;
            this.y = y;
            dataArray = new int[x, y];
        }
        public int[,] GetDataArray()
        {
            return dataArray;
        }

        public void ClearGrid()
        {
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    dataArray[i, j] = 0;
                }
            }
        }
        public bool SpawnNext()
        {
            if (gameOver)
                return false;

            bool canSpawn = true;
            int[,] pieceArray = RandomPiece();
            for (int i = 0; i < pieceArray.GetLength(0); i++)
            {
                for (int j = 0; j < pieceArray.GetLength(1); j++)
                {
                    if (dataArray[i + x / 2, y - 1 - j] == 0)
                    {
                        dataArray[i + x / 2, y - 1 - j] = pieceArray[i, j];
                    }
                    else
                    {
                        Debug.Log("GAME OVEEEEEEEER");
                        canSpawn = false;
                        gameOver = true;
                        break;
                    }
                }
            }
            return canSpawn;
        }

        private int randomColor()
        {
            return Random.Range(0, 5) + 1;
        }

        private void GameOver()
        {
            gameOver = true;
            //clear from spawned
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    if (dataArray[i, j] < 0)
                        dataArray[i, j] = 0;
                }
            }
        }

        private int[,] RandomPiece()
        {
            switch (Random.Range(0, 5))
            {
                case 0://square
                    return new int[,] { { randomColor() * -1, randomColor() * -1 }, { randomColor() * -1, randomColor() * -1 } };
                case 1://line
                    return new int[,] { { randomColor() * -1 }, { randomColor() * -1 }, { randomColor() * -1 }, { randomColor() * -1 } };
                case 2://triangle 
                    return new int[,] { { 0, randomColor() * -1 }, { randomColor() * -1, randomColor() * -1 }, { 0, randomColor() * -1 } };
                case 3://z 
                    return new int[,] { { randomColor() * -1, 0 }, { randomColor() * -1, randomColor() * -1 }, { 0, randomColor() * -1 } };
                case 4://s
                default:
                    return new int[,] { { 0, randomColor() * -1 }, { randomColor() * -1, randomColor() * -1 }, { randomColor() * -1, 0 } };

            }

        }

        private void removeLine(int lineNr)
        {
            //remove line
            for (int j = 0; j < x; j++)
            {
                dataArray[j, lineNr] = 0;

            }
            //move blocks down
            for (int i = lineNr; i < y - 1; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    if (dataArray[j, i + 1] >= 0 && dataArray[j, i] >= 0)
                        dataArray[j, i] = dataArray[j, i + 1];
                }
            }
        }
        public int CheckForLines()
        {
            int score = 0;
            bool check = true;
            do
            {
                for (int i = 0; i < y; i++)
                {
                    bool shouldDelete = true;
                    for (int j = 0; j < x; j++)
                    {
                        if (dataArray[j, i] == 0)
                        {
                            shouldDelete = false;
                            break;
                        }

                    }
                    if (shouldDelete)
                    {
                        score++;
                        removeLine(i);
                        break;
                    }
                    else
                    {
                        check = false;
                    }
                }
            } while (check);
            return score;
        }
        public bool MoveRight()
        {
            bool collision = false;
            for (int i = 0; i < y; i++)
            {
                for (int j = x - 1; j >= 0; j--)
                {
                    //wall
                    if (dataArray[j, i] < 0 && j == (x - 1))
                    {
                        Debug.Log("Wall");
                        collision = true;
                        break;
                    }//another block
                    else if (dataArray[j, i] < 0 && dataArray[j + 1, i] > 0)
                    {
                        collision = true;
                        break;
                    }
                }
            }
            //move
            if (!collision)
            {
                for (int i = 0; i < y; i++)
                {
                    for (int j = x - 1; j >= 0; j--)
                    {
                        if (dataArray[j, i] < 0)
                        {
                            dataArray[j + 1, i] = dataArray[j, i];
                            dataArray[j, i] = 0;
                        }

                    }
                }
            }
            return !collision;
        }

        public bool MoveLeft()
        {
            bool collision = false;
            for (int i = 0; i < y; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    //wall
                    if (dataArray[j, i] < 0 && j == 0)
                    {
                        Debug.Log("Wall");
                        collision = true;
                        break;
                    }//another block  
                    else if (dataArray[j, i] < 0 && dataArray[j - 1, i] > 0)
                    {
                        collision = true;
                        break;
                    }
                }
            }
            //move
            if (!collision)
            {
                for (int i = 0; i < y; i++)
                {
                    for (int j = 0; j < x; j++)
                    {
                        if (dataArray[j, i] < 0)
                        {
                            dataArray[j - 1, i] = dataArray[j, i];
                            dataArray[j, i] = 0;
                        }

                    }
                }
            }
            return !collision;

        }
        public void MoveDown(bool allTheWayDown = false)
        {
            do
            {
                bool collision = false;
                for (int i = 0; i < y; i++)
                {
                    for (int j = 0; j < x; j++)
                    {   //wall
                        if (dataArray[j, i] < 0 && i == 0)
                        {
                            Debug.Log("Wall");
                            collision = true;
                            break;
                        }//another block  
                        else if (dataArray[j, i] < 0 && dataArray[j, i - 1] > 0)
                        {
                            collision = true;
                            break;
                        }
                    }
                }
                //move
                if (!collision)
                {
                    for (int i = 1; i < y; i++)
                    {
                        for (int j = 0; j < x; j++)
                        {
                            if (dataArray[j, i] < 0)
                            {
                                dataArray[j, i - 1] = dataArray[j, i];
                                dataArray[j, i] = 0;
                            }

                        }
                    }
                }
                else//land piece
                {
                    for (int i = 0; i < y; i++)
                    {
                        for (int j = 0; j < x; j++)
                        {
                            if (dataArray[j, i] < 0)
                            {
                                dataArray[j, i] = dataArray[j, i] * -1;
                            }
                        }
                    }
                    allTheWayDown = false;
                    if (!gameOver)
                    {
                        if (!SpawnNext())
                        {
                            GameOver();

                        }
                    }
                }
            } while (allTheWayDown);

        }
    }
}