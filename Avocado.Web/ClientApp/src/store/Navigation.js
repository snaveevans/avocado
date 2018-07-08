const openDrawer = 'OPEN_DRAWER';
const closeDrawer = 'CLOSE_DRAWER';

const initialState = {
    isDrawerOpen: false
};

export const actionCreators = {
    openDrawer: () => async (dispatch) => {
        dispatch({ type: openDrawer });
    },
    closeDrawer: () => async (dispatch) => {
        dispatch({ type: closeDrawer });
    }
};

export const reducer = (state, action) => {
    state = state || initialState;

    if (action.type === openDrawer) {
        return {
            ...state,
            isDrawerOpen: true
        };
    }

    if (action.type === closeDrawer) {
        return {
            ...state,
            isDrawerOpen: false
        };
    }

    return state;
};
