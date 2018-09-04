import React, { Component } from "react";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import { actionCreators } from "../store/Events";

class EventList extends Component {
  componentWillMount() {
    // This method runs when the component is first added to the page
    this.props.requestEvents();
  }

  render() {
    return (
      <div>
        <h1>Events</h1>
        {renderEventsTable(this.props)}
      </div>
    );
  }
}

function renderEventsTable(props) {
  return (
    <table className="table">
      <thead>
        <tr>
          <th>Id</th>
          <th>Title</th>
          <th>Description</th>
        </tr>
      </thead>
      <tbody>
        {props.events.map(event => (
          <tr key={event.id}>
            <td>{event.id}</td>
            <td>{event.title}</td>
            <td>{event.description}</td>
          </tr>
        ))}
      </tbody>
    </table>
  );
}

export default connect(
  state => state.events,
  dispatch => bindActionCreators(actionCreators, dispatch)
)(EventList);
