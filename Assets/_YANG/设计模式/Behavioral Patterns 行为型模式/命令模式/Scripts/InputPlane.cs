using UnityEngine;

namespace Yang.DesignPattern.Command
{
    public class InputPlane : MonoBehaviour
    {
        public Transform cubePrefab;

        private RaycastHit _hitInfo;
        private Camera _mainCam;
        private CommandInvoker _invoker;

        private void Awake()
        {
            _mainCam = Camera.main;
            _invoker = GetComponent<CommandInvoker>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = _mainCam.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out _hitInfo, Mathf.Infinity))
                {
                    // 创建一条关于生成物体的命令，并由命令调用器追踪
                    ICommand command = new PlaceCubeCommand(cubePrefab, _hitInfo.point, Random.Range(0, 9));
                    _invoker.AddCommand(command);
                }
            }
        }
    }
}