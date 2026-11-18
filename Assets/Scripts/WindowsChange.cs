using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowsChange : MonoBehaviour
{
    public GameObject mainWindows;
    public GameObject gardenPlace;
    public GameObject labPlace;

    public void ToGarden()
    {
        mainWindows.SetActive(false);
        gardenPlace.SetActive(true);
    }

    public void FromGardenToMainWindows()
    {
        mainWindows.SetActive(true);
        gardenPlace.SetActive(false);
    }

    public void ToLab()
    {
        mainWindows.SetActive(false);
        labPlace.SetActive(true);
    }

    public void FromLabToMainWindows()
    {
        mainWindows.SetActive(true);
        labPlace.SetActive(false);
    }
}
