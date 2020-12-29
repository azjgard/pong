using UnityEngine;

public class GoalController : MonoBehaviour
{
    private GameController gameController = null;
    private int? ballLayer = null;
    private PlayerType playerWhoScores;

    public void Init(
        GameController _gameController,
        int _ballLayer,
        PlayerType _playerWhoScores
    ) {
        gameController = _gameController;
        ballLayer = _ballLayer;
        playerWhoScores = _playerWhoScores;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer == ballLayer) {
            gameController.PlayerScored(playerWhoScores);
        }
    }
}
