const receiveToken = 'RECEIVE_TOKEN';
const receiveAccessToken = 'RECEIVE_ACCESS_TOKEN';
const requestAccountInformation = 'REQUEST_ACCOUNT_INFORMATION';
const receiveAccountInformation = 'RECEIVE_ACCOUNT_INFORMATION';
const clearUserData = "CLEAR_USER_DATA";
const initialState = {
    token: "",
    accessToken: "",
    information: {
        isLoading: false,
        name: "",
        picture: ""
    }
};

export const actionCreators = {
    receiveAccessToken: ({ accessToken }) => (dispatch) => {
        localStorage.setItem("accessToken", accessToken);
        dispatch({ type: receiveAccessToken, payload: { accessToken } });
    },
    receiveToken: ({ token }) => (dispatch) => {
        localStorage.setItem("token", token);
        dispatch({ type: receiveToken, payload: { token } });
    },
    clearUserData: () => (dispatch) => {
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

    if (action.type === receiveAccessToken) {
        return {
            ...state,
            accessToken: action.payload.accessToken
        };
    }

    if (action.type === clearUserData) {
        return initialState;
    }

    return state;
};
