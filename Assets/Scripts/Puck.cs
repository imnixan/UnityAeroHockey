using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Puck : MonoBehaviour
{
    public static event UnityAction <int> PlayerScoredGoal;
    public static event UnityAction Goal;
    
    [SerializeField] private AudioClip[] borderHits;
    [SerializeField] private AudioClip[] strikerHits;
    [SerializeField] private AudioClip startGame, endGame, goalSound, goalMusic;
    private AudioSource soundPlayer;
    private float redPlayerGate;
    private float bluePlayerGate;
    private float borders;
    private const int RedPlayerId = 0;
    private const int BluePlayerId = 1;
    private Rigidbody2D rb;
    private Transform puckTransform;
    private float speed;
    private float maxVelocity;
    
    private void Start()
    {
        GameGoalsCounter.GameEnd += OnGameEnd;

        soundPlayer = GetComponent<AudioSource>();
        redPlayerGate = Camera.main.ViewportToWorldPoint(Vector2.zero).y;
        bluePlayerGate = Camera.main.ViewportToWorldPoint(new Vector2(1,1)).y;
        borders = Camera.main.ViewportToWorldPoint(new Vector2(1,1)).x;
        rb = GetComponent<Rigidbody2D>();
        puckTransform = transform;
        PlaySound(startGame);
        maxVelocity = 100;
    }

    void OnDisable()
    {
        GameGoalsCounter.GameEnd -= OnGameEnd;
    }
    

   private void LateUpdate()
   {
        if(puckTransform.position.y > bluePlayerGate)
        {
            PlayGoalEffects();
            PlayerScoredGoal?.Invoke(RedPlayerId);
        }

        if(puckTransform.position.y < redPlayerGate)
        {
            PlayGoalEffects();
            PlayerScoredGoal?.Invoke(BluePlayerId);
        }

        if(puckTransform.position.x > borders || puckTransform.position.x < -borders){
            SpawnPuck();
        }

        speed = rb.velocity.sqrMagnitude;
        if(speed > maxVelocity * 2)
        {
            rb.velocity *= 0.5f;
        }
        else if(speed > maxVelocity)
        {
            rb.velocity *= 0.9f;
        }
   }

   private void SpawnPuck()
   {
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
        puckTransform.position = Vector2.zero;
   }


   private void PlaySound(AudioClip soundEffect)
   {
        if(PlayerPrefs.GetInt("SoundEffects", 1) == 1)
        {
            soundPlayer.PlayOneShot(soundEffect);
        }
   }

   private void PlayVibration()
   {
        if(PlayerPrefs.GetInt("VibroEffects", 1) == 1)
        {
            Handheld.Vibrate();
        }
   }

   private void OnCollisionEnter2D(Collision2D other)
   {
        if(other.gameObject.CompareTag("striker"))
        {
            PlaySound(strikerHits[Random.Range(0, strikerHits.Length)]);
        }
        else
        {
            PlaySound(borderHits[Random.Range(0, borderHits.Length)]);
        }
   }

    private void PlayGoalEffects()
    {
        SpawnPuck();
        Debug.Log(Goal.GetInvocationList().Length);
        Goal?.Invoke();
        PlaySound(goalSound);
        PlaySound(goalMusic);
        PlayVibration();
    }

   private void OnGameEnd(int winner)
   {
        PlaySound(endGame);
        PlayVibration();
   }

   public void Push()
   {
        if(rb.velocity.y < 0)
        {
            rb.velocity *= new Vector2(1, 20);
        }
   }
}
