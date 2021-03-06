import * as React from 'react';
import { NavMenu } from './NavMenu';
export class Layout extends React.Component {
    render() {
        return <div className='container-fluid'>
            <div className='row'>
                <div className='col-sm-3'>
                    <NavMenu />
                </div>
                <div className='col-sm-9'>
                    {this.props.body}
                </div>
            </div>
        </div>;
    }
}
//# sourceMappingURL=Layout.jsx.map