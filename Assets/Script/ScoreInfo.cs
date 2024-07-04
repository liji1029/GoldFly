using UnityEngine;

public class ScoreInfo : MonoBehaviour
{
    public int Score { get { return _score; } }
    [SerializeField]
    private int _score;
    void Start()
    {
        _score = Random.Range(0, 200);
    }

}
