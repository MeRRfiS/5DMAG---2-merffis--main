using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform _targetZone;

    [Header("Main Parameters")]
    [SerializeField] private EnemyType _enemyType;

    [Header("Components")]
    [SerializeField] private Animator _animator;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Slider _hpBar;

    [Header("Body parts")]
    [SerializeField] private GameObject _rightHandAtack;
    [SerializeField] private GameObject _leftHandAtack;

    private bool _playerIsDetected;
    private float _distanceToPlayer;
    private bool _screamed;
    private GameObject _player;

    public bool IsDead { get; private set; }

    public EnemyModel EnemyModel { get; private set; }

    private void Awake()
    {
        switch (_enemyType)
        {
            case EnemyType.Zombe:
                EnemyModel = new ZombiModel();
                break;
        }
    }

    private void Start()
    {
        _player = PlayerController.Instance.gameObject;
        _playerIsDetected = false;
        IsDead = false;
        _screamed = false;

        _hpBar.maxValue = EnemyModel.Health;
        _hpBar.value = EnemyModel.Health;

        DisableDamageColliders();
    }




    private void Update()
    {
        if (!IsDead)
        {
            //_hpBar.transform.LookAt(Camera.main.transform.position);
            if (_player != null)
            {
                _distanceToPlayer = Vector3.Distance(gameObject.transform.position, _player.transform.position);

                if (_distanceToPlayer < EnemyModel.ChaseDistance)
                {
                    _animator.SetBool("idle", false);
                    _animator.SetBool("run", false);
                    _animator.SetBool("scream", true);
                    _animator.SetBool("atack1", false);
                    _animator.SetBool("kicking", false);
                    _animator.SetBool("punch", false);
                    _animator.SetBool("death", false);
                    _animator.SetBool("hit", false);

                    if (_playerIsDetected)
                    {
                        FightLogic();
                    }
                    
                }
                else if (_distanceToPlayer > EnemyModel.ChaseDistance)
                {
                    IdelingLogic();
                }
            }
        }
    }


    private void IdelingLogic()
    {
        _animator.SetBool("idle", true); //t
        _animator.SetBool("run", false);
        _animator.SetBool("scream", false);
        _animator.SetBool("atack1", false);
        _animator.SetBool("kicking", false);
        _animator.SetBool("punch", false);
        _animator.SetBool("death", false);
        _animator.SetBool("hit", false);
    }
    private void FightLogic()
    {
        if (IsDead) return;

        if (_distanceToPlayer <= _agent.stoppingDistance)
        {
            _animator.SetBool("idle", false);
            _animator.SetBool("run", false);
            _animator.SetBool("scream", false);
            _animator.SetBool("atack1", true); //t
            _animator.SetBool("kicking", false);
            _animator.SetBool("punch", false);
            _animator.SetBool("death", false);
            _animator.SetBool("hit", false);
        }
        if (_distanceToPlayer >= _agent.stoppingDistance)
        {
            _agent.isStopped = false;
            _agent.SetDestination(_player.transform.position);

            _animator.SetBool("idle", false);
            _animator.SetBool("run", true); //t
            _animator.SetBool("scream", false);
            _animator.SetBool("atack1", false);
            _animator.SetBool("kicking", false);
            _animator.SetBool("punch", false);
            _animator.SetBool("death", false);
            _animator.SetBool("hit", false);
        }
    }
    private void DeathLogic()
    {
        if (IsDead) return;

        DisableDamageColliders();
        IsDead = true;
        _agent.isStopped = true;

        _animator.SetBool("idle", false);
        _animator.SetBool("run", false);
        _animator.SetBool("scream", false);
        _animator.SetBool("atack1", false);
        _animator.SetBool("kicking", false);
        _animator.SetBool("punch", false);
        _animator.SetBool("death", true); //t
        _animator.SetBool("hit", false);

        Destroy(_hpBar.gameObject);
        PlayerController.Instance.ClaimMoney(EnemyModel.Reward);
        
    }

    public void BecomeDead()
    {
        GameManager.Instance.CheckFinishRound();
    }

    public void ClaimDamage(int damageToClaim)
    {
        if (IsDead) return;

        _animator.SetBool("hit", true); // idk, this anim do not want to play itself. 
        _animator.SetBool("idle", false);
        _animator.SetBool("run", false);
        _animator.SetBool("scream", false);
        _animator.SetBool("atack1", false);
        _animator.SetBool("kicking", false);
        _animator.SetBool("punch", false);
        _animator.SetBool("death", false);
        EnemyModel.Health -= damageToClaim;
        _hpBar.value = EnemyModel.Health;
        if (EnemyModel.Health <= 0)
        {
            DeathLogic();
        }
    }

    public void ZombieScreamed()
    {
        _playerIsDetected = true;
    }


    public void AnableDamageColliders()
    {
        _rightHandAtack.SetActive(true);
        _leftHandAtack.SetActive(true);
    }

    public void DisableDamageColliders()
    {
        _rightHandAtack.SetActive(false);
        _leftHandAtack.SetActive(false);
    }

    public void ToAttack()
    {
        _agent.isStopped = true;
        RaycastHit[] hist;
        hist = Physics.SphereCastAll(_targetZone.position, 0.5f, _targetZone.position, 0);

        foreach (RaycastHit hit in hist)
        {
            var player = hit.collider.GetComponent<PlayerController>();
            if (player)
            {
                player.ClaimDamage(EnemyModel.Damage);
                break;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(_targetZone.position, _targetZone.position + _targetZone.position * 0);
        Gizmos.DrawWireSphere(_targetZone.position + _targetZone.position * 0, 0.5f);
    }
}
