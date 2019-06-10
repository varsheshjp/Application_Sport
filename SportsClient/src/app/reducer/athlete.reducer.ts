import { Action } from '@ngrx/store'
import { Athlete } from '../models/athlete.model'
import * as AthleteActions from '../actions/athlete.action'

export function athletereducer(state: Athlete[] = [], action: AthleteActions.Actions) {
    
    switch(action.type) {
        case AthleteActions.ADD_ATHLETE:
            return [...action.payload];
        case AthleteActions.REMOVE_ATHLETE:
            state.splice(action.payload, 1)
            return state;
        default:
            return state;
    }
}