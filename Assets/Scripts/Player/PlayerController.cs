using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DynamicInstancer;

public enum PlayerForm
{
    Being,
    Ghost,
    Returning
}

public class PlayerController : MonoBehaviour
{
    public PlayerForm PlayerForm = PlayerForm.Being;
    
    public List<AudioClip> _sounds = new();
    
    public Rigidbody _rigidbody;
    private Collider _collider;
    private Transform _modelHolder;
    private GameObject _body;
    private GameObject _ghost;
    private Animator _beingAnimator;
    private Animator _ghostAnimator;
    [SerializeField] private GameObject _dyingBody;
    [SerializeField] private Material _ghostMaterial;
    
    private Vector3 _movement;

    public float gravity = 100f;
    public float ghostLastingTime = 3f;
    public float speed = 5f;
    public float returnSpeed = 20f;
    public float ghostSpeedMultiplier = 3f;
    public float linearModelSmoothing = 20f;
    public float angularModelSmoothing = 20f;
    
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _modelHolder = transform.GetChild(0);
        _body = transform.GetChild(0).transform.GetChild(0).gameObject;
        _ghost = transform.GetChild(0).transform.GetChild(1).gameObject;
        
        _beingAnimator = _body.transform.GetChild(0).GetComponent<Animator>();
        _ghostAnimator = _ghost.transform.GetChild(0).GetComponent<Animator>();
        
