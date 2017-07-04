using UnityEngine;

namespace UnityStandardAssets.Utility
{
    [RequireComponent(typeof (TextMesh))]
    public class FPSCounterVR : MonoBehaviour
    {
        private TextMesh m_Text;
        private MeshRenderer m_Renderer;

        [SerializeField][Tooltip("Interval at which FPS is recalculated. Higher value means take longer time to update.")]
        private float frameRateUpdateInterval = 0.5f;
        public float frameRate { get { return lastFPSValue; } }
        private float lastFrameRateUpdateTime = 0;
        private int lastFrameIndex = 0;
        private float lastFPSValue = 0;
        private Color32 color = new Color32(15,231,61,255);
        private void Start()
        {
            m_Text = GetComponent<TextMesh>();
            m_Renderer = GetComponent<MeshRenderer> ();
        }

        private void Update()
        {
            UpdateFramerate();
            UpdateFPSColor ();

            m_Text.text = string.Format("{0:0.0} fps", frameRate);
            m_Text.color = color;
            
            if (Input.GetKeyDown (KeyCode.F))
            {
                m_Renderer.enabled = !m_Renderer.enabled;
            }
        }

        /// OVRInspector FPS Counter
        void UpdateFramerate()
        {
            if (Time.unscaledTime > lastFrameRateUpdateTime + frameRateUpdateInterval)
            {
                lastFPSValue = (Time.frameCount - lastFrameIndex) / frameRateUpdateInterval;
                lastFrameIndex = Time.frameCount;
                lastFrameRateUpdateTime = Time.unscaledTime;
            }
        }

        void UpdateFPSColor ()
        {
            // PC Max FPS: 90 , Gear VR Max FPS: 60
            if (frameRate > 55f)
                color = new Color32(15,231,61,255);
            if (frameRate < 55f && frameRate > 50f)
                color = new Color32(232,231,61,255);
            if (frameRate < 50f && frameRate > 45f)
                color = new Color32(232,231,61,255);
            if (frameRate < 45f && frameRate > 40f)
                color = new Color32(232,130,61,255);
            if (frameRate < 40f)
                color = new Color32(232,30,15,255);
        }
    }
}
