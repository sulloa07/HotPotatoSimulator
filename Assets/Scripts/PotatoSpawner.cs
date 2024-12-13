using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Alteruna;
using System.Threading;


public class PotatoSpawner : MonoBehaviour
{

    private Spawner _spawner;
    private Alteruna.Avatar _avatar;

    //[SerializeField] private float timer = 10f;
    [SerializeField] private int indexToSpawn = 0;

    private GameManager _gameManager;
    private void Awake()
    {
        _avatar = GetComponent<Alteruna.Avatar>();
        _spawner = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<Spawner>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (!_avatar.IsMe)
            //return;

        if(Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            SpawnPotato();
            GameObject gameManagerGO = GameObject.Find("GameManager");
            _gameManager = gameManagerGO.GetComponent<GameManager>();
            _gameManager.gameBegins();
        }
    }

    public void SpawnPotato()
    {
        _spawner.Spawn(indexToSpawn, new Vector3(2f, 2f, 2f));
    }
}
