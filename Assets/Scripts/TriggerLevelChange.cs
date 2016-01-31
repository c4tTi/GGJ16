using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TriggerLevelChange : MonoBehaviour {


    void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene("RunningBrideStartMenu");
    }
}
