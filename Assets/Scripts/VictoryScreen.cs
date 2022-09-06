using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScreen : MonoBehaviour
{
    public string AnaMenuSahnesiIsmi = "Main Menu";
    // Start is called before the first frame update
    void Start()
    {
        Destroy(FindObjectOfType<GameSessionController>().gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MenuyeDon()
    {
        SceneManager.LoadScene(AnaMenuSahnesiIsmi);
    }

}
