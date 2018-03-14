using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fractal
{
    class HSB
    { 
    public float rChan, gChan, bChan;
    public HSB()
    {
        rChan = gChan = bChan = 0;
    }
    public void fromHSB(float h, float s, float b)
    {
        float red = b;
        float green = b;
        float blue = b;
        if (s != 0)
        {
            float Max = b;
            float dif = b * s / 255f;
            float Min = b - dif;

            float h2 = h * 360f / 255f;

            if (h2 < 60f)
            {
                red = Max;
                green = h2 * dif / 60f + Min;
                blue = Min;
            }
            else if (h2 < 120f)
            {
                red = -(h2 - 120f) * dif / 60f + Min;
                green = Max;
                blue = Min;
            }
            else if (h2 < 180f)
            {
                red = Min;
                green = Max;
                blue = (h2 - 120f) * dif / 60f + Min;
            }
            else if (h2 < 240f)
            {
                red = Min;
                green = -(h2 - 240f) * dif / 60f + Min;
                blue = Max;
            }
            else if (h2 < 300f)
            {
                red = (h2 - 240f) * dif / 60f + Min;
                green = Min;
                blue = Max;
            }
            else if (h2 <= 360f)
            {
                red = Max;
                green = Min;
                blue = -(h2 - 360f) * dif / 60 + Min;
            }
            else
            {
                red = 0;
                green = 0;
                blue = 0;
            }
        }

        rChan =(float) Math.Round(Math.Min(Math.Max(red, 0f), 255));
        gChan = (float)Math.Round(Math.Min(Math.Max(green, 0), 255));
        bChan = (float)Math.Round(Math.Min(Math.Max(blue, 0), 255));

    }
}
}