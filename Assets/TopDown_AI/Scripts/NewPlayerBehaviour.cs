using UnityEngine;


public class NewPlayerBehavior : MonoBehaviour
{
    public float moveSpeed = 10.0f;
    public Transform hitTestPivot, gunPivot;
    public GameObject mousePointer, projectilePrefab;
    public Animator animator;
    
    private Rigidbody myRigidBody;
    private int hashSpeed;
    private float attackTime = 0.4f;
    private PlayerWeaponType currentWeapon = PlayerWeaponType.NULL;
    private float attackCooldown = 0.1f;
    private float attackTimer = 0f;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
        hashSpeed = Animator.StringToHash("Speed");
        SetWeapon(PlayerWeaponType.PISTOL);
    }

    void Update()
    {
        HandleMovement();
        HandleWeaponSwitch();
        HandleAttack();
        UpdateAim();
    }

    private void HandleMovement()
    {
        float inputHorizontal = Input.GetAxis("Horizontal");
        float inputVertical = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(inputVertical * moveSpeed, 0.0f, -inputHorizontal * moveSpeed);
        myRigidBody.linearVelocity = moveDirection;

        animator.SetFloat(hashSpeed, myRigidBody.linearVelocity.magnitude);
    }

    private void HandleWeaponSwitch()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SetWeapon(PlayerWeaponType.KNIFE);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            SetWeapon(PlayerWeaponType.PISTOL);
    }

    private void HandleAttack()
    {
        attackTimer -= Time.deltaTime;

        if (attackTimer <= 0)
        {
            if (currentWeapon == PlayerWeaponType.KNIFE && Input.GetMouseButton(0))
            {
                Attack();
            }
            else if (currentWeapon == PlayerWeaponType.PISTOL && Input.GetMouseButtonDown(0))
            {
                Attack();
            }
        }
    }

    private void Attack()
    {
        attackTimer = attackCooldown;

        if (currentWeapon == PlayerWeaponType.KNIFE)
        {
            Invoke("DoHitTest", 0.2f);
        }
        else if (currentWeapon == PlayerWeaponType.PISTOL)
        {
            ShootProjectile();
        }

        animator.SetBool("Attack", true);
        Invoke("ResetAttackAnimation", attackTime);
    }

    private void ShootProjectile()
    {
        GameObject bullet = Instantiate(projectilePrefab, gunPivot.position, gunPivot.rotation);
        bullet.transform.LookAt(mousePointer.transform);
        bullet.transform.Rotate(0, Random.Range(-7.5f, 7.5f), 0);
        AlertEnemies();
    }

    private void DoHitTest()
    {
        RaycastHit[] hits = Physics.SphereCastAll(hitTestPivot.position, 2.0f, hitTestPivot.up);
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                hit.collider.GetComponent<NPC_Enemy>().Damage();
            }
        }
    }

    private void AlertEnemies()
    {
        RaycastHit[] hits = Physics.SphereCastAll(hitTestPivot.position, 20.0f, hitTestPivot.up);
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                hit.collider.GetComponent<NPC_Enemy>().SetAlertPos(transform.position);
            }
        }
    }

    private void UpdateAim()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.y = transform.position.y;
        mousePointer.transform.position = mousePos;

        Vector3 direction = mousePos - transform.position;
        float angle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, -angle, 0);
    }

    private void SetWeapon(PlayerWeaponType weaponType)
    {
        if (weaponType != currentWeapon)
        {
            currentWeapon = weaponType;
            animator.SetTrigger("WeaponChange");

            if (weaponType == PlayerWeaponType.KNIFE)
            {
                attackTime = 0.4f;
                animator.SetInteger("WeaponType", 0);
            }
            else if (weaponType == PlayerWeaponType.PISTOL)
            {
                attackTime = 0.1f;
                animator.SetInteger("WeaponType", 3);
            }
        }
    }

    private void ResetAttackAnimation()
    {
        animator.SetBool("Attack", false);
    }
}
