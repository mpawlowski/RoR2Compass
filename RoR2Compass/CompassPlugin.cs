using System;
using BepInEx;
using BepInEx.Configuration;
using RoR2;
using UnityEngine;
using TMPro;

namespace CompassPlugin
{
    [BepInPlugin("io.atflac.ror2.compassplugin", "Compass", "0.0.3")]
    public class CompassPlugin : BaseUnityPlugin
    {
        public const string Guid = "io.atflac.ror2.compassplugin";

        private CompassUI ui;
        private CharacterBody body;

        private ConfigEntry<int> x;
        private ConfigEntry<int> y;
        private ConfigEntry<int> fontSize;
        private ConfigEntry<DirectionMode> directionMode;

        public CompassPlugin() : base()
        {
            this.ui = new CompassUI();
        }

        void Awake()
        {

            this.x = Config.Bind(
                "Position",
                "CompassPositionX",
                50,
                "X position of the compass."
                );

            this.y = Config.Bind(
                "Position",
                "CompassPositionY",
                8,
                "Y position of the compass."
                );

            this.fontSize = Config.Bind(
                "Text",
                "FontSize",
                18,
                "Font size of the compass text."
                );

            this.directionMode = Config.Bind(
                "Text",
                "DirectionMode",
                DirectionMode.Long,
                "Type of compass to display."
                );

        }

        void Update()
        {

            if (Run.instance == null)
            {
                return;
            }

            if (Camera.main == null || Camera.main.transform == null)
            {
                return;
            }

            var camera = Camera.main.transform.forward;
            var angle = DirectionCalculator.ComputeDegrees(new Vector2(camera.x, camera.z));
            var d = DirectionCalculator.FromDegrees(angle);
            switch (directionMode.Value)
            {
                case DirectionMode.Long:
                    UpdateUI(d.LongString());
                    break;
                case DirectionMode.Short:
                    UpdateUI(d.ShortString());
                    break;
                case DirectionMode.Numeric:
                    UpdateUI(angle.ToString("0.0"));
                    break;
            }
        }

        void UpdateUI(string compassText)
        {

            if (CameraRigController.readOnlyInstancesList == null
                || CameraRigController.readOnlyInstancesList.Count == 0
                || CameraRigController.readOnlyInstancesList[0].viewer == null
                || CameraRigController.readOnlyInstancesList[0].viewer.masterController == null
                || CameraRigController.readOnlyInstancesList[0].viewer.masterController.master == null)
            {
                return;
            }
            this.body = CameraRigController.readOnlyInstancesList[0].viewer.masterController.master.GetBody();

            if (this.ui == null && this.body)
            {
                this.ui = this.body.gameObject.AddComponent<CompassUI>();
                this.ui.transform.SetParent(this.body.transform);               
            }

            if (this.ui)
            {
                this.ui.SetPosition(new Vector3((float)(Screen.width * x.Value / 100f), (float)(Screen.height * y.Value) / 100f, 0.0f));
                this.ui.compassUI.fontSize = fontSize.Value;
                this.ui.compassUI.SetText(compassText);
                this.ui.compassUI.alignment = TextAlignmentOptions.Center;
            }
        }
    }
}
