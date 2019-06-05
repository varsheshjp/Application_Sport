import { Injectable } from '@angular/core'
import { Action } from '@ngrx/store'
import { Athlete } from '../Models/athlete.model';
export const ADD_ATHLETE       = '[Athlete] Add'
export const REMOVE_ATHLETE    = '[Athlete] Remove'
export class AddAthlete implements Action {
    readonly type = ADD_ATHLETE
    constructor(public payload: Athlete[]) {}
}
export class RemoveAthlete implements Action {
    readonly type = REMOVE_ATHLETE
    constructor(public payload: number) {}
}
export type Actions = AddAthlete | RemoveAthlete 