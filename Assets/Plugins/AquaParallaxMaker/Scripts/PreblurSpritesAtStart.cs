using UnityEngine;
using System.Threading;
using System;

namespace Mkey
{
    public enum ExtendType { Replicate, Symmetric, CirCular, Value }
    public enum ColorComponent { R, G, B, A }
    public enum SizeOptions { Full, Same }
    public enum FilterMod { Corr, Conv }
    public class PreblurSpritesAtStart : MonoBehaviour
    {
        [SerializeField]
        private float blur;
        [SerializeField]
        private bool useThreading;

        private int radius;
        private int KernSize;
        private SpriteRenderer[] sprites;
        private int width;
        private int height;
        public float[] gWeights;
        Color empty = new Color(0, 0, 0, 0);
        private static bool blured = false; 

        void Start()
        {
            if (!blured) // to avoid bluring by duplicating
            {
                sprites = GetComponentsInChildren<SpriteRenderer>();
                radius = (int)(blur);
                CreateGaussBlurKernel(blur, radius);

                if (useThreading)
                {
                    Thread[] tt = new Thread[sprites.Length];
                    BlurTexture[] bTs = new BlurTexture[sprites.Length];
                    Measure("Multi threads blur ", () =>
                    {
                        for (int i = 0; i < sprites.Length; i++)
                        {
                            if (sprites[i].sprite)
                            {
                                Texture2D t2d = sprites[i].sprite.texture;
                                bTs[i] = new BlurTexture(t2d, gWeights);
                                tt[i] = bTs[i].BlurThread();
                            }
                        }

                        bool isAlive = true;
                        while (isAlive) // wait all threads
                    {
                            isAlive = false;
                            for (int i = 0; i < tt.Length; i++)
                            {
                                if (tt[i].IsAlive)
                                {
                                    Thread.Sleep(200);
                                    isAlive = true;
                                }
                            }
                        }

                        for (int i = 0; i < sprites.Length; i++)
                        {
                            if (sprites[i].sprite)
                            {
                                sprites[i].sprite = bTs[i].GetSprite();
                            }
                        }
                    });
                }
                else
                {
                    Measure("Single thread blur ", () =>
                    {
                        for (int i = 0; i < sprites.Length; i++)
                        {
                            if (sprites[i].sprite)
                            {
                                Texture2D t2d = sprites[i].sprite.texture;
                                BlurTexture bT = new BlurTexture(t2d, gWeights);
                                bT.Blur();
                                sprites[i].sprite = bT.GetSprite();
                            }
                        }
                    });
                }

                blured = true;
            }
        }

        private void CreateGaussBlurKernel(float sigma, int KernRadius)
        {
            int cpk;
            float sqr2si;
            float kernel_sum;
            KernSize = KernRadius * 2 + 1;
            if (sigma < 0.001f) sigma = 0.001f;
            sqr2si = -1.0f / (2.0f * sigma * sigma);
            kernel_sum = 0.0f;

            if (gWeights == null || gWeights.Length != KernSize) gWeights = new float[KernSize];

            for (int c = -KernRadius; c <= 0; c++)
            {
                cpk = c + KernRadius;
                gWeights[cpk] = Mathf.Exp(((c * c)) * sqr2si);
                gWeights[KernRadius - c] = gWeights[cpk];
                kernel_sum += gWeights[cpk];
            }

            kernel_sum = 2.0f * (kernel_sum - gWeights[KernRadius]) + gWeights[KernRadius];
            kernel_sum = 1.0f / kernel_sum;

            for (int c = 0; c < gWeights.Length; c++)
            {
                gWeights[c] *= kernel_sum;
            }
        }

