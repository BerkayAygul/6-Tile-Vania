using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RememberPickups : MonoBehaviour
{
    static RememberPickups ClassOrnegi = null;

    int BaslangicSahnesiIndexNumarasi;

    //private void Awake()
    //{
    //    int HatirlamaObjesiSayisi = FindObjectsOfType<RememberPickups>().Length;
    //    if(HatirlamaObjesiSayisi > 1)
    //    {
    //        Destroy(gameObject);
    //    }
    //    else
    //    {
    //        DontDestroyOnLoad(gameObject);
    //    }
    //}

    // Start is called before the first frame update
    void Start()
    {
        if (!ClassOrnegi)
        {
            ClassOrnegi = this;
            SceneManager.sceneLoaded += SahneyiYukle;
            BaslangicSahnesiIndexNumarasi = SceneManager.GetActiveScene().buildIndex;
            DontDestroyOnLoad(gameObject);
        }
        else if (ClassOrnegi != this)
        {
            Destroy(gameObject);
        }
    }

     void SahneyiYukle(Scene SahneyiAl, LoadSceneMode SahneYuklemeModu)
     {
        if(BaslangicSahnesiIndexNumarasi != SceneManager.GetActiveScene().buildIndex)
        {
            ClassOrnegi = null;
            SceneManager.sceneLoaded -= SahneyiYukle;
            Destroy(gameObject);
        }
     }

    // Update is called once per frame
    /*void Update()
    {
        int AnlikSahneIndexNumarasi = SceneManager.GetActiveScene().buildIndex;

        if(AnlikSahneIndexNumarasi != BaslangicSahnesiIndexNumarasi)
        {
            Destroy(gameObject);
        }
    }*/
}
