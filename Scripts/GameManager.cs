using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private GameObject _prefabCube;
    private GameObject _prefabPlayer;

    private Transform _cubeParent;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        _prefabCube = Resources.Load<GameObject>("Cube");
        _cubeParent = GameObject.Find("CubeParent").transform;
    }

    public void GameOver()
    {
        MainPanelController.Instance.Init(GetScore());
        PlayerController.Instance.GameOver();
        BallController.Instance.GameOver();
    }

    public void Play()
    {
        CreateCubeWall();
        PlayerController.Instance.Play();
        BallController.Instance.Play();
    }

    private int GetScore()
    {
        int childCount = _cubeParent.childCount;
        return 90 - childCount;
    }

    private void CreateCubeWall()
    {
        int childCount = _cubeParent.childCount;
        if (childCount > 0)
        {
            for (int i = 0; i < childCount; i++)
            {
                Destroy(_cubeParent.GetChild(i).gameObject);
            }
        }

        for (int i = -1; i < 9; i++)
        {
            for (int j = -4; j < 5; j++)
            {
                Instantiate(_prefabCube, new Vector3(2 * j * 1.1f, 0, i * 1.1f),
                    Quaternion.identity, _cubeParent);
            }
        }
    }
}