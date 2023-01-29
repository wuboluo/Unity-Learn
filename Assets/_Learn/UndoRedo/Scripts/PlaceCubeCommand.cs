using UnityEngine;

public class PlaceCubeCommand : ICommand
{
    private readonly Transform _cube;
    private readonly Vector3 _position;
    private readonly int _id;

    public PlaceCubeCommand(Transform cube, Vector3 position, int id)
    {
        _cube = cube;
        _position = position;
        _id = id;
    }

    // 重做，前进命令
    public void Redo()
    {
        CubePlacer.PlaceCube(_cube, _position, _id);
    }

    // 撤销，回退命令
    public void Undo()
    {
        CubePlacer.RemoveCube(_position, _id);
    }
}