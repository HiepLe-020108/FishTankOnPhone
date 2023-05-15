using System.Collections;
using UnityEngine;

namespace Mkey
{
    public class LightController : MonoBehaviour
    {
        [SerializeField]
        private BeamController[] beams;
        private bool cancel = false;
        WaitForSeconds wfs0_02;

        void Start()
        {
            wfs0_02 = new WaitForSeconds(0.02f);
            beams = GetComponentsInChildren<BeamController>();
            if (beams != null)
            {
                StartCoroutine(Flashing());
            }
        }

        IEnumerator Flashing()
        {
            while (!cancel)
            {
                int beamIndex = Random.Range(0, beams.Length);
                float duration = Random.Range(3, 10);

                if (GetFlashedCount() < 2)
                {
                  beams[beamIndex].FlashBeam(beamIndex, duration, null);
                }
                yield return wfs0_02;
            }
        }

        private int GetFlashedCount()
        {
            int res = 0;
            for (int i = 0; i < beams.Length; i++)
            {
                if (beams[i].IsFlashing) res++;
            }
            return res;
        }

        private void Cancel()
        {
            cancel = true;
            StopCoroutine(Flashing());
        }

        private void OnDisable()
        {
            Cancel();
        }

        private void OnDestroy()
        {
            Cancel();
        }
    }
}