using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip MadeniParaSFX;
    [SerializeField] int MadeniParalarinPuanKarsiligi = 100; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        FindObjectOfType<GameSessionController>().SkorEkle(MadeniParalarinPuanKarsiligi);
        // to spatial blend and other settings).
        SesiBelirliBirVektordeCal(Camera.main.transform.position, 0.0F, this.MadeniParaSFX);

        Destroy(gameObject);
    }

    public AudioSource SesiBelirliBirVektordeCal(Vector3 Pozisyon, float UzaysalBlend, AudioClip SesClip)
    {
        // Oyun objesi olustur.

        GameObject GeciciSesObjesi = new GameObject("TmpAudio");

        // Ge�ici ses objesinin pozisyonunu kamera pozisyonuna e�itle.

        GeciciSesObjesi.transform.position = Pozisyon;

        // Ses efektini al..

        AudioSource SesKaynagi = GeciciSesObjesi.AddComponent<AudioSource>();

        // Spatial blend.

        SesKaynagi.spatialBlend = UzaysalBlend;

        // Ses klibini ayarla.

        SesKaynagi.clip = SesClip;

        // Klibi �al.
        SesKaynagi.Play();

        // Klip bitiminde oyun objesini yok et.

        Destroy(GeciciSesObjesi, SesClip.length);

        // Ses kayna�ini return et.

        return SesKaynagi;
    }
}
