using UnityEngine;

public enum PlayerType {
    player1,
    player2
}

public class GameController : MonoBehaviour
{
    public BallController ball;
    public PaddleController player1;
    public PaddleController player2;
    public GoalController player1Goal;
    public GoalController player2Goal;

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
        Invoke(nameof(StartMovingBall), 2f);
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

        ball.Reset();
        Invoke(nameof(StartMovingBall), 2f);
    }

    private void StartMovingBall() {
        ball.StartMoving();
    }
}
