using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Alteruna;
using System.Threading;


public class PotatoSpawner : MonoBehaviour
{

    private Spawner _spawner;
    private Alteruna.Avatar _avatar;
    [SerializeField] private float timer = 10f;
    [SerializeField] private int indexToSpawn = 0;

    private void Awake()
    {
        _avatar = GetComponent<Alteruna.Avatar>();
        _spawner = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<Spawner>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (!_avatar)
           // return;

        timer -= Time.deltaTime;
        if (timer == 0)
        {
            SpawnPotato();
        }
    }

    void SpawnPotato()
    {
        _spawner.Spawn(indexToSpawn, new Vector3(2f, 2f, 2f));
    }
}
