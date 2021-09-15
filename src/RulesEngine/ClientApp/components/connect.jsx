import * as React from "react";
import * as Rx from 'rxjs';
function bindActions(actions) {
    return Object.keys(actions).reduce((boundActions, key) => {
        const member = actions[key];
        if (member instanceof Rx.Subject) {
            boundActions[key] = member.next.bind(member);
        }
        if (typeof member === 'function') {
            boundActions[key] = member.bind(actions);
        }
        return boundActions;
    }, {});
}
function connect(observable, actions = null, receiveProps = null) {
    const actionsProps = actions ? bindActions(actions) : null;
    return function wrap(WrappedComponent) {
        return class Connect extends React.Component {
            componentWillMount() {
                this.subscription = observable.subscribe(this.setState.bind(this));
                receiveProps ? receiveProps(this.props) : null;
            }
            componentWillReceiveProps(nextProps) {
                receiveProps ? receiveProps(nextProps) : null;
            }
            componentWillUnmount() {
                this.subscription.unsubscribe();
            }
            render() {
                return React.createElement(WrappedComponent, Object.assign({}, actionsProps, this.state, this.props));
            }
        };
    };
}
export default connect;
//# sourceMappingURL=connect.jsx.map