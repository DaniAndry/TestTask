using System;
using UnityEngine;

namespace Door
{
    [RequireComponent(typeof(Animator))]
    public class Door : MonoBehaviour
    {
        [SerializeField] private DoorScanner scanner;

        private Animator _animator;
        private static int OpenHash = Animator.StringToHash("Open");
        private static int ClosedHash = Animator.StringToHash("Close");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            scanner.OnOpen += Open;
            scanner.OnClose += Close;
        }

        private void OnDisable()
        {
            scanner.OnOpen -= Open;
            scanner.OnClose -= Close;
        }

        private void Open()
        {
            _animator.Play(OpenHash);
        }

        private void Close()
        {
            _animator.Play(ClosedHash);
        }
    }
}