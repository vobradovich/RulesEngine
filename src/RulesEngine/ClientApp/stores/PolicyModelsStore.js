import * as Rx from 'rxjs';
export const PolicyModelsActions = {
    request: new Rx.Subject(),
    receive: new Rx.Subject(),
    createModel: new Rx.Subject(),
    updateModel: new Rx.Subject(),
    deleteModel: new Rx.Subject(),
    cancelEditModel: new Rx.Subject(),
    exportModel: new Rx.Subject(),
    importModel: new Rx.Subject(),
};
export const PolicyModelsReducer = Rx.Observable.merge(PolicyModelsActions.request
    .do(PolicyModelsActions.receive)
    .map((startDateIndex) => (state) => (Object.assign({}, state, { isLoading: true }))), PolicyModelsActions.receive
    .switchMap(() => {
    //return Rx.Observable.ajax(`/api/SampleData/WeatherForecasts?startDateIndex=${startDateIndex}`);
    let fetchTask = fetch(`/api/model`);
    return Rx.Observable.fromPromise(fetchTask).catch(e => Rx.Observable.empty());
})
    //.map((response: Rx.AjaxResponse) => response.response)
    .switchMap((response) => Rx.Observable.fromPromise(response.json()))
    .map((data) => (state) => (Object.assign({}, state, { policyModels: data, isLoading: false }))), PolicyModelsActions.createModel
    .map(() => (state) => (Object.assign({}, state, { policyModels: state.policyModels.concat({ id: 0, created: new Date(), name: null, typeName: null }) }))));
const initialState = { isLoading: false, policyModels: [] };
// STORE 
export const PolicyModelsStore = new Rx.BehaviorSubject(initialState);
PolicyModelsReducer.scan((state, reducer) => reducer(state), PolicyModelsStore.getValue()).subscribe(PolicyModelsStore);
// DEBUG
PolicyModelsStore.subscribe(console.log);
//# sourceMappingURL=PolicyModelsStore.js.map