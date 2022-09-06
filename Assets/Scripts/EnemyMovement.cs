using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float DusmanHareketHizi = 1f;
    Rigidbody2D DusmanRigidBody;

    // Start is called before the first frame update
    void Start()
    {
        DusmanRigidBody = GetComponent<Rigidbody2D>();   
    }

    // Update is called once per frame
    void Update()
    {
        if(DusmanSagaMiBakiyor())
        {
            DusmanRigidBody.velocity = new Vector2(DusmanHareketHizi, 0f);
        }
        else
        {
            DusmanRigidBody.velocity = new Vector2(-DusmanHareketHizi, 0f);
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale = new Vector2(-(Mathf.Sign(DusmanRigidBody.velocity.x)), 1f);
    }

    bool DusmanSagaMiBakiyor()
    {
        return transform.localScale.x > 0;
    }

    private void OnTriggerEnter2D(Collider2D CarpismaBedeniAl)
    {
        Player OyuncuBedeni = CarpismaBedeniAl.gameObject.GetComponent<Player>();
        if (OyuncuBedeni)
        {
            OyuncuBedeni.DusmanIleCarpismaEylemi();
        }
    }
}
