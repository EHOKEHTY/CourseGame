using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private Animator _anim;
    [SerializeField] private Hero _hero;

    void Update()
    {
        Vector3 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        float angle = Vector3.Angle(Vector3.up, direction);

        Vector3 cross = Vector3.Cross(Vector3.up, direction);

        if (_hero.GunMode == true)
        {
            if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
            {
                if (cross.z < 0)
                {
                    angle = 360 - angle;
                }

                if (angle >= 315 || angle < 45)
                {
                    _anim.Play("Hero_GunMode_Up");
                }
                else if (angle >= 45 && angle < 135)
                {
                    _anim.Play("Hero_GunMode_Left");
                }
                else if (angle >= 135 && angle < 225)
                {
                    _anim.Play("Hero_GunMode_Down");
                }
                else
                {
                    _anim.Play("Hero_GunMode_Right");
                }
            }
            else
            {
                if (cross.z < 0)
                {
                    angle = 360 - angle;
                }

                if (angle >= 315 || angle < 45)
                {
                    _anim.Play("Hero_GunMode_Walk_Up");
                }
                else if (angle >= 45 && angle < 135)
                {
                    _anim.Play("Hero_GunMode_Walk_Left");
                }
                else if (angle >= 135 && angle < 225)
                {
                    _anim.Play("Hero_GunMode_Walk_Down");
                }
                else
                {
                    _anim.Play("Hero_GunMode_Walk_Right");
                }
            }
        }
        else
        {
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                _anim.Play("Hero_Walk_Right");
            }
            else if (Input.GetAxisRaw("Horizontal") < 0)
            {
                _anim.Play("Hero_Walk_Left");
            }
            else if (Input.GetAxisRaw("Vertical") > 0)
            {
                _anim.Play("Hero_Walk_Up");
            }
            else if (Input.GetAxisRaw("Vertical") < 0)
            {
                _anim.Play("Hero_Walk_Down");
            }
            else
            {
                _anim.Play("Hero_Idle");
            }
        }
    }

    void GunModeLooking()
    {
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            _anim.Play("Hero_GunMode_Walk_Left");
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            _anim.Play("Hero_GunMode_Walk_Right");
        }
        else if (Input.GetAxisRaw("Vertical") > 0)
        {
            _anim.Play("Hero_GunMode_Walk_Down");
        }
        else if (Input.GetAxisRaw("Vertical") < 0)
        {
            _anim.Play("Hero_GunMode_Walk_Up");
        }
        else
        {
            _anim.Play("Hero_Idle");
        }
    }
}
