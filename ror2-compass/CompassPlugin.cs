using System;
using BepInEx;
using BepInEx.Configuration;
using RoR2;
using UnityEngine;
using RoR2.UI;
using TMPro;

namespace CompassPlugin
{
    [BepInDependency("com.bepis.r2api")]
    [BepInPlugin("io.atflac.ror2.compassplugin", "CompassPlugin", "0.0.1")]
    public class CompassPlugin : BaseUnityPlugin
    {
        public const string Guid = "io.atflac.ror2.compassplugin";

        private CompassUI ui;
        private CharacterBody currBody;

        private ConfigEntry<int> x;
        private ConfigEntry<int> y;
        private ConfigEntry<int> fontSize;

        public CompassPlugin() : base()
        {
            this.ui = new CompassUI();
        }

        void Awake()
        {

            this.x = Config.Bind("Position",
                  "CompassPositionX",
                        50,
                        "X position of the compass.");

            this.y = Config.Bind("Position",
                "CompassPositionY",
                8,
                "Y position of the compass.");

            this.fontSize = Config.Bind("Font",
                "FontSize",
                18,
                "Font size of the compass text.");

        }

        float radToDeg(float rad) { return rad * (180 / (float)Math.PI); }

        float vectorAngle(float x, float y)
        {
            if (x == 0) // special cases
                return (y > 0) ? 90
                    : (y == 0) ? 0
                    : 270;
            else if (y == 0) // special cases
                return (x >= 0) ? 0
                    : 180;
            float ret = radToDeg(Mathf.Atan(y / x));
            if (x < 0 && y < 0) // quadrant Ⅲ
                ret = 180 + ret;
            else if (x < 0) // quadrant Ⅱ
                ret = 180 + ret; // it actually substracts
            else if (y < 0) // quadrant Ⅳ
                ret = 270 + (90 + ret); // it actually substracts
            return ret;
        }

        void Update()
        {

            if (Run.instance == null)
            {
                return;
            }



            var camera = Camera.main.transform.forward;
            var angle = vectorAngle(camera.x, camera.z);
            Direction d = Direction.UNKNOWN;
            if (angle <= 45)
            {
                d = Direction.NORTH;
            }

            if (angle > 45 && angle <= 90)
            {
                d = Direction.NORTH_WEST;
            }

            if (angle > 90 && angle <= 135)
            {
                d = Direction.WEST;
            }

            if (angle > 135 && angle <= 180)
            {
                d = Direction.SOUTH_WEST;
            }

            if (angle > 180 && angle <= 225)
            {
                d = Direction.SOUTH;
            }

            if (angle > 225 && angle <= 270)
            {
                d = Direction.SOUTH_EAST;
            }

            if (angle > 270 && angle <= 315)
            {
                d = Direction.EAST;
            }

            if (angle > 315)
            {
                d = Direction.NORTH_EAST;
            }

            UpdateUI(d);

        }

        void UpdateUI(Direction d)
        {
            if (Application.isBatchMode)
            {
                return;
            }

            if (CameraRigController.readOnlyInstancesList == null || CameraRigController.readOnlyInstancesList.Count == 0)
            {
                return;
            }

            this.currBody = CameraRigController.readOnlyInstancesList[0].viewer.masterController.master.GetBody();

            if (this.ui == null && this.currBody)
            {
                this.ui = this.currBody.gameObject.AddComponent<CompassUI>();
                this.ui.transform.SetParent(this.currBody.transform);
                this.ui.SetPosition(new Vector3((float)(Screen.width * x.Value / 100f), (float)(Screen.height * y.Value) / 100f, 0.0f));
                var component = this.ui.compassUI.GetComponent<RectTransform>();
                Vector2 sizeDelta = component.sizeDelta;

                sizeDelta.x = 200;
                sizeDelta.y = 200;

                component.sizeDelta = sizeDelta;
                this.ui.compassUI.fontSize = fontSize.Value;
            }
            this.ui.compassUI.SetText(d.LongString());
            this.ui.compassUI.alignment = TextAlignmentOptions.Center;
        }
    }
}
