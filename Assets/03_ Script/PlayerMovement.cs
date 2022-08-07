using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private bl_Joystick joystick;// Joystick reference for assign in inspector
    [SerializeField] private float moveSpeed = 5f;

    Vector2 minBounds;
    Vector2 maxBounds;

    [SerializeField] float paddingLeft;
    [SerializeField] float paddingRight;
    [SerializeField] float paddingTop;
    [SerializeField] float paddingBottom;

    Shooter shooter;// - Shooter 만들기 -

    void Awake()// - Shooter 만들기 -
    {
        shooter = GetComponent<Shooter>();// - Shooter 만들기 -
    }

    void Start()
    {
        InitBounds();
    }

    void Update()
    {
        Move();
        onFire();
    }

    void InitBounds()
    {
        Camera mainCamera = Camera.main;
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));

    }

    // Mathf.Clamp()  최소/최대을 설정하여 flot 값이 범위 이외의 값을 넘지 않도록함
    // direction.Normalize(); : 
    void Move()
    {
        float v = joystick.Vertical; //조이스틱의 수직값을 가져온다.
        float h = joystick.Horizontal; // 수평
        //Vector3 delta = (new Vector3(h, v, 0f) * Time.deltaTime) * moveSpeed;
        Vector2 delta = (new Vector2(h, v) * Time.deltaTime) * moveSpeed;
        Vector2 newPos = new Vector2(0f, 0f);
        newPos.x = Mathf.Clamp(transform.position.x + delta.x, minBounds.x + paddingLeft, maxBounds.x - paddingRight);
        newPos.y = Mathf.Clamp(transform.position.y + delta.y, minBounds.y + paddingBottom, maxBounds.y - paddingTop);

        transform.position = newPos;
    }

    void onFire()// - Shooter 만들기 -
    {
        if (shooter != null)// - Shooter 만들기 -
        {
            shooter.isFiring = joystick.GetPointerDown();// - Shooter 만들기 -
        }
    }
}
