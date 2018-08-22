import React from 'react';
import AvocadoBottomNavigation from "../navigation/AvocadoBottomNavigation";
import AvocadoDrawer from "../navigation/AvocadoDrawer"
import AvocadoAppBar from "../navigation/AvocadoAppBar";
import "./Layout.css";

export default props => (
  <div className="root">
    <AvocadoAppBar />
    <AvocadoDrawer />
    {props.children}
    <AvocadoBottomNavigation />
  </div>
);
