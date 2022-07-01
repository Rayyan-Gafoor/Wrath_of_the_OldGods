using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class playerController : MonoBehaviour
{
    [Header("Movement")]
    public float move_speed;
    public float ground_drag;
    public float air_drag;
    public float ground_mass;
    public float air_mass;
    public float jump_force;
    public float jump_cooldown;
    public float air_multiply;
    bool can_jump;
    //movement states
    public float walk_speed;
    public float run_speed;
    public movement_state player_state;
    public enum movement_state
    {
        walking,
        sprinting,
        crouching,
        air
    }
    [Header("Crouching")]
    public bool is_crouching;
    public float crouch_speed;
    public float crouch_scale;
    public float start_scale;

    [Header("KeyBindings")]
    public KeyCode jump_key = KeyCode.Space;
    public KeyCode sprint_key = KeyCode.LeftShift;
    public KeyCode crouch_key = KeyCode.LeftControl;

    [Header("Ground Check")]
    public float player_height;
    public LayerMask ground_mask;
    public bool is_grounded;
    public Transform ground_check;
    public float ground_dis = 0.4f;

    [Header("Slope Check")]
    public float max_angle;
    RaycastHit slope_hit;
    bool exit_slope;

    [Header("Inputs")]
    float hori_input;
    float vert_input;
    Vector3 move_dir;
    Rigidbody rb;
    public Transform orientation;
    [Header("Gravity Controls")]
    public float ground_gravity;
    public float air_gravity;
    public float jump_gravity;
    public float gravity_pull;
    public float gravity_active_time;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
       
        rb.freezeRotation = true;
        can_jump = true;
        start_scale = transform.localScale.y;
        is_crouching = false;
    }
    private void Update()
    {
        
        //is_grounded = Physics.Raycast(transform.position, Vector3.down, player_height * 0.5f + 0.3f, ground);
        is_grounded = Physics.CheckSphere(ground_check.position, ground_dis, ground_mask);
        player_input();
        speed_control();
        state_handler();
        if (is_grounded)
        {
            rb.drag = ground_drag;
            rb.mass = ground_mass;
        }
        else
        {
            rb.drag = air_drag;
            rb.mass = air_mass;
        }
    }
    private void FixedUpdate()
    {
        move_player();
    }
    void player_input()
    {
        hori_input = Input.GetAxisRaw("Horizontal");
        vert_input = Input.GetAxisRaw("Vertical");
        if(Input.GetKey(jump_key) && can_jump && is_grounded)
        {
            can_jump = false;
            jump();
            Invoke(nameof(reset_jump), jump_cooldown);
        }
        if (Input.GetKeyDown(crouch_key))
        {
            transform.localScale = new Vector3(transform.localScale.x, crouch_scale, transform.localScale.z);
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
            is_crouching = true;
        }
        if (Input.GetKeyUp(crouch_key))
        {
            transform.localScale = new Vector3(transform.localScale.x, start_scale, transform.localScale.z);
            is_crouching = false;
        }
    }
    void move_player()
    {
        move_dir = orientation.forward * vert_input + orientation.right * hori_input;
       
        if (on_slope() && !exit_slope)
        {
            rb.AddForce(get_slope_dir() * move_speed * 20, ForceMode.Force);
        }
        if (is_grounded)
        {
            rb.AddForce(move_dir.normalized * move_speed * 10f, ForceMode.Force);
        }
        else if (!is_grounded)
        {
            rb.AddForce(move_dir.normalized * move_speed * 10f * air_multiply, ForceMode.Force);
            if (rb.velocity.y > 0)
            {
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
            }
        }
        rb.useGravity = !on_slope();
    }
    void speed_control()
    {
        if (on_slope())
        {
            if(rb.velocity.magnitude> move_speed)
            {
                rb.velocity = rb.velocity.normalized * move_speed;            }
        }
        Vector3 flat_vel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if (flat_vel.magnitude > move_speed)
        {
            Vector3 limit_vel = flat_vel.normalized * move_speed;
            rb.velocity = new Vector3(limit_vel.x, rb.velocity.y, limit_vel.z);
        }
    }
    void jump()
    {
        exit_slope = true;
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        Physics.gravity = new Vector3(0, -(jump_gravity), 0);
        rb.AddForce(transform.up * jump_force, ForceMode.Impulse);
        StartCoroutine(pull_force());
    }
    void reset_jump()
    {
        can_jump = true;
        exit_slope = false;
    }
    void state_handler()
    {
        
        if(is_grounded && Input.GetKey(sprint_key) && !is_crouching)
        {
            player_state = movement_state.sprinting;
            move_speed = run_speed;
            Physics.gravity = new Vector3(0, -(ground_gravity), 0);

        }
        else if (is_grounded && !is_crouching)
        {
            player_state = movement_state.walking;
            move_speed = walk_speed;
            Physics.gravity = new Vector3(0, -(ground_gravity), 0);
        }
        else if (Input.GetKeyDown(crouch_key) )
        {
            player_state = movement_state.crouching;
            move_speed = crouch_speed;
            Physics.gravity = new Vector3(0, -(ground_gravity), 0);

        }
        else if(!is_crouching && !is_grounded)
        {
            player_state = movement_state.air;
            Physics.gravity = new Vector3(0, -(air_gravity), 0);

        }
    }
    bool on_slope()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out slope_hit, player_height*0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slope_hit.normal);
            return angle < max_angle && angle != 0;
        }
        else
        {
            return false;
        }
    }
    Vector3 get_slope_dir()
    {
        return Vector3.ProjectOnPlane(move_dir, slope_hit.normal).normalized;
    }
    IEnumerator pull_force()
    {
        yield return new WaitForSeconds(gravity_active_time);
        Debug.Log("gravity pull called");
        rb.AddForce(Vector3.down * gravity_pull, ForceMode.Force);
    }

}
