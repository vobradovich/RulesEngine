import * as React from 'react';
import { Link } from 'react-router';
export class NavMenu extends React.Component {
    render() {
        return <div className='main-nav'>
                <div className='navbar navbar-inverse'>
                <div className='navbar-header'>
                    <button type='button' className='navbar-toggle' data-toggle='collapse' data-target='.navbar-collapse'>
                        <span className='sr-only'>Toggle navigation</span>
                        <span className='icon-bar'></span>
                        <span className='icon-bar'></span>
                        <span className='icon-bar'></span>
                    </button>
                    <Link className='navbar-brand' to={'/'}>RulesEngine</Link>
                </div>
                <div className='clearfix'></div>
                <div className='navbar-collapse collapse'>
                    <ul className='nav navbar-nav'>
                        <li>
                            <Link to={'/'} activeClassName='active'>
                                <span className='glyphicon glyphicon-home'></span> Home
                            </Link>
                        </li>
                        <li>
                            <Link to={'/counter'} activeClassName='active'>
                                <span className='glyphicon glyphicon-education'></span> Counter
                            </Link>
                        </li>
                        <li>
                            <Link to={'/fetchdata'} activeClassName='active'>
                                <span className='glyphicon glyphicon-th-list'></span> Fetch data
                            </Link>
                        </li>
                        <li>
                            <Link to={'/model'} activeClassName='active'>
                                <span className='glyphicon glyphicon-th-list'></span> Models
                            </Link>
                        </li>
                    </ul>
                </div>
            </div>
        </div>;
    }
}
//# sourceMappingURL=NavMenu.jsx.map