using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NDA.BoolUtil;
using NDA.FloatUtil;
using NDA.KeyUtil;
using UnityEngine.Experimental;
using XInputDotNetPure;

//Controls classes relating to player input
namespace NDA.PlayerInput
{
    //Base class for all input controllers
    public abstract class InputController
    {
        public enum ControllerType
        {
            XboxOne,
            Keyboard
        };

        public bool canVibrate = true;
        public bool cameraLockToggle;
        
        public float deadZone;
        public float forward;
        public float back;
        public float left;
        public float right;
        public float roll;
        public float sprint;
        public float camLockToggle;
        public float block;
        public float lightAttack;
        public float heavyAttack;
        public float raiseCamera;
        public float lowerCamera;
        public float rotateCamera;
        public float rotateCameraVertical;
        //ryan
        public float jump;

        public float deadZoneDown;
        public float forwardDown;
        public float backDown;
        public float leftDown;
        public float rightDown;
        public float rollDown;
        public float sprintDown;
        public float camLockToggleDown;
        public float blockDown;
        public float lightAttackDown;
        public float heavyAttackDown;
        public float healDown;
        //ryan
        public float jumpDown;

        //Runs every frame
        public virtual void Update() { }
    }

    //Controls and control updates for the keyboard and mouse
    public class KeyboardInputController : InputController
    {
        public KeyCode inputForward = KeyCode.W;
        public KeyCode inputBack = KeyCode.S;
        public KeyCode inputRight = KeyCode.D;
        public KeyCode inputLeft = KeyCode.A;
        public KeyCode inputRoll = KeyCode.Space;
        public KeyCode inputSprint = KeyCode.F;
        //public KeyCode inputBlock = KeyCode.Mouse1;
        public KeyCode inputLightAttack = KeyCode.Mouse0;
        public KeyCode inputHeavyAttack = KeyCode.Mouse2;
        public KeyCode inputHeal = KeyCode.F;
        public KeyCode inputCameraLock = KeyCode.Q;

        //Runs every frame
        public override void Update()
        {
            canVibrate = false;

            forward = BoolCasting.ToFloat(Input.GetKey(inputForward));
            back = BoolCasting.ToFloat(Input.GetKey(inputBack));
            left = BoolCasting.ToFloat(Input.GetKey(inputLeft));
            right = BoolCasting.ToFloat(Input.GetKey(inputRight));
            roll = BoolCasting.ToFloat(Input.GetKey(inputRoll));
            sprint = BoolCasting.ToFloat(Input.GetKey(inputSprint));
            lightAttack = BoolCasting.ToFloat(Input.GetKey(inputLightAttack));
            //block = BoolCasting.ToFloat(Input.GetKey(inputBlock));
            raiseCamera = FloatMath.GetAmountAboveZero(Input.GetAxis("Vertical"));
            lowerCamera = FloatMath.GetAmountBelowZero(Input.GetAxis("Vertical"));
            rotateCamera = Input.GetAxis("Horizontal");
            cameraLockToggle = Input.GetKeyDown(inputCameraLock);

            forwardDown = BoolCasting.ToFloat(Input.GetKeyDown(inputForward));
            backDown = BoolCasting.ToFloat(Input.GetKeyDown(inputBack));
            leftDown = BoolCasting.ToFloat(Input.GetKeyDown(inputLeft));
            rightDown = BoolCasting.ToFloat(Input.GetKeyDown(inputRight));
            rollDown = BoolCasting.ToFloat(Input.GetKeyDown(inputRoll));
            sprintDown = BoolCasting.ToFloat(Input.GetKeyDown(inputSprint));
            lightAttackDown = BoolCasting.ToFloat(Input.GetKeyDown(inputLightAttack));
            //blockDown = BoolCasting.ToFloat(Input.GetKeyDown(inputBlock));
            healDown = BoolCasting.ToFloat(Input.GetKeyDown(inputHeal));
        }
    }
    
    //Controls and control updates for the Xbox One controller
    public class XboxOneInputController : InputController
    { 
        public KeyCode inputBack = KeyCode.S;
        public KeyCode inputRight = KeyCode.D;
        public KeyCode inputLeft = KeyCode.A;
        public KeyCode inputRoll = KeyCode.LeftShift;
        public KeyCode inputSprint = KeyCode.Space;
        public KeyCode inputCamLockToggle = KeyCode.Tab;
        public KeyCode inputBlock = KeyCode.Mouse1;
        public KeyCode inputLightAttack = KeyCode.Mouse0;
        public KeyCode inputHeavyAttack = KeyCode.Mouse2;
        //ryan
        public KeyCode inputJump = KeyCode.V;

        //Runs every frame
        public override void Update()
        {
            forward = FloatMath.GetAmountAboveZero(Input.GetAxis("Vertical"));
            back = FloatMath.GetAmountBelowZero(Input.GetAxis("Vertical"));
            left = FloatMath.GetAmountBelowZero(Input.GetAxis("Horizontal"));
            right = FloatMath.GetAmountAboveZero(Input.GetAxis("Horizontal"));
            roll = Input.GetAxis("Fire2");
            sprint = BoolCasting.ToFloat(Input.GetButton("Joystick 4"));
            raiseCamera = FloatMath.GetAmountBelowZero(Input.GetAxis("Right Stick Vertical"));
            lowerCamera = FloatMath.GetAmountAboveZero(Input.GetAxis("Right Stick Vertical"));
            rotateCamera = Input.GetAxis("Right Stick Horizontal");
            rotateCameraVertical = Input.GetAxis("Right Stick Vertical");
            cameraLockToggle = Input.GetKeyDown(KeyCode.JoystickButton3);
            block = Input.GetAxis("Left Trigger");
            
            rollDown = BoolCasting.ToFloat(Input.GetButtonDown("Fire2"));
            lightAttackDown = BoolCasting.ToFloat(Input.GetButtonDown("Fire3")); 
            healDown = BoolCasting.ToFloat(Input.GetButtonDown("Joystick 5"));
            heavyAttackDown = Input.GetAxis("Right Trigger");
            
            //ryan
            jumpDown = BoolCasting.ToFloat(Input.GetButtonDown("Fire1"));
        }
    }
}