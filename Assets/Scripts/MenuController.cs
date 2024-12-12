using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    public void removeMenu()
    {
        mainMenu.SetActive(false);
    }

    public void addMenu()
    {
        mainMenu.SetActive(true);
    }
}
