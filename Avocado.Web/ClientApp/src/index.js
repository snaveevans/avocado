import "typeface-roboto";
import "./index.css";
import React from "react";
import ReactDOM from "react-dom";
import firebase from "firebase/app";
import { BrowserRouter } from "react-router-dom";
import App from "./App";
import registerServiceWorker from "./registerServiceWorker";

// Create browser history to use in the Redux store
const baseUrl = document.getElementsByTagName("base")[0].getAttribute("href");

// Initialize Firebase
const config = {
  apiKey: "AIzaSyA8ywmoMF2iSp0TX4Z1D9IIYbCPkP-Ho30",
  authDomain: "avocado-208414.firebaseapp.com",
  databaseURL: "https://avocado-208414.firebaseio.com",
  projectId: "avocado-208414",
  storageBucket: "",
  messagingSenderId: "36370953457"
};
firebase.initializeApp(config);

const rootElement = document.getElementById("root");

ReactDOM.render(
  <BrowserRouter basename={baseUrl}>
    <App />
  </BrowserRouter>,
  rootElement
);

registerServiceWorker();
