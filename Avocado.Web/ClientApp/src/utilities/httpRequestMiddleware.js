import axios from "axios";
import { httpRequestType } from "../store/HttpRequest";

export default ({ dispatch, getState }) => next => action => {
  if (action.type === httpRequestType) {
    const { url, method, body, onSuccessType, onErrorType } = action.payload;

    const idToken = getState().account.idToken;
    const token = getState().account.token;

    let headers = {};
    if (idToken !== "") {
      headers["Id-Token"] = idToken;
    }

    if (token !== "") {
      headers["Authorization"] = "Bearer " + token;
    }

    var instance = axios.create({
      baseURL: "/api/",
      headers
    });

    instance
      .request({
        url,
        method,
        data: body
      })
      .then(r => {
        dispatch({
          type: onSuccessType,
          payload: r.data
        });
      })
      .catch(r => {
        dispatch({
          type: onErrorType,
          payload: r.data
        });
      });
  }

  return next(action);
};
