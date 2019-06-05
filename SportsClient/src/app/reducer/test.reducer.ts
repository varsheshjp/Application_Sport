import { Action } from '@ngrx/store'
import { Test } from '../models/test.model'
import * as TestActions from '../actions/test.action'

export function testreducer(state: Test[] = [], action: TestActions.Actions) {
    
    switch(action.type) {
        case TestActions.ADD_TEST:
            return action.payload;
        case TestActions.REMOVE_TEST:
            state.splice(action.payload, 1)
            return state;
        default:
            return state;
    }
}