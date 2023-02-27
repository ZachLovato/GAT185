using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class CharecterPlayer : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;

    [SerializeField] private Animator animator;
    [SerializeField] private InputRouter inputRouter;

    [SerializeField] private Inventory inventory;

    CharacterController controller;
    Vector3 inputAxis;

    Camera mainCamera;
    Vector3 velocity = Vector3.zero;

    float AirTime = 0;

	void Start()
    {
        controller = GetComponent<CharacterController>();
        mainCamera = Camera.main;

        inputRouter.jumpEvent += OnJump;
        inputRouter.moveEvent += OnMove;
        inputRouter.fireEvent += OnFire;
        inputRouter.fireStopEvent += OnFireStop;
        inputRouter.nextItemEvent += OnNextItem;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = Vector3.zero;

        direction.x = inputAxis.x;
        direction.z = inputAxis.y;

        direction = mainCamera.transform.TransformDirection(direction);


        if (controller.isGrounded) 
        {
            velocity.x = direction.x * playerData.speed;
            velocity.z = direction.z * playerData.speed;
            AirTime = 0;
        }
        else
        {
            velocity.y += playerData.gravity * Time.deltaTime;
            velocity.x = direction.x * playerData.speed * .7f;
            velocity.z = direction.z * playerData.speed * .7f;
            AirTime += Time.deltaTime;
        }

        
        
        controller.Move(velocity * playerData.speed * Time.deltaTime);
        Vector3 look = direction;
        look.y = 0;
        if (look.magnitude > 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(look), playerData.turnRate * Time.deltaTime);
        }

        // set animator param
        animator.SetFloat("VelocityY", controller.velocity.y);
        animator.SetFloat("speed", controller.velocity.magnitude);
        animator.SetFloat("AirTime", AirTime);
		animator.SetBool("IsGrounded", controller.isGrounded);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;

        // no rigidbody
        if (body == null || body.isKinematic)
        {
            return;
        }

        // We dont want to push objects below us
        if (hit.moveDirection.y < -0.3)
        {
            return;
        }

        // Calculate push direction from move direction,
        // we only push objects to the sides never up and down
        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

        // If you know how fast your character is trying to move,
        // then you can also multiply the push velocity by that.

        // Apply the push
        body.velocity = pushDir * playerData.hitForce;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed) 
        {
            Debug.Log("Jump");
        }
    }

    public void OnJump()
    {
        animator.SetTrigger("Jump");
        velocity.y = Mathf.Sqrt(playerData.jumpHeight * -0.8f * playerData.gravity);
    }

    public void OnFire()
    {
        //animator.SetTrigger("Attack");
        inventory.Use();
    }

    public void OnFireStop()
    {
		inventory.StopUse();
    }
    public void OnNextItem()
    {
        inventory.EquipNextItem();

	}

    public void OnMove(Vector2 axis)
    {
        inputAxis = axis;
    }

    public void OnAnimEventItemUse()
    {
        inventory.OnAnimEventItemUse();
    }

    public void OnLeftFootSpawn(GameObject go)
    {
        Transform bone = animator.GetBoneTransform(HumanBodyBones.LeftFoot);
        Instantiate(go, bone.position, bone.rotation);
    }
	public void OnRightFootSpawn(GameObject go)
	{
		Transform bone = animator.GetBoneTransform(HumanBodyBones.RightFoot);
		Instantiate(go, bone.position, bone.rotation);
	}
}
