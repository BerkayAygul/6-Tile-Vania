using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSessionController : MonoBehaviour
{
    [SerializeField] int OyuncuCanSayisi = 2;
    [SerializeField] int OyuncuSkorSayisi = 0;

    [SerializeField] Text OyuncuYasamHaklariText;
    [SerializeField] Text OyuncuSkoruText;

    private void Awake()     //Singleton
    {
        int OyunOturumlariSayisi = FindObjectsOfType<GameSessionController>().Length;

        if(OyunOturumlariSayisi > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        OyuncuYasamHaklariText.text = OyuncuCanSayisi.ToString();
        OyuncuSkoruText.text = OyuncuSkorSayisi.ToString();
    }

    public void SkorEkle(int EklenilecekPuan)
    {
        OyuncuSkorSayisi += EklenilecekPuan;
        OyuncuSkoruText.text = OyuncuSkorSayisi.ToString();
    }

    public void OyuncuOlumunuIsle()
    {
        if(OyuncuCanSayisi > 1)
        {
            OyuncuCanEksilt();
        }
        else
        {
            OyunuYenidenBaslat();
        }
    }

    private void OyuncuCanEksilt()
    {
        OyuncuCanSayisi--;
        var AnlikSahneIndexNumarasi = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(AnlikSahneIndexNumarasi);

        OyuncuYasamHaklariText.text = OyuncuCanSayisi.ToString();
    }

    public void OyunuYenidenBaslat()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
}
