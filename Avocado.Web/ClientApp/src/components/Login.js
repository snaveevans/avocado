import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import Typography from '@material-ui/core/Typography';
import PropTypes from 'prop-types';
import { withStyles } from '@material-ui/core/styles';
import { actionCreators } from '../store/Account';
import Button from '@material-ui/core/Button';

const styles = theme => ({
    button: {
        margin: theme.spacing.unit
    }
});

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
        const { token, classes } = this.props;
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
                    <Typography variant="headline" color="inherit" align="center">
                        Login/Register
                    </Typography>
                    <Button onClick={() => this.changeMode("Login")} variant="contained" color="primary" className={classes.button}>
                        Login
                    </Button>
                    <Button onClick={() => this.changeMode("Login")} variant="contained" color="primary" className={classes.button}>
                        Register
                    </Button>
                </div>
            );
        }

        if (mode === "Login" || mode === "Register") {
            const action = mode.toLowerCase();
            const redirectUri = `http://local.tylerevans.co:5000/api/token/google/${action}`;
            return (
                <div>
                    <Typography variant="headline" color="inherit" align="center">
                        {mode}
                    </Typography>
                    <form action="https://accounts.google.com/o/oauth2/v2/auth" method="get" target="_blank">
                        <input type="hidden" name="client_id" value="36370953457-31iaemqdva2uoio9brcptifcc3cbnl2t.apps.googleusercontent.com" />
                        <input type="hidden" name="redirect_uri" value={redirectUri} />
                        <input type="hidden" name="response_type" value="code" />
                        <input type="hidden" name="scope" value="https://www.googleapis.com/auth/contacts.readonly" />
                        <input type="hidden" name="include_granted_scopes" value="true" />
                        <input type="hidden" name="state" value="pass-through value" />
                        <Button type="submit" variant="contained" color="primary" className={classes.button}>
                            Google
                        </Button>
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

Login.propTypes = {
    classes: PropTypes.object.isRequired,
};

export default connect(
    state => state.account,
    dispatch => bindActionCreators(actionCreators, dispatch)
)(withStyles(styles)(Login));