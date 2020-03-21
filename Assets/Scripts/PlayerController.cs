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

    private Rigidbody rb;
    private int count;
    private float time = 0;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        WinText.gameObject.SetActive(false);
        RestartButton.gameObject.SetActive(false);
    }
    void Update()
    {
        if(count >=2)
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
        if(count >=2)
        {
            CountText.gameObject.SetActive(false);
            WinText.gameObject.SetActive(true);
            
        }
    }
    public void Restart()
    {
        count = 0;
        time = 0;
        SetCountText();
        player.transform.position = new Vector3(0, 0.5f, 0);
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.Sleep();
        GameObject test = GameObject.FindGameObjectWithTag("Pick Up");
        test.gameObject.SetActive(true);
        GameObject[] PickUps = GameObject.FindGameObjectsWithTag("Pick Up");
        foreach(GameObject i in PickUps)
        {
            i.gameObject.SetActive(true);
        }
        CountText.gameObject.SetActive(true);
        WinText.gameObject.SetActive(false);
        RestartButton.gameObject.SetActive(false);
    }
}
