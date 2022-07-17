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
    public PlayerAudioController audioController;
    public PlayerAbilities abilities;
    public ExampleCharacterController character;

    [Header("Weapons")]
    public PlayerWeaponSwitcher weaponSwitcher;
    public WeaponController basicBlaster;
    public WeaponController revolverBlaster;

}
