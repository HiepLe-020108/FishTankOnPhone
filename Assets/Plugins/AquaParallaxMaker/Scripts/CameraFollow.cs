using UnityEngine;

namespace Mkey
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField]
        private  float speed = 1f;

        private Transform player;
        private Camera m_camera;
        private float targetX;
        private Vector3 position;

        #region regular
        void Awake()
        {
            GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
            if (playerGO) player = playerGO.transform;
            m_camera = GetComponent<Camera>();
        }

        void LateUpdate()
        {
            position = transform.position;
            if (!player)
            {
                return;
            }
            targetX = Mathf.Lerp(position.x, player.position.x, speed * Time.deltaTime);
            transform.position = new Vector3(targetX, position.y, position.z);
        }
        #endregion regular 
    }
}