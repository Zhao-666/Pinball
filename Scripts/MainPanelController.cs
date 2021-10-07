using UnityEngine;
using UnityEngine.UI;

public class MainPanelController : MonoBehaviour
{
    public static MainPanelController Instance { get; private set; }

    private Transform _title;
    private Transform _startBtn;
    private Transform _score;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        _title = transform.Find("Title");
        _startBtn = transform.Find("StartButton");
        _score = transform.Find("Score");
        _score.gameObject.SetActive(false);
        _startBtn.GetComponent<Button>().onClick.AddListener(Play);
    }

    public void Init(int score)
    {
        _title.gameObject.SetActive(true);
        _startBtn.gameObject.SetActive(true);
        _score.gameObject.SetActive(true);
        _score.Find("Num").GetComponent<Text>().text = score.ToString();
    }

    private void Play()
    {
        _title.gameObject.SetActive(false);
        _startBtn.gameObject.SetActive(false);
        _score.gameObject.SetActive(false);
        GameManager.Instance.Play();
    }
}