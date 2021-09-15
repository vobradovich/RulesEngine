import * as React from 'react';
import { Link } from 'react-router';
import { PolicyModelsStore, PolicyModelsActions } from '../stores/PolicyModelsStore';
import connect from './connect';
import { Button } from 'office-ui-fabric-react/lib/Button';
import { List } from 'office-ui-fabric-react/lib/List';
const PolicyModels = (props) => (<div>
        <h1>Policy models</h1>
        <PolicyControls {...props}/>
        <PolicyModelsTable {...props}/>
        <PolicyModelsLiss {...props}/>
    </div>);
const PolicyControls = (props) => (<div>
        <Button onClick={() => props.createModel()}>Add</Button>
    </div>);
const PolicyModelsLiss = (props) => (<div>
        <List items={props.policyModels} onRenderCell={(item, index) => (<div className='ms-ListBasicExample-itemCell' data-is-focusable={true}>
                <div className='ms-ListBasicExample-itemContent'>
                  <div className='ms-ListBasicExample-itemName ms-font-xl'>{item.name}</div>
                  <div className='ms-ListBasicExample-itemIndex'>{`Item ${item.id}`}</div>
                  <div className='ms-ListBasicExample-itemDesc ms-font-s'>{item.typeName}</div>
                </div>
                <i className='ms-ListBasicExample-chevron ms-Icon ms-Icon--chevronRight'/>
              </div>)}/>
    </div>);
const PolicyEditControls = (props) => (<div>
        <Button onClick={() => props.cancelEditModel()}>Cancel</Button>
    </div>);
const PolicyModelsTable = (props) => (<table className='table'>
        <thead>
            <tr>
                <th>Id</th>
                <th>Name</th>
                <th>TypeName</th>
                <th>Policies</th>
            </tr>
        </thead>
        <tbody>
            {props.policyModels.map(forecast => <tr key={forecast.id}>
                    <td>{forecast.id}</td>
                    <td><Link className='' to={`/model/${forecast.name}`}>{forecast.name}</Link></td>
                    <td>{forecast.typeName}</td>
                    <td>{forecast.policies ? forecast.policies.length : 0}</td>
                </tr>)}
        </tbody>
    </table>);
export default connect(PolicyModelsStore, PolicyModelsActions, (props) => {
    PolicyModelsActions.request.next();
})(PolicyModels);
//# sourceMappingURL=PolicyModels.jsx.map