import React, { Component } from "react";
import PropTypes from "prop-types";
import { withStyles } from "@material-ui/core/styles";
import Drawer from "@material-ui/core/Drawer";
import DrawerContents from "./DrawerContents";

const styles = {
  list: {
    width: 250
  }
};

class AvocadoDrawer extends Component {
  render() {
    const { classes, isDrawerOpen, toggleDrawer } = this.props;

    return (
      <Drawer open={isDrawerOpen} onClose={() => toggleDrawer()}>
        <div
          tabIndex={0}
          role="button"
          onClick={() => toggleDrawer()}
          onKeyDown={() => toggleDrawer()}>
          <div className={classes.list}>
            <DrawerContents />
          </div>
        </div>
      </Drawer>
    );
  }
}

AvocadoDrawer.propTypes = {
  classes: PropTypes.object.isRequired,
  isDrawerOpen: PropTypes.bool.isRequired,
  toggleDrawer: PropTypes.func.isRequired
};

export default withStyles(styles)(AvocadoDrawer);
