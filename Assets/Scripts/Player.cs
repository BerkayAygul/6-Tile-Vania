using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Config
    [SerializeField] float KosmaHizi = 5f;
    [SerializeField] float ZiplamaHizi = 5f;
    [SerializeField] float TirmanmaHizi = 5f;
    [SerializeField] Vector2 OlmeVektorHizi; 

    // State
    bool OyuncuCanliMi = true;

    // Cached Component References
    Rigidbody2D OyuncuRigidBody;
    Animator OyuncuAnimatorController;
    CapsuleCollider2D OyuncuKapsulCarpisma2D;
    BoxCollider2D OyuncuKutuCarpisma2D;
    float BaslangictakiYercekimiAyari;


    // Start is called before the first frame update
    void Start()
    {
        OyuncuRigidBody = GetComponent<Rigidbody2D>();
        OyuncuAnimatorController = GetComponent<Animator>();
        OyuncuKapsulCarpisma2D = GetComponent<CapsuleCollider2D>();
        OyuncuKutuCarpisma2D = GetComponent<BoxCollider2D>();
        BaslangictakiYercekimiAyari = OyuncuRigidBody.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if(!OyuncuCanliMi)
        {
            return;
        }

        KosmaEylemi();
        TirmanmaEylemi();
        ZiplamaEylemi();
        SpriteTersDondur();
        //OlmeEylemi(); Her karede kontrol edilmesi gereksiz. OnTriggerEnter2D
    }

    private void SpriteTersDondur() // Oyuncu Yatay hareket yapiyorsa
    {
        bool OyuncununHareketiYatay = Mathf.Abs(OyuncuRigidBody.velocity.x) > Mathf.Epsilon;

        if (OyuncununHareketiYatay)
        {
            transform.localScale = new Vector2(Mathf.Sign(OyuncuRigidBody.velocity.x), 1f);
        }
    }

    private void KosmaEylemi()
    {
        float KontrolDegiskeni = Input.GetAxis("Horizontal"); // -1 to +1

        Vector2 OyuncuKosmaVektor = new Vector2(KontrolDegiskeni * KosmaHizi, OyuncuRigidBody.velocity.y);
        OyuncuRigidBody.velocity = OyuncuKosmaVektor;

        bool OyuncununYatayHareketiVarMi = Mathf.Abs(OyuncuRigidBody.velocity.x) > Mathf.Epsilon;
        OyuncuAnimatorController.SetBool("RunningBool", OyuncununYatayHareketiVarMi);
    }

    private void ZiplamaEylemi() // Yercekimi ayarlarini yap. Player collision detection = continious
    {
        if(!OyuncuKutuCarpisma2D.IsTouchingLayers(LayerMask.GetMask("Ground_Layer"))) // Yuzeye degmiyorsa metottan cikis yap, ziplamaya izin verme.
        {
            return;
        }

        if(Input.GetButtonDown("Jump")) // Layer ayarý yap, surekli ziplamayi onle. User Layer 6 = Ground, Foreground Tile Layer = Ground
        {
            Vector2 OyuncuYKoordinatiZiplamaVektor = new Vector2(0f, ZiplamaHizi);
            OyuncuRigidBody.velocity += OyuncuYKoordinatiZiplamaVektor;
        }
    }

    private void TirmanmaEylemi()
    {
        if(!OyuncuKutuCarpisma2D.IsTouchingLayers(LayerMask.GetMask("Climbing_Layer"))) // Merdivene degmiyorsa metottan cikis yap.
        {
            OyuncuAnimatorController.SetBool("ClimbingBool", false);
            OyuncuRigidBody.gravityScale = BaslangictakiYercekimiAyari;
            return;
        }

        float KontrolDegiskeni = Input.GetAxis("Vertical");

        Vector2 OyuncuTirmanisVektor = new Vector2(OyuncuRigidBody.velocity.x, KontrolDegiskeni * TirmanmaHizi);

        OyuncuRigidBody.velocity = OyuncuTirmanisVektor;
        OyuncuRigidBody.gravityScale = 0f; // Merdivenden dusmemesi icin yercekimi 0

        bool OyuncununDikeyHareketiVarMi = Mathf.Abs(OyuncuRigidBody.velocity.y) > Mathf.Epsilon;
        OyuncuAnimatorController.SetBool("ClimbingBool", OyuncununDikeyHareketiVarMi);
    }

    private void OlmeEylemi()
    {
        if(!OyuncuCanliMi)
        {
            return;
        }
        OyuncuCanliMi = false;
        OyuncuAnimatorController.SetTrigger("DyingTrigger");
        if (OyuncuKapsulCarpisma2D.IsTouchingLayers(LayerMask.GetMask("Enemy_Layer")) && OyuncuCanliMi)
        {
            GetComponent<Rigidbody2D>().velocity = OlmeVektorHizi;
        }
        else if(OyuncuKapsulCarpisma2D.IsTouchingLayers(LayerMask.GetMask("Hazard_Layer")) && OyuncuCanliMi)
        {
            OlmeVektorHizi = new Vector2(-10f, 0f);
            GetComponent<Rigidbody2D>().velocity = OlmeVektorHizi;
        }
        StartCoroutine(OyuncuVektorHiziniSifirYap());

        FindObjectOfType<GameSessionController>().OyuncuOlumunuIsle();
    }

    public void DusmanIleCarpismaEylemi() // Dusman Classinda kullanýlýyor
    {
        if (OyuncuCanliMi)
        {
            OlmeEylemi();
        }
    }

    private void OnTriggerEnter2D(Collider2D CarpismaBedeniAl)
    {
        if (CarpismaBedeniAl.gameObject.tag == "Hazard_Tag") 
        {
            OlmeEylemi();
        }
    }

    IEnumerator OyuncuVektorHiziniSifirYap()
    {
        yield return new WaitForSeconds(0.1f);
        OyuncuRigidBody.velocity = new Vector2(0f, 0f);
    }
}
