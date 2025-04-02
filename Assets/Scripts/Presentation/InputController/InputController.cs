using System.Collections.Generic;
using UnityEngine;
namespace MultiTetris.Presentation
{
    public class InputController
    {
        private PlayerControls _controls;
        private float moveDuration = .5f;
        private float _moveDuration = 0f;
        private List<Player> players;
        private GameManager _gm = GameManager.Instance;
        public void Start()
        {
            players = _gm.players;
            _controls = new PlayerControls();
            _controls.Enable();
        }

        public void ManageInput()
        {
            //PLAYER 1  
            if (!players[0].gameOver)
            {
                if (players[0].canMove)
                {
                    if (_controls.Player1.left.IsPressed())
                    {
                        if (players[0].gridControler.MoveLeft())
                        {
                            players[0].canMove = false;
                        }
                    }
                    else if (_controls.Player1.right.IsPressed())
                    {
                        if (players[0].gridControler.MoveRight())
                        {
                            players[0].canMove = false;
                        }
                    }
                    if (_controls.Player1.down.IsPressed())
                    {
                        players[0].gridControler.MoveDown(true);
                        players[0].canMove = false;
                    }
                }
                _moveDuration += Time.deltaTime;
                if (_moveDuration >= moveDuration)
                {
                    players[0].canMove = true;
                    _moveDuration -= moveDuration;
                    players[0].gridControler.MoveDown();
                }
            }
            //PLAYER 2
            if (players.Count > 1)
            {
                if (!players[1].gameOver)
                {
                    if (players[1].canMove)
                    {
                        if (_controls.Player2.left.IsPressed())
                        {
                            if (players[1].gridControler.MoveLeft())
                            {
                                players[1].canMove = false;
                            }
                        }
                        else if (_controls.Player2.right.IsPressed())
                        {
                            if (players[1].gridControler.MoveRight())
                            {
                                players[1].canMove = false;
                            }
                        }
                        if (_controls.Player2.down.IsPressed())
                        {
                            players[1].gridControler.MoveDown(true);
                            players[1].canMove = false;
                        }
                    }
                    _moveDuration += Time.deltaTime;
                    if (_moveDuration >= moveDuration)
                    {
                        players[1].canMove = true;
                        _moveDuration -= moveDuration;
                        players[1].gridControler.MoveDown();
                    }
                }
            }


        }
    }
}