using System;
using UnityEngine;

namespace Mkey
{
    public class FishController : MonoBehaviour
    {
        private int baseOrder = 0;
        [SerializeField]
        private float speed = 0.1f;
        [SerializeField]
        private ParticleSystem pS;
        private Animator animator;
        private TwoPassTimer twoPassTimer; // simple timer for randomized lip animatin
        private ParticleSystem.EmissionModule eM;
        private SpriteRenderer spriteRenderer;
        private PlayerController player;
        
        private bool IsVisible // true if object visible in any camera
        {
            get { return (spriteRenderer && spriteRenderer.isVisible); }
        }

        private float timeToChange = 0;

        [SerializeField]
        private float timeToChangeInit = 10;
        [SerializeField]
        private float distX; // distance to player
        float distXold;
        [SerializeField]
        bool movingInOneDir; // moving in one direction with player
        [SerializeField]
        bool convergence;  // convergence with player
        [SerializeField]
        private float offsetX; // offset from player 


        #region regular
        void Start()
        {
            animator = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>(); 
            twoPassTimer = new TwoPassTimer(5,3, SetLipEat, SetLipIdle);
            pS = GetComponent<ParticleSystem>();
            eM = pS.emission;
            SetLipIdle();
            GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
            if(playerGO) player = playerGO.GetComponent<PlayerController>();
            timeToChangeInit = UnityEngine.Random.Range(timeToChangeInit, timeToChangeInit*3f);

        }

        void Update()
        {
            twoPassTimer.Update(Time.deltaTime); // update timer

            if (twoPassTimer.IsTimePassed)      // animate lip
            {
                twoPassTimer = new TwoPassTimer(UnityEngine.Random.Range(1,3), UnityEngine.Random.Range(4, 5), SetLipEat, SetLipIdle);
            }

            if (!player) return;


            // try to moving in visible zone
            movingInOneDir = Mathf.Sign(player.Speed) == Mathf.Sign(speed);
            offsetX = player.transform.position.x - transform.position.x;
            distX = Mathf.Abs(offsetX);
            convergence = distX < distXold;

            if (IsVisible)
            {
                timeToChange = 0;
            }
            else
            {
                timeToChange += Time.deltaTime;
                if (timeToChange > timeToChangeInit)
                {
                    if (movingInOneDir && !convergence) // player move in the same direction as fish
                    {
                        float absSpeed = Mathf.Abs(speed) + Mathf.Sign(offsetX) * UnityEngine.Random.Range(0.2f, 0.7f); // increase fish speed or decrease
                        absSpeed = (absSpeed < 0) ? 0.1f : absSpeed;                                                    // avoid negative speed
                        speed = Mathf.Sign(speed) * absSpeed;
                        timeToChange = 0;

                        if (offsetX < 0 && UnityEngine.Random.Range(1, 10) < 3)                                         // sometimes turn back
                        {
                            speed = -speed;
                        }
                    }
                    else if (!movingInOneDir && !convergence)                   // turn back
                    {
                            speed = -speed;
                    }
                }
            }
            Flip();
            distXold = distX;
            transform.Translate(new Vector3(Time.deltaTime * speed, 0, 0), Space.World);
        }
        #endregion regular

        private void SetLipIdle()
        {
            animator.SetFloat("lip_speed", 0);
            if (pS )
            {
                eM.rateOverTime = 0;
            }
        }

        private void SetLipEat()
        {
            animator.SetFloat("lip_speed", 1);
            if (pS)
            {
                eM.rateOverTime = 5;
            }
        }

        private void Flip()
        {
            transform.localEulerAngles = (speed>0)? new Vector3(0,0,0) : new Vector3(0,180,0);
        }
    }

    public class TwoPassTimer
    {
        float first;
        float second;
        bool pause = false;
        bool isFirstPassed = false;
        bool isSecondPassed = false;
        float passedTime = 0;
        Action firstCallback;
        Action secondCallBack;

        public bool IsTimePassed
        {
            get { return isFirstPassed && isSecondPassed; }
        }

        public TwoPassTimer(float first, float second, Action firstCallback, Action secondCallBack)
        {
            this.first = first;
            this.second = second;
            passedTime = 0;
            pause = false;
            this.firstCallback = firstCallback;
            this.secondCallBack = secondCallBack;
            isFirstPassed = false;
            isSecondPassed = false;

        }

        /// <summary>
        /// for timer update set Time.deltaTime
        /// </summary>
        /// <param name="time"></param>
        public void Update(float deltaTime)
        {
            if (pause) return;
            if (isFirstPassed && isSecondPassed) return;

            passedTime += deltaTime;
            if(!isFirstPassed && passedTime > first)
            {
                isFirstPassed = true;
                if (firstCallback != null) firstCallback();
            }

            if (!isSecondPassed && passedTime > (first + second))
            {
                isSecondPassed = true;
                if (secondCallBack != null) secondCallBack();
            }
        }
    }
}