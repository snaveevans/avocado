import React, { Component } from "react";
import firebase from "firebase/app";
import "firebase/auth";

class Login extends Component {
  constructor() {
    super();

    this.onLoginClick = this.onLoginClick.bind(this);
  }

  onLoginClick = () => {
    const provider = new firebase.auth.GoogleAuthProvider();
    provider.addScope("https://www.googleapis.com/auth/contacts.readonly");
    firebase.auth().useDeviceLanguage();
    firebase
      .auth()
      .signInWithPopup(provider)
      .then(result => {
        const token = result.credential.accessToken;
        const { user } = result;
        // TODO: call token controller
      })
      .catch(error => {
        const { errorCode, errorMessage, email, credential } = error;
        // TODO: what do we do?
      });
  };

  render() {
    return (
      <div>
        Login page
        <button onClick={() => this.onLoginClick()} type="button">
          Google Login
        </button>
      </div>
    );
  }
}

export default Login;
