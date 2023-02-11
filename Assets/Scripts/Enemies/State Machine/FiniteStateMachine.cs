using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachine
{
        public State currentState {get; private set; }

        //dam nhiem trach nhiem trang thai dau tien
        public void Initialize(State startingState) //tiem phu thuoc 
        {
            currentState = startingState;
            currentState.Enter();
        }

        public void ChangeState(State newState)
        {
            currentState.Exit();
            currentState = newState;
            currentState.Enter();
        }
}
