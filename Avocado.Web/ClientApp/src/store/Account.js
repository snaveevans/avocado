export const receiveToken = "RECEIVE_TOKEN";
const receiveIdToken = "RECEIVE_PROVIDER_TOKEN";
// const requestAccountInformation = "REQUEST_ACCOUNT_INFORMATION";
// const receiveAccountInformation = "RECEIVE_ACCOUNT_INFORMATION";
const clearUserData = "CLEAR_USER_DATA";
const initialState = {
  token: "",
  idToken: "",
  information: {
    isLoading: false,
    name: "",
    picture: ""
  }
};

export const actionCreators = {
  receiveIdToken: ({ idToken }) => dispatch => {
    localStorage.setItem("idToken", idToken);
    dispatch({ type: receiveIdToken, payload: { idToken } });
  },
  receiveToken: ({ token }) => dispatch => {
    localStorage.setItem("token", token);
    dispatch({ type: receiveToken, payload: { token } });
  },
  clearUserData: () => dispatch => {
    localStorage.clear();
    dispatch({ type: clearUserData });
  }
};

export const reducer = (state, action) => {
  state = state || initialState;

  if (action.type === receiveToken) {
    return {
      ...state,
      token: action.payload.token
    };
  }

  if (action.type === receiveIdToken) {
    return {
      ...state,
      idToken: action.payload.idToken
    };
  }

  if (action.type === clearUserData) {
    return initialState;
  }

  return state;
};
