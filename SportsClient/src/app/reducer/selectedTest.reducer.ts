import { Action } from '@ngrx/store'
import { Test } from '../models/test.model'
import * as TestActions from '../actions/selectedTest.action'

export function selectedtestreducer(state: Test, action: TestActions.Actions) {
    
    switch(action.type) {
        case TestActions.ADD_SELECTEDTEST:
            return action.payload;
        case TestActions.REMOVE_SELECTEDTEST:
            return null;
        default:
            return state;
    }
}