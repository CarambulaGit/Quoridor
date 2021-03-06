using System;
using System.Collections.Generic;
using System.Linq;
using Project.Classes;
using Project.Classes.Field;
using Project.Classes.Player;
using UnityEngine;

namespace Project.Scripts {
    public class GameManager : MonoBehaviour {
        private bool _initialized;
        public Game Game { get; private set; }

        public bool Initialized {
            get => _initialized;
            private set {
                if (_initialized == value) {
                    return;
                }

                _initialized = value;
                if (_initialized) {
                    OnGameModeChosen?.Invoke();
                }
            }
        }

        public event Action OnGameModeChosen;

        public Field Field => Game.Field;
        public Player CurrentPlayer => Game.CurrentPlayer;

        public void CreatePlayerVsPlayer() {
            Game = Game.CreatePlayerVsPlayer();
            Initialized = true;
            Game.StartGame();
        }

        public void CreatePlayerVsBot(bool playerMoveFirst = true) {
            Game = Game.CreatePlayerVsBot(playerMoveFirst);
            Initialized = true;
            Game.StartGame();
        }

        public void Update() {
            if (!Initialized) {
                return;
            }

            Game.Tick();
        }

        public void Restart() {
            Game.Restart();
        }

        public void Restart(List<Player> players) {
            Game.Restart(players);
        }

        public void RestartWithReverseOrder() {
            Game.Restart(new List<Player> {Game.Players[1], Game.Players[0]});
        }
    }
}