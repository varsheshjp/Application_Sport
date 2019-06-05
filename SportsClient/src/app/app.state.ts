import { Athlete } from './Models/athlete.model';
import { Test } from './Models/test.model';
export interface AppState{
    readonly test:Test[];
    readonly athlete:Athlete[];
}