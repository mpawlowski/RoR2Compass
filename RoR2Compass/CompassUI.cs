﻿using RoR2;
using RoR2.UI;
using UnityEngine;

namespace CompassPlugin
{
    internal class CompassUI : MonoBehaviour
    {
        private Transform parent;
        private GameObject compass;
        public HGTextMeshProUGUI compassUI;

        void Awake()
        {
            this.parent = RoR2Application.instance.mainCanvas.transform;
            this.compass = new GameObject(CompassPlugin.Guid + ".compass");
            this.compassUI = this.compass.AddComponent<HGTextMeshProUGUI>();
            this.compassUI.transform.SetParent(this.parent);
        }

        public void SetPosition(Vector3 position)
        {
            this.compass.transform.position = position;
        }

        void Update()
        {

        }

        private void OnDestroy()
        {
            Object.Destroy(this.compassUI);
            Object.Destroy(this.compass);
        }

        public CompassUI() : base()
        {
        }
    }
}
