export const httpRequestType = "HTTP_REQUEST";

export const httpRequest = ({ url, method = "GET", body = {}, onSuccessType, onErrorType = "ERROR" }) => dispatch => {
    dispatch({
      type: httpRequestType,
      payload: {
        url,
        method,
        body,
        onSuccessType,
        onErrorType
      }
    });
  };
