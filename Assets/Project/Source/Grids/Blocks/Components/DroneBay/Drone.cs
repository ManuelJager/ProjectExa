using System;
using Exa.Grids.Blocks.BlockTypes;
using UnityEngine;

namespace Exa.Grids.Blocks.Components {
    public class Drone : MonoBehaviour {
        private DroneBayBehaviour bay;
        private IGridInstance parent;

        private Block target;
        private Action targetRemovedHandler;
        
        public void Setup(DroneBayBehaviour bay) {
            this.bay = bay;
            parent = bay.Parent;

            transform.SetParent(GS.SpawnLayer.ships);
        }

        private void SetTarget(Block target) {
            if (targetRemovedHandler != null) {
                this.target.OnRemoved -= targetRemovedHandler;
            }
            
            this.target = target;

            targetRemovedHandler = () => {
                // Set to null
                targetRemovedHandler = null;
                this.target = null;
                
                OnTargetDestroyed();
            };

            target.OnRemoved += targetRemovedHandler;
        }

        private void OnTargetDestroyed() {
            
        }
    }
}