using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts {
    internal class JobsOnMainThread : MonoBehaviour {
        internal static JobsOnMainThread Worker;
        private readonly Queue<Action> _jobs = new Queue<Action>();

        private void Awake() {
            Worker = this;
        }

        private void Update() {
            while (_jobs.Count > 0)
                _jobs.Dequeue().Invoke();
        }

        internal void AddJob(Action newJob) {
            _jobs.Enqueue(newJob);
        }
    }
}