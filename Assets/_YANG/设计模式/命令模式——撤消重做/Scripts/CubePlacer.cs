using System.Collections.Generic;
using UnityEngine;

namespace Yang.DesignMode.Command
{
    // 命令的具体执行内容
    
    public static class CubePlacer
    {
        private static List<Transform> _cubes;

        public static void PlaceCube(Transform obj, Vector3 pos, int id)
        {
            Transform newCube = Object.Instantiate(obj, pos, Quaternion.identity);
            newCube.GetComponent<Cube>().id = id;

            _cubes ??= new List<Transform>();
            _cubes.Add(newCube);
        }

        public static void RemoveCube(Vector3 pos, int id)
        {
            for (int i = 0; i < _cubes.Count; i++)
            {
                Transform item = _cubes[i];
                if (item.position == pos && item.GetComponent<Cube>().id == id)
                {
                    Object.Destroy(item.gameObject);
                    _cubes.RemoveAt(i);
                }
            }
        }
    }
}