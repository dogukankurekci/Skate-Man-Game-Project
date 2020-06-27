using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkateMovement : MonoBehaviour
{

    CapsuleCollider capsuleCollider;
    Rigidbody m_Rigidbody;

    public Animator animator;
    public GameObject[] Particle;


    public GameObject skateboard;
    public GameObject Man;

    private GameObject bringCamera;
    private CameraFollow cameraFollow;

    public float skateSpeed = 17f;
    Vector3 movement;
    float moveHorizontal;

    bool finish = false;
    bool isGround;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        bringCamera = Camera.main.gameObject;

        cameraFollow = bringCamera.GetComponent<CameraFollow>();

        isGround = true;

    }

    void Update()
    {
        movement = new Vector3(0, 0, moveHorizontal);

        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                moveHorizontal = 1f;
                animator.SetBool("start", true);
            }

            if (touch.phase == TouchPhase.Ended)
            {
                StartCoroutine(delay());
            }

        }

        if (Input.GetKey(KeyCode.W))
        {
            moveHorizontal = 1f;
            animator.SetBool("start", true);
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            StartCoroutine(delay());
        }

        Finished();
        Failed();


    }

    IEnumerator delay()
    {
        yield return new WaitForSeconds(.1f);

        moveHorizontal = 0f;

        animator.SetBool("start", false);

    }

    private void FixedUpdate()
    {
        moveCharacter(movement);
    }

    void moveCharacter(Vector3 direction)
    {
        m_Rigidbody.MovePosition((Vector3)transform.position + (direction * skateSpeed * Time.deltaTime));
    }


    private void OnTriggerEnter(Collider other)
    {
        //Extra speed
        if(other.tag == "DriveHills_SpeedUp")
        {
            skateSpeed = 27f;
            Particle[2].SetActive(true);
        }

        if (other.tag == "DriveHills_Fly")
        {
            skateSpeed = 20f;
            animator.SetBool("start", false);

            Particle[2].SetActive(true);
        }

        if (other.tag == "DriveHills_Ground")
        {
            isGround = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "DriveHills_SpeedUp")
        {
            skateSpeed = 17f;
            Particle[2].SetActive(false);
        }
        if (other.tag == "DriveHills_Fly")
        {
            skateSpeed = 20f;
            Particle[2].SetActive(false);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "DriveHills_Finish")
        {
            finish = true;
        }


    }

    void Finished()
    {
        if(finish == true)
        {
            cameraFollow.offset = new Vector3(0.1f, 1.3f, -20f);

            skateSpeed = 0f;
            m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

            animator.SetBool("finish", true);
            skateboard.SetActive(false);
            capsuleCollider.enabled = true;


            Particle[0].SetActive(true);
            Particle[1].SetActive(true);

            Man.transform.rotation = Quaternion.Euler(0, 180, 0);

            StartCoroutine(finish_delay());
        }
    }
    void Failed()
    {
        if (transform.rotation.x > 0.7f || transform.rotation.x < -0.7f)
        {
            isGround = false;
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        if (isGround == false)
        {
            cameraFollow.offset = new Vector3(0f, 0f, 1f);

            skateSpeed = 0f;
            capsuleCollider.enabled = false;
            Particle[0].SetActive(true);
            animator.SetBool("fall", true);
            Man.transform.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);

            StartCoroutine(failed_delay());
        }

    }
    IEnumerator finish_delay()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    IEnumerator failed_delay()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
