using System.Collections.Generic;

using UnityEngine;


namespace TheIntruder_Remake
{
    public class LightSwitch : MonoBehaviour
    {
        [Header("Values")]
        [SerializeField] List<Color> colorsForSwitch;
        [SerializeField] float timeBetweenSwitch;

        [Header("Own References")]
        [SerializeField] Light light;

        int actualColor;
        float counter;



        void Awake()
        {
            actualColor = 1;
            counter = timeBetweenSwitch;
        }

        void Update()
        {
            counter -= Time.deltaTime;

            if (counter <= 0.0f)
            {
                counter = timeBetweenSwitch;

                if (actualColor < colorsForSwitch.Count)
                {
                    actualColor++;
                }
                else
                {
					actualColor = 1;
				}

                light.color = colorsForSwitch[actualColor - 1];
            }
        }
    }
}
