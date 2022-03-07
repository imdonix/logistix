using System.Collections;
using UnityEngine;

namespace Scripts
{
    public class FireworkSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject[] Fireworks;
        [SerializeField] private int Count;
        [SerializeField] private float Sway;
        [SerializeField] private float Delay;

        private bool active;

        private void Start() { active = false; }

        #region PUBLIC

        public void Spawn()
        {
            active = true;
            for (int i = 0; i < Count; i++)
                StartCoroutine(Spawner());
        }

        public void Stop()
        {
            active = false;
        }

        #endregion

        private IEnumerator Spawner()
        {
            GameObject firework = null;

            void Delete()
            {
                if (!ReferenceEquals(firework, null)) Destroy(firework);
            }

            while (active)
            {
                Delete();
                firework = Instantiate(GetRandomFirework(), transform.position + GetRandomDelta(), Quaternion.identity);
                yield return new WaitForSeconds(Random.Range(Delay / 2, Delay));
            }
            Delete();
        }

        private GameObject GetRandomFirework()
        {
            return Fireworks[Random.Range(0, Fireworks.Length)];
        }

        private Vector3 GetRandomDelta()
        {
            return new Vector2(Random.Range(-Sway, Sway), Random.Range(-Sway, Sway));
        }

    }
}