using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void StopAnimation(string paramName)
    {
        _animator.SetBool(paramName, false);
    }
}
