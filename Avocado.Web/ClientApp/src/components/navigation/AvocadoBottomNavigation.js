import React, { Component } from 'react';
import PropTypes from 'prop-types';
import { withStyles } from '@material-ui/core/styles';
import BottomNavigation from '@material-ui/core/BottomNavigation';
import BottomNavigationAction from '@material-ui/core/BottomNavigationAction';
import EventAvailable from '@material-ui/icons/EventAvailable';
import EventNote from '@material-ui/icons/EventNote';
import Person from '@material-ui/icons/Person';

const styles = {
  root: {
    position: "fixed",
    bottom: "0",
    left: "0",
    right: "0"
  },
};

class AvocadoBottomNavigation extends Component {
  state = {
    value: 0,
  };

  handleChange = (event, value) => {
    this.setState({ value });
  };

  render() {
    const { classes } = this.props;
    const { value } = this.state;

    return (
      <BottomNavigation
        value={value}
        onChange={this.handleChange}
        showLabels
        className={classes.root}        
      >
        <BottomNavigationAction label="Upcoming" icon={<EventAvailable />} />
        <BottomNavigationAction label="My Events" icon={<EventNote />} />
        <BottomNavigationAction label="Contacts" icon={<Person />} />
      </BottomNavigation>
    );
  }
}

AvocadoBottomNavigation.propTypes = {
  classes: PropTypes.object.isRequired,
};

export default withStyles(styles)(AvocadoBottomNavigation);
