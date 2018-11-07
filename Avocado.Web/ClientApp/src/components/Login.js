import React, { Component } from "react";
import firebase from "firebase/app";
import "firebase/auth";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import {
  actionCreators as accountActionCreators,
  receiveToken
} from "../store/Account";
import { httpRequest } from "../store/HttpRequest";

class Login extends Component {
  constructor() {
    super();

    this.onLoginClick = this.onLoginClick.bind(this);
  }

  onLoginClick = mode => {
    const provider = new firebase.auth.GoogleAuthProvider();
    provider.addScope("https://www.googleapis.com/auth/contacts.readonly");
    firebase.auth().useDeviceLanguage();
    firebase
      .auth()
      .signInWithPopup(provider)
      .then(() => firebase.auth().currentUser.getIdToken(false))
      .then(idToken => {
        const url = `token/${mode}`;

        this.props.receiveIdToken({ idToken });

        this.props.httpRequest({
          url,
          method: "GET",
          onSuccessType: receiveToken
        });
      })
      .catch(error => {
        // const { errorCode, errorMessage, email, credential } = error;
        // TODO: what do we do?
      });
  };

  render() {
    const { token } = this.props;
    return (
      <div>
        Login page
        <button onClick={() => this.onLoginClick("register")} type="button">
          Register
        </button>
        <button onClick={() => this.onLoginClick("login")} type="button">
          Login
        </button>
        <div>Token: {token}</div>
      </div>
    );
  }
}

export default connect(
  state => state.account,
  dispatch =>
    bindActionCreators({ httpRequest, ...accountActionCreators }, dispatch)
)(Login);
