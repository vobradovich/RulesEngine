import * as Rx from 'rxjs';
import { PolicyModel } from '../models';

export interface IPolicyModelsState {
    isLoading: boolean;
    policyModels: PolicyModel[];
}

export const PolicyModelsActions = {
    request: new Rx.Subject(),
    receive: new Rx.Subject(),     
    createModel: new Rx.Subject(), 
    updateModel: new Rx.Subject<PolicyModel>(), 
    deleteModel: new Rx.Subject<number>(), 
    cancelEditModel: new Rx.Subject<PolicyModel>(), 
    exportModel: new Rx.Subject<PolicyModel>(), 
    importModel: new Rx.Subject<any>(),
};

export const PolicyModelsReducer = Rx.Observable.merge(
    PolicyModelsActions.request
        .do(PolicyModelsActions.receive)
        .map((startDateIndex) => (state: IPolicyModelsState): IPolicyModelsState => (Object.assign({}, state, { isLoading: true }))),

    PolicyModelsActions.receive
        .switchMap(() => {
            //return Rx.Observable.ajax(`/api/SampleData/WeatherForecasts?startDateIndex=${startDateIndex}`);
            let fetchTask = fetch(`/api/model`)
            return Rx.Observable.fromPromise<Response>(fetchTask).catch(e => Rx.Observable.empty());
        })
        //.map((response: Rx.AjaxResponse) => response.response)
        .switchMap((response: Response) => Rx.Observable.fromPromise(response.json()))
        .map((data) => (state: IPolicyModelsState): IPolicyModelsState => (Object.assign({}, state, { policyModels: data, isLoading: false }))),

    PolicyModelsActions.createModel 
        .map(() => (state: IPolicyModelsState): IPolicyModelsState => (Object.assign({}, state, { policyModels: state.policyModels.concat({ id: 0, created: new Date(), name: null, typeName: null }) }))));


const initialState: IPolicyModelsState = { isLoading: false, policyModels: [] };

// STORE 
export const PolicyModelsStore = new Rx.BehaviorSubject<IPolicyModelsState>(initialState);
PolicyModelsReducer.scan((state: IPolicyModelsState, reducer) => reducer(state), PolicyModelsStore.getValue()).subscribe(PolicyModelsStore);

// DEBUG
PolicyModelsStore.subscribe(console.log);
