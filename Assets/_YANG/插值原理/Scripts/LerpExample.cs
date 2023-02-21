using System.Collections;
using UnityEngine;

namespace Yang.LerpPrinciple
{
    public class LerpExample : MonoBehaviour
    {
        public Transform tran;

        [Header("Lerp")] private float timer;
        private float x;

        [Header("AnimationCurve")] public AnimationCurve curve;
        private float currentTime;

        private IEnumerator _moveCor;

        private void Update()
        {
            // x坐标在3秒内从0递增到10
            if (Input.GetKeyDown(KeyCode.Q)) MoveLerp(0, 10, 3);

            if (Input.GetKeyDown(KeyCode.W)) MoveAnimation(10, 0, 3);

            if (Input.GetKeyDown(KeyCode.Space)) MoveEasing(0, 10, 3);
        }

        private void MoveLerp(float originalX, float targetX, float duration)
        {
            IEnumerator MoveCor()
            {
                timer = 0;
                while (timer < duration)
                {
                    float t = timer / duration;
                    x = LerpUnclamped(originalX, targetX, t);
                    timer += Time.deltaTime;

                    tran.position = new Vector3(x, 0, 0);
                    yield return null;
                }

                // 达到时间后，由于计算精度的问题，将目标点位置单独设置
                tran.position = new Vector3(targetX, 0, 0);
            }

            Replay(MoveCor());
        }

        private void MoveAnimation(float originalX, float targetX, float duration)
        {
            IEnumerator MoveCor()
            {
                timer = 0;
                while (timer < duration)
                {
                    currentTime = timer / duration;
                    currentTime = curve.Evaluate(currentTime);
                    x = LerpUnclamped(originalX, targetX, currentTime);
                    timer += Time.deltaTime;

                    tran.position = new Vector3(x, 0, 0);
                    yield return null;
                }

                tran.position = new Vector3(targetX, 0, 0);
            }

            Replay(MoveCor());
        }

        private void MoveEasing(float originalX, float targetX, float duration)
        {
            IEnumerator MoveCor()
            {
                timer = 0;
                while (timer < duration)
                {
                    float t = Easing.Bounce.EaseOut(timer, duration);
                    x = LerpUnclamped(originalX, targetX, t);
                    timer += Time.deltaTime;

                    tran.position = new Vector3(x, 0, 0);
                    yield return null;
                }

                tran.position = new Vector3(targetX, 0, 0);
            }

            Replay(MoveCor());
        }

        private void Replay(IEnumerator newCor)
        {
            if (_moveCor != null) StopCoroutine(_moveCor);

            _moveCor = newCor;
            StartCoroutine(_moveCor);
        }


        // Lerp 的Unity实现源码
        private static float LerpUnclamped(float a, float b, float t)
        {
            return a + (b - a) * t;
        }
    }
}