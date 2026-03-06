using UnityEngine;
using UnityEngine.InputSystem;

namespace ThreeD {

    public class PlayerState {

        private Animator _animator;
        private PlayerInput _playerInput;
        private PlayerController _playerController;

        public PlayerState(Animator animator, PlayerInput playerInput, PlayerController playerController){
            _animator = animator;
            _playerInput = playerInput;
            _playerController = playerController;
        }

    }

}