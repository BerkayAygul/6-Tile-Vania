using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMovement : MonoBehaviour
{
    [Tooltip("Saniyedeki Oyun Zaman Birimi")]
    [SerializeField] float SuHareketHizi = 0.2f;

    // Update is called once per frame
    void Update()
    {
        float SuyunYDuzlemindekiHareketi = SuHareketHizi * Time.deltaTime;
        transform.Translate(new Vector2(0f, SuyunYDuzlemindekiHareketi));
    }
}
