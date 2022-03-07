using UnityEngine;

namespace Logistix.Core
{
    public class Crane : MonoBehaviour
    {
        private const float CRANE_SIZE = 2.75f;
        private const float ANCHOR_DIVIDER = 4;
        private const float DISTANCE_DIVIDER = 2;
        private const float MAX_BOX_EXCURSION = 15;


        [Header("Properties")]
        [SerializeField] private float Speed;


        private float Excursion;
        private bool CanMove;
        private int Direction;
        private Box Attached;
        private SpringJoint2D RopeJoin;
        private LineRenderer RopeRenderer;

        #region UNITY

        void Awake()
        {
            Attached = null;
            CanMove = true;
            Direction = 1;
            RopeJoin = GetComponent<SpringJoint2D>();
            RopeRenderer = GetComponent<LineRenderer>();
        }

        void FixedUpdate()
        {
            Move();
            UpdateRopeRenderer();
            //DoSuspension();
        }

        #endregion

        #region PUBLIC

        public void SetExcursion(float ex)
        {
            Excursion = ex;
        }

        public void SetPosition(float shipSize)
        {
            transform.position = new Vector3(0, shipSize + CRANE_SIZE);
        }


        public void Attach(Box box)
        {
            Attached = box;
            SetSpawnPoint();
            RopeJoin.distance = GetDistanceFromCrane();
            RopeJoin.connectedBody = Attached.GetRigidbody();
            RopeJoin.connectedAnchor = GetAnchorPosition();
        }

        public Box Release()
        {
            Box tmp = Attached;

            RopeJoin.connectedBody = null;
            Attached = null;
            return tmp;
        }

        /// <summary>
        /// Return the attached box.
        /// </summary>
        /// <returns>return true is there is a attached box</returns>
        public bool GetBox(out Box box)
        {
            box = Attached;
            return !ReferenceEquals(box, null);
        }

        #endregion

        private void Move()
        {
            float pos = transform.position.x;

            if (CanMove)
            {
                if (Mathf.Abs(pos) > Excursion - (CRANE_SIZE / 6))
                    Direction *= -1;

                transform.position += Vector3.right * Direction * Speed * Time.deltaTime;
            }
        }

        private Vector2 GetAnchorPosition()
        {
            float height = Attached.GetSize().y;
            return new Vector2(0, height / 2);
        }

        private float GetDistanceFromCrane()
        {
            return Mathf.Clamp(Attached.GetSize().y / DISTANCE_DIVIDER, 0.75f, 3);
        }

        private void SetSpawnPoint()
        {
            Attached.transform.position = transform.position + (Vector3.down * GetDistanceFromCrane() + Vector3.down * CRANE_SIZE / 2);
        }

        private void UpdateRopeRenderer()
        {
            RopeRenderer.enabled = !ReferenceEquals(Attached, null);

            if (RopeRenderer.enabled)
            {
                Vector3 craneAnchoredPos = transform.position + (Vector3)RopeJoin.anchor;
                Vector3 boxAnchoredPos = Attached.transform.position + Attached.transform.rotation * (Vector3)RopeJoin.connectedAnchor;
                RopeRenderer.widthCurve = AnimationCurve.Constant(0, 1, 0.1f);
                RopeRenderer.SetPositions(new Vector3[] { craneAnchoredPos, boxAnchoredPos });
            }
        }

        private void DoSuspension()
        {
            float rotation = Attached.transform.rotation.eulerAngles.z;
            if (RopeRenderer.enabled)
                Attached.transform.rotation = Quaternion.Euler(0, 0, Mathf.Clamp(rotation, -MAX_BOX_EXCURSION, MAX_BOX_EXCURSION));
        }
    }
}