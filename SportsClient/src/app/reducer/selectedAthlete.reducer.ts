import { Action } from '@ngrx/store'
import { Test } from '../models/test.model'
import * as AthleteActions from '../actions/selectedAthlete.action'
import { ADD_SELECTEDATHLETE } from '../actions/selectedAthlete.action';

export function selectedathletereducer(state: Test, action: AthleteActions.Actions) {
    
    switch(action.type) {
        case AthleteActions.ADD_SELECTEDATHLETE:
            return action.payload;
        case AthleteActions.REMOVE_SELECTEDATHLETE:
            return null;
        default:
            return state;
    }
}