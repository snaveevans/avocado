const openDrawer = 'OPEN_DRAWER';
const closeDrawer = 'CLOSE_DRAWER';

const initialState = {
    isDrawerOpen: false
};

export const actionCreators = {
    openDrawer: () => (dispatch) => {
        dispatch({ type: openDrawer });
    },
    closeDrawer: () => (dispatch) => {
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
