using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region publicvariables
    [SerializeField] CharacterController playerController;
    [SerializeField] float speed, gravity = -12 * 2, jump = 5f, groundDistace = 1.0f, runningSpeed = 10f, stamina = 100;
    [SerializeField] float turnSmoothTime = 0.1f;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundMask;
    [SerializeField] bool isGrounded;
    //[SerializeField] Transform cam;

    #endregion

    #region privatevariables 
    bool isRunning;
    float turnSmoothVel;

    Vector3 velY;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        isRunning = Input.GetKey(KeyCode.LeftShift);


        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistace, groundMask);

        if (isGrounded && velY.y < 0)
        {
            velY.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");


        Vector3 move = transform.right * x + transform.forward * z;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velY.y = Mathf.Sqrt(jump * -2f * gravity);

        }
        if (isRunning && stamina > 0)
        {
            playerController.Move(move * runningSpeed * Time.deltaTime);
            stamina -= 3f;
        }
        else
        {
            playerController.Move(move * runningSpeed * Time.deltaTime);
            if (stamina < 100)
            {
                stamina += 1f;
            }
            if (stamina > 100)
            {
                stamina = 100;
            }
        }

        //agregados
        Vector3 direction = new Vector3(x, 0, z).normalized;
        velY.y += gravity * Time.deltaTime;

        //staminaText.text = stamina.ToString();
        playerController.Move(move * speed * Time.deltaTime);
        velY.y += gravity * Time.deltaTime;
        playerController.Move(velY * Time.deltaTime);

        /*//rotacion jugador
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVel, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            playerController.Move(moveDir.normalized * speed * Time.deltaTime);
        }*/
    }
}
