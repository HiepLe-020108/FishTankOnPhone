using System;
using UnityEngine;

namespace Mkey
{
    public class BeamController : MonoBehaviour
    {
        private SpriteRenderer beam;
        private Color sourceColor;
        private Vector3 sourceScale;
        public bool IsFlashing { get; private set; }
        private static readonly Color transparent = new Color(1, 1, 1, 0);

        void Start()
        {
            beam = GetComponent<SpriteRenderer>();
            sourceColor = beam.color;
            IsFlashing = false;
            sourceScale = transform.localScale;
        }

        public void FlashBeam(int i, float duration, Action completeCallBack)
        {
            if (IsFlashing)
            {
                if (completeCallBack != null) completeCallBack();
                return;
            }
            IsFlashing = true;

            float fadeK = 0.2f; // part of duration time
            float fadeTime = fadeK * duration;
            float sumLightDuration = duration + fadeTime + fadeTime;
            float deltaAlpha = sourceColor.a / fadeTime;
            float deltaScale = sourceScale.x / fadeTime;
            float dTime = 0;

            // complete tween fadeout - duration - fadein
            SimpleTween.Value(gameObject, 0, sumLightDuration, sumLightDuration).SetOnUpdate(
                (float val) =>
                {
                    if (val <= fadeTime) // fadeout
                    {
                        dTime = fadeTime - val;
                        beam.color = new Color(sourceColor.r, sourceColor.g, sourceColor.b, deltaAlpha * (dTime)); //sourceColor.a - val * deltaAlpha
                        beam.transform.localScale = new Vector3(deltaScale, deltaScale, deltaScale) * (dTime);
                    }
                    else if (val >= duration + fadeTime) // fadein
                    {
                        dTime = val - duration - fadeTime;
                        beam.color = new Color(sourceColor.r, sourceColor.g, sourceColor.b, dTime * deltaAlpha);
                        beam.transform.localScale = new Vector3(deltaScale, deltaScale, deltaScale) * dTime;
                    }
                    else // disable object
                    {
                        beam.color = transparent;
                        beam.transform.localScale = Vector3.zero;
                    }
                }).AddCompleteCallBack(() =>
                {
                    beam.color = sourceColor;
                    beam.transform.localScale = sourceScale;
                    IsFlashing = false;
                    if (completeCallBack != null) completeCallBack();
                });
        }

        private void Cancel()
        {
            SimpleTween.Cancel(gameObject, true);
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