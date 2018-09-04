import axios from "axios";

export const httpRequest = "HTTP_REQUEST";

export default ({ dispatch, getState }) => next => action => {
  if (action.type === httpRequest) {
    const { url, method, body, onSuccessType, onErrorType } = action.payload;

    const providerToken = getState().account.providerToken;
    const token = getState().account.token;

    let headers = {};
    if (providerToken !== "") {
      headers["Provider-Token"] = providerToken;
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
        console.log(r);
        dispatch({
          type: onSuccessType,
          payload: r.data
        });
      })
      .catch(r => {
        console.log(r);
        dispatch({
          type: onErrorType,
          payload: r.data
        });
      });
  }

  return next(action);
};
