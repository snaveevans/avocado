import React, { Component } from "react";
import PropTypes from "prop-types";
import AvocadoBottomNavigation from "../navigation/AvocadoBottomNavigation";
import AvocadoDrawer from "../navigation/AvocadoDrawer";
import AvocadoAppBar from "../navigation/AvocadoAppBar";
import "./Layout.css";

class Layout extends Component {
  state = {
    isDrawerOpen: false
  };

  constructor() {
    super();

    this.toggleDrawer = this.toggleDrawer.bind(this);
  }

  toggleDrawer = () => {
    this.setState({ isDrawerOpen: !this.state.isDrawerOpen });
  };

  render() {
    const { children } = this.props;
    const { isDrawerOpen } = this.state;

    return (
      <div className="root">
        <AvocadoAppBar toggleDrawer={this.toggleDrawer} />
        <AvocadoDrawer
          toggleDrawer={this.toggleDrawer}
          isDrawerOpen={isDrawerOpen}
        />
        {children}
        <AvocadoBottomNavigation />
      </div>
    );
  }
}

Layout.propTypes = {
  children: PropTypes.object
};

export default Layout;