        _body.SetActive(true);
        _ghost.SetActive(false);
    }

    void Update()
    {
        FormChangeInput();
        ManageAnimationStates();
        RotateBody();
    }

    private bool debugTime = false;
    private Animator deadAnim;
    private void FormChangeInput()
    {
        if (debugTime)
        {
            ghostLastingTime -= Time.deltaTime;
        }
        
        if (ghostLastingTime <= 0)
        {
            ghostLastingTime = 5f;
            debugTime = false;
        }

        if (PlayerForm != PlayerForm.Ghost && PlayerForm != PlayerForm.Returning)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                StartCoroutine(KillSelf());
                debugTime = true;
            }   
        }

        if (ghostLastingTime <= 2f)
        {
            deadAnim.SetBool("alive", true);
            ghostC.a = Mathf.Lerp(ghostC.a, 0f, Time.deltaTime * 2f);
        }
        else if (ghostLastingTime > 2f && PlayerForm != PlayerForm.Being && PlayerForm != PlayerForm.Returning) ghostC.a = Mathf.Lerp(ghostC.a, 1f, Time.deltaTime * 2f);
        else ghostC.a = Mathf.Lerp(ghostC.a, 0f, Time.deltaTime * 2f);
        
        _ghostMaterial.color = ghostC;
        ghostC.a = Mathf.Clamp(ghostC.a, 0, 1);
        
        if (ghostS != null) ghostS.transform.position = transform.position;
    }
    
    private Color ghostC = Color.white;
    private AudioSource ghostS;
    private IEnumerator KillSelf()
    {
        // become ghost

        ghostC.a = 0f;
        _ghostMaterial.color = ghostC;

        ghostS = InstanceAudioSource(_sounds[0], transform.position, 0.4f, 1f, 0f, 0f);
        ghostS.Play();
        
        PlayerForm = PlayerForm.Ghost;
        _body.SetActive(false);
        var deadBody = Instantiate(_dyingBody, transform.position, _modelHolder.transform.rotation);
        deadBody.transform.GetChild(0).localPosition = new Vector3(0, -1.1f, 0);
        
        deadBody.SetActive(true);
        deadBody.tag = "DeadBody";
        deadBody.layer = LayerMask.NameToLayer("Ignore Raycast");
        
        var col = deadBody.AddComponent<SphereCollider>();
        var rb = deadBody.AddComponent<Rigidbody>();
        var anim =  deadBody.transform.GetChild(0).GetChild(0).GetComponent<Animator>();
        deadAnim = anim;

        rb.isKinematic = true;
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeAll;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        
        col.radius = 2f;
        col.isTrigger = true;

        LayerMask curr = _collider.excludeLayers;
        
        int lr = LayerMask.NameToLayer("GhostPhase");
        int lrMask = 1 << lr;
        
        _collider.excludeLayers = curr | lrMask;
        _rigidbody.useGravity = false;

        speed *= ghostSpeedMultiplier;
        
        _ghost.SetActive(true);
        
        yield return new WaitForSeconds(ghostLastingTime);

        // returning to body
        
        PlayerForm = PlayerForm.Returning;

        while (Vector3.Distance(transform.position, deadBody.transform.position) > 0.1f)
        {
            transform.position = Vector3.Lerp(
                transform.position,
                deadBody.transform.position,
                Time.deltaTime * returnSpeed
            );
            
            deadBody.transform.forward = Vector3.Lerp(deadBody.transform.forward, _modelHolder.transform.forward, Time.deltaTime * 10f);
            
            yield return null; // wait for the next frame
        }
        
        yield return new WaitUntil(() => Vector3.Distance(transform.position, deadBody.transform.position) <= 0.1f);
        
        // reincarnated
         
        PlayerForm = PlayerForm.Being;
        _body.SetActive(true);
        _ghost.SetActive(false);

        _collider.excludeLayers = curr & ~lrMask;
        _rigidbody.useGravity = true;
        
        speed /= ghostSpeedMultiplier;

        deadAnim = null;
        Destroy(ghostS);
        ghostS = null;
        Destroy(deadBody);
    }

    private float x;
    private float z;
    private float angleX;
    private float angleZ;
    private void RotateBody()
    {
        if (Move().magnitude > 0.05f)
        {
            x = Mathf.Lerp(x, Move().x, Time.deltaTime * angularModelSmoothing);
            z = Mathf.Lerp(z, Move().z, Time.deltaTime * angularModelSmoothing);
            
            angleX = Mathf.LerpAngle(angleX, x, Time.deltaTime * angularModelSmoothing);
            angleZ = Mathf.LerpAngle(angleZ, z, Time.deltaTime * angularModelSmoothing);
            
            Vector3 direction = new Vector3(angleX, 0, angleZ);
            Vector3 dir = Vector3.Slerp(_modelHolder.forward, direction, Time.deltaTime * linearModelSmoothing);

            _modelHolder.forward = dir;
        }
    }

    private Vector3 direction;
    private void ManageAnimationStates()
    {
        var dir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        
        if (PlayerForm == PlayerForm.Being)
        {
            if (dir.magnitude >= 0.1f) _beingAnimator.SetBool("hasMovement", true);
            else _beingAnimator.SetBool("hasMovement", false);
        }
    }
    
    /// <summary>
    /// Camera Relative Movement / Returns the players input position
    /// </summary>
    /// <returns></returns>
    private Vector3 Move()
    {
        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.z = Input.GetAxisRaw("Vertical");

        var forward = Vector3.ProjectOnPlane(Camera.main.transform.forward, Camera.main.transform.up);
        var right = Vector3.ProjectOnPlane(Camera.main.transform.right, Camera.main.transform.up);

        forward.Normalize();
        right.Normalize();

        forward.y = 0;
        right.y = 0;

        var fin = right * _movement.x + forward * _movement.z;

        return fin.normalized;
    }

    private void FixedUpdate()
    {
        ApplyPhysics();
    }

    private void ApplyPhysics()
    {
        if (PlayerForm == PlayerForm.Returning) return;
        
        if (PlayerForm == PlayerForm.Being)
        {
            _rigidbody.AddForceAtPosition(Vector3.down * gravity, _rigidbody.transform.position, ForceMode.Force);
        }
        
        _rigidbody.AddForceAtPosition(Move() * (Time.fixedDeltaTime * speed), _rigidbody.transform.position, ForceMode.VelocityChange);
    }
}
