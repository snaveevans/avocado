import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { actionCreators } from '../store/Account';

class Login extends Component {
    constructor(props) {
        super(props);
        this.state = {
            mode: "",
        };

        this.changeMode = this.changeMode.bind(this);
    }
    componentWillMount() {
        window.CallMeWhenYouAreDone = (tokens) => {
            const { accessToken, token } = tokens;
            // set tokens in store
            this.props.receiveAccessToken({ accessToken });
            this.props.receiveToken({ token });
            // navigate "home page"
            this.props.history.push("/");
        }
    }
    componentWillUnmount() {
        delete window.CallMeWhenYouAreDone;
    }
    changeMode(mode) {
        this.setState({
            mode
        });
    }
    render() {
        const { token } = this.props;
        const { mode } = this.state;

        if (token !== "") {
            return (
                <div>
                    <h3>You are already logged in.</h3>
                </div>
            );
        }

        if (mode === "") {
            return (
                <div>
                    <h2>Login</h2>
                    <button onClick={() => this.changeMode("Login")}>Login</button>
                    <br /><br />
                    <button onClick={() => this.changeMode("Register")}>Register</button>
                </div>
            );
        }

        if (mode === "Login" || mode === "Register") {
            const action = mode.toLowerCase();
            const redirectUri = `http://local.tylerevans.co:5000/api/token/google/${action}`;
            return (
                <div>
                    <h1>{mode}</h1>
                    <form action="https://accounts.google.com/o/oauth2/v2/auth" method="get" target="_blank">
                        <input type="hidden" name="client_id" value="36370953457-31iaemqdva2uoio9brcptifcc3cbnl2t.apps.googleusercontent.com" />
                        <input type="hidden" name="redirect_uri" value={redirectUri} />
                        <input type="hidden" name="response_type" value="code" />
                        <input type="hidden" name="scope" value="https://www.googleapis.com/auth/contacts.readonly" />
                        <input type="hidden" name="include_granted_scopes" value="true" />
                        <input type="hidden" name="state" value="pass-through value" />
                        <button type="submit" >Google</button>
                    </form>
                </div>
            );
        }

        return (
            <div>
                Error
            </div>
        );
    }
};

export default connect(
    state => state.account,
    dispatch => bindActionCreators(actionCreators, dispatch)
)(Login);