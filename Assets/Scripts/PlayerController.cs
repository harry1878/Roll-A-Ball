using System;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Text CountText;
    public Text WinText;
    public Button RestartButton;
    public GameObject player;
    //public Transform[] coin;

    private Rigidbody rb;
    private int count;
    private float time = 0;
    private Rotator[] golds = null; 

    private void Start()
    {
        golds = FindObjectsOfType<Rotator>();
        rb = GetComponent<Rigidbody>();
    }

    void Awake()
    {
        count = 0;
        SetCountText();
        WinText.gameObject.SetActive(false);
        RestartButton.gameObject.SetActive(false);
    }
    void Update()
    {
        if(count >= golds.Length)
        {
            time += Time.deltaTime;
        }
        if (time >= 3)
        {
            RestartButton.gameObject.SetActive(true);
        }
    }
    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);
        rb.AddForce(movement * speed);
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }
        
    }
    void SetCountText()
    {
        CountText.text = "Count: " + count.ToString();
        if(count >= golds.Length)
        {
            CountText.gameObject.SetActive(false);
            WinText.gameObject.SetActive(true);
            
        }
    }
    public void Restart()
    {
        Awake();
        count = 0;
        time = 0;
        CountText.gameObject.SetActive(true);
        player.transform.position = new Vector3(0, 0.5f, 0);
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.Sleep();
        //for(int i = 0; i < coin.Length; ++i)
        //{
        //    coin[i].gameObject.SetActive(true);
        //}
        for(int i =0; i < golds.Length; ++i)
        {
            golds[i].transform.localRotation = Quaternion.identity;
            golds[i].gameObject.SetActive(true);
        }
    }
}
