import { applyMiddleware, combineReducers, compose, createStore } from "redux";
import thunk from "redux-thunk";
import { routerReducer, routerMiddleware } from "react-router-redux";
import * as Events from "./Events";
import * as Account from "./Account";
import * as Navigation from "./Navigation";
import httpRequestMiddleware from "../utilities/httpRequestMiddleware";

export default function configureStore(history, initialState) {
  const reducers = {
    events: Events.reducer,
    account: Account.reducer,
    navigation: Navigation.reducer
  };

  const middleware = [thunk, routerMiddleware(history), httpRequestMiddleware];

  // In development, use the browser's Redux dev tools extension if installed
  const enhancers = [];
  const isDevelopment = process.env.NODE_ENV === "development";
  if (
    isDevelopment &&
    typeof window !== "undefined" &&
    window.devToolsExtension
  ) {
    enhancers.push(window.devToolsExtension());
  }

  const rootReducer = combineReducers({
    ...reducers,
    routing: routerReducer
  });

  return createStore(
    rootReducer,
    initialState,
    compose(
      applyMiddleware(...middleware),
      ...enhancers
    )
  );
}
