import React, { Component } from "react";
import PropTypes from "prop-types";

class EventList extends Component {
  componentWillMount() {
    // TODO fetch events
  }

  render() {
    return (
      <div>
        <h1>Events</h1>
        {this.props.events ? renderEventsTable(this.props) : null}
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

EventList.propTypes = {
  events: PropTypes.arrayOf({
    id: PropTypes.string.isRequired,
    title: PropTypes.string.isRequired,
    description: PropTypes.string.isRequired
  })
};

export default EventList;
