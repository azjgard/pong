﻿using TMPro;
using UnityEngine;

public enum PlayerType {
    player1,
    player2
}

public class GameController : MonoBehaviour
{
    [Header("Dependencies")]
    public BallController ball;
    public PaddleController player1;
    public PaddleController player2;
    public GoalController player1Goal;
    public GoalController player2Goal;
    public TextMeshProUGUI scoreText;

    [Header("Sounds")]
    public AudioSource playerScored_sound;

    private int player1Score = 0;
    private int player2Score = 0;

    void Start()
    {
        player1Goal.Init(this, ball.gameObject.layer, PlayerType.player1);
        player2Goal.Init(this, ball.gameObject.layer, PlayerType.player2);

        Invoke(nameof(NewGame), 1);
    }

    private void NewGame() {
        player1Score = 0;
        player2Score = 0;
        ball.Reset();
        Invoke(nameof(StartMovingBall), 0.5f);
    }

    public void PlayerScored(PlayerType playerType) {
        switch (playerType) {
            case PlayerType.player1:
                player1Score++;
                break;
            case PlayerType.player2:
                player2Score++;
                break;
            default:
                throw new System.Exception("Invalid PlayerType passed to Score");
        }

        if (playerScored_sound != null) {
            playerScored_sound.Play();
        }

        UpdateScoreText();

        ball.Reset();
        player1.Reset();
        player2.Reset();

        Invoke(nameof(StartMovingBall), 2f);
    }

    private void StartMovingBall() {
        ball.StartMoving();
    }

    private void UpdateScoreText() {
        scoreText.text = player1Score + " - " + player2Score;
    }
}
