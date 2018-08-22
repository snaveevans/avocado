import React, { Component } from 'react';
import PropTypes from 'prop-types';
import { withStyles } from '@material-ui/core/styles';
import Drawer from '@material-ui/core/Drawer';
import DrawerContents from "./DrawerContents";
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { actionCreators } from '../../store/Navigation';

const styles = {
    list: {
        width: 250,
    }
};

class AvocadoDrawer extends Component {

    toggleDrawer = () => {
        if (this.props.isDrawerOpen) {
            this.props.closeDrawer();
        } else {
            this.props.openDrawer();
        }
    }

    render() {
        const { classes, isDrawerOpen } = this.props;

        return (
            <Drawer open={isDrawerOpen} onClose={() => this.toggleDrawer()}>
                <div
                    tabIndex={0}
                    role="button"
                    onClick={() => this.toggleDrawer()}
                    onKeyDown={() => this.toggleDrawer()}
                >
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
};

export default connect(
    state => state.navigation,
    dispatch => bindActionCreators(actionCreators, dispatch)
)(withStyles(styles)(AvocadoDrawer));