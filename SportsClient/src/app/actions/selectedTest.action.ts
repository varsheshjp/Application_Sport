import { Injectable } from '@angular/core'
import { Action } from '@ngrx/store'
import { Athlete } from '../Models/athlete.model';
import { Test } from '../Models/test.model';
export const ADD_SELECTEDTEST       = '[Test] AddSelected'
export const REMOVE_SELECTEDTEST    = '[Test] RemoveSelected'
export class AddSelectedTest implements Action {
    readonly type = ADD_SELECTEDTEST
    constructor(public payload: Test) {}
}
export class RemoveSelectedTest implements Action {
    readonly type = REMOVE_SELECTEDTEST
    constructor(public payload: number) {}
}
export type Actions = AddSelectedTest | RemoveSelectedTest