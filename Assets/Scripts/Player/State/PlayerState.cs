using UnityEngine;
using UnityEngine.InputSystem;

namespace ThreeD {

    public class PlayerState {

        protected Animator _animator;
        protected PlayerInput _playerInput;
        protected PlayerController _playerController;

        public PlayerState(PlayerController playerController, Animator animator, PlayerInput playerInput){
            _animator = animator;
            _playerInput = playerInput;
            _playerController = playerController;
        }

        //카메라 방향을 기점으로 플레이어 회전
        protected void Rotate(float x, float z){
            if (_playerInput.camera){
                var cameraTransform = _playerInput.camera.transform;
                var cameraForward = cameraTransform.forward;
                var cameraRight = cameraTransform.right;

                cameraForward.y = cameraRight.y = 0;

                var moveDirection = cameraForward * z + cameraRight.normalized * x;

                if (moveDirection != Vector3.zero){
                    moveDirection.Normalize();
                    _playerController.transform.rotation = Quaternion.LookRotation(moveDirection);
                }
            }
        }
        
        protected void Jump(InputAction.CallbackContext context){
            _playerController.Jump();
            _playerController.SetState(PlayerController.EPlayerState.Jump);
        }

    }

}