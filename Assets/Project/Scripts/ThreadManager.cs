﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class ThreadManager : MonoBehaviour {
    public static ThreadManager Worker;
    private readonly List<Action> executeOnMainThread = new List<Action>(); 
    private readonly List<Action> executeCopiedOnMainThread = new List<Action>();
    private static bool actionToExecuteOnMainThread = false;

    private void Awake() {
        Worker = this;
    }

    private void Update() {
        UpdateMain();
    }
    
    /// <summary>Sets an action to be executed on the main thread.</summary>
    /// <param name="_action">The action to be executed on the main thread.</param>
    public void ExecuteOnMainThread(Action _action) {
        if (_action == null) {
            Debug.Log("No action to execute on main thread!");
            return;
        }

        lock (executeOnMainThread) {
            executeOnMainThread.Add(_action);
            actionToExecuteOnMainThread = true;
        }
    }

    /// <summary>Executes all code meant to run on the main thread. NOTE: Call this ONLY from the main thread.</summary>
    public void UpdateMain() {
        if (actionToExecuteOnMainThread) {
            executeCopiedOnMainThread.Clear();
            lock (executeOnMainThread) {
                executeCopiedOnMainThread.AddRange(executeOnMainThread);
                executeOnMainThread.Clear();
                actionToExecuteOnMainThread = false;
            }

            for (int i = 0; i < executeCopiedOnMainThread.Count; i++) {
                executeCopiedOnMainThread[i]();
            }
        }
    }
}