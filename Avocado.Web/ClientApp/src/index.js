import 'typeface-roboto'
import './index.css';
import React from 'react';
import ReactDOM from 'react-dom';
import { Provider } from 'react-redux';
import { ConnectedRouter } from 'react-router-redux';
import { createBrowserHistory } from 'history';
import firebase from "firebase/app";
import configureStore from './store/configureStore';
import App from './App';
import registerServiceWorker from './registerServiceWorker';

// Create browser history to use in the Redux store
const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href');
const history = createBrowserHistory({ basename: baseUrl });

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

// Get the application-wide store instance, prepopulating with state from the server where available.
const initialState = window.initialReduxState;
const store = configureStore(history, initialState);

const rootElement = document.getElementById('root');

ReactDOM.render(
  <Provider store={store}>
    <ConnectedRouter history={history}>
      <App />
    </ConnectedRouter>
  </Provider>,
  rootElement);

registerServiceWorker();
