using UnityEngine;

namespace Project.Scripts.Wall {
    public class PlaceForWall : MonoBehaviour {
        [Range(0, 1f)] [SerializeField] private float radius = 0.01f;
        [SerializeField] private int y;
        [SerializeField] private int x;
        [SerializeField] private GameObject horizontal;
        [SerializeField] private GameObject vertical;

        // public GameObject GetHorizontal => horizontal;
        // public GameObject GetVertical => vertical;

        public int GetY => y;
        public int GetX => x;

        public void ActivateHorizontalWall() => JobsOnMainThread.Worker.AddJob(() => horizontal.SetActive(true));

        public void ActivateVerticalWall() => JobsOnMainThread.Worker.AddJob(() => vertical.SetActive(true));

        public void DeactivateHorizontalWall() => JobsOnMainThread.Worker.AddJob(() => horizontal.SetActive(false));

        public void DeactivateVerticalWall() => JobsOnMainThread.Worker.AddJob(() => vertical.SetActive(false));

        private void OnDrawGizmos() {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(transform.position, radius);
        }
    }
}