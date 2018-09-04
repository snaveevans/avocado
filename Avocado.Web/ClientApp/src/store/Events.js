const requestEventsType = "REQUEST_EVENTS";
const receiveEventsType = "RECEIVE_EVENTS";
const initialState = { events: [], isLoading: false };

export const actionCreators = {
  requestEvents: () => async (dispatch, getState) => {
    if (getState().events.isLoading)
      // Don't issue a duplicate request (we already have or are loading the requested data)
      return;

    dispatch({ type: requestEventsType });

    const url = `api/events`;
    const response = await fetch(url);
    const events = await response.json();

    dispatch({ type: receiveEventsType, payload: { events } });
  }
};

export const reducer = (state, action) => {
  state = state || initialState;

  if (action.type === requestEventsType) {
    return {
      ...state,
      isLoading: true
    };
  }

  if (action.type === receiveEventsType) {
    return {
      ...state,
      events: action.payload.events,
      isLoading: false
    };
  }

  return state;
};
