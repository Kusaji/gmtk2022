using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KinematicCharacterController;
using KinematicCharacterController.Examples;

public class PlayerComponents : MonoBehaviour
{
    [Header("Components")]
    public PlayerAnimationController animationController;
    public PlayerHealth health;
    public KinematicCharacterMotor motor;
    public PlayerUI ui;
    public PlayerAttackController attackController;
    public PlayerAudioController audioController;

}
