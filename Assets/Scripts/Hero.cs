using UnityEngine;

public class Hero : Entity
{
    [Header("Hero Settings")]
    [SerializeField] private bool gunMode = false;
    [SerializeField] private float animSpeed = 1f;
    Camera camera;
    Vector3 pos;
    Animator anim;
    Animation heroAnim;

    private Vector3 moveTo;

    void Start()
    {
        camera = FindAnyObjectByType<Camera>();
        pos = camera.WorldToScreenPoint(transform.position);

        anim = FindAnyObjectByType<Animator>();
    }

    void Update()
    {
        moveTo = new Vector3
                (
                transform.position.x + Input.GetAxisRaw("Horizontal")
                , transform.position.y + Input.GetAxisRaw("Vertical")
                , transform.position.z
                );
        HeroMove();

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            gunMode = !gunMode;
            anim.SetBool("GunMode", gunMode);
            if (gunMode)
            {
                animSpeed -= 0.1f;
                anim.speed = anim.speed;
            }
            else
            {
                animSpeed += 0.1f;
                anim.speed = anim.speed;
            }
        }
    }

    private void FixedUpdate()
    {
        Debug.Log("GunMode: " + gunMode);

        if (gunMode)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector3 direction = mousePosition - transform.position;

            float angle = Vector3.Angle(Vector3.up, direction);

            Vector3 cross = Vector3.Cross(Vector3.up, direction);
            if (cross.z < 0)
            {
                angle = 360 - angle;
            }

            if (angle >= 315 || angle < 45)
            {
                anim.SetFloat("LookVertical", 1);

                anim.SetFloat("LookHorizontal", 0);

                if (Input.GetAxisRaw("Horizontal") == 1)
                {

                }
            }
            else if (angle >= 45 && angle < 135)
            {
                anim.SetFloat("LookHorizontal", -1);

                anim.SetFloat("LookVertical", 0);

                if (Input.GetAxisRaw("Vertical") == 1)
                {

                }
            }
            else if (angle >= 135 && angle < 225)
            {
                anim.SetFloat("LookVertical", -1);

                anim.SetFloat("LookHorizontal", 0);

                if (Input.GetAxisRaw("Vertical") == -1)
                {

                }
            }
            else
            {

                anim.SetFloat("LookHorizontal", 1);

                anim.SetFloat("LookVertical", 0);

                if (Input.GetAxisRaw("Horizontal") == -1)
                {
                    
                }
            }

        }
    }

    private void HeroMove()
    {


        if (gunMode)
        {
            anim.SetFloat("HorizontalMove", Input.GetAxisRaw("Horizontal"));
            anim.SetFloat("VerticalMove", Input.GetAxisRaw("Vertical"));

            if (Input.GetAxisRaw("Debug Multiplier") > 0) //Если нажат shift, бежим
            {
                //anim.speed = -animSpeed * 1.5f;
                base.Run(moveTo);
            }
            else
            {
                //anim.speed = -animSpeed;
                base.Walk(moveTo);
            }
        }
        else
        {
            anim.SetFloat("HorizontalMove", Input.GetAxisRaw("Horizontal"));
            anim.SetFloat("VerticalMove", Input.GetAxisRaw("Vertical"));

            if (Input.GetAxisRaw("Debug Multiplier") > 0) //Если нажат shift, бежим
            {
                anim.speed = animSpeed * 1.5f;
                base.Run(moveTo);
            }
            else
            {
                anim.speed = animSpeed;
                base.Walk(moveTo);
            }
        }
    }

    //private void RotateAroundHero()
    //{
    //    var diference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
    //    var rotateZ = Mathf.Atan2(diference.y, diference.x) * Mathf.Rad2Deg;
    //    if (rotateZ % 90 == 0)
    //    {
    //        transform.rotation = Quaternion.Euler(0f, 0f, rotateZ + Offset);
    //    }
    //}


}
