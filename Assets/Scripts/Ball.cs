using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Ball : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject[] SplashPrefabs;
    public Material[] BallColors;
    TrailRenderer BallTrail;
    public float jumpForce;
    public bool isJumping, isDied;
    GameManager gm;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        gm = FindObjectOfType<GameManager>();
    }

    void Start()
    {
        BallTrail = GetComponent<TrailRenderer>();
    }

    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject splash = Instantiate(SplashPrefabs[gm.levelIndex], transform.position - new Vector3(0f, 0.23f, 0f), transform.rotation);
        splash.transform.SetParent(collision.gameObject.transform);

        Destroy(splash, 2);

        if (isJumping == false)
        {
            rb.AddForce(Vector3.up * jumpForce);
            isJumping = true;
        }

        if (collision.collider.tag == "Engel")
        {
            isDied = true;
            this.rb.isKinematic = true;
        }

        if (collision.collider.tag == "NextLevel")
        {
            gm.levelIndex += 1;
            gm.loadingPanel.SetActive(true);

            transform.DOMove(gm.levelPozisions[gm.levelIndex].position, 1f).OnComplete(() =>
            {
                this.GetComponent<MeshRenderer>().material = BallColors[gm.levelIndex];
                BallTrail.material = BallColors[gm.levelIndex];
                gm.loadingPanel.SetActive(false);
                rb.isKinematic = false;
                gm.parkours[gm.levelIndex].transform.rotation = Quaternion.Euler(0, 0, 0);
            });
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        isJumping = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Puan")
        {
            gm.point += 10;
            //Destroy(other.transform.parent);
        }
    }
}
