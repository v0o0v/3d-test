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

    }

}