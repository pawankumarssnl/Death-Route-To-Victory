using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public float speed;
    public float speed1;
    private int count;
    public Text ScoreText;
    private GameObject[] gems;
    private GameObject[] gems1;
    private Vector3 startPos;
    public Text loseText;
    public Text winText;
    public Button playAgainButton;
    public Button backToMainMenu;
    private bool gameStarted;
    private bool touchStart=false;
    private Vector3 pointA;
    private Vector3 pointB;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = this.GetComponent<Rigidbody>();
        gems=GameObject.FindGameObjectsWithTag("Gem");
        gems1=GameObject.FindGameObjectsWithTag("Gem1");
        startPos=playerRb.position;
        playAgainButton.onClick.AddListener(playAgainButtonAction);
        backToMainMenu.onClick.AddListener(backToMainMenuAction);
        gameStarted=true;
        count=0;
        UpdateScoreText();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            pointA= Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,  Camera.main.transform.position.z,Input.mousePosition.y));
        }
        if(Input.GetMouseButton(0)){
            touchStart=true;
            pointB= Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,   Camera.main.transform.position.z,Input.mousePosition.y));
        }else{
            touchStart=false;
        }
    }

    void FixedUpdate()
    {
        if(touchStart){
            Vector3 offset=pointB-pointA;
            Vector3 direction=Vector3.ClampMagnitude(offset,1.0f);
            playerRb.AddForce(direction *speed1 * Time.deltaTime);

        }
        if(gameStarted){
        float moveHorizontal=Input.GetAxis("Horizontal");
        float moveVertical=Input.GetAxis("Vertical");

        Vector3 move = new Vector3(moveHorizontal , 0.0f, moveVertical);

        playerRb.AddForce(move*Time.deltaTime*speed);}
    }

    void OnTriggerEnter(Collider collider){
        if(collider.gameObject.CompareTag("Gem") || collider.gameObject.CompareTag("Gem1"))
        {
            collider.gameObject.SetActive(false);
            if(collider.gameObject.CompareTag("Gem")){
            count+=1;}
            else{
                count+=2;
            }
            //Debug.Log("GEM COUNT IS : "+ count);
            UpdateScoreText();
        }
        else if(collider.gameObject.CompareTag("Win")){
            gameStarted=false;
            
            winText.text = "You Win !!!\n Final Score : "+ count;
            resetGameState();
            winText.gameObject.SetActive(true);
            playAgainButton.gameObject.SetActive(true);
            backToMainMenu.gameObject.SetActive(true);
            ScoreText.gameObject.SetActive(false);
        }
        else if(collider.gameObject.CompareTag("Gameover"))
        {
            gameStarted=false;
            
            loseText.text = "Game Over !!!\n Final Score : "+count;
            resetGameState();
            loseText.gameObject.SetActive(true);
            playAgainButton.gameObject.SetActive(true);
            backToMainMenu.gameObject.SetActive(true);
            ScoreText.gameObject.SetActive(false);
        }
    }

    void UpdateScoreText(){
        ScoreText.text = "Score :" + count; 
    }

    void resetGameState(){
        count = 0;
        UpdateScoreText();

        for(int i=0;i<gems.Length;i=i+1)
        {
            gems[i].gameObject.SetActive(true);
        }
        for(int i=0;i<gems1.Length;i=i+1)
        {
            gems1[i].gameObject.SetActive(true);
        }
        playerRb.position=startPos;
        playerRb.velocity=Vector3.zero;
        playerRb.angularVelocity=Vector3.zero;
    }

    void playAgainButtonAction()
    {
        count=0;
        UpdateScoreText();

        ScoreText.gameObject.SetActive(true);
        winText.gameObject.SetActive(false);
        loseText.gameObject.SetActive(false);
        playAgainButton.gameObject.SetActive(false);
        backToMainMenu.gameObject.SetActive(false);

        gameStarted=true;
    }
    void backToMainMenuAction(){
        SceneManager.LoadScene("LevelSelector");
    }
}
