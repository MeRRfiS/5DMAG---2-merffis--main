using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.Image;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform _targetZone;

    [Header("Stats")]
    public PlayerStat Statistics;

    [Header("Components")]
    [SerializeField] private Animator _animator;
    [SerializeField] private CharacterController _chController;

    private bool _forward;
    private float _velocityDown;
    private Vector2 _inputMovement;
    private Vector3 _direction;
    private WeaponModel _weapon;

    private static PlayerController _instance;

    public static PlayerController Instance { get { return _instance; } }

    public bool IsCanMoving { private get; set; }

    private void Awake()
    {
        _instance = this;
        _weapon = new SwordModel();

        Statistics.SetDefValue();
    }

    private void Start()
    {
        _forward = true;
        _direction = Vector3.zero;

        //IF SOMEONE FUCKING TAKES vSYNC OUT OF HERE -BITCH I'LL RIP OFF YOUR FUCKING HANDS !!!!!
        QualitySettings.vSyncCount = 1;
        //DO NOT TOUCH !!
    }

    private void Update()
    {
        ApplyGravity();
        ApplyMovement();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_targetZone.position, _targetZone.position + _targetZone.position * 0);
        Gizmos.DrawWireSphere(_targetZone.position + _targetZone.position * 0, 1);
    }

    public void ChangeMovement(Vector2 movement)
    {
        if (!IsCanMoving) return;

        _inputMovement = movement;
    }

    public WeaponModel GetWeapon()
    {
        return _weapon;
    }

    private void ApplyGravity()
    {
        if (!IsCanMoving) return;

        if (_chController.isGrounded && _velocityDown < 0.0f)
        {
            _velocityDown = -1.0f;
        }
        else
        {
            _velocityDown += Physics.gravity.y * Statistics.Speed * Time.deltaTime;
        }

        _direction.y = _velocityDown;
    }

    private void ApplyMovement()
    {
        if (!IsCanMoving) return;

        _direction.z = _inputMovement.x;

        _chController.Move(_direction * Statistics.Speed * Time.deltaTime);

        _animator.SetFloat("speed", _inputMovement.x);
        if (_inputMovement.x > 0)
        {
            if (!_forward)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                _forward = true;
            }
            
        }
        else if (_inputMovement.x < 0)
        {
            if (_forward)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                _forward = false;
            }
            
        }
    }

    public void ToAttack()
    {
        if (!IsCanMoving) return;

        PlayAttackAnimation();

        RaycastHit[] hist;
        hist = Physics.SphereCastAll(_targetZone.position, 1f, _targetZone.position, 0);

        foreach (RaycastHit hit in hist)
        {
            var enemy = hit.collider.GetComponent<EnemyController>();
            if (enemy)
            {
                enemy.ClaimDamage(_weapon.Damage + Statistics.AdditionalDamage);
                break;
            }
        }
    }

    public void ClaimDamage(int damage)
    {
        Statistics.Health -= damage;
        UIController.Instance.UpdateHPUI(Statistics.Health);
    }

    public void ClaimMoney(int reward)
    {
        Statistics.Money += (int)(reward * Statistics.MoneyMultipler);
        UIController.Instance.UpdateCoinText(Statistics.Money);
    }

    private void PlayAttackAnimation()
    {
        if (_inputMovement.x == 0)
        {
            _animator.SetBool("atack_1", true);
            _animator.SetBool("atack_2", false);
        }
        else if (_inputMovement.x != 0)
        {
            _animator.SetBool("atack_1", false);
            _animator.SetBool("atack_2", true);
        }
    }
}
