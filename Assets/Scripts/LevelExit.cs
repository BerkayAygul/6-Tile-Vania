using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] float SeviyeYuklemeZamani = 0.5f;
    [SerializeField] float SeviyedenCikisYavaslamaZamani = 0.2f;

    GameSessionController OyunKontrolSinifi = new GameSessionController();

    private void OnTriggerEnter2D(Collider2D OyuncuIleCarpisma)
    {
        StartCoroutine(SonrakiSeviyeyiYukle());
    }

    IEnumerator SonrakiSeviyeyiYukle()
    {
        Time.timeScale = SeviyedenCikisYavaslamaZamani;
        yield return new WaitForSecondsRealtime(SeviyeYuklemeZamani);
        Time.timeScale = 1f;
        var AnlikSahneIndexNumarasi = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(AnlikSahneIndexNumarasi + 1);
    }
}
