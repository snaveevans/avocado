const receiveToken = 'RECEIVE_TOKEN';
const receiveAccessToken = 'RECEIVE_ACCESS_TOKEN';
const requestAccountInformation = 'REQUEST_ACCOUNT_INFORMATION';
const receiveAccountInformation = 'RECEIVE_ACCOUNT_INFORMATION';
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
    receiveAccessToken: ({ accessToken }) => async (dispatch) => {
        dispatch({ type: receiveAccessToken, payload: { accessToken } });
    },
    receiveToken: ({ token }) => async (dispatch) => {
        dispatch({ type: receiveToken, payload: { token } });
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

    return state;
};
