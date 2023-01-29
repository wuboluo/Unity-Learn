using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CommandInvoker : MonoBehaviour
{
    // 命令缓存队列，记录被添加的命令，需要按照添加顺序依次执行命令，即先进先出
    private static Queue<ICommand> _commandBuffer;
    private static List<ICommand> _commandHistory;

    // 当前处于第几条命令状态
    [SerializeField] private int currentCommandIndex;

    private void Awake()
    {
        _commandBuffer = new Queue<ICommand>();
        _commandHistory = new List<ICommand>();
    }

    private void Update()
    {
        // 命令缓存池中为排好队等待执行的命令，当存在时，依次执行，并添加历史命令记录和命令数量
        if (_commandBuffer.Count > 0)
        {
            // 执行一条新的命令
            ICommand c = _commandBuffer.Dequeue();
            c?.Redo();

            _commandHistory.Add(c);
            currentCommandIndex++;
            Debug.Log($"当前命令序号：{currentCommandIndex}");
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                Debug.Log($"<color=red>按下Z：当前命令序号：{currentCommandIndex}</color>");
                // 若当前还有存在的命令（理解为不为初始状态），则可以撤销命令，即撤销
                if (currentCommandIndex > 0)
                {
                    currentCommandIndex--;
                    _commandHistory[currentCommandIndex].Undo();
                }
            }
            else if (Input.GetKeyDown(KeyCode.Y))
            {
                Debug.Log($"<color=green>按下Y：当前命令序号：{currentCommandIndex}, 历史总命令数：{_commandHistory.Count}</color>");
                // 若当前命令不为最后一条命令（理解为不是最新状态），则可以前进至下一条命令，即执行
                if (currentCommandIndex < _commandHistory.Count)
                {
                    _commandHistory[currentCommandIndex].Redo();
                    currentCommandIndex++;
                }
            }
        }
    }

    public void AddCommand(ICommand command)
    {
        // >currentCommandIndex意味着插入新的命令时，处于保存的命令集合之间。那么相当于此条新命令开辟了新的道路，舍弃以此为界限之后保存的所有命令
        while (_commandHistory.Count > currentCommandIndex)
        {
            _commandHistory.RemoveAt(currentCommandIndex);
        }
        
        // 将此条新命令添加至待执行队列
        _commandBuffer.Enqueue(command);
    }
}