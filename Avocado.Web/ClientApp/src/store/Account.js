export const receiveToken = "RECEIVE_TOKEN";
const receiveProviderToken = "RECEIVE_PROVIDER_TOKEN";
const requestAccountInformation = "REQUEST_ACCOUNT_INFORMATION";
const receiveAccountInformation = "RECEIVE_ACCOUNT_INFORMATION";
const clearUserData = "CLEAR_USER_DATA";
const initialState = {
  token: "",
  providerToken: "",
  information: {
    isLoading: false,
    name: "",
    picture: ""
  }
};

export const actionCreators = {
  receiveProviderToken: ({ providerToken }) => dispatch => {
    localStorage.setItem("providerToken", providerToken);
    dispatch({ type: receiveProviderToken, payload: { providerToken } });
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

  if (action.type === receiveProviderToken) {
    return {
      ...state,
      providerToken: action.payload.providerToken
    };
  }

  if (action.type === clearUserData) {
    return initialState;
  }

  return state;
};
