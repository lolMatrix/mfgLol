using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // Собственно сам персонаж
    [SerializeField] private float speed;               //
    [SerializeField] private float jumpHeight;          //
    [SerializeField] private Transform groundChecker;   //
    [SerializeField] private int healthPoint;           //
    [SerializeField] private Text hpText;               //  характеристики персонажа и некоторые объекты ui
    [SerializeField] private Text scoreText;            //  ui объекты будут переданы классам, не привязанных к игровым объектам
    [SerializeField] private GameObject ui;             //

    private Rigidbody2D player;                         //------------------------------------------
    private Animator playerAnimations;                  // Обработка физики персонажа и всего такого
    private bool onEarth;                               //-----------------------------------------
    private SceneObserver observer;                     // могильщик)) смотрит, когда умрет перс
    private bool haveKey = false;
    private int score = 0;

    // Start is called before the first frame update
    private void Start()
    {
        player = GetComponent<Rigidbody2D>();
        playerAnimations = GetComponent<Animator>();
        observer = new SceneObserver();
        DieManager manager = new DieManager(observer, ui);
    }

    // Update is called once per frame
    private void Update()
    {
        MoveHero(); // обратка движения персонажа
    }

    private void FixedUpdate()
    {
        AnimateHero(); // анимация
        editText();    // ui хп и очков перса
    }

    private void MoveHero()
    {
        player.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, player.velocity.y);
        onEarth = IsGrounded(Physics2D.OverlapCircleAll(groundChecker.position, 0.2f));
        if (Input.GetKeyDown(KeyCode.Space) && onEarth)
            player.AddForce(transform.up * jumpHeight, ForceMode2D.Impulse);
    }

    private void AnimateHero()
    {
        RotatePlayer();
        int state = playerAnimations.GetInteger("State");
        if (onEarth && Input.GetAxis("Horizontal") != 0)
            state = 1;
        else if (onEarth) 
            state = 0;
        if (!onEarth)
            state = 2;
        playerAnimations.SetInteger("State", state);
    }

    private bool IsGrounded(Collider2D[] downElements) => downElements.Length > 1;

    private void RotatePlayer() //поворот персонажа
    {
        float rotation = transform.localRotation.eulerAngles.y;
        if (Input.GetAxis("Horizontal") > 0)
            rotation = 0;
        else if (Input.GetAxis("Horizontal") < 0)
            rotation = 180;
        transform.localRotation = Quaternion.Euler(0, rotation, 0);
    }

    public void HitPlayer(int strange) // обработка удара персонажа врагом
    {
        healthPoint -= strange;
        if (healthPoint <= 0)
        {
            GetComponent<CapsuleCollider2D>().enabled = false;
            Invoke("lose", 1.4f);
        }
        playerAnimations.PlayInFixedTime("Hit");
        player.AddForce(new Vector2(-1 * player.velocity.normalized.x, 1) * jumpHeight, ForceMode2D.Impulse);
    }

    private void lose() => observer.Invoke();

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag.Equals("ladder"))
        {
            player.bodyType = RigidbodyType2D.Kinematic;
            player.velocity = new Vector2(speed * Input.GetAxis("Horizontal"), speed * Input.GetAxis("Vertical"));
        }
        if (collision.tag.Equals("door") && Input.GetKeyDown(KeyCode.E))
        {
            collision.GetComponent<Door>()?.TryEnterToDoor(observer);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("key"))
        {
            Destroy(collision.gameObject);
            haveKey = true;
        }
        if (collision.tag.Equals("door") && haveKey)
        { 
            collision.GetComponent<Door>()?.OpenDoor();
            haveKey = false;
        }
        if (collision.tag.Equals("Death"))
        {
            HitPlayer(healthPoint);
        }
        if (collision.tag.Equals("HitEnemyTrigger"))
        {
            player.AddForce(Vector2.up * 30 , ForceMode2D.Impulse);
            score += 50;
            collision.SendMessageUpwards("HitEnemy");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("ladder"))
        {
            player.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    private void editText()
    {
        hpText.text = "HP: " + healthPoint;
        scoreText.text = "Score: " + score;
    }

}