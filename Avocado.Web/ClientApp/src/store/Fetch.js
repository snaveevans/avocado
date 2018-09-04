import { httpRequest } from "../utilities/fetch";

export const actionCreators = {
  fetch: ({ url, method = "GET", body = {}, onSuccessType, onErrorType = "ERROR" }) => dispatch => {
    dispatch({
      type: httpRequest,
      payload: {
        url,
        method,
        body,
        onSuccessType,
        onErrorType
      }
    });
  }
};
