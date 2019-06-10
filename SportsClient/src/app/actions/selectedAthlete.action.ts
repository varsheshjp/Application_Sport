import { Injectable } from '@angular/core'
import { Action } from '@ngrx/store'
import { Athlete } from '../Models/athlete.model';
import { Test } from '../Models/test.model';
export const ADD_SELECTEDATHLETE       = '[Athlete] AddSelected'
export const REMOVE_SELECTEDATHLETE    = '[Athlete] RemoveSelected'
export class AddSelectedAthlete implements Action {
    readonly type = ADD_SELECTEDATHLETE
    constructor(public payload: Athlete) {}
}
export class RemoveSelectedAthlete implements Action {
    readonly type = REMOVE_SELECTEDATHLETE
    constructor(public payload: number) {}
}
export type Actions = AddSelectedAthlete | RemoveSelectedAthlete