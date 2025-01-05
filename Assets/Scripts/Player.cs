using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    public float moveSpeed = 2f;
    GameObject currentFloor;
    [SerializeField] int HP;
     [SerializeField] GameObject HpBar;
     [SerializeField] Text  scoreText;
     
     int score ;
     float scoreTime;
     AudioSource deathSound;

     [SerializeField] GameObject  replayButton;
    // Start is called before the first frame update
    void Start()
    {
        HP = 10;
        score = 0;
        scoreTime = 0f;
        deathSound =  GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.D))
        {
            transform.Translate(moveSpeed*Time.deltaTime,0,0);
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else if(Input.GetKey(KeyCode.A))
        {
            transform.Translate(-moveSpeed*Time.deltaTime,0,0);
            GetComponent<SpriteRenderer>().flipX = true;
        }
        UpdateScore();
        
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "NormalFloor"){
            if(other.contacts[0].normal == new Vector2(0f,1f)){
                Debug.Log("NormalFloor Collision!");
                currentFloor = other.gameObject;
                ModifyHP(+1);
                other.gameObject.GetComponent<AudioSource>().Play();
            }
        }
        else if(other.gameObject.tag == "NailsFloor"){
            if(other.contacts[0].normal == new Vector2(0f,1f)){
                Debug.Log("NailsFloor Collision!");
                currentFloor = other.gameObject;
                ModifyHP(-2);
                other.gameObject.GetComponent<AudioSource>().Play();
            }
        }
        else if(other.gameObject.tag == "CeilingFloor"){
            Debug.Log("CeilingFloor Collision!");
            currentFloor.GetComponent<BoxCollider2D>().enabled = false;            
            ModifyHP(-2);
            other.gameObject.GetComponent<AudioSource>().Play();
        }

    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "DeathLine")
        {
            Die();
        }
    }

    void ModifyHP(int num) {
        HP += num;
        if(HP > 10) {
            HP = 10;
        }else if(HP < 0){
            Die();
        }
        Debug.Log(HP);
        UpdateHpBar();
    }
    void UpdateHpBar()
    {
        for(int i= 0; i<HpBar.transform.childCount;i++){
            if(i<HP){
                HpBar.transform.GetChild(i).gameObject.SetActive(true);
            }else{
                Debug.Log("hide");
                HpBar.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    void UpdateScore(){
        scoreTime += Time.deltaTime ;
        if(scoreTime>2f)
        {
            score++;
            scoreTime = 0f;
            scoreText.text = "Score :" + score.ToString();
        }
    }

    void Die(){
        HP =0;
        Debug.Log("You Lose!");
        deathSound.Play();
        Time.timeScale = 0;
        replayButton.SetActive(true);
    }

    public void Replay(){
        Time.timeScale = 1f;  
        SceneManager.LoadScene("Scenes1");
    }
}