        /// <summary>
        /// Measure execute time
        /// </summary>
        /// <param name="message"></param>
        /// <param name="measProc"></param>
        public static void Measure(string message, Action measProc)
        {
            System.Diagnostics.Stopwatch stopWatch = new System.Diagnostics.Stopwatch();//https://msdn.microsoft.com/ru-ru/library/system.diagnostics.stopwatch%28v=vs.110%29.aspx
            stopWatch.Start();
            if (measProc != null) { measProc(); }
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:0}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
            UnityEngine.Debug.Log(message + "- elapsed time, ms: " + ts.TotalMilliseconds);
        }
    }


    public class BlurTexture
    {
        int width;
        int height;
        int radius;
        float[] gWeights;
        Color empty = new Color(0,0,0,0);
        Texture2D source;
        Color[,] s;
        public BlurTexture(Texture2D source, float [] gWeights)
        {
            this.width = source.width;
            height = source.height;
            radius = (gWeights.Length - 1) / 2;
            this.source = source;
            this.gWeights = gWeights;
            s = ToArray(source);
        }

        public Color[,] ImFilterHor(Color[,] colors)
        {
            Color[,] result = new Color[height, width];
            Color col;
            int ePos;
            int fPos;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    col = empty;
                    for (int fX = -radius; fX <= radius; fX++)
                    {
                        ePos = x + fX;
                        fPos = fX + radius;
                        if ((ePos >= 0 && ePos < width))
                            col += colors[y, ePos] * gWeights[fPos];
                    }
                    result[y, x] = col;
                }
            }
            return result;
        }

        public Color[,] ImFilterVer(Color[,] colors)
        {
            Color[,] result = new Color[height, width];
            Color col;
            int ePos;
            int fPos;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    col = empty;
                    for (int fY = -radius; fY <= radius; fY++)
                    {
                        ePos = y + fY;
                        fPos = fY + radius;
                        if ((ePos >= 0 && ePos < height))
                            col += colors[ePos, x] * gWeights[fPos];
                    }
                    result[y, x] = col;
                }
            }
            return result;
        }

        public Color[,] ToArray(Texture2D sourceT2D)
        {
            Color[,] res = new Color[height, width];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    res[y, x] = sourceT2D.GetPixel(x, y);
                }
            }
            return res;
        }

        public Texture2D CreateTexture(Color[,] colors)
        {
            Texture2D resultT2D = new Texture2D(width, height);
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Color c = colors[y, x];
                    resultT2D.SetPixel(x, y, c);
                }
            }
            resultT2D.Apply();
            return resultT2D;
        }

        /// <summary>
        /// Newsize  newWidth = width + 2 * dw; newHeight = height + 2 * dh;
        /// </summary>
        /// <param name="dw"></param>
        /// <param name="dh"></param>
        /// <param name="eType"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public Color[,] Extend(Color[,] colors, int dw, int dh, ExtendType eType, Color val)
        {
            int width = colors.GetLength(1);
            int height = colors.GetLength(0);
            int newWidth = width + 2 * dw;
            int newHeight = height + 2 * dh;
            int heightMinusOne = height - 1;
            int widthMinusOne = width - 1;

            Color botLeft = colors[0, 0];
            Color botRight = colors[0, width - 1];
            Color topRight = colors[height - 1, width - 1];
            Color topLeft = colors[height - 1, 0];

            int xs = 0;
            int ys = 0;

            int dlx = 0;

            int dby = 0;

            Color[,] result = new Color[newHeight, newWidth];

            switch (eType)
            {
                case ExtendType.Replicate:
                    for (int y = 0; y < newHeight; y++)
                    {
                        for (int x = 0; x < newWidth; x++)
                        {
                            if (y < dh)
                            {
                                if (x < dw)
                                {
                                    result[y, x] = botLeft;
                                }

                                else if (x >= dw && x < dw + width)
                                {
                                    result[y, x] = colors[0, x - dw];
                                }

                                else
                                {
                                    result[y, x] = botRight;
                                }
                            }

                            else if (y >= dh && y < dh + height)
                            {
                                if (x < dw)
                                {
                                    result[y, x] = colors[y - dh, 0];
                                }

                                else if (x >= dw && x < dw + width)
                                {
                                    result[y, x] = colors[y - dh, x - dw];
                                }

                                else
                                {
                                    result[y, x] = colors[y - dh, widthMinusOne];
                                }
                            }

                            else // y >= dh+height
                            {
                                if (x < dw)
                                {
                                    result[y, x] = topLeft;
                                }

                                else if (x >= dw && x < dw + width)
                                {
                                    result[y, x] = colors[heightMinusOne, x - dw];
                                }

                                else
                                {
                                    result[y, x] = topRight;
                                }
                            }
                        }
                    }
                    break;
                case ExtendType.Symmetric:

                    int restX = ((dw % width) > 0) ? 1 : 0;
                    int restY = ((dh % height) > 0) ? 1 : 0;
                    if (dw == 0 || dw == width) dlx = 0;
                    else dlx = width - dw % width;

                    if (dh == 0 || dh == height) dby = 0;
                    else dby = height - dh % height;

                    bool reverseX0 = (((dw / width) + restX) % 2 == 1);
                    bool reverseY = (((dh / height) + restY) % 2 == 1);
                    ys = dby;

                    for (int y = 0; y < newHeight; y++)
                    {
                        xs = dlx;
                        bool reverseX = reverseX0;
                        for (int x = 0; x < newWidth; x++)
                        {
                            result[y, x] = colors[(reverseY) ? heightMinusOne - ys : ys, (reverseX) ? widthMinusOne - xs : xs];
                            xs++;
                            if (xs > widthMinusOne)
                            {
                                xs = 0;
                                reverseX = !reverseX;
                            }
                        }
                        ys++;
                        if (ys > heightMinusOne)
                        {
                            ys = 0;
                            reverseY = !reverseY;
                        }
                    }
                    break;

                case ExtendType.CirCular:
                    if (dw == 0 || dw == width) dlx = 0;
                    else dlx = width - dw % width;

                    if (dh == 0 || dh == height) dby = 0;
                    else dby = height - dh % height;

                    ys = dby;

                    for (int y = 0; y < newHeight; y++)
                    {
                        xs = dlx;
                        for (int x = 0; x < newWidth; x++)
                        {
                            result[y, x] = colors[ys, xs];
                            xs++;
                            if (xs > widthMinusOne)
                            {
                                xs = 0;
                            }
                        }
                        ys++;
                        if (ys > heightMinusOne)
                        {
                            ys = 0;
                        }
                    }
                    break;
                case ExtendType.Value:
                    for (int y = 0; y < newHeight; y++)
                    {
                        for (int x = 0; x < newWidth; x++)
                        {
                            if (y >= dh && y < dh + height)
                            {
                                if (x >= dw && x < dw + width)
                                {
                                    result[y, x] = colors[y - dh, x - dw];
                                }

                                else
                                {
                                    result[y, x] = val;
                                }
                            }

                            else
                            {
                                result[y, x] = val;
                            }
                        }
                    }
                    break;
            }

            return result;
        }

        Texture2D nT;
        Color[,] v;
        public Sprite GetSprite()
        {
            Texture2D nT = CreateTexture(v);
            return Sprite.Create(nT, new Rect(0, 0, nT.width, nT.height), new Vector2(0.5f, 0.5f), 100f);
        }

        public Thread BlurThread()
        {
            Thread t = new Thread(new ThreadStart(() =>
            {
                Blur();
            }));

            t.Start();
            return t;
        }

        public void Blur()
        {
            Color[,] h = ImFilterHor(s);
            v = ImFilterVer(h);
        }
    }
}


